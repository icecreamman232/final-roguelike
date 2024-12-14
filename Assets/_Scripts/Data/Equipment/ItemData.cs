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
        [SerializeField] private Rarity m_rarity;
        [SerializeField] private ItemCategory m_itemCategory;
        [SerializeField] private Sprite m_icon;
        
        public string ItemID => m_itemID;
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
    }
}

