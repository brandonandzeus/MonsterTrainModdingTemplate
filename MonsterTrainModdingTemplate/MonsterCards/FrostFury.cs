using MonsterTrainModdingTemplate.Clans;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    class FrostFury
    {
        public static readonly string ID = TestPlugin.CLANID + "_FrostFuryCard";
        public static readonly string CharID = TestPlugin.CLANID + "_FrostFuryCharacter";
        public static readonly string RoomModifierID = TestPlugin.CLANID + "_FrostFuryRoomModifier";

        public static void BuildAndRegister()
        {
            /// TODO the room modifier double dips.
            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Frost Fury",
                Size = 3,
                AttackDamage = 20,
                Health = 20,
                AssetPath = "assets/FrostFury_Character.png",
                PriorityDraw = true,
                StartingStatusEffects =
                {
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 3}
                },
                RoomModifierBuilders =
                {
                    new RoomModifierDataBuilder
                    {
                        RoomModifierID = RoomModifierID,
                        RoomModifierClassType = typeof(RoomStateStatusEffectDamageModifier),
                        Description = "Units with [frostbite] take 10 more damage per stack.",
                        ParamInt = 10,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 0},
                        }
                    }
                }
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Frost Fury",
                Cost = 2,
                CardType = CardType.Monster,
                OverrideDescriptionKey = "[frostbite] deals 10 more damage per stack.",
                Rarity = CollectableRarity.Common,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/FrostFury.png",
                ClanID = Clan.ID,
                CardPoolIDs = { VanillaCardPoolIDs.StygianBanner, VanillaCardPoolIDs.UnitsAllBanner },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = typeof(CardEffectSpawnMonster),
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = character
                    }
                }
            }.BuildAndRegister();
        }
    }
}
