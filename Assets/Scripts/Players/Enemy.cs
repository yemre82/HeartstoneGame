using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Enemy : MonoBehaviour
    {
        public Action OnIsDead;
        public Action<int> OnHealthChanged;
        public Action<int> OnManaChanged;
        public int health = 20;
        public int mana = 5;
        public int maxHealth = 20;
        public int maxMana = 10;
        private int attackModifier = 0;
        private int healModifier = 0;
        private List<BuffDebuffEffect> activeEffects = new List<BuffDebuffEffect>();

        public void ApplyEffect(BuffDebuffEffect effect)
        {
            activeEffects.Add(effect);
            UpdateModifiers();
        }

        public int GetModifiedAttack(int baseAttack)
        {
            return baseAttack + attackModifier;
        }

        public int GetModifiedHeal(int baseHeal)
        {
            return baseHeal + healModifier;
        }

        public void UpdateEffects()
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                activeEffects[i].duration--;

                if (activeEffects[i].duration <= 0)
                {
                    activeEffects.RemoveAt(i);
                }
            }

            UpdateModifiers();
        }

        private void UpdateModifiers()
        {
            attackModifier = 0;
            healModifier = 0;

            foreach (var effect in activeEffects)
            {
                if (effect.type == EffectTypeEnum.Buff)
                {
                    attackModifier += effect.attackModifier;
                    healModifier += effect.healModifier;
                }
                else if (effect.type == EffectTypeEnum.Debuff)
                {
                    attackModifier -= effect.attackModifier;
                    healModifier -= effect.healModifier;
                }
            }
        }

        public void Heal(int amount)
        {
            int finalHeal = GetModifiedHeal(amount);
            health = Mathf.Min(health + amount, maxHealth);
            OnHealthChanged?.Invoke(health);
            Debug.Log($"Enemy Healed: {finalHeal} (Total Health: {health})");
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            OnHealthChanged?.Invoke(health);
            Debug.Log($"Enemy Health: {health}");

            if (health <= 0)
            {
                Debug.Log("Enemy Defeated!");
                OnIsDead?.Invoke();
                Destroy(gameObject);
            }
        }

        public void GainMana()
        {
            mana += 5;
            OnManaChanged?.Invoke(mana);
            Debug.Log($"Enemy gained 5 mana. Total Mana: {mana}");
        }

        public bool SpendMana(int cost)
        {
            if (mana >= cost)
            {
                mana -= cost;
                OnManaChanged?.Invoke(mana);
                Debug.Log($"Mana spent: {cost}. Remaining Mana: {mana}");
                return true;
            }
            Debug.Log("Enemy does not have enough mana!");
            return false;
        }
    }
}
