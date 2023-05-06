using HarmonyLib;
using MonsterTrainModdingTemplate.Relics;
using Trainworks.Managers;

namespace MonsterTrainModdingTemplate.HarmonyPatches
{
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.SetupRun))]
    class AddRelicAtStartOfRunPatch
    {
        static void Postfix(ref SaveManager __instance)
        {
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(Wimpcicle.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(EmberRefunder.ID));
            //__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(EmberExchanger.ID));
        }
    }
}
