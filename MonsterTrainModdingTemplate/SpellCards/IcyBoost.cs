using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.SpellCards
{
    public class IcyBoost
    {
        public static readonly string ID = TestPlugin.CLANID + "_IcyBoost";
        public static readonly string UpgradeID = TestPlugin.CLANID + "_IcyBoostCardUpgrade";
        public static readonly string UpgradeMaskID = TestPlugin.CLANID + "_IcyBoostCardUpgradeMask";
        public static readonly string UpgradeMaskBannedPoolID = TestPlugin.CLANID + "_IcyBoostCardUpgradeMaskBannedPool";

        public static void BuildAndRegister()
        {
            // Ban these card from the effect as they would break if upgraded.
            var bannedCardPool = new CardPoolBuilder
            {
                CardPoolID = UpgradeMaskBannedPoolID,
                CardIDs =
                {
                    VanillaCardIDs.UnleashtheWildwood,
                    VanillaCardIDs.AdaptiveMutation,
                }
            }.Build();

            var onlyDamagingHealingSpells = new CardUpgradeMaskDataBuilder
            {
                CardUpgradeMaskDataID = UpgradeMaskID,
                CardType = CardType.Spell,
                RequiredCardEffectsOperator = CardUpgradeMaskDataBuilder.CompareOperator.Or,
                RequiredCardEffects =
                {
                    "CardEffectDamage",
                    "CardEffectHeal",
                    "CardEffectHealAndDamageRelative" 
                },
                DisallowedCardPools = { bannedCardPool },
            };

            var cheapen = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                BonusDamage = 30,
                BonusHeal = 30,
                CostReduction = 2,
                XCostReduction = 2,
                TraitDataUpgradeBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitFreeze),
                    }
                },
                FiltersBuilders =
                {
                    onlyDamagingHealingSpells,
                }
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Icy Boost",
                Description = "Apply +[effect0.upgrade.bonusdamage] [magicpower], -[effect0.upgrade.costreduction][ember], and [permafrost] to spells in hand.",
                Cost = 2,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = VanillaClanIDs.Stygian,
                // Image credit from Stockvault stockvault-melting-ice-cube294324.
                AssetPath = "assets/icyboost.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectAddTempCardUpgradeToCardsInHand),
                        TargetMode = TargetMode.Hand,
                        TargetTeamType = Team.Type.Monsters,
                        ParamCardUpgradeDataBuilder = cheapen,
                    }
                },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitExhaustState),
                    }
                }
            }.BuildAndRegister();
        }
    }
}
