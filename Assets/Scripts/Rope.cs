using UnityEngine;

namespace Assets.Scripts
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] GameObject ConnectedTo;
        [SerializeField] float MaxLength = 5;
        [SerializeField] float PullStrength = 3;

        private bool _maxLengthReached;

        private Rigidbody2D _thisRigidbody2D;
        private Rigidbody2D _otherRigidBody2D;
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _thisRigidbody2D = GetComponent<Rigidbody2D>();
            _otherRigidBody2D = ConnectedTo.GetComponent<Rigidbody2D>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            // debug color switch
            var sprite = ConnectedTo.GetComponent<SpriteRenderer>();
            sprite.color = _maxLengthReached ? Color.red : Color.white;

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

            // apply forces - only on the connected object
            var direction = this.transform.position - ConnectedTo.transform.position;
            _otherRigidBody2D.AddForce(direction * PullStrength);
        }

        private void DrawRope()
        {
            _lineRenderer.SetPosition(0, this.transform.position);
            _lineRenderer.SetPosition(1, ConnectedTo.transform.position);
        }
    }
}