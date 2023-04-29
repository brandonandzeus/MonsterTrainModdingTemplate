using Trainworks.BuildersV2;
using UnityEngine;

namespace MonsterTrainModdingTemplate.Clans
{
    class Clan
    {
        public static readonly string ID = TestPlugin.CLANID;

        public static void BuildAndRegister()
        {
            new ClassDataBuilder
            {
                ClassID = ID,
                Name = "Test Clan",
                Description = "Test Clan Description",
                SubclassDescription = "Test Clan Sub Description",
                CardStyle = ClassCardStyle.Stygian,
                IconAssetPaths =
                {
                    "assets/testclan-large.png",
                    "assets/testclan-large.png",
                    "assets/testclan-large.png",
                    "assets/testclan-silhouette.png"
                },
                DraftIconPath = "assets/TestClan_CardBack.png",
                UiColor = new Color(0.43f, 0.15f, 0.81f, 1f),
                UiColorDark = new Color(0.12f, 0.42f, 0.39f, 1f),
            }.BuildAndRegister();
        }
    }
}
