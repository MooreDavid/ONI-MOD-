using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Harmony;
using KSerialization;
using STRINGS;
using BUILDINGS = TUNING.BUILDINGS;

namespace BriskCleanPumping
{
    public class BriskCleanPumpingMod
	{
		[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class BriskArcticBuildingsPatch
		{
			private static void Prefix()
			{
                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANFLUSHTOILET.NAME", "Clean Toilet");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANFLUSHTOILET.DESC", "Lavatories transmit fewer germs to Duplicants' skin and require no manual emptying.");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANFLUSHTOILET.EFFECT", ("Gives Duplicants a place to " + UI.FormatAsLink("Hygienically", "HYGIENE") + " relieve themselves."));

                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANSHOWER.NAME", "Clean Shower");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANSHOWER.DESC", "Showers remove the \"Grimy\" effect and kill all germs present on Duplicant's skin.");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.CLEANSHOWER.EFFECT", ("Improves Duplicant " + UI.FormatAsLink("Hygiene", "HYGIENE") + " and removes " + UI.FormatAsLink("Germs", "DISEASE") + "."));


                ModUtil.AddBuildingToPlanScreen("Plumbing", BriskCleanToiletConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Plumbing", BriskCleanShowerConfig.ID);
                                
			}
		}

		[HarmonyPatch(typeof(Db), "Initialize")]
        public class BriskArcticDbPatch
		{
			private static void Prefix()
			{


                List<string> ls = new List<string>(Database.Techs.TECH_GROUPING["PRECISIONPLUMBING"]) { BriskCleanToiletConfig.ID };
                Database.Techs.TECH_GROUPING["PRECISIONPLUMBING"] = ls.ToArray();

                List<string> Cs = new List<string>(Database.Techs.TECH_GROUPING["PRECISIONPLUMBING"]) { BriskCleanShowerConfig.ID };
                Database.Techs.TECH_GROUPING["PRECISIONPLUMBING"] = Cs.ToArray();
                

			}
		}

	}
}