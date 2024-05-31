using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    /// <summary>
    /// Example of a card with a Trigger and RoomModifier.
    /// </summary>
    class FrostFury
    {
        public static readonly string ID = TestPlugin.CLANID + "_FrostFuryCard";
        public static readonly string CharID = TestPlugin.CLANID + "_FrostFuryCharacter";
        public static readonly string RoomModifierID = TestPlugin.CLANID + "_FrostFuryRoomModifier";
        public static readonly string TriggerID = TestPlugin.CLANID + "_FrostFuryHarvest";

        public static void BuildAndRegister()
        {
            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Frost Fury",
                Size = 2,
                AttackDamage = 20,
                Health = 20,
                AssetPath = "assets/FrostFury_Character.png",
                PriorityDraw = true,
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor,
                        Description = "Gain <nobr>[regen] [effect0.status0.power]</nobr>",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.Room,
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

                        // Note tooltipTitle in game is tied to the RoomStateModifier class.
                        RoomModifierClassType = typeof(RoomStateStatusEffectDamageModifier),
                        // Note the <br> as its used as part of the Card's Description to make it look nicer as
                        // the trigger text appears on the same line otherwise.
                        Description = "<br><br>Units with [regen] gain and deal [paramint] additional damage.",
                        DescriptionInPlay = "Units with [regen] gain and deal [paramint] additional damage.",
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