using UnityEngine;
namespace Assets.Scripts.Players
{
    public class Player : MonoBehaviour
    {
        public bool canPlay = false;
        public int health = 30;

        public void Heal(int amount)
        {
            health += amount;
            Debug.Log($"Player Health: {health}");
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            Debug.Log($"Player Health: {health}");

            if (health <= 0)
            {
                Debug.Log("Player Defeated!");
                Destroy(gameObject);
            }
        }
    }
}