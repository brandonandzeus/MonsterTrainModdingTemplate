using HarmonyLib;
using MonsterTrainModdingTemplate.Clans;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.CustomCardTraits;
using Trainworks.Managers;
using static CardStatistics;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    /// <summary>
    /// Example of a monster card that has an effect spawning units.
    /// </summary>
    public class ABoulder
    {
        public static readonly string ID = TestPlugin.CLANID + "_ABoulderCard";
        public static readonly string CharID = TestPlugin.CLANID + "_ABoulderCharacter";
        public static readonly string TriggerID = TestPlugin.CLANID + "_ABoulderResolve";

        public static void BuildAndRegister()
        {
            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "A Boulder",
                Size = 3,
                AttackDamage = 0,
                Health = 50,
                AssetPath = "assets/ABoulder_Character.png",
                PriorityDraw = true,
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.PostCombat,
                        Description = "Spawns a Rock",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectSpawnMonster),
                                TargetTeamType = Team.Type.None,
                                TargetMode = TargetMode.Self,
                                // ParamInt is number to spawn
                                ParamInt = 1,
                                ParamCharacterData = CustomCharacterManager.GetCharacterDataByID(ARock.CharID),
                            }
                        }
                    },
                },
            };

            CustomCharacterManager.MarkCharacterForPreloading(CustomCharacterManager.GetCharacterDataByID(ARock.CharID));

            new CardDataBuilder
            {
                CardID = ID,
                Name = "A Boulder",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/ABoulder.png",
                ClanID = Clan.ID,
                CardPoolIDs = { VanillaCardPoolIDs.UmbraBanner, VanillaCardPoolIDs.UnitsAllBanner },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = character
                    }
                },
            }.BuildAndRegister();
        }
    }
}