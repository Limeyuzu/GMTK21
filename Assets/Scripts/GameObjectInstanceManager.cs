using UnityEngine;

namespace Assets.Scripts
{
    public class GameObjectInstanceManager : MonoBehaviour
    {
        [SerializeField] PlayerCharacter Character1Prefab;
        [SerializeField] Transform Character1Spawn;
        [SerializeField] PlayerCharacter Character2Prefab;
        [SerializeField] Transform Character2Spawn;
        [SerializeField] Rope RopePrefab;

        public static PlayerCharacter GetPlayer1() => _character1;
        public static PlayerCharacter GetPlayer2() => _character2;
        public static IRope GetPlayerRope() => _playerRope;


        private static IRope _playerRope;
        private static PlayerCharacter _character1;
        private static PlayerCharacter _character2;

        private void Awake()
        {
            _character1 = Instantiate(Character1Prefab, Character1Spawn.position, Quaternion.identity);
            _character2 = Instantiate(Character2Prefab, Character2Spawn.position, Quaternion.identity);
            _playerRope = Instantiate(RopePrefab, _character1.transform);
        }
    }
}

