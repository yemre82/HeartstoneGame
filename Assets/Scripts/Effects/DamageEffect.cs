using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Effects
{
    public class DamageEffect : ICardEffect
    {
        private int damage;
        private Enemy enemyTarget;
        private Player playerTarget;

        public DamageEffect(int dmg, Enemy target, Player player, bool isPlayer = false)
        {
            if (isPlayer)
            {
                damage = dmg;
                enemyTarget = target;
            }
            else
            {
                damage = dmg;
                playerTarget = player;
            }

        }

        public void ApplyEffect()
        {
            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damage);
                Debug.Log($"Enemy took {damage} damage!");
            }
            else if (playerTarget != null)
            {
                playerTarget.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage!");
            }
        }
    }
}