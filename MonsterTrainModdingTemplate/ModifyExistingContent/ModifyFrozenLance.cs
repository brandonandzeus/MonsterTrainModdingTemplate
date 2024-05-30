using HarmonyLib;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.ModifyExistingContent
{
    class ModifyFrozenLance
    {
        public static void Modify()
        {
            string frozenLanceID = VanillaCardIDs.FrozenLance;
            var frozenLanceData = CustomCardManager.GetCardDataByID(frozenLanceID);

            // Add piercing to Frozen Lance's trait list using reflection
            /*var piercingTrait = new CardTraitData();
            string piercingTraitName = VanillaCardTraitTypes.CardTraitIgnoreArmor.AssemblyQualifiedName;
            piercingTrait.Setup(piercingTraitName);
            frozenLanceData.GetTraits().Add(piercingTrait);*/

            // Add piercing using CardTraitDataBuilder
            var piercingTrait = new CardTraitDataBuilder
            {
                TraitStateType = VanillaCardTraitTypes.CardTraitIgnoreArmor,
            }.Build();
            frozenLanceData.GetTraits().Add(piercingTrait);


            // Set Frozen Lance's damage to 12
            var frozenLanceDamageEffect = frozenLanceData.GetEffects()[0];
            Traverse.Create(frozenLanceDamageEffect).Field("paramInt").SetValue(12);

            // Add 327 stacks of frostbite to the target using reflection.
            /*
            // Instantiate the Frostbite CardEffect
            var frostbiteEffect = new CardEffectData();

            // Set its effect type
            string addStatusEffectName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName;
            Traverse.Create(frostbiteEffect).Field("effectStateName").SetValue(addStatusEffectName);

            // Set targeting mode to be the same one used by Flash Freeze: Last Targeted Characters
            Traverse.Create(frostbiteEffect).Field("targetMode").SetValue(TargetMode.LastTargetedCharacters);

            // Create the Frostbite status and add it to the effect's status array
            StatusEffectStackData frostbiteStatus = new StatusEffectStackData();
            frostbiteStatus.statusId = VanillaStatusEffectIDs.Frostbite;
            frostbiteStatus.count = 327;
            var paramStatusEffects = new StatusEffectStackData[] { frostbiteStatus };
            Traverse.Create(frostbiteEffect).Field("paramStatusEffects").SetValue(paramStatusEffects);

            // Add the Frostbite effect to Frozen Lance's card effect list
            frozenLanceData.GetEffects().Add(frostbiteEffect);
            */

            // Add 327 stacks of frostbite to the target using CardEffectDataBuilder.
            var frostbiteEffect = new CardEffectDataBuilder
            {
                EffectStateType = typeof(CardEffectAddStatusEffect),
                TargetMode = TargetMode.LastTargetedCharacters,
                ParamStatusEffects =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 327}
                },
            }.Build();
            frozenLanceData.GetEffects().Add(frostbiteEffect);
        }
    }
}
