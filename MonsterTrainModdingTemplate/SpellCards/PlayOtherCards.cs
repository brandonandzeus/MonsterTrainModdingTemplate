using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class PlayOtherCards
    {
        public static readonly string ID = TestPlugin.CLANID + "_PlayOtherCards";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                Name = "Play Other Cards",
                Description = "Give a friendly unit +<nobr>{[trait0.power]}[attack]</nobr> for each card played this battle.",
                Cost = 2,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                ClanID = Clan.ID,
                AssetPath = "assets/playothercards.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = VanillaCardTraitTypes.CardTraitScalingBuffDamage,
                        ParamTrackedValue = CardStatistics.TrackedValueType.AnyCardPlayed,
                        ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
                        ParamInt = 2,
                        ParamTeamType = Team.Type.Monsters
                    }
                },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
                        TargetMode = TargetMode.DropTargetCharacter,
                        TargetTeamType = Team.Type.Monsters
                    }
                }
            }.BuildAndRegister();
        }
    }
}
