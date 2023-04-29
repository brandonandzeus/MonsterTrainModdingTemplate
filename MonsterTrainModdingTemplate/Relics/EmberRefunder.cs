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
                Description = "Gain 1 ember for every 2 train steward in your deck.",
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
                                ParamTrackTriggerCount = true,
                                AllowMultipleTriggersPerDuration = true,
                                ParamInt = 2,
                                ParamComparator = RelicEffectCondition.Comparator.GreaterThan,
                            }
                        }
                    }
                },
                Rarity = CollectableRarity.Common
            }.BuildAndRegister();
        }
    }
}
