using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.CustomEffects.StatusEffects
{
    public class StatusEffectWeakenState : StatusEffectState
    {
        public const string StatusId = "weaken";

        public override bool TestTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
        {
            if (inputTriggerParams.attacked != null && inputTriggerParams.attacked.IsAlive)
            {
                return TestAttackTrigger(inputTriggerParams, outputTriggerParams);
            }
            else
            {
                return true;
            }
        }

        private bool TestAttackTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
        {
            CharacterState characterState = null;
            if (inputTriggerParams.attacked != null && inputTriggerParams.attacked.IsAlive)
            {
                characterState = inputTriggerParams.attacked;
            }
            if (characterState == null)
            {
                return false;
            }
            if (inputTriggerParams.attacker == null)
            {
                return false;
            }
            if (characterState.HasStatusEffect(VanillaStatusEffectIDs.Phased))
            {
                return false;
            }
            if (characterState.GetStatusEffectStacks(VanillaStatusEffectIDs.DamageShield) > 0)
            {
                return true;
            }
            int statusEffectStacks = characterState.GetStatusEffectStacks(StatusId);
            int damageAdded = GetEffectMagnitude(statusEffectStacks);
            outputTriggerParams.damage = inputTriggerParams.damage + damageAdded;
            outputTriggerParams.count = statusEffectStacks;

            return outputTriggerParams.damage != inputTriggerParams.damage;
        }

        public override int GetEffectMagnitude(int stacks = 1)
        {
            return GetParamInt() * stacks;
        }
    }
}
