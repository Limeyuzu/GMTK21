using UnityEngine;

namespace Assets.Scripts
{
    public interface IRope
    {
        void Anchor(Rigidbody2D body);
        void Attach(Rigidbody2D body);
        void Detach(Rigidbody2D body);
        bool MaxLengthReached();
        void PullRope();
        void Unanchor(Rigidbody2D body);
        void UnpullRope();
    }
}