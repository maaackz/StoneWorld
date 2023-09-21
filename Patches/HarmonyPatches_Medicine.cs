using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;
using Verse.AI;

namespace StoneWorld
{
    partial class HarmonyPatches
    {

        [HarmonyPatch(typeof(HealthCardUtility), "DrawMedOperationsTab")]
        static class Patch_HealthCardUtility_DrawMedOperationsTab
        {
            static void Prefix()
            {
                //Log.Message("DrawMedOperationsTab prefix");
            }

            static void Postfix()
            {
                //Log.Message("DrawMedOperationsTab postfix");
            }
        }

        [HarmonyPatch(typeof(Pawn), "CurrentlyUsableForBills")]
        static class Patch_Pawn_CurrentlyUsableForBills
        {
            static void Prefix()
            {
                Log.Message("CurrentlyUsableForBills prefix");
            }

            static void Postfix(ref bool __result, Pawn __instance)
            {
                Log.Message("CurrentlyUsableForBills postfix");
                if (__instance.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification))
                {
                    __result = true;
                }
            }
        }   
    }
}