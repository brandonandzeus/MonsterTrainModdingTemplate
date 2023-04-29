using MonsterTrainModdingTemplate;
using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.SpellCards;
using System.Collections.Generic;
using Trainworks.BuildersV2;
using Trainworks.ConstantsV2;
using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.Champions
{
    class Champion
    {
        public static readonly string ID = TestPlugin.CLANID + "_Champion";
        public static readonly string CharID = TestPlugin.CLANID + "_ChampionCharacter";

        public static void BuildAndRegister()
        {
            var championCharacterBuilder = new CharacterDataBuilder
            {
                CharacterID = CharID,
                Name = "Slimeboy",
                Size = 1,
                Health = 10,
                AttackDamage = 5,
                AssetPath = "assets/slimeboy-character.png"
            };

            new ChampionCardDataBuilder()
            {
                Champion = championCharacterBuilder,
                ChampionIconPath = "assets/slimeboy-character.png",
                //StarterCardData = CustomCardManager.GetCardDataByID(VanillaCardIDs.AlphaFiend),
                StarterCardData = CustomCardManager.GetCardDataByID(Rustify.ID),
                CardID = ID,
                Name = "Slimeboy",
                ClanID = Clan.ID,
                UpgradeTree = FormUpgradeTree(),
                AssetPath = "assets/slimeboy.png"
            }.BuildAndRegister(0);
        }

        public static CardUpgradeTreeDataBuilder FormUpgradeTree()
        {
            return new CardUpgradeTreeDataBuilder
            {
                UpgradeTrees =
                {
                    new List<CardUpgradeDataBuilder> { SpikeyBoiI(), SpikeyBoiII(), SpikeyBoiIII() },
                    // Normally champions have 3 upgrade paths, but 1 is the minimum required.
                }
            };
        }

        public static CardUpgradeDataBuilder SpikeyBoiI()
        {
            return new CardUpgradeDataBuilder
            {
                UpgradeID = TestPlugin.CLANID + "_SpikeyI",
                UpgradeTitle = "Spikey I",
                BonusHP = 10,
                BonusDamage = 10,
                TriggerUpgradeBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TestPlugin.CLANID + "_SpikeyTriggerI",
                        Trigger = CharacterTriggerData.Trigger.OnHit,
                        Description = "Gain [spikes] [effect0.status0.power]",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Spikes, count = 1}
                                }
                            }
                        }
                    }
                }
            };
        }

        public static CardUpgradeDataBuilder SpikeyBoiII()
        {
            return new CardUpgradeDataBuilder
            {
                UpgradeID = TestPlugin.CLANID + "_SpikeyII",
                UpgradeTitle = "Spikey II",
                BonusHP = 40,
                BonusDamage = 20,
                TriggerUpgradeBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TestPlugin.CLANID + "_SpikeyTriggerII",
                        Trigger = CharacterTriggerData.Trigger.OnHit,
                        Description = "Gain [spikes] [effect0.status0.power]",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Spikes, count = 3}
                                }
                            }
                        }
                    }
                }
            };
        }

        public static CardUpgradeDataBuilder SpikeyBoiIII()
        {
            return new CardUpgradeDataBuilder
            {
                UpgradeID = TestPlugin.CLANID + "_SpikeyIII",
                UpgradeTitle = "Spikey III",
                BonusHP = 80,
                BonusDamage = 40,
                TriggerUpgradeBuilders =
                {
                    new CharacterTriggerDataBuilder
                    {
                        TriggerID = TestPlugin.CLANID + "_SpikeyTriggerIII",
                        Trigger = CharacterTriggerData.Trigger.OnHit,
                        Description = "Gain [spikes] [effect0.status0.power]",
                        EffectBuilders =
                        {
                            new CardEffectDataBuilder
                            {
                                EffectStateType = typeof(CardEffectAddStatusEffect),
                                TargetMode = TargetMode.Self,
                                TargetTeamType = Team.Type.Monsters,
                                ParamStatusEffects =
                                {
                                    new StatusEffectStackData {statusId = VanillaStatusEffectIDs.Spikes, count = 7}
                                }
                            }
                        }
                    }
                }
            };
        }

    }
}
