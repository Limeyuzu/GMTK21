using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeRope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> RopeSegments = new List<RopeSegment>();
    public float RopeSegmentLength = 0.25f;

    private int SegmentLength;
    public int MaxSegmentLength = 30;
    public int MinSegmentLength = 5;
    private float LineWidth = 0.1f;

    [SerializeField] private Transform StartPoint;
    [SerializeField] private Transform EndPoint;

    private bool MoveToMouse = false;
    private Vector3 MousePositionWorld;
    private int IndexMousePos;
    [SerializeField]
    public GameObject Collider;
    private List<GameObject> FollowTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        FindDistance();
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 RopeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < SegmentLength; ++i)
        {
            this.RopeSegments.Add(new RopeSegment(RopeStartPoint));
            RopeStartPoint.y -= RopeSegmentLength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();

        if (Input.GetMouseButtonDown(0))
        {
            this.MoveToMouse = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.MoveToMouse = false;
        }

        Vector3 ScreenMousePos = Input.mousePosition;
        float XStart = StartPoint.position.x;
        float XEnd = EndPoint.position.x;

        for (int i = 0; i < SegmentLength; ++i)
        {
            float CurrX = this.FollowTargets[i].transform.position.x;

            float Ratio = (CurrX - XStart) / (XEnd - XStart);

            if (Ratio > 0.0f)
            {
                this.IndexMousePos = (int)(this.SegmentLength * Ratio);
            }
        }

        //float CurrX = this.FollowTarget.transform.position.x;

        //float Ratio = (CurrX - XStart) / (XEnd - XStart);
        //if (Ratio > 0.0f)
        //{
            //this.IndexMousePos = (int)(this.SegmentLength * Ratio);
        //}
    }

    void FixedUpdate()
    {
        this.Simulate();
    }

    void FindDistance()
    {
        if (Vector3.Distance(StartPoint.position, EndPoint.position) < (MinSegmentLength * RopeSegmentLength))
        {
            SegmentLength = MinSegmentLength;
        }
        else if (Vector3.Distance(StartPoint.position, EndPoint.position) < (MaxSegmentLength / RopeSegmentLength))
        {
            SegmentLength = Mathf.RoundToInt(Vector3.Distance(StartPoint.position, EndPoint.position) / RopeSegmentLength);
        }
        else
        {
            SegmentLength = MaxSegmentLength;
        }
    }

    void Simulate()
    {
        // SIMULATION
        Vector2 ForceGravity = new Vector2(0.0f, -1.0f);

        for (int i = 0; i < this.SegmentLength; ++i)
        {
            RopeSegment FirstSegment = this.RopeSegments[i];
            Vector2 velocity = FirstSegment.Curr_Pos - FirstSegment.Old_Pos;
            FirstSegment.Old_Pos = FirstSegment.Curr_Pos;
            FirstSegment.Curr_Pos += velocity;
            FirstSegment.Curr_Pos += ForceGravity * Time.deltaTime;
            this.RopeSegments[i] = FirstSegment;
        }

        // CONSTRAINTS
        for (int i = 0; i < 20; ++i)
        {
            this.ApplyConstraints();
        }
    }

    void ApplyConstraints()
    {
        RopeSegment firstSegment = this.RopeSegments[0];
        firstSegment.Curr_Pos = this.StartPoint.position;
        this.RopeSegments[0] = firstSegment;

        RopeSegment endSegment = this.RopeSegments[this.SegmentLength - 1];
        endSegment.Curr_Pos = this.EndPoint.position;
        this.RopeSegments[this.SegmentLength - 1] = endSegment;


        for (int i = 0; i < this.SegmentLength - 1; ++i)
        {
            RopeSegment FirstSeg = this.RopeSegments[i];
            RopeSegment SecondSeg = this.RopeSegments[i + 1];

            float Dist = (FirstSeg.Curr_Pos - SecondSeg.Curr_Pos).magnitude;
            float error = Mathf.Abs(Dist - this.RopeSegmentLength);
            Vector2 ChangeDirection = Vector2.zero;

            if (Dist > RopeSegmentLength)
            {
                ChangeDirection = (FirstSeg.Curr_Pos - SecondSeg.Curr_Pos).normalized;
            }
            else if (Dist < RopeSegmentLength)
            {
                ChangeDirection = (SecondSeg.Curr_Pos - FirstSeg.Curr_Pos).normalized;
            }

            Vector2 ChangeAmount = ChangeDirection * error;
            if (i != 0)
            {
                FirstSeg.Curr_Pos -= ChangeAmount * 0.5f;
                this.RopeSegments[i] = FirstSeg;
                SecondSeg.Curr_Pos += ChangeAmount * 0.5f;
                this.RopeSegments[i + 1] = SecondSeg;
            }
            else
            {
                SecondSeg.Curr_Pos += ChangeAmount;
                this.RopeSegments[i + 1] = SecondSeg;
            }

            if (IndexMousePos > 0 && IndexMousePos < this.SegmentLength - 1 && i == IndexMousePos)
            {
                RopeSegment segment = this.RopeSegments[i];
                RopeSegment segment2 = this.RopeSegments[i + 1];
                segment.Curr_Pos = new Vector2(this.FollowTargets[i].transform.position.x, this.FollowTargets[i].transform.position.y);
                segment2.Curr_Pos = new Vector2(this.FollowTargets[i].transform.position.x, this.FollowTargets[i].transform.position.y);
                this.RopeSegments[i] = segment;
                this.RopeSegments[i + 1] = segment2;
            }
        }
    }

    void DrawRope()
    {
        FindDistance();

        float LineWidth = this.LineWidth;
        lineRenderer.startWidth = LineWidth;
        lineRenderer.endWidth = LineWidth;

        Vector3[] RopePositions = new Vector3[this.SegmentLength];
        for (int i = 0; i < this.SegmentLength; ++i)
        {
            RopePositions[i] = this.RopeSegments[i].Curr_Pos;
            Vector3 Collider_Pos = new Vector3(this.RopeSegments[i].Curr_Pos.x, this.RopeSegments[i].Curr_Pos.y, 0.0f);
            GameObject NewCollider = Instantiate(Collider, Collider_Pos, Quaternion.identity);
            FollowTargets.Add(NewCollider);
        }
        for (int i = 0; i < this.SegmentLength; ++i)
        {
            Destroy(FollowTargets[i]);
            FollowTargets.RemoveAt(i);
        }

        lineRenderer.positionCount = RopePositions.Length;
        lineRenderer.SetPositions(RopePositions);
    }

    public struct RopeSegment
    {
        public Vector2 Curr_Pos;
        public Vector2 Old_Pos;

        public RopeSegment(Vector2 Pos)
        {
            this.Curr_Pos = Pos;
            this.Old_Pos = Pos;
        }
    }
}
