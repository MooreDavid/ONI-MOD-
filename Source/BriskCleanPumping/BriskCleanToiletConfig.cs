using System.Collections.Generic;
using TUNING;
using STRINGS;
using UnityEngine;

namespace BriskCleanPumping
{
    public class BriskCleanToiletConfig : IBuildingConfig
    {
        private const float WATER_USAGE = 5f;
        public const string ID = "CleanFlushToilet";

        public override BuildingDef CreateBuildingDef()
        {
            string id = "CleanFlushToilet";
            int width = 2;
            int height = 3;
            string anim = "toiletflush_kanim";
            int hitpoints = 30;
            float construction_time = 30f;
            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
            string[] rawMetals = MATERIALS.RAW_METALS;
            float melting_point = 800f;
            BuildLocationRule build_location_rule = BuildLocationRule.OnFloor;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR4, rawMetals, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
            buildingDef.Overheatable = false;
            buildingDef.ExhaustKilowattsWhenActive = 0.25f;
            buildingDef.SelfHeatKilowattsWhenActive = 0.0f;
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
            buildingDef.DiseaseCellVisName = "FoodPoisoning";
            buildingDef.AudioCategory = "Metal";
            buildingDef.UtilityInputOffset = new CellOffset(0, 0);
            buildingDef.UtilityOutputOffset = new CellOffset(1, 1);
            buildingDef.PermittedRotations = PermittedRotations.FlipH;
            SoundEventVolumeCache.instance.AddVolume("toiletflush_kanim", "Lavatory_flush", NOISE_POLLUTION.NOISY.TIER3);
            SoundEventVolumeCache.instance.AddVolume("toiletflush_kanim", "Lavatory_door_close", NOISE_POLLUTION.NOISY.TIER1);
            SoundEventVolumeCache.instance.AddVolume("toiletflush_kanim", "Lavatory_door_open", NOISE_POLLUTION.NOISY.TIER1);
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<LoopingSounds>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Toilet);
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.FlushToilet);
            FlushToilet flushToilet = go.AddOrGet<FlushToilet>();
            flushToilet.massConsumedPerUse = 5f;
            flushToilet.massEmittedPerUse = 11.7f;
            flushToilet.diseaseId = "FoodPoisoning";
            flushToilet.diseasePerFlush = 0;
            flushToilet.diseaseOnDupePerFlush = 0;
            KAnimFile[] kanimFileArray = new KAnimFile[1]
    {
      Assets.GetAnim((HashedString) "anim_interacts_toiletflush_kanim")
    };
            ToiletWorkableUse toiletWorkableUse = go.AddOrGet<ToiletWorkableUse>();
            toiletWorkableUse.overrideAnims = kanimFileArray;
            toiletWorkableUse.workLayer = Grid.SceneLayer.BuildingFront;
            toiletWorkableUse.resetProgressOnStop = true;
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
            conduitConsumer.capacityKG = 5f;
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Liquid;
            conduitDispenser.invertElementFilter = true;
            conduitDispenser.elementFilter = new SimHashes[1]
    {
      SimHashes.Water
    };
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 25f;
            storage.doDiseaseTransfer = false;
            storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
            Ownable ownable = go.AddOrGet<Ownable>();
            ownable.slotID = Db.Get().AssignableSlots.Toilet.Id;
            ownable.canBePublic = true;
            go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;

            ManualDeliveryKG manualDeliveryKg1 = go.AddOrGet<ManualDeliveryKG>();
            manualDeliveryKg1.SetStorage(storage);
            manualDeliveryKg1.requestedItemTag = SimHashes.BleachStone.CreateTag();
            manualDeliveryKg1.capacity = 2f;
            manualDeliveryKg1.refillMass = 1f;
            manualDeliveryKg1.minimumMass = 1f;
            manualDeliveryKg1.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;

            ElementConverter elementConverter = go.AddComponent<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[1]
			{
				new ElementConverter.ConsumedElement(SimHashes.BleachStone.CreateTag(), 0.0005f),
			};
            elementConverter.outputElements = new ElementConverter.OutputElement[0]
			{
			};
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}

