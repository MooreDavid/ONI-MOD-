using Klei.AI;
using STRINGS;
using System.Collections.Generic;
using UnityEngine;
using TUNING;

public class BackPackConfig : IEquipmentConfig
{

    public const string ID = "BackPack";
    public const string Recipe_Desc = "Adds a clean 100kg to the dupe"; 
    public const int decorMod = 0;
    public const float CarryAmount = 1000f;

    public EquipmentDef CreateEquipmentDef()
    {
        Dictionary<string, float> InputElementMassMap = new Dictionary<string, float>();
        InputElementMassMap.Add("BasicFabric", 3);
        ClothingWearer.ClothingInfo clothingInfo = new ClothingWearer.ClothingInfo((string) ID, decorMod, 0f, 0f);
        List<AttributeModifier> AttributeModifiers = new List<AttributeModifier>()
        {
         new AttributeModifier(Db.Get().Attributes.CarryAmount.Id, (float) BackPackConfig.CarryAmount, (string) BackPackConfig.ID, true, false, true),
        };
        EquipmentDef equipmentDef1 = EquipmentTemplates.CreateEquipmentDef(
            Id: "BackPack",
            Slot: TUNING.EQUIPMENT.TOOLS.TOOLSLOT,
            OutputElement: SimHashes.Carbon,
            Mass: TUNING.EQUIPMENT.VESTS.FUNKY_VEST_MASS,
            Anim: "vacillator_charge_kanim",
            SnapOn: (string)null,
            BuildOverride: "vacillator_charge_kanim",
            BuildOverridePriority: 4,
            AttributeModifiers: AttributeModifiers,
            SnapOn1: (string)null,
            IsBody: true,
            CollisionShape: EntityTemplates.CollisionShape.RECTANGLE,
            width: 0.75f,
            height: 0.4f,
            additional_tags: (Tag[])null);

        equipmentDef1.OnEquipCallBack = (System.Action<Equippable>)(eq => CoolVestConfig.OnEquipVest(eq, clothingInfo));
        equipmentDef1.RecipeDescription = Recipe_Desc;

        return equipmentDef1;
    }

    public static void SetupVest(GameObject go)
    {
        go.GetComponent<KPrefabID>().AddTag(GameTags.Clothes);
        Equippable equippable = go.GetComponent<Equippable>();
        if ((Object)equippable == (Object)null)
            equippable = go.AddComponent<Equippable>();
        equippable.SetQuality(QualityLevel.Poor);
        go.GetComponent<KBatchedAnimController>().sceneLayer = Grid.SceneLayer.BuildingBack;
    }

    public void DoPostConfigure(GameObject go)
    {

        BackPackConfig.SetupVest(go);
    }
}