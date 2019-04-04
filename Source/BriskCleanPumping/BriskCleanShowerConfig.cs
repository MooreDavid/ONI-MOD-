using System.Collections.Generic;
using TUNING;
using STRINGS;
using UnityEngine;

namespace BriskCleanPumping
{
    public class BriskCleanShowerConfig : IBuildingConfig
    {
        public static string ID = "CleanShower";

        public override BuildingDef CreateBuildingDef()
        {
            string id = "CleanShower";
            int width = 2;
            int height = 4;
            string anim = "shower_kanim";
            int hitpoints = 30;
            float construction_time = 30f;
            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
            string[] rawMetals = MATERIALS.RAW_METALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
            EffectorValues tieR3 = NOISE_POLLUTION.NOISY.TIER3;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR4, rawMetals, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.BONUS.TIER1, tieR3, 0.2f);
            buildingDef.Overheatable = false;
            buildingDef.ExhaustKilowattsWhenActive = 0.25f;
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
            buildingDef.AudioCategory = "Metal";
            buildingDef.UtilityInputOffset = new CellOffset(0, 0);
            buildingDef.UtilityOutputOffset = new CellOffset(1, 1);
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<LoopingSounds>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation);
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.AdvancedWashStation);
            Shower shower = go.AddOrGet<Shower>();
            shower.overrideAnims = new KAnimFile[1]
    {
      Assets.GetAnim((HashedString) "anim_interacts_shower_kanim")
    };
            shower.workTime = 15f;
            shower.outputTargetElement = SimHashes.DirtyWater;
            shower.fractionalDiseaseRemoval = 0.95f;
            shower.absoluteDiseaseRemoval = -2000;
            shower.workLayer = Grid.SceneLayer.BuildingFront;
            shower.trackUses = true;
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
            conduitConsumer.capacityKG = 5f;
            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Liquid;
            conduitDispenser.invertElementFilter = true;
            conduitDispenser.elementFilter = new SimHashes[1]
    {
      SimHashes.Water
    };
            ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[2]
    {
      new ElementConverter.ConsumedElement(new Tag("Water"), 1f),
      new ElementConverter.ConsumedElement(SimHashes.BleachStone.CreateTag(), 0.0005f),
    };
            elementConverter.outputElements = new ElementConverter.OutputElement[1]
    {
      new ElementConverter.OutputElement(1f, SimHashes.DirtyWater, 0.0f, true, 0.0f, 0.5f, true, 0f, 0, 0)
    };
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 12f;
            storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
            go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;

            ManualDeliveryKG manualDeliveryKg1 = go.AddOrGet<ManualDeliveryKG>();
            manualDeliveryKg1.SetStorage(storage);
            manualDeliveryKg1.requestedItemTag = SimHashes.BleachStone.CreateTag();
            manualDeliveryKg1.capacity = 2f;
            manualDeliveryKg1.refillMass = 1f;
            manualDeliveryKg1.minimumMass = 1f;
            manualDeliveryKg1.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}

