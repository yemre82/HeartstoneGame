using UnityEngine;
namespace Assets.Scripts.Core
{
    public class Player : MonoBehaviour
    {
        public int health = 30;

        public void Heal(int amount)
        {
            health += amount;
            Debug.Log($"Player Health: {health}");
        }
    }
}