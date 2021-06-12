using UnityEngine;

namespace Assets.Scripts
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] Rigidbody2D ConnectedToRigidBody;
        [SerializeField] float MaxLength = 5;
        [SerializeField] float PullLength = 1;
        [SerializeField] float PullStrength = 3;

        // When false, ConnectedTo will be pulled. When true, this will be pulled.
        private bool _ropeFlipped = true;
        private bool _maxLengthReached;
        private Color _ropeOriginalColor;

        private float CurrentMaxLength;

        private Rigidbody2D _thisRigidbody2D;
        private LineRenderer _lineRenderer;

        public bool MaxLengthReached()
        {
            return _maxLengthReached;
        }

        public void FlipRopeTarget()
        {
            _ropeFlipped = !_ropeFlipped;
        }

        private void Start()
        {
            _thisRigidbody2D = GetComponent<Rigidbody2D>();
            _lineRenderer = GetComponent<LineRenderer>();
            _ropeOriginalColor = _lineRenderer.startColor;
            CurrentMaxLength = MaxLength;
        }

        private void Update()
        {
            DrawRope();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PullRope();
                return;
            }
            if (Input.GetKeyUp(KeyCode.Q))
            {
                ReleaseRope();
            }
        }
        public void PullRope()
        {
            CurrentMaxLength = PullLength;
        }
        public void ReleaseRope()
        {
            CurrentMaxLength = MaxLength;
        }
        private void FixedUpdate()
        {
            CheckDistance();
            ExecuteRopeForces();
        }

        private void CheckDistance()
        {
            var distance = Vector2.Distance(this.transform.position, ConnectedToRigidBody.transform.position);
            _maxLengthReached = distance > CurrentMaxLength;
        }

        private void ExecuteRopeForces()
        {
            if (!_maxLengthReached) return;

            if (_ropeFlipped)
            {
                // apply forces - only on this object
                var direction = ConnectedToRigidBody.transform.position - this.transform.position;
                _thisRigidbody2D.AddForce(direction.normalized * PullStrength);
            } 
            else
            {
                // apply forces - only on the connected object
                var direction = this.transform.position - ConnectedToRigidBody.transform.position;
                ConnectedToRigidBody.AddForce(direction.normalized * PullStrength);
            }
        }

        private void DrawRope()
        {
            _lineRenderer.SetPosition(0, this.transform.position);
            _lineRenderer.SetPosition(1, ConnectedToRigidBody.transform.position);
            if (_maxLengthReached)
            {
                _lineRenderer.startColor = Color.red;
                _lineRenderer.endColor = Color.red;
                return;
            }
            _lineRenderer.startColor = _ropeOriginalColor;
            _lineRenderer.endColor = _ropeOriginalColor;
        }
    }
}