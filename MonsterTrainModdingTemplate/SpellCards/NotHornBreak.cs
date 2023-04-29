using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class NotHornBreak
    {
        public static readonly string ID = TestPlugin.CLANID + "_NotHornBreak";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                Name = "Not Horn Break",
                Description = "Deal [effect0.power] damage",
                Cost = 1,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                ClanID = Clan.ID,
                AssetPath = "assets/nothornbreak.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
                        ParamInt = 5,
                        TargetMode = TargetMode.DropTargetCharacter
                    }
                },
                TraitBuilders =
                {
                    new CardTraitDataBuilder
                    {
                        TraitStateType = VanillaCardTraitTypes.CardTraitIgnoreArmor
                    }
                }
            }.BuildAndRegister();
        }
    }
}
