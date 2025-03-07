using UnityEngine;
using Assets.Scripts.Players;

namespace Assets.Scripts.Effects
{
    public class HealEffect : ICardEffect
    {
        private int healAmount;
        private Player playerTarget;

        public HealEffect(int heal, Player target)
        {
            healAmount = heal;
            playerTarget = target;
        }

        public void ApplyEffect()
        {
            if (playerTarget != null)
            {
                playerTarget.Heal(healAmount);
                Debug.Log($"Player healed for {healAmount} HP!");
            }
        }
    }
}