using HarmonyLib;
using System.Collections.Generic;
using static CombatManager;

namespace MonsterTrainModdingTemplate.HarmonyPatches
{
    // Messy patch, completely rewrites frostbite just to change one thing. See below. Prefix return false patches should be avoided if possible.
    /*  [HarmonyPatch(typeof(StatusEffectPoisonState), "OnTriggered")]
        class FrostbiteDamagePatchSetAttacker
        {
            public static bool Prefix(StatusEffectPoisonState __instance, ref IEnumerator __result, StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, CombatManager ___combatManager, CharacterState ___associatedCharacter, int ___stacks)
            {
                CoreSignals.DamageAppliedPlaySound.Dispatch(Damage.Type.Poison);

                var GetDamageAmount = __instance.GetType().GetMethod("GetDamageAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                int damageAmount = (int) GetDamageAmount.Invoke(__instance, new object[] { ___stacks });

                __result =  ___combatManager.ApplyDamageToTarget(damageAmount, ___associatedCharacter, new CombatManager.ApplyDamageToTargetParameters
                {
                    damageType = Damage.Type.Poison,
                    selfTarget = ___associatedCharacter,
                    vfxAtLoc = __instance.GetSourceStatusEffectData()?.GetOnAffectedVFX(),
                    showDamageVfx = true,
                    relicState = inputTriggerParams.suppressingRelic
                });
                return false;
            }
        }
    */

    [HarmonyPatch(typeof(CombatManager), nameof(CombatManager.ApplyDamageToTarget))]
    class FrostbiteDamagePatchSetAttacker
    {
        public static void Prefix(CharacterState target, ref ApplyDamageToTargetParameters parameters)
        {
            // Hack to set selfTarget for Frosbite Damage. This is required later when the damage is calculated to consider what RoomModifiers are in play.
            // Damage.Type.Posion is only used by StatusEffectPoisonState so this should be safe to do.
            // Essentially when computing the damage, selfTarget is used as the attacker which is why its being set here.
            if (parameters.damageType == Damage.Type.Poison)
            {
                parameters.selfTarget = target;
            }
        }
    }

    // This patch runs through all IRoomStateDamageModifier for all units on the floor and modifies the Frostbite Damage.
    [HarmonyPatch(typeof(RoomState), nameof(RoomState.GetRoomStateModifiedDamage))]
    class FrostbiteDamagePatchActivate
    {
        private static List<CharacterState> toProcessCharacters = new List<CharacterState>();
        private static TargetHelper.CollectTargetsData collectTargetsData;

        // Here since its a simple enough, rather than using Reflection to Call the same method from RoomState.
        private static void ResetToCollectTargetsLists(TargetMode targetMode, HeroManager heroManager, MonsterManager monsterManager, RoomManager roomManager)
        {
            toProcessCharacters.Clear();
            collectTargetsData.Reset(targetMode);
            collectTargetsData.heroManager = heroManager;
            collectTargetsData.monsterManager = monsterManager;
            collectTargetsData.roomManager = roomManager;
            if (collectTargetsData.targetModeStatusEffectsFilter == null)
            {
                collectTargetsData.targetModeStatusEffectsFilter = new List<string>();
            }
            collectTargetsData.targetModeStatusEffectsFilter.Clear();
            collectTargetsData.targetModeHealthFilter = CardEffectData.HealthFilter.Both;
        }

        public static void Postfix(RoomState __instance, ref int __result, CharacterState attacker /* unit with frostbite*/, Damage.Type damageType, HeroManager heroManager, MonsterManager monsterManager, RoomManager roomManager)
        {
            if (damageType != Damage.Type.Poison)
            {
                return;
            }

            // Code to grab every unit on the floor
            ResetToCollectTargetsLists(TargetMode.Room, heroManager, monsterManager, roomManager);
            collectTargetsData.targetTeamType = Team.Type.Heroes | Team.Type.Monsters;
            collectTargetsData.roomIndex = __instance.GetRoomIndex();
            TargetHelper.CollectTargets(collectTargetsData, ref toProcessCharacters);

            // Modify the damage by calling each interested RoomModifier.
            foreach (CharacterState toProcessCharacter in toProcessCharacters)
            {
                foreach (IRoomStateModifier roomStateModifier in toProcessCharacter.GetRoomStateModifiers())
                {
                    if (roomStateModifier is IRoomStateDamageModifier roomStateDamageModifier)
                    {
                        __result += roomStateDamageModifier.GetModifiedDamage(damageType, attacker, false);
                    }
                }
            }
        }
    }

}
