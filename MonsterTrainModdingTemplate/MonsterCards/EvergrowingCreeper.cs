using HarmonyLib;
using MonsterTrainModdingTemplate.Clans;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.CustomCardTraits;
using static CardStatistics;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    public class EvergrowingCreeper
    {
        public static readonly string ID = TestPlugin.CLANID + "_EvergrowingCreeperCard";
        public static readonly string CharID = TestPlugin.CLANID + "_EvergrowingCreeperCharacter";
        public static readonly string TriggerID = TestPlugin.CLANID + "_EvergrowingCreeperSummon";
        public static readonly string Trigger2ID = TestPlugin.CLANID + "_EvergrowingCreeperHarvest";
        public static readonly string UpgradeID = TestPlugin.CLANID + "_EvergrowingCreeperBaseUpgrade";
        public static readonly string Upgrade2ID = TestPlugin.CLANID + "_EvergrowingCreeperHarvestUpgrade";

        public static void BuildAndRegister()
        {
            var baseUpgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                // Empty.
            }.BuildAndRegister();

            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Evergrowing Creeper",
                Size = 3,
                AttackDamage = 0,
                Health = 25,
                AssetPath = "assets/EvergrowingCreeper_Character.png",
                PriorityDraw = true,
                StartingStatusEffects =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Endless, count = 1},
                },
                TriggerBuilders =
                {
                    // Scaled upgrade, BaseUpgrade is empty and will be Scaled by the CardTrait.
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnUnscaledSpawn,
                        // No description text here.
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddTempCardUpgradeToUnits),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamCardUpgradeData = baseUpgrade,
                            }
                        }
                    },
                    // This upgrade should not be scaled.
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = Trigger2ID,
                        Trigger = CharacterTriggerData.Trigger.OnAnyHeroDeathOnFloor,
                        Description = "Gains [effect0.upgrade.bonusdamage][attack] [effect0.upgrade.bonushp][health].[halfbreak]",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddTempCardUpgradeToUnits),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamCardUpgradeDataBuilder = new CardUpgradeDataBuilder
                                {
                                    UpgradeID = Upgrade2ID,
                                    BonusDamage = 4,
                                    BonusHP = 1,
                                }
                            }
                        }
                    }
                },
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Evergrowing Creeper",
                Cost = 2,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/EvergrowingCreeper.png",
                ClanID = Clan.ID,
                CardPoolIDs = { VanillaCardPoolIDs.StygianBanner, VanillaCardPoolIDs.UnitsAllBanner },
                Description = "When played, gain +[trait0.power][attack] for each enemy killed by a card.",
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = character
                    }
                },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        // Important Not to use CardTraitScalingUpgradeUnitAttack here.
                        // CardTraitScalingUpgradeUnitAttack is ok to use for Spells but not Units since it will scale any upgrade the unit applies to itself or others.
                        // Try changing to CardTraitScalingUpgradeUnitAttack and see the Harvest effect will get the # Heroes killed bonus applied on top.
                        TraitStateType = typeof(CardTraitScalingUpgradeUnitAttackSafely),
                        // Tracked Value for Heroes killed by Cards. Unfortunately its not All Heroes Killed by status effects and being slain since the TrackedValue is only incremented
                        // If it was a direct result of playing a card.
                        ParamTrackedValue = CardStatistics.TrackedValueType.AnyHeroKilled,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamSubtype = VanillaSubtypeIDs.None,
                        ParamCardType = CardStatistics.CardTypeTarget.Any,
                        ParamInt = 8,
                        ParamUseScalingParams = true,
                        ParamCardUpgradeData = baseUpgrade,
                    },
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitPermafrost),
                    }
                }
            }.BuildAndRegister();
        }
    }

    [HarmonyPatch(typeof(CardStatistics), "OnCharacterKilled")]
    class Test
    {
        public static void Postfix(CharacterState deadCharacter, CardState attackingCard, TrackedValueType valueForTeam)
        {
            Trainworks.Trainworks.Log(deadCharacter + " " + attackingCard + " " + valueForTeam);
        }
    }
}
