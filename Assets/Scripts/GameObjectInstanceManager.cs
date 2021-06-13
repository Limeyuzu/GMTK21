using UnityEngine;

namespace Assets.Scripts
{
    public class GameObjectInstanceManager : MonoBehaviour
    {
        [SerializeField] PlayerCharacter Character1Prefab;
        [SerializeField] PlayerCharacter Character2Prefab;
        [SerializeField] Rope RopePrefab;

        public static PlayerCharacter GetPlayer1() => _character1;
        public static PlayerCharacter GetPlayer2() => _character2;
        public static IRope GetPlayerRope() => _playerRope;


        private static IRope _playerRope;
        private static PlayerCharacter _character1;
        private static PlayerCharacter _character2;

        void Awake()
        {
            _character1 = Instantiate(Character1Prefab);
            _character2 = Instantiate(Character2Prefab);
            var character1Body = _character1.GetComponent<Rigidbody2D>();
            var character2Body = _character2.GetComponent<Rigidbody2D>();

            _playerRope = Instantiate(RopePrefab, _character1.transform);
            _playerRope.Attach(character1Body);
            _playerRope.Anchor(character1Body);
            _playerRope.Attach(character2Body);
        }
    }
}

