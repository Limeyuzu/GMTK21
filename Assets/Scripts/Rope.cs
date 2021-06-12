using UnityEngine;

namespace Assets.Scripts
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] GameObject ConnectedTo;
        [SerializeField] float MaxLength = 5;
        [SerializeField] float PullStrength = 3;

        // When false, ConnectedTo will be pulled. When true, this will be pulled.
        private bool _ropeFlipped = true;
        private bool _maxLengthReached;

        private Rigidbody2D _thisRigidbody2D;
        private Color _thisOriginalColor;
        private Rigidbody2D _otherRigidBody2D;
        private Color _otherOriginalColor;
        private LineRenderer _lineRenderer;

        Color _ropeOriginalColor;

        public void FlipRopeTarget()
        {
            _ropeFlipped = !_ropeFlipped;
        }

        private void Start()
        {
            _thisRigidbody2D = GetComponent<Rigidbody2D>();
            _thisOriginalColor = GetComponent<SpriteRenderer>().color;
            _otherRigidBody2D = ConnectedTo.GetComponent<Rigidbody2D>();
            _otherOriginalColor = ConnectedTo.GetComponent<SpriteRenderer>().color;
            _lineRenderer = GetComponent<LineRenderer>();
            _ropeOriginalColor = _lineRenderer.startColor;
        }

        private void Update()
        {
            DrawRope();
        }

        private void FixedUpdate()
        {
            CheckDistance();
            ExecuteRopeForces();
        }

        private void CheckDistance()
        {
            var distance = Vector2.Distance(this.transform.position, ConnectedTo.transform.position);
            _maxLengthReached = distance > MaxLength;
        }

        private void ExecuteRopeForces()
        {
            if (!_maxLengthReached) return;

            if (_ropeFlipped)
            {
                // apply forces - only on this object
                var direction = ConnectedTo.transform.position - this.transform.position;
                _thisRigidbody2D.AddForce(direction * PullStrength);
            } 
            else
            {
                // apply forces - only on the connected object
                var direction = this.transform.position - ConnectedTo.transform.position;
                _otherRigidBody2D.AddForce(direction * PullStrength);
            }
        }

        private void DrawRope()
        {
            _lineRenderer.SetPosition(0, this.transform.position);
            _lineRenderer.SetPosition(1, ConnectedTo.transform.position);
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