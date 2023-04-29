using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class GiveEveryoneArmor
    {
        public static readonly string ID = TestPlugin.CLANID + "_GiveEveryoneArmor";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                Name = "Give Everyone Armor",
                Description = "Give everyone <nobr>[armor] <b>[effect0.status0.power]</b></nobr>.",
                Cost = 0,
                Rarity = CollectableRarity.Uncommon,
                TargetsRoom = true,
                Targetless = true,
                ClanID = Clan.ID,
                AssetPath = "assets/giveeveryonearmor.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.Room,
                        TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData
                            {
                                statusId = VanillaStatusEffectIDs.Armor,
                                count = 2
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}
