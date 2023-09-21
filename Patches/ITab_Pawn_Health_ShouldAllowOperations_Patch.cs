using System.Diagnostics;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace StoneWorld
{
    /*[HarmonyPatch(typeof(ITab_Pawn_Health), "ShouldAllowOperations")]
    public static class ITab_Pawn_Health_ShouldAllowOperations_Patch
    {
        public static void Postfix(ref bool __result, ITab_Pawn_Health __instance)
        {
            if (!__result && __instance.PawnForHealth.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification_Complete))
            {
                Debug.Log("Showed anyways");
                __result = true;
            }
        }
    }*/
}
