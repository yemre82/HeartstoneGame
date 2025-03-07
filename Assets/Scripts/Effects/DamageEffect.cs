using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Effects
{
    public class DamageEffect : ICardEffect
    {
        private int damage;
        private Enemy enemyTarget;

        public DamageEffect(int dmg, Enemy target)
        {
            damage = dmg;
            enemyTarget = target;
        }

        public void ApplyEffect()
        {
            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damage);
                Debug.Log($"Enemy took {damage} damage!");
            }
        }
    }
}