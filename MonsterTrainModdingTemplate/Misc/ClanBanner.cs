using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.MonsterCards;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.Misc
{
    class ClanBanner
    {
        public static readonly string ID = Clan.ID + "_Banner";
        public static readonly string RewardID = Clan.ID + "_BannerReward";
        public static readonly string CardPoolID = Clan.ID + "_BannerUnits";

        public static void BuildAndRegister()
        {
            CardPool cardPool = new CardPoolBuilder
            {
                CardPoolID = CardPoolID,
                CardIDs =
                {
                    BlueEyesWhiteDragon.ID,
                    DragonCostume.ID,
                },
            }.BuildAndRegister();

            new RewardNodeDataBuilder()
            {
                RewardNodeID = ID,
                MapNodePoolIDs =
                {
                    VanillaMapNodePoolIDs.RandomChosenMainClassUnit,
                    VanillaMapNodePoolIDs.RandomChosenSubClassUnit
                },
                Name = "Test Clan Banner",
                Description = "Offers units from the illustrious Test Clan",
                RequiredClass = CustomClassManager.GetClassDataByID(Clan.ID),
                ControllerSelectedOutline = "assets/TestClanBanner_ControllerSelectedOutline.png",
                FrozenSpritePath = "assets/TestClanBanner_Frozen.png",
                EnabledSpritePath = "assets/TestClanBanner_Enabled.png",
                EnabledVisitedSpritePath = "assets/TestClanBanner_Enabled_Visited.png",
                DisabledSpritePath = "assets/TestClanBanner_Disabled.png",
                DisabledVisitedSpritePath = "assets/TestClanBanner_Disabled_Visited.png",
                GlowSpritePath = "assets/TestClanBanner_Glow.png",
                MapIconPath = "assets/TestClanBanner_Enabled.png",
                MinimapIconPath = "assets/TestClanBanner_Minimap.png",
                SkipCheckInBattleMode = true,
                OverrideTooltipTitleBody = false,
                NodeSelectedSfxCue = "Node_Banner",
                RewardBuilders =
                {
                    new DraftRewardDataBuilder()
                    {
                        DraftRewardID = RewardID,
                        AssetPath = "assets/TestClanBanner_Enabled.png",
                        Name = "Test Clan Banner",
                        Description = "Choose a card!",
                        Costs = new int[] { 100 },
                        IsServiceMerchantReward = false,
                        DraftPool = cardPool,
                        ClassType = RunState.ClassType.MainClass | RunState.ClassType.SubClass | RunState.ClassType.NonClass,
                        DraftOptionsCount = 2,
                        RarityFloorOverride = CollectableRarity.Uncommon
                    }
                }
            }.BuildAndRegister();
        }
    }
}
