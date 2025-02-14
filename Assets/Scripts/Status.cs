using UnityEngine;
using static Status;

public class Status : MonoBehaviour
{
    [System.Serializable]
    public struct battleData
    {
        public int attackPower;
        public int hp;
        public float attackSpeed;
        public int cost;
        public float moveSpeed;
    }

}
