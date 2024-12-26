using UnityEngine;

namespace SGGames.Scripts.Common
{
    public enum GameEventType
    {
        ENTER_THE_ROOM,
        ROOM_CLEARED,
        PAUSED,
        UNPAUSED,
    }

    public enum PlayerEventType
    {
        TAKE_DAMAGE,
        HEALING,
        DODGE,
        
        USE_WEAPON,
        EQUIP_WEAPON,
        
        USE_DEFENSE_ABILITY,
    }
    
    public enum InteractType
    {
        None,
        Chest,
        Weapon,
        Helmet,
        Armor,
        Boots,
        Gloves,
        Accessories,
        Charm,
    }
    
    public enum ChestType
    {
        Common,
        Golden,
        Legendary,
    }

    public enum WeaponCategory
    {
        Melee,
        Ranged,
    }

    public enum AttributeType
    {
        Strength,
        Agility,
        Intelligence,
    }

    public enum UpgradeAttributeRate
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
    }
}
