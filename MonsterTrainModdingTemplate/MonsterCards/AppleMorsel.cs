using System.Collections.Generic;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    class AppleMorsel
    {
        public static readonly string ID = TestPlugin.CLANID + "_AppleMorselCard";
        public static readonly string CharID = TestPlugin.CLANID + "_AppleMorselCharacter";
        public static readonly string TriggerID = TestPlugin.CLANID + "_AppleMorselEaten";

        public static void BuildAndRegister()
        {
            var addRage = new CardEffectDataBuilder
            {
                EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                TargetMode = TargetMode.LastFeederCharacter,
                TargetTeamType = Team.Type.Monsters,
                ParamStatusEffects =
                {
                    new StatusEffectStackData
                    {
                        statusId = Trainworks.ConstantsV2.VanillaStatusEffectIDs.Rage,
                        count = 3
                    }
                }
            };

            var deal5Damage = new CardEffectDataBuilder
            {
                EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                TargetMode = TargetMode.LastFeederCharacter,
                TargetTeamType = Team.Type.Monsters,
                ParamInt = 5
            };

            var appleMorselCharacter = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Apple Morsel",
                Size = 1,
                Health = 1,
                AttackDamage = 0,
                AssetPath = "assets/applemorsel_character.png",
                SubtypeKeys = { VanillaSubtypeIDs.Morsel },
                PriorityDraw = false,
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnEaten,
                        Description = "Eater takes {[effect1.power]} damage and gains <nobr><b>Rage</b> <b>{[effect0.status0.power]}</b></nobr>",
                        EffectBuilders =
                        {
                            addRage,
                            deal5Damage,
                        }
                    }
                }
            };

            List<string> cardPoolIDs = new List<string>();
            for (int i = 0; i < 10; i++)
                cardPoolIDs.Add(Trainworks.ConstantsV2.VanillaCardPoolIDs.MorselPool);
            for (int i = 0; i < 110; i++)
                cardPoolIDs.Add(Trainworks.ConstantsV2.VanillaCardPoolIDs.MorselPoolStarter);

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Apple Morsel",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/applemorsel.png",
                ClanID = Trainworks.ConstantsV2.VanillaClanIDs.Umbra,
                CardPoolIDs = cardPoolIDs,
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = appleMorselCharacter
                    }
                }
            }.BuildAndRegister();
        }
    }
}
