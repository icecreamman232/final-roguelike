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
        
        public string ItemID => m_itemID;
        public string TranslatedName => m_translatedName;
        public Rarity Rarity => m_rarity;
        public ItemCategory ItemCategory => m_itemCategory;
        public Sprite Icon
        {
            get
            {
                return m_icon;
            }
            set
            {
                m_icon = value;
            }
        }
        
        public GameObject PickerPrefab => m_pickerPrefab;
    }
}

