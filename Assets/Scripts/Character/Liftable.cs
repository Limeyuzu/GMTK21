using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Liftable : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D ObjectRigidBody;
    public bool OpenToLift = true;

    private TrailRenderer _trailRenderer;

    private void Start()
    {
        ObjectRigidBody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    public void ToggleLiftable(bool On)
    {
        OpenToLift = On;
    }
    public bool CheckLiftableStatus()
    {
        return OpenToLift;
    }
    public void Lock()
    {
        ObjectRigidBody.isKinematic = true;
        ObjectRigidBody.velocity = Vector2.zero;
    }
    public void Unlock()
    {
        ObjectRigidBody.isKinematic = false;
    }
    public void RenderTrail()
    {
        if (_trailRenderer)
        {
            _trailRenderer.enabled = true;
        }
    }
}