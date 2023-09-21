using System;
using System.Collections;
using HugsLib;
using HugsLib.Utils;
using RimWorld;
using Verse;
using UnityEngine;
using System.Collections.Generic;

namespace StoneWorld
{
	[DefOf]
	public static class SW_DefOf
	{
		public static HediffDef SW_Petrification;
	}

	public class StoneWorld : ModBase
	{
		public static StoneWorld Instance { get; private set; }

		public override string ModIdentifier
		{
			get { return "StoneWorld"; }
		}

		private ModLogger GetLogger
		{
			get { return base.Logger; }
		}
		internal new static ModLogger Logger
		{
			get { return Instance.GetLogger; }
		}

		private StoneWorld()
		{
			Instance = this;
		}

		List<Pawn> statues = new List<Pawn>(100);

        private static System.Random random;
		private static object syncObj = new object();
		private static void InitRandomNumber(int seed)
		{
			random = new System.Random(seed);
		}
		private static int GenerateRandomNumber(int max)
		{
			lock (syncObj)
			{
				if (random == null)
					random = new System.Random(); // Or exception...
				return random.Next(max);
			}
		}

		public override void MapGenerated(Map map)
		{

			Log.Message("[StoneWorld] Map has been generated.");

			// Generate pawns.
			for (int i = 0; i < 100; i++)
			{
				IntVec3 loc = new IntVec3(GenerateRandomNumber(map.Size.x), GenerateRandomNumber(map.Size.y), GenerateRandomNumber(map.Size.z));

				PawnKindDef kind = PawnKindDefOf.Colonist;
				PawnGenerationRequest request = new PawnGenerationRequest(
					kind,
					null,
					PawnGenerationContext.NonPlayer,
					-1,
					true,
					false,
					true,
					false,
					false,
					0,
					false,
					true,
					true,
					false,
					true,
					false,
					false,
					false,
					false,
					0,
					0,
					null,
					0,
					null,
					null,
					null,
					null,
					0,
					null,
					null,
					null,
					null,
					null,
					null,
					null,
					false,
					false,
					false,
					false,
					null,
					null,
					null,
					null,
					null,
					0,
					(DevelopmentalStage)8,
					null,
					null,
					null,
					false
				);
				Pawn pawn = PawnGenerator.GeneratePawn(request);

                int rAge = Rand.Range(1, 360000000);
                pawn.ageTracker.AgeBiologicalTicks = rAge;

				pawn.apparel.DestroyAll();
				pawn.equipment.DestroyAllEquipment();

				Hediff hediff = HediffMaker.MakeHediff(SW_DefOf.SW_Petrification, pawn);
				pawn.health.AddHediff(hediff);

				pawn.mindState.Active = false;
				pawn.health.forceDowned = true;

				Color color = new Color(0.2f, 0.2f, 0.2f, 1);
				pawn.story.skinColorOverride = color;
				pawn.story.HairColor = color;

				PawnRenderer pr = pawn.Drawer.renderer;
                float rDA = Rand.Range(0f, 180f);
				if ((double)Rand.Value < 0.5)
				{
                    rDA += 180f; 
				}

                pr.wiggler.SetToCustomRotation(rDA);
                pr.wiggler.ticksToIncapIcon = 99 ^ 99;

				Thing thing = GenSpawn.Spawn(pawn, loc, map);
				thing.Rotation = Rot4.South;

				Log.Message(pawn.NameShortColored);

				statues.Add(pawn);
			}

		}

		public override void MapLoaded(Map map)
		{
			Log.Message("[StoneWorld] Map has been loaded.");
		}

		public override void Update()
		{
			Color newColor = new Color(0, 1, 0, 1);
			
			foreach (Pawn statue in statues)
			{
				if (statue != null)
				{
					if (!statue.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification))
					{
						statue.story.skinColorOverride = null;
                        Color newHairColor = PawnHairColors.RandomHairColor(statue, statue.story.SkinColor, statue.ageTracker.AgeBiologicalYears);
                        statue.story.HairColor = newHairColor;
						statue.mindState.Active = true;
                        //statue.SetFaction(Faction.OfPlayer);
						statues.Remove(statue);

                        Log.Message("[StoneWorld] Depetrified " + statue.Name.ToStringShort + ".");
                        statue.Drawer.renderer.graphics.ResolveAllGraphics();
                    }
				}
			}

			List<Pawn> allPawns = Current.Game.CurrentMap.mapPawns.AllPawns;
			foreach (Pawn pawn in allPawns)
			{
				if (pawn != null)
                {
					if (!pawn.health.hediffSet.HasHediff(SW_DefOf.SW_Petrification))
					{
						pawn.story.skinColorOverride = null;
						//pawn.story.HairColor = newHairColor;
					}
				}
			}

		}
	}
}