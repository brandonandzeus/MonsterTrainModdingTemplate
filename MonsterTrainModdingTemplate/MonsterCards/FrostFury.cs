using MonsterTrainModdingTemplate.Clans;
using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using static CharacterTriggerData;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    class FrostFury
    {
        public static readonly string ID = TestPlugin.CLANID + "_FrostFuryCard";
        public static readonly string CharID = TestPlugin.CLANID + "_FrostFuryCharacter";
        public static readonly string RoomModifierID = TestPlugin.CLANID + "_FrostFuryRoomModifier";
        public static readonly string TriggerID = TestPlugin.CLANID + "_FrostFuryHarvest";

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
                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 3}
                },
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor,
                        Description = "Gain [regen] <b>[effect0.status0.power]</b>",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 2},
                                }
                            }
                        }
                    }
                },
                RoomModifierBuilders =
                {
                    new RoomModifierDataBuilder
                    {
                        RoomModifierID = RoomModifierID,
                        RoomModifierClassType = typeof(RoomStateStatusEffectDamageModifier),
                        Description = "Units with [regen] deal 10 additional damage.",
                        ParamInt = 10,
                        ParamStatusEffects =
                        {
                            new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Regen, count = 0},
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
                OverrideDescriptionKey = "Units with [regen] deal 10 additional damage.",
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
