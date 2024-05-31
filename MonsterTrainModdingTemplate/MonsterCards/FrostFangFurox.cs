using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.CustomEffects.RoomModifiers;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    /// <summary>
    /// Example of a Monster Card with a Custom RoomModifier.
    /// </summary>
    class FrostFangFerox
    {
        public static readonly string ID = TestPlugin.CLANID + "_FrostFangFeroxCard";
        public static readonly string CharID = TestPlugin.CLANID + "_FrostFangFeroxCharacter";
        public static readonly string RoomModifierID = TestPlugin.CLANID + "_FrostFangFeroxRoomModifier";
        public static readonly string TriggerID = TestPlugin.CLANID + "_FrostFangFeroxAttacking";

        public static void BuildAndRegister()
        {
            var character = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Frostfang Ferox",
                Size = 3,
                AttackDamage = 10,
                Health = 30,
                AssetPath = "assets/FrostfangFerox_Character.png",
                PriorityDraw = true,
                TriggerBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TriggerID,
                        Trigger = CharacterTriggerData.Trigger.OnAttacking,
                        Description = "Apply <nobr>[frostbite] [effect0.status0.power]</nobr> to the attacked unit.",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetTeamType = Team.Type.Heroes,
                                TargetMode = TargetMode.LastAttackedCharacter,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Frostbite, count = 3}
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
                        RoomModifierClassType = typeof(RoomStateFrostbiteDamageModifier),
                        // Note the <br> as its used as part of the Card's Description to make it look nicer as
                        // the trigger text appears on the same line otherwise.
                        Description = "<br>[frostbite] deals [paramint] more damage per stack.",
                        DescriptionInPlay = "[frostbite] deals [paramint] more damage per stack.",
                        ParamInt = 4,
                    }
                }
            };

            new CardDataBuilder
            {
                CardID = ID,
                Name = "Frostfang Ferox",
                Cost = 3,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/FrostfangFerox.png",
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