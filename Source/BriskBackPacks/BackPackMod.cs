﻿using System;
using System.Collections.Generic;
using Database;
using Harmony;
using KSerialization;
using STRINGS;
using BUILDINGS = TUNING.BUILDINGS;

namespace BriskBackPack
{
    public class BackPackMod
    {
        [HarmonyPatch(typeof(GeneratedEquipment), "LoadGeneratedEquipment")]
        public class GeneratedEquipmentConfigManagerPatch
        {

            private static void Prefix()
            {
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.BACKPACK.NAME", "BackPack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.BACKPACK.GENERICNAME", "BackPack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.BACKPACK.EFFECT", "Adds a clean 100kg to the dupe");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.BACKPACK.DESC", "Adds a clean 100kg to the dupe");

                Strings.Add("STRINGS.EQUIPMENT.PREFABS.LIGHTPACK.NAME", "LightPack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.LIGHTPACK.GENERICNAME", "LightPack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.LIGHTPACK.EFFECT", "Produces Light for a cost");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.LIGHTPACK.DESC", "Produces Light for a cost");

                Strings.Add("STRINGS.EQUIPMENT.PREFABS.STRESSPACK.NAME", "Morale Improvement Pack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.STRESSPACK.GENERICNAME", "Morale Improvement Pack");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.STRESSPACK.EFFECT", "Gives a high level of Morale");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.STRESSPACK.DESC", "Gives a high level of Morale");

                Strings.Add("STRINGS.EQUIPMENT.PREFABS.SLICKSTERSHOES.NAME", "Slickster Shoes");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.SLICKSTERSHOES.GENERICNAME", "SlicksterShoes");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.SLICKSTERSHOES.EFFECT", "Faster running speed");
                Strings.Add("STRINGS.EQUIPMENT.PREFABS.SLICKSTERSHOES.DESC", "Faster running speed");
            }

            private static void Postfix()
            {
                object obj = Activator.CreateInstance(typeof(BackPackConfig));
                EquipmentConfigManager.Instance.RegisterEquipment(obj as IEquipmentConfig);

                object obj1 = Activator.CreateInstance(typeof(LightPackConfig));
                EquipmentConfigManager.Instance.RegisterEquipment(obj1 as IEquipmentConfig);

                object obj2 = Activator.CreateInstance(typeof(StressPackConfig));
                EquipmentConfigManager.Instance.RegisterEquipment(obj2 as IEquipmentConfig);

                object obj3 = Activator.CreateInstance(typeof(SlicksterShoesConfig));
                EquipmentConfigManager.Instance.RegisterEquipment(obj3 as IEquipmentConfig);
            }
        }

    }
}
