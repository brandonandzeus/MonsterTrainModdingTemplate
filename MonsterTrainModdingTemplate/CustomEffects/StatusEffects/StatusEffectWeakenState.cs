using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.CustomEffects.StatusEffects
{
    public class StatusEffectWeakenState : StatusEffectState
    {
        // Each status effect subclass has this field defined don't forget it.
        public const string StatusId = "weaken";

        // This is called Based on TriggerStages and AdditionalTriggerStages in the corresponding StatusEffectData
        // If True is returned then Trigger will be called.
        // This is only called when a unit has this status effect.
        // In this case the true/false value doesn't matter as the effect handling is done at the very end of TestAttackTrigger.
        public override bool TestTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
        {
            // If the unit attacked is alive then do some additional testing.
            if (inputTriggerParams.attacked != null && inputTriggerParams.attacked.IsAlive)
            {
                return TestAttackTrigger(inputTriggerParams, outputTriggerParams);
            }
            else
            {
                return true;
            }
        }

        // 
        private bool TestAttackTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
        {
            CharacterState characterState = null;
            // This should always be true given when this is called, but just in case.
            if (inputTriggerParams.attacked != null && inputTriggerParams.attacked.IsAlive)
            {
                characterState = inputTriggerParams.attacked;
            }
            if (characterState == null)
            {
                return false;
            }
            // This catches damage from a Card and not to unit.
            if (inputTriggerParams.attacker == null)
            {
                return false;
            }
            // If the unit has Phased they can't be targeted anyway.
            if (characterState.HasStatusEffect(VanillaStatusEffectIDs.Phased))
            {
                return false;
            }
            // Handler for damage shield since it totally blocks damage.
            if (characterState.GetStatusEffectStacks(VanillaStatusEffectIDs.DamageShield) > 0)
            {
                return true;
            }
            // The effect handling code is here. We get the amount of status the character has.
            // And then add EffectMagnitude stacks to it.
            int statusEffectStacks = characterState.GetStatusEffectStacks(StatusId);
            int damageAdded = GetEffectMagnitude(statusEffectStacks);
            // We adjust the damage here outputTriggerParams is the new amount of damage.
            // and count is the new amount of statusEffectStacks the unit will have.
            outputTriggerParams.damage = inputTriggerParams.damage + damageAdded;
            outputTriggerParams.count = statusEffectStacks;

            return outputTriggerParams.damage != inputTriggerParams.damage;
        }

        public override int GetEffectMagnitude(int stacks = 1)
        {
            // GetParamInt will get the ParamInt from StatusEffectData from the Builder.
            return GetParamInt() * stacks;
        }
    }
}
