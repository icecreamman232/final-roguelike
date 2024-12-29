using SGGames.Scripts.Modifiers;
using UnityEngine;


namespace SGGames.Scripts.Data
{
    public enum ItemCategory
    {
        Weapon,
        Helmet,
        Armor,
        Boots,
        Gloves,
        Accessories,
        Charm,
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }
    
    public class ItemData : ScriptableObject
    {
        [Header("Base")]
        //This is for later translation and use internal
        [SerializeField] private string m_itemID;
        [SerializeField] private string m_translatedName; //TODO:Use translation from data instead of putting here in SO
        [SerializeField] private Rarity m_rarity;
        [SerializeField] private ItemCategory m_itemCategory;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private GameObject m_pickerPrefab;
        [Header("Modifier")] 
        [SerializeField] private Modifier[] m_modifierList;
        
        public Modifier[] ModifierList
        {
            get => m_modifierList;
            set => m_modifierList = value;
        }

        public string ItemID
        {
            get => m_itemID;
            set => m_itemID = value;
        }

        public string TranslatedName
        {
            get => m_translatedName;
            set => m_translatedName = value;
        }

        public Rarity Rarity
        {
            get => m_rarity;
            set => m_rarity = value;
        }
        public ItemCategory ItemCategory
        {
            get => m_itemCategory;
            set => m_itemCategory = value;
        }

        public Sprite Icon
        {
            get => m_icon;
            set => m_icon = value;
        }
        
        public GameObject PickerPrefab
        {
            get => m_pickerPrefab;
            set => m_pickerPrefab = value;
        }
    }
}

