using HarmonyLib;
using System;

namespace MonsterTrainModdingTemplate.SamplePatches
{
    // Note the third parameter in this case is required because HasDiscoveredCard is overloaded.
    [HarmonyPatch(typeof(MetagameSaveData), nameof(MetagameSaveData.HasDiscoveredCard), new Type[] { typeof(CardData) })]
    class ShowAllLogbookCardsPatch
    {
        static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

    [HarmonyPatch(typeof(MetagameSaveData), nameof(MetagameSaveData.HasDiscoveredCard), new Type[] { typeof(string) })]
    class ShowAllLogbookCardsPatch2
    {
        static bool Prefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
