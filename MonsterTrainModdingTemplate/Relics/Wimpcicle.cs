using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.Relics
{
    class Wimpcicle
    {
        public static readonly string ID = TestPlugin.CLANID + "_WimpcicleRelic";

        public static void BuildAndRegister()
        {
            var cardPool = new CardPoolBuilder
            {
                CardPoolID = TestPlugin.CLANID + "_TrainStewardCardPool",
                CardIDs = { VanillaCardIDs.TrainSteward }
            }.BuildAndRegister();

            var addTrainSteward = new RelicEffectDataBuilder
            {
                RelicEffectClassType = typeof(RelicEffectAddBattleCardToHand),
                ParamInt = 1,
                ParamCardPool = cardPool,
                ParamTrigger = CharacterTriggerData.Trigger.PreCombat,
                ParamTargetMode = TargetMode.FrontInRoom
            };

            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                Name = "Wimp-cicle",
                Description = "At the start of your turn, add a Train Steward to your hand",
                RelicPoolIDs = { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "assets/wimpcicle.png",
                ClanID = Clan.ID,
                EffectBuilders =
                {
                    addTrainSteward,
                },
                Rarity = CollectableRarity.Common
            }.BuildAndRegister();
        }
    }
}
