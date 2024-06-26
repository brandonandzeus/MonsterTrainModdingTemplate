﻿using MonsterTrainModdingTemplate.Clans;
using MonsterTrainModdingTemplate.Misc;
using Trainworks.BuildersV2;
using Trainworks.Constants;

namespace MonsterTrainModdingTemplate.MonsterCards
{
    /// <summary>
    /// Minimal example of how to create a Monster Card with an Essence.
    /// </summary>
    class BlueEyesWhiteDragon
    {
        public static readonly string ID = TestPlugin.CLANID + "_BlueEyesCard";
        public static readonly string CharID = TestPlugin.CLANID + "_BlueEyesCharacter";

        public static void BuildAndRegister()
        {
            new CardDataBuilder
            {
                CardID = ID,
                Name = "Blue-Eyes White Dragon",
                Cost = 3,
                CardType = CardType.Monster,
                Rarity = CollectableRarity.Rare,
                TargetsRoom = true,
                Targetless = false,
                AssetPath = "assets/blueeyes.png",
                ClanID = Clan.ID,
                CardPoolIDs = { VanillaCardPoolIDs.StygianBanner, VanillaCardPoolIDs.UnitsAllBanner },
                EffectBuilders =
                {
                    new CardEffectDataBuilder
                    {
                        EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
                        TargetMode = TargetMode.DropTargetCharacter,
                        ParamCharacterDataBuilder = new CharacterDataBuilder
                        {
                            CharacterID = CharID,
                            Name = "Blue-Eyes White Dragon",
                            Size = 5,
                            Health = 2500,
                            AttackDamage = 3000,
                            AssetPath = "assets/blueeyes_character.png",
                            SubtypeKeys = { Subtypes.Dragon },
                            UnitSynthesisBuilder = new CardUpgradeDataBuilder
                            {
                                UpgradeID = "BlueEyesWhiteDragonSynthesis",
                                UpgradeDescription = "+3000[attack] +2500[health]",
                                HideUpgradeIconOnCard = true,
                                UseUpgradeHighlightTextTags = true,
                                BonusDamage = 3000,
                                BonusHP = 2500,
                            }
                        }
                    }
                },
            }.BuildAndRegister();
        }
    }
}
