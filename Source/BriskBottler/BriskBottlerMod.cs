using System;
using System.Collections.Generic;
using Database;
using Harmony;
using KSerialization;
using STRINGS;
using BUILDINGS = TUNING.BUILDINGS;

namespace BriskBottler
{
    public class BriskBottlerMod
	{
		[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class BriskBottlerBuildingsPatch
		{
  			private static void Prefix()
			{
                Strings.Add("STRINGS.BUILDINGS.PREFABS.BRISKBOTTLER.NAME", "Liquid Bottler");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.BRISKBOTTLER.DESC", "This Bottler station has access to: {Liquids");
                Strings.Add("STRINGS.BUILDINGS.PREFABS.BRISKBOTTLER.EFFECT", "Liquid Available: {Liquids}");

                List<string> oxygenBuildings = new List<string>((string[])BUILDINGS.PLANORDER[4].data) { BriskBottlerConfig.ID };
				BUILDINGS.PLANORDER[4].data = oxygenBuildings.ToArray();
                BUILDINGS.COMPONENT_DESCRIPTION_ORDER.Add(BriskBottlerConfig.ID);
			}

			private static void Postfix()
			{
                object obj = Activator.CreateInstance(typeof(BriskBottlerConfig));
				BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
			}
		}

		[HarmonyPatch(typeof(Db), "Initialize")]
        public class BriskBottlerDbPatch
		{
			private static void Prefix()
			{
                List<string> ls = new List<string>(Techs.TECH_GROUPING["ImprovedLiquidPiping"]) { BriskBottlerConfig.ID };
                Techs.TECH_GROUPING["ImprovedLiquidPiping"] = ls.ToArray();
			}
		}

	}
}