using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.Misc
{
    class Subtypes
    {
        public static readonly string Dragon = TestPlugin.CLANID + "_DragonSubtype";

        public static void BuildAndRegister()
        {
            CustomCharacterManager.RegisterSubtype(Dragon, "Dragon");
        }
    }
}
