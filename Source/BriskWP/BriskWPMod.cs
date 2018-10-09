using System;
using System.Collections.Generic;
using Database;
using Harmony;
using KSerialization;
using STRINGS;
using BUILDINGS = TUNING.BUILDINGS;

namespace BriskWP
{
    public class BriskWPMod
	{
		[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class BriskLRBuildingsPatch
		{
			private static void Prefix()
			{
                Strings.Add("STRINGS.BUILDINGS.PREFABS.LIQUIDPERMEABLEMRMBRANE.NAME", "WaterFlow Tile");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.LIQUIDPERMEABLEMRMBRANE.DESC", "Building with Waterflow permeable tiles promotes better liquid circulation within a colony");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.LIQUIDPERMEABLEMRMBRANE.EFFECT", "Used as floor and wall tile to build rooms.\n\nBlocks " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " flow without obstructing " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".");

                List<string> oxygenBuildings = new List<string>((string[])BUILDINGS.PLANORDER[0].data) { LIQUIDPERMEABLEMRMBRANEConfig.ID };
				BUILDINGS.PLANORDER[0].data = oxygenBuildings.ToArray();
                BUILDINGS.COMPONENT_DESCRIPTION_ORDER.Add(LIQUIDPERMEABLEMRMBRANEConfig.ID);
			}

			private static void Postfix()
			{
                object obj = Activator.CreateInstance(typeof(LIQUIDPERMEABLEMRMBRANEConfig));
				BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
			}
		}

		[HarmonyPatch(typeof(Db), "Initialize")]
        public class BriskLRDbPatch
		{
			private static void Prefix()
			{
                List<string> ls = new List<string>(Techs.TECH_GROUPING["PressureManagement"]) { LIQUIDPERMEABLEMRMBRANEConfig.ID };
                Techs.TECH_GROUPING["PressureManagement"] = ls.ToArray();
			}
		}

	}
}