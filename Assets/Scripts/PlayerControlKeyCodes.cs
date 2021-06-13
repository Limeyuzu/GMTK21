using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerControlKeyCodes
    {
        public static KeyCode MOVE_UP = KeyCode.W;
        public static KeyCode MOVE_DOWN = KeyCode.S;
        public static KeyCode MOVE_LEFT = KeyCode.A;
        public static KeyCode MOVE_RIGHT = KeyCode.D;

        public static KeyCode SPECIAL_ABILITY = KeyCode.LeftShift;
        public static KeyCode PULL_ROPE = KeyCode.E;
        public static KeyCode SWITCH_CHARACTER = KeyCode.Q;
        public static KeyCode THROW_OBJECT = KeyCode.Space;

        public static KeyCode DETACH_ROPE = KeyCode.None;
    }
}