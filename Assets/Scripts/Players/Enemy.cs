using System;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Enemy : MonoBehaviour
    {
        public Action OnIsDead;

        public int health = 20;

        public void TakeDamage(int amount)
        {
            health -= amount;
            Debug.Log($"Enemy Health: {health}");

            if (health <= 0)
            {
                Debug.Log("Enemy Defeated!");
                OnIsDead?.Invoke();
                Destroy(gameObject);
            }
        }

        public void Heal(int amount)
        {
            health += amount;
            Debug.Log($"Enemy Health: {health}");
        }
    }
}