#if UNITY_EDITOR
using System;
using SGGames.Scripts.Data;
using SGGames.Scripts.Events;
using SGGames.Scripts.Pickables;
using SGGames.Scripts.UI;
using UnityEditor;
using UnityEngine;

public class CreateItemEditorWindow : EditorWindow
{
    private GUIStyle m_windowStyleCache;
    private string m_itemID;
    private string m_itemName;
    private ItemCategory m_itemCategory;
    private Rarity m_rarity;
    private Sprite m_itemSprite;

    #region Paths
    private readonly string C_WEAPON_DATA_PATH = "Assets/_Data/Item/Weapon/";
    private readonly string C_HELMET_DATA_PATH = "Assets/_Data/Item/Helmet/";
    private readonly string C_ARMOR_DATA_PATH = "Assets/_Data/Item/Armor/";
    private readonly string C_GLOVES_DATA_PATH = "Assets/_Data/Item/Gloves/";
    private readonly string C_ACCESSORIES_DATA_PATH = "Assets/_Data/Item/Accessories/";
    private readonly string C_CHARM_DATA_PATH = "Assets/_Data/Item/Charm/";
    private readonly string C_BOOTS_DATA_PATH = "Assets/_Data/Item/Boots/";
    
    private readonly string C_PICKER_PREFAB_PATH = "Assets/_Prefabs/Item/ItemPickableTemplate.prefab";
    private readonly string C_ITEM_PICKED_EVENT_ASSET_PATH = "Assets/_Data/Events/ItemPickedEvent.asset";

    private readonly string C_WEAPON_PREFAB_PATH = "Assets/_Prefabs/Item/Weapon/";
    private readonly string C_HELMET_PREFAB_PATH = "Assets/_Prefabs/Item/Helmet/";
    private readonly string C_ARMOR_PREFAB_PATH = "Assets/_Prefabs/Item/Armor/";
    private readonly string C_GLOVES_PREFAB_PATH = "Assets/_Prefabs/Item/Gloves/";
    private readonly string C_ACCESSORIES_PREFAB_PATH = "Assets/_Prefabs/Item/Accessories/";
    private readonly string C_BOOTS_PREFAB_PATH = "Assets/_Prefabs/Item/Boots/";
    private readonly string C_CHARM_PREFAB_PATH = "Assets/_Prefabs/Item/Charm/";

    private readonly string C_WEAPON_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/WeaponHUDTemplate.prefab";
    private readonly string C_HELMET_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/HelmetHUDTemplate.prefab";
    private readonly string C_ARMOR_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/ArmorHUDTemplate.prefab";
    private readonly string C_GLOVES_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/GlovesHUDTemplate.prefab";
    private readonly string C_ACCESSORIES_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/AccessoriesHUDTemplate.prefab";
    private readonly string C_BOOTS_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/BootsHUDTemplate.prefab";
    private readonly string C_CHARM_PICKER_HUB_PREFAB_PATH = "Assets/_Prefabs/UI/CharmHUDTemplate.prefab";
    #endregion
    
    [MenuItem("SGGames/Create Item Tool")]
    private static void OpenWindow()
    {
        var window = EditorWindow.GetWindow<CreateItemEditorWindow>("Create Item Tool");
        window.ShowPopup();
    }

    private void OnGUI()
    {
        m_windowStyleCache = EditorStyles.label;
        m_windowStyleCache.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.LabelField("CREATE ITEM TOOL V1.0");
        m_windowStyleCache.alignment = TextAnchor.MiddleLeft;
        m_itemID = EditorGUILayout.TextField("Item ID",m_itemID);
        m_itemName = EditorGUILayout.TextField("Item Name",m_itemName);
        m_itemCategory = (ItemCategory)EditorGUILayout.EnumPopup("Item Category",m_itemCategory);
        m_rarity = (Rarity)EditorGUILayout.EnumPopup("Item Rarity",m_rarity);
        m_itemSprite = EditorGUILayout.ObjectField("Item Icon",m_itemSprite, typeof(Sprite), false) as Sprite;
        if (GUILayout.Button("Create item"))
        {
            CreateItem();
        }
    }

    private void CreateItem()
    {
        UnityEngine.Object asset = null;
        var path = "";
        var formatedName = string.Join("", m_itemName.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        switch (m_itemCategory)
        {
            case ItemCategory.Weapon:
                
                break;
            case ItemCategory.Helmet:
                asset = ScriptableObject.CreateInstance<HelmetData>();
                path = $"{C_HELMET_DATA_PATH}{formatedName}_HelmetData.asset";
                break;
            case ItemCategory.Armor:
                asset = ScriptableObject.CreateInstance<ArmorData>();
                path = $"{C_ARMOR_DATA_PATH}{formatedName}_ArmorData.asset";
                break;
            case ItemCategory.Boots:
                asset = ScriptableObject.CreateInstance<BootsData>();
                path = $"{C_BOOTS_DATA_PATH}{formatedName}_BootsData.asset";
                break;
            case ItemCategory.Gloves:
                asset = ScriptableObject.CreateInstance<GlovesData>();
                path = $"{C_GLOVES_DATA_PATH}{formatedName}_GlovesData.asset";
                break;
            case ItemCategory.Accessories:
                asset = ScriptableObject.CreateInstance<AccessoriesData>();
                path = $"{C_ACCESSORIES_DATA_PATH}{formatedName}_AccessoriesData.asset";
                break;
            case ItemCategory.Charm:
                asset = ScriptableObject.CreateInstance<CharmData>();
                path = $"{C_CHARM_DATA_PATH}{formatedName}_CharmData.asset";
                break;
        }

        ((ItemData)asset).ItemID = m_itemID;
        ((ItemData)asset).TranslatedName = m_itemName;
        ((ItemData)asset).ItemCategory = m_itemCategory;
        ((ItemData)asset).Rarity = m_rarity;
        ((ItemData)asset).Icon = m_itemSprite;
        
        AssetDatabase.CreateAsset(asset,path);
        AssetDatabase.SaveAssets();
        
        GameObject templatePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(C_PICKER_PREFAB_PATH);
        var variant = PrefabUtility.InstantiatePrefab(templatePrefab) as GameObject;
        
        var savePath = "";
        var eventData = AssetDatabase.LoadAssetAtPath<ItemPickedEvent>(C_ITEM_PICKED_EVENT_ASSET_PATH);
        switch (m_itemCategory)
        {
            case ItemCategory.Weapon:
                var weaponPicker = variant.AddComponent<WeaponPicker>();
                weaponPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_WEAPON_PREFAB_PATH}{formatedName}_WeaponPicker.prefab";
                CreatePickerHUD(C_WEAPON_PICKER_HUB_PREFAB_PATH, variant,weaponPicker);
                break;
            case ItemCategory.Helmet:
                var helmetPicker = variant.AddComponent<HelmetPicker>();
                helmetPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_HELMET_PREFAB_PATH}{formatedName}_HelmetPicker.prefab";
                CreatePickerHUD(C_HELMET_PICKER_HUB_PREFAB_PATH, variant,helmetPicker);
                break;
            case ItemCategory.Armor:
                var armorPicker = variant.AddComponent<ArmorPicker>();
                armorPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_ARMOR_PREFAB_PATH}{formatedName}_ArmorPicker.prefab";
                CreatePickerHUD(C_ARMOR_PICKER_HUB_PREFAB_PATH, variant,armorPicker);
                break;
            case ItemCategory.Boots:
                var bootsPicker = variant.AddComponent<BootsPicker>();
                bootsPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_BOOTS_PREFAB_PATH}{formatedName}_BootsPicker.prefab";
                CreatePickerHUD(C_BOOTS_PICKER_HUB_PREFAB_PATH, variant,bootsPicker);
                break;
            case ItemCategory.Gloves:
                var glovesPicker = variant.AddComponent<GlovesPicker>();
                glovesPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_GLOVES_PREFAB_PATH}{formatedName}_GlovesPicker.prefab";
                CreatePickerHUD(C_GLOVES_PICKER_HUB_PREFAB_PATH, variant,glovesPicker);
                break;
            case ItemCategory.Accessories:
                var accessoriesPicker = variant.AddComponent<AccessoriesPicker>();
                accessoriesPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_ACCESSORIES_PREFAB_PATH}{formatedName}_AccessoriesPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/AccessoriesHUDTemplate.prefab", variant,accessoriesPicker);
                break;
            case ItemCategory.Charm:
                var charmPicker = variant.AddComponent<CharmPicker>();
                charmPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"{C_CHARM_PREFAB_PATH}{formatedName}_CharmPicker.prefab";
                CreatePickerHUD(C_CHARM_PICKER_HUB_PREFAB_PATH, variant,charmPicker);
                break;
        }

        //Set picker icon
        var modelSprite = variant.transform.GetChild(0).GetComponent<SpriteRenderer>();
        modelSprite.sprite = m_itemSprite;
        
        var savedPrefab = PrefabUtility.SaveAsPrefabAsset(variant,savePath);

        ((ItemData)asset).PickerPrefab = savedPrefab;
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        DestroyImmediate(variant);
        
        //Focus to data asset in project window
        PostCreating(asset);
    }
    
    private void PostCreating(UnityEngine.Object data)
    {
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = data;
    }
    
    #region Create picker HUD 
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, WeaponPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<WeaponPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }

    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, HelmetPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<HelmetPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, ArmorPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<ArmorPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, BootsPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<BootsPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, GlovesPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<GlovesPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, AccessoriesPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<AccessoriesPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    
    private void CreatePickerHUD(string hudTemplatePath, GameObject parentGO, CharmPicker picker)
    {
        GameObject helmetPickerHUD = AssetDatabase.LoadAssetAtPath<GameObject>(hudTemplatePath);
        var variantPickerHUD = PrefabUtility.InstantiatePrefab(helmetPickerHUD,parentGO.transform) as GameObject;
        var pickerHUD = variantPickerHUD.GetComponent<CharmPickerHUD>();
        picker.PickerHUD = pickerHUD;
    }
    
    #endregion  
}
#endif
