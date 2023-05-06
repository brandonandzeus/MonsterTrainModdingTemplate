using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.RelicEffects;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.Relics
{
    public class EmberExchanger
    {
        public static readonly string ID = TestPlugin.CLANID + "_EmberExchanger";

        public static void BuildAndRegister()
        {
            new CollectableRelicDataBuilder
            {
                CollectableRelicID = ID,
                Name = "Ember Exchanger",
                Description = "Draw 1 card for every 1 ember you have at the end of the turn.",
                RelicPoolIDs = { VanillaRelicPoolIDs.MegaRelicPool },
                IconPath = "assets/ember.png",
                ClanID = Clan.ID,
                EffectBuilders =
                {
                    new RelicEffectDataBuilder
                    {
                        RelicEffectClassType = typeof(RelicEffectDrawCardPerEmber),
                        ParamInt = 1,
                        ParamSourceTeam = Team.Type.Monsters,
                    }
                },
                Rarity = CollectableRarity.Common
            }.BuildAndRegister();
        }
    }
}