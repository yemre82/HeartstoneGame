using UnityEngine;

namespace Assets.Scripts.Players
{
    public enum EffectTypeEnum
    {
        Buff,
        Debuff
    }

    public class BuffDebuffEffect
    {
        public EffectTypeEnum type;
        public int attackModifier;
        public int healModifier;
        public int duration;

        public BuffDebuffEffect(EffectTypeEnum type, int attackModifier, int healModifier, int duration)
        {
            this.type = type;
            this.attackModifier = attackModifier;
            this.healModifier = healModifier;
            this.duration = duration;
        }
    }
}
