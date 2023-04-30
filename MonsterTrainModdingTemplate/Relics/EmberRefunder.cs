using MonsterTrainModdingTemplate.Clans;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.Relics
{
    public class EmberRefunder
    {
        public static readonly string ID = TestPlugin.CLANID + "_EmberRefunder";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                Name = "Ember Refunder",
                Description = "Gain 1 ember as long as you have at least 4 train stewards in your deck.",
                RelicPoolIDs = { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "assets/ember.png",
                ClanID = Clan.ID,
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectModifyEnergy),
                        ParamInt = 1,
                        ParamSourceTeam = Team.Type.Monsters,
                        EffectConditionBuilders =
                        {
                            new RelicEffectConditionBuilder
                            {
                                ParamTrackedValue = CardStatistics.TrackedValueType.SubtypeInDeck,
                                ParamCardType = CardStatistics.CardTypeTarget.Monster,
                                ParamSubtype = VanillaSubtypeIDs.Steward,
                                ParamTrackTriggerCount = false,
                                AllowMultipleTriggersPerDuration = false,
                                ParamInt = 4,
                                ParamComparator = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan,
                            }
                        }
                    }
                },
                Rarity = CollectableRarity.Common
            }.BuildAndRegister();
        }
    }
}
