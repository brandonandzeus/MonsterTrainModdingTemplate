using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using UnityEngine;

namespace MonsterTrainModdingTemplate.Enhancers
{
    public class StealthyStone
    {
        public static readonly string ID = TestPlugin.CLANID + "_SteathyStone";
        public static readonly string UpgradeID = TestPlugin.CLANID + "_SteathyStoneUpgrade";
        public static readonly string UpgradeMaskID = TestPlugin.CLANID + "_SteathyStoneUpgradeMask";

        public static void BuildAndRegister()
        {
            var filter = new CardUpgradeMaskDataBuilder
            {
                CardUpgradeMaskID = UpgradeMaskID,
                ExcludeNonAttackingMonsters = true,
                CardType = CardType.Monster,
                CostRange = new Vector2 { x = 0, y = 99 },
            };

            var upgrade = new CardUpgradeDataBuilder
            {
                UpgradeID = UpgradeID,
                UpgradeTitle = "Stealthystone",
                UpgradeDescription = "Upgrade a unit with -5[attack] and [stealth] 5",
                BonusDamage = -5,
                StatusEffectUpgrades =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Stealth, count = 5},
                },
                HideUpgradeIconOnCard = false,
                FiltersBuilders = { filter },
                AssetPath = "assets/StealthyStone.png",
            }.Build();

            new EnhancerDataBuilder
            {
                EnhancerID = ID,
                Name = "Stealthystone",
                Description = "Upgrade a unit with -5[attack] and [stealth] [effect0.upgrade.status0.power]",
                EnhancerPoolIDs = { VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon, VanillaEnhancerPoolIDs.UnitUpgradePoolCommon },
                ClanID = Clan.ID,
                Upgrade = upgrade,
                Rarity = CollectableRarity.Common,
                CardType = CardType.Monster,
                IconPath = "assets/StealthyStone.png",
            }.BuildAndRegister();
        }
    }
}