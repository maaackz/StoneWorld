
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using RimWorld;
using Verse;
using StoneWorld;
using System.Diagnostics.Eventing.Reader;

namespace StoneWorld
{
    [StaticConstructorOnStartup]
    partial class HarmonyPatches
    {
        private static readonly Type patchType = typeof(HarmonyPatches);
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("maaackz.stoneworld");

            harmony.Patch((MethodBase)AccessTools.Method(typeof(GenMapUI), "DrawPawnLabel",
                new Type[8]
                {
                    typeof (Pawn),
                    typeof (Vector2),
                    typeof (float),
                    typeof (float),
                    typeof (Dictionary<string, string>),
                    typeof (GameFont),
                    typeof (bool),
                    typeof (bool)
                }, (Type[])null), new HarmonyMethod(patchType, "GenMapUI_DrawPawnLabel_Prefix", (Type[])null), (HarmonyMethod)null, (HarmonyMethod)null, (HarmonyMethod)null);
            Log.Message("[StoneWorld] Harmony pawn labels patch succeeded.");

            harmony.Patch(AccessTools.Method(typeof(ITab_Pawn_Health), name: "ShouldAllowOperations"),
                postfix: new HarmonyMethod(patchType, nameof(ShouldAllowOperationsPostfix)));
            Log.Message("[StoneWorld] Harmony ShouldAllowOperations patch succeeded.");

            /*harmony.Patch(AccessTools.Method(typeof(HealthCardUtility), name: "DrawMedOperationsTab"),
                postfix: new HarmonyMethod(patchType, nameof(DrawMedOperationsTabPostfix)));
            Log.Message("[StoneWorld] Harmony DrawMedOperationsTab patch succeeded.");*/
        }

        public static bool GenMapUI_DrawPawnLabel_Prefix(Pawn pawn)
        {
            if (pawn.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*public static bool DrawMedOperationsTabPostfix(
            Rect leftRect,
            Pawn pawn,
            Thing thingForMedBills,
            float curY
        )
        {
            Log.Message("Postfix ran");
            return true;
        }*/

        public static void ShouldAllowOperationsPostfix(ref bool __result, ITab_Pawn_Health __instance)
        {
        
            __result = true;
            
        }

    }
}
