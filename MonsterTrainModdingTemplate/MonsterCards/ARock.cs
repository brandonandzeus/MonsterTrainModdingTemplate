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
    /// Example of a monster card that has an effect generating Cards.
    /// </summary>
    public class ARock
    {
        public static readonly string ID = TestPlugin.CLANID + "_ARockCard";
        public static readonly string CharID = TestPlugin.CLANID + "_ARockCharacter";
        public static readonly string TriggerID = TestPlugin.CLANID + "_ARockExtinguish";

        public static void BuildAndRegister()
        {
            var myMorselPool = new CardPoolBuilder
            {
                CardIDs =
                {
                    AppleMorsel.ID,
                    VanillaCardIDs.MorselJeweler,
                    VanillaCardIDs.RubbleMorsel,
                    VanillaCardIDs.MorselJeweler,
                    VanillaCardIDs.RubbleMorsel,
                }
            }.BuildAndRegister();

            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "A Rock",
                Size = 2,
                AttackDamage = 0,
                Health = 10,
                AssetPath = "assets/ARock_Character.png",
                PriorityDraw = true,
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnDeath,
                        Description = "Add two random morsel cards to your hand",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddBattleCard),
                                TargetTeamType = Team.Type.None,
                                TargetMode = TargetMode.Self,
                                // ParamInt is where to add the cards
                                ParamInt = (int) CardPile.HandPile,
                                // AdditionalParamInt is the number of cards to add.
                                AdditionalParamInt = 2,
                                ParamCardPool = myMorselPool,
                            }
                        }
                    },
                },
            };

            // Required function call to ensure the Cards present in the pool have been loaded.
            // Normally the game_assets parameter should be set to false, but if you are testing this card while not playing Umbra it should be set to true
            // You will encounter an error message saying "Game Data is corrupted" so it is set to true here for testing.
            // game_assets should only be set to true if the CardPool is part of a StoryReward, a Scourge/Blight, or any card that can be
            // obtained regardless of clan combo.
            CustomCardPoolManager.MarkCardPoolForPreloading(myMorselPool, clan_assets: true, game_assets: true);

            new CardDataBuilder
            {
                CardID = ID,
                Name = "A Rock",
                Cost = 0,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/ARock.png",
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
