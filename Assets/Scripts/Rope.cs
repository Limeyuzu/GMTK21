using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Rope : MonoBehaviour, IRope
    {
        [SerializeField] float MaxLength = 5;
        [SerializeField] float PullLength = 1;
        [SerializeField] float PullStrength = 3;

        private List<RopeAttachedBody> _ropeConnections = new List<RopeAttachedBody>();
        private float _currentMaxLength;
        private bool _maxLengthReached;

        private LineRenderer _lineRenderer;
        private Color _ropeOriginalColor;

        public void Anchor(Rigidbody2D body)
        {
            var ropeBody = _ropeConnections.Find(v => v.Body == body);
            if (ropeBody == null)
            {
                Debug.LogError("Could not find attached RigidBody2D to rope");
            }

            ropeBody.IsAnchored = true;
        }

        public void Unanchor(Rigidbody2D body)
        {
            var ropeBody = _ropeConnections.Find(v => v.Body == body);
            if (ropeBody == null)
            {
                Debug.LogError("Could not find attached RigidBody2D to rope");
            }

            ropeBody.IsAnchored = false;
        }

        public void Attach(Rigidbody2D body)
        {
            var ropeBody = _ropeConnections.Find(v => v.Body == body);
            if (ropeBody == null)
            {
                _ropeConnections.Add(new RopeAttachedBody { Body = body, IsAnchored = false });
            }
        }

        public void Detach(Rigidbody2D body)
        {
            var ropeBody = _ropeConnections.Find(v => v.Body == body);
            if (ropeBody != null)
            {
                _ropeConnections.Remove(ropeBody);
            }
        }

        public void PullRope()
        {
            _currentMaxLength = PullLength;
        }

        public void UnpullRope()
        {
            _currentMaxLength = MaxLength;
        }

        public bool MaxLengthReached()
        {
            return _maxLengthReached;
        }

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _ropeOriginalColor = _lineRenderer.startColor;
            _currentMaxLength = MaxLength;
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
            float sumDistance = 0;
            for (int i = 0; i < _ropeConnections.Count - 1; i++)
            {
                sumDistance += Vector2.Distance(_ropeConnections[i].Body.position, _ropeConnections[i + 1].Body.position);
            }

            _maxLengthReached = sumDistance > _currentMaxLength;
        }

        private void ExecuteRopeForces()
        {
            if (_ropeConnections.Count <= 1)
            {
                return;
            }
            if (!_maxLengthReached) return;

            for (int i = 0; i < _ropeConnections.Count; i++)
            {
                if (_ropeConnections[i].IsAnchored)
                    continue;

                if (i == 0)
                {
                    ForceBodyToOtherBody(_ropeConnections[i].Body, _ropeConnections[i + 1].Body);
                }
                else if (i == _ropeConnections.Count - 1)
                {
                    ForceBodyToOtherBody(_ropeConnections[i].Body, _ropeConnections[i - 1].Body);
                }
                else
                {
                    ForceBodyToOtherBody(_ropeConnections[i].Body, _ropeConnections[i + 1].Body);
                    ForceBodyToOtherBody(_ropeConnections[i].Body, _ropeConnections[i - 1].Body);
                }
            }
        }

        private void ForceBodyToOtherBody(Rigidbody2D thisBody, Rigidbody2D otherBody)
        {
            var direction = otherBody.position - thisBody.position;
            thisBody.AddForce(direction.normalized * PullStrength);
        }

        private void DrawRope()
        {
            if(_ropeConnections.Count <= 1)
            {
                _lineRenderer.enabled = false;
                return;
            }
            _lineRenderer.enabled = true;
            var color = _maxLengthReached ? Color.red : _ropeOriginalColor;
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;

            for (int i = 0; i < _ropeConnections.Count; i++)
            {
                _lineRenderer.SetPosition(i, _ropeConnections[i].Body.position);
            }
        }

        private class RopeAttachedBody
        {
            public Rigidbody2D Body;
            public bool IsAnchored;
        }
    }
}