using System;
using System.Collections;
using HugsLib;
using HugsLib.Utils;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using Verse.AI;

namespace StoneWorld
{
    public class Recipe_CurePetrification : Recipe_Surgery
    {

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (!pawn.RaceProps.IsFlesh)
            {
                return;
            }
            //pawn.health.forceIncap = true;

            if (pawn.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification))
            {
                pawn.mindState.JoinColonyBecauseRescuedBy(billDoer);
                Log.Message("[StoneWorld] Petrification cured.");
                pawn.health.RemoveAllHediffs();
            }
            
            //pawn.health.forceIncap = false;
        }
        /*
        public override void ConsumeIngredient(Thing ingredient, RecipeDef recipe, Map map) {
        }
*/
        public override bool IsViolationOnPawn(Pawn pawn, BodyPartRecord part, Faction billDoerFaction)
        {
            /*if (pawn.Faction == billDoerFaction)
            {
                return false;
            }*/
            return false;
        }

    }
}