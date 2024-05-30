using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.CardTraits;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class TrinityBurst
    {
        public static readonly string ID = TestPlugin.CLANID + "_TrinityBurst";

        public static void BuildAndRegister()
        {
            var damage = new CardEffectDataBuilder
            {
                EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                ParamInt = 1,
                TargetTeamType = Team.Type.Heroes,
                TargetMode = TargetMode.RandomInRoom,
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Trinity Burst",
                Description = "Deal [effect0.power] damage to a random unit three times",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = true,
                ClanID = Clan.ID,
                AssetPath = "assets/trinityburst.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    damage, damage, damage
                },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitTrinity),
                        // You can automatically activate the Trinity version by setting this to 2.
                        //ParamInt = 2,
                    },
                    /*new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitIntrinsicState),
                    },
                    new CardTraitDataBuilder
                    {
                        TraitStateType = typeof(CardTraitRetain),
                    }*/
                }
            }.BuildAndRegister();
        }
    }
}