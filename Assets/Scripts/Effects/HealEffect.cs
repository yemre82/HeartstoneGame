using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Effects
{
    public class HealEffect : ICardEffect
    {
        private int healAmount;
        private Player playerTarget;
        private Enemy enemyTarget;

        public HealEffect(int heal, Player target, Enemy enemy, bool isPlayer = false)
        {
            if (isPlayer)
            {
                healAmount = heal;
                playerTarget = target;
            }
            else
            {
                healAmount = heal;
                enemyTarget = enemy;
            }
        }

        public void ApplyEffect()
        {
            if (playerTarget != null)
            {
                playerTarget.Heal(healAmount);
                Debug.Log($"Player healed for {healAmount} HP!");
            }
            else if (enemyTarget != null)
            {
                enemyTarget.Heal(healAmount);
                Debug.Log($"Enemy healed for {healAmount} HP!");
            }
        }
    }
}