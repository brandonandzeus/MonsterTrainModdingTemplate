﻿using MonsterTrainModdingTemplate;
using MonsterTrainModdingTemplate.Clans;
using Trainworks.BuildersV2;
using Trainworks.Constants;
using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.Champions
{
    class ExiledChampion
    {
        public static readonly string ID = TestPlugin.CLANID + "_ExiledChampion";
        public static readonly string CharID = TestPlugin.CLANID + "_ExiledChampionCharacter";

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
                StarterCardData = CustomCardManager.GetCardDataByID(VanillaCardIDs.AlphaFiend),
                CardID = ID,
                Name = "Slimeboy",
                ClanID = Clan.ID,
                AssetPath = "assets/slimeboy.png"
            }.BuildAndRegister(1);
        }
    }
}
