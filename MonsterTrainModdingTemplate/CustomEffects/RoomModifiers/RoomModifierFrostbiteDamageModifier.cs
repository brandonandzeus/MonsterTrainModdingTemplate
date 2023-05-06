using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Trainworks.ConstantsV2;
using Trainworks.Managers;
using UnityEngine;

namespace MonsterTrainModdingTemplate.CustomEffects.RoomModifiers
{
    public class RoomStateFrostbiteDamageModifier : RoomStateModifierBase, IRoomStateDamageModifier, IRoomStateModifier, ILocalizationParamInt, ILocalizationParameterContext
    {
        private int modifiedDamage;

        public override void Initialize(RoomModifierData roomModifierData, RoomManager roomManager)
        {
            base.Initialize(roomModifierData, roomManager);
            modifiedDamage = roomModifierData.GetParamInt();
        }

        public int GetModifiedDamage(Damage.Type damageType, CharacterState attacker, bool requestingForCharacterStats)
        {
            if (requestingForCharacterStats)
                return 0;

            // Note Frostbite's StatusID is actually called Poison.
            if (damageType != Damage.Type.Poison)
            {
                return 0;
            }

            // Note that "attacker" in this context is the target with Frostbite.
            // Additionally the stacks of frostbite will have already decremented by the time this is called so add 1).
            // However if the user has Cuttlebeard it will have not been decremented so don't in that case.
            int stacks = attacker.GetStatusEffectStacks(VanillaStatusEffectIDs.Frostbite);
            if (!ProviderManager.SaveManager.GetHasRelic(CustomCollectableRelicManager.GetRelicDataByID(VanillaRelicIDs.Cuttlebeard)))
            {
                stacks += 1;
            }

            return modifiedDamage * stacks;
        }
    }
}
