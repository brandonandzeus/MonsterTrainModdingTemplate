using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.Misc;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    class DragonCostume
    {
        public static readonly string ID = TestPlugin.CLANID + "_DragonCostumeCard";
        public static readonly string CharID = TestPlugin.CLANID + "_DragonCostumeCharacter";
        public static readonly string TriggerID = TestPlugin.CLANID + "_DragonCostumeRevenge";

        public static void BuildAndRegister()
        {
            var dragonCostumeCharacter = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Dragon Costume",
                Size = 5,
                Health = 50,
                AttackDamage = 5,
                AssetPath = "assets/dragoncostume_character.png",
                SubtypeKeys = { Subtypes.Dragon },
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnHit,
                        Description = "Gain <nobr><b>Damage Shield</b> <b>{[effect0.status0.power]}</b></nobr>",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData
                                    {
                                        count = 1,
                                        statusId = VanillaStatusEffectIDs.DamageShield
                                    }
                                }
                            }
                        }
                    }
                }
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Dragon Costume",
                Cost = 2,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/dragoncostume.png",
                ClanID = Clan.ID,
                CardPoolIDs = { VanillaCardPoolIDs.StygianBanner, VanillaCardPoolIDs.UnitsAllBanner },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = dragonCostumeCharacter
                    }
                }
            }.BuildAndRegister();
        }
    }
}
