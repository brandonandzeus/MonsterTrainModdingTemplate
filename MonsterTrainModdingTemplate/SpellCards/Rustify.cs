using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.StatusEffects;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.SpellCards
{
    class Rustify
    {
        public static readonly string ID = TestPlugin.CLANID + "_Rustify";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                Name = "Rustify",
                Description = "Apply <nobr><b>Weaken</b> [effect0.status0.power]</nobr> to the front enemy unit.",
                Cost = 1,
                Rarity = CollectableRarity.Starter,
                TargetsRoom = true,
                Targetless = false,
                ClanID = Clan.ID,
                AssetPath = "assets/rustyshield.png",
                CardPoolIDs = { VanillaCardPoolIDs.MegaPool },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
                        TargetMode = TargetMode.FrontInRoom,
                        TargetTeamType = Team.Type.Heroes,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData
                            {
                                statusId = StatusEffectWeakenState.StatusId,
                                count = 10
                            }
                        }
                    }
                }
            }.BuildAndRegister();
        }
    }
}
