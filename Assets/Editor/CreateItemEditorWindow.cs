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

    private readonly string C_HELMET_DATA_PATH = "Assets/_Data/Item/Helmet/";
    private readonly string C_ARMOR_DATA_PATH = "Assets/_Data/Item/ARMOR/";
    
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
        CreateDataAsset();
    }

    private void CreateDataAsset()
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
                path = $"Assets/_Data/Item/Armor/{formatedName}_ArmorData.asset";
                break;
            case ItemCategory.Boots:
                asset = ScriptableObject.CreateInstance<BootsData>();
                path = $"Assets/_Data/Item/Boots/{formatedName}_BootsData.asset";
                break;
            case ItemCategory.Gloves:
                asset = ScriptableObject.CreateInstance<GlovesData>();
                path = $"Assets/_Data/Item/Gloves/{formatedName}_GlovesData.asset";
                break;
            case ItemCategory.Accessories:
                asset = ScriptableObject.CreateInstance<AccessoriesData>();
                path = $"Assets/_Data/Item/Accessories/{formatedName}_AccessoriesData.asset";
                break;
            case ItemCategory.Charm:
                asset = ScriptableObject.CreateInstance<CharmData>();
                path = $"Assets/_Data/Item/Charm/{formatedName}_CharmData.asset";
                break;
        }

        ((ItemData)asset).ItemID = m_itemID;
        ((ItemData)asset).TranslatedName = m_itemName;
        ((ItemData)asset).ItemCategory = m_itemCategory;
        ((ItemData)asset).Rarity = m_rarity;
        ((ItemData)asset).Icon = m_itemSprite;
        
        AssetDatabase.CreateAsset(asset,path);
        AssetDatabase.SaveAssets();
        
        GameObject templatePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Prefabs/Item/ItemPickableTemplate.prefab");
        var variant = PrefabUtility.InstantiatePrefab(templatePrefab) as GameObject;
        
        var savePath = "";
        var eventData = AssetDatabase.LoadAssetAtPath<ItemPickedEvent>("Assets/_Data/Events/ItemPickedEvent.asset");
        switch (m_itemCategory)
        {
            case ItemCategory.Weapon:
                var weaponPicker = variant.AddComponent<WeaponPicker>();
                weaponPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Weapon/{formatedName}_WeaponPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/WeaponHUDTemplate.prefab", variant,weaponPicker);
                break;
            case ItemCategory.Helmet:
                var helmetPicker = variant.AddComponent<HelmetPicker>();
                helmetPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Helmet/{formatedName}_HelmetPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/HelmetHUDTemplate.prefab", variant,helmetPicker);
                break;
            case ItemCategory.Armor:
                var armorPicker = variant.AddComponent<ArmorPicker>();
                armorPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Armor/{formatedName}_ArmorPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/ArmorHUDTemplate.prefab", variant,armorPicker);
                break;
            case ItemCategory.Boots:
                var bootsPicker = variant.AddComponent<BootsPicker>();
                bootsPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Boots/{formatedName}_BootsPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/BootsHUDTemplate.prefab", variant,bootsPicker);
                break;
            case ItemCategory.Gloves:
                var glovesPicker = variant.AddComponent<GlovesPicker>();
                glovesPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Gloves/{formatedName}_GlovesPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/GlovesHUDTemplate.prefab", variant,glovesPicker);
                break;
            case ItemCategory.Accessories:
                var accessoriesPicker = variant.AddComponent<AccessoriesPicker>();
                accessoriesPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Accessories/{formatedName}_AccessoriesPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/AccessoriesHUDTemplate.prefab", variant,accessoriesPicker);
                break;
            case ItemCategory.Charm:
                var charmPicker = variant.AddComponent<CharmPicker>();
                charmPicker.SetDataForEditor(m_itemCategory,(ItemData)asset,eventData);
                savePath = $"Assets/_Prefabs/Item/Charm/{formatedName}_CharmPicker.prefab";
                CreatePickerHUD("Assets/_Prefabs/UI/CharmHUDTemplate.prefab", variant,charmPicker);
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
