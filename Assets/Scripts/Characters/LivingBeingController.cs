using System;
using System.Collections.Generic;
using OrbReaper;
using OrbReaper.Modifiers;

namespace Characters
{
    public class LivingBeingController
    {
        private readonly LivingBeingModel livingBeingModel;
        private readonly List<TemporaryModifier> incomingDamageModifiers = new();
        private readonly List<TemporaryModifier> healingModifiers = new();
        
        public LivingBeingController(LivingBeingModel livingBeingModel)
        {
            this.livingBeingModel = livingBeingModel;
        }
        
        public bool IsAlive { get; set; }

        public virtual void GetDamage(float rawDamage)
        {
            var finalDamage = ModifyReceivedDamage(rawDamage);
            livingBeingModel.Health -= finalDamage;

            if (livingBeingModel.Health < 0)
            {
                livingBeingModel.Health = 0f;
                Die();
            }

            OnDamageReceived();
        }

        public virtual void ReceiveHealing(float rawHealing)
        {
            var finalHealing = ModifyHealing(rawHealing);
            livingBeingModel.Health += finalHealing;
            livingBeingModel.Health = Math.Min(livingBeingModel.Health, livingBeingModel.MaxHealth);
            OnHealingReceived();
        }

        private void Die()
        {
            IsAlive = false;
        }

        protected virtual void CheckAlive() => IsAlive = livingBeingModel.Health > 0;

        protected virtual void OnDamageReceived()
        {
        }
        
        protected virtual void OnHealingReceived()
        {
        }

        /// <summary>
        /// Modify damage by modifiers if character hase them
        /// </summary>
        /// <param name="receivedDamage"></param>
        /// <returns></returns>
        private float ModifyReceivedDamage(float receivedDamage)
        {
            incomingDamageModifiers.ForEach(hm => receivedDamage = hm.ModifyValue(receivedDamage));
            return receivedDamage;
        }
        
        /// <summary>
        /// Modify healing by modifiers if character hase them
        /// </summary>
        /// <param name="receivedHealing"></param>
        /// <returns></returns>
        private float ModifyHealing(float receivedHealing)
        {
            healingModifiers.ForEach(hm => receivedHealing = hm.ModifyValue(receivedHealing));
            return receivedHealing;
        }
    }
}