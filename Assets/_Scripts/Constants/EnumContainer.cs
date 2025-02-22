
namespace SGGames.Scripts.Common
{
    public enum GameEventType
    {
        ENTER_THE_ROOM,
        ROOM_CLEARED,
        PAUSED,
        UNPAUSED,
        PAUSED_WITH_DELAY,
    }

    public enum PlayerEventType
    {
        ON_ATTACK,
        ON_BEING_HIT,
        ON_DODGE_ATTACK,
        ON_ENTER_ROOM,
        ON_KILL_ENEMY,
        ON_KILL_BOSS,
        ON_CRITICAL_HIT,
        ON_USING_POTION,
        ON_USING_SKILL,
        ON_DASHING,
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

    public enum AttributeTier
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
    }

    public enum HealthPotionType
    {
        SMALL,
        MEDIUM,
        GRANDE,
    }
    
    public enum ManaPotionType
    {
        SMALL,
        MEDIUM,
        GRANDE,
    }

    public enum SelectableAbility
    {
        Money_Talk,
        Blood_Rage,
        Burning_Circle,
        Frozen_Field,
        Poison_Cloud,
        
        PLACE_HOLDER_ABILITY_1 = 999,
        PLACE_HOLDER_ABILITY_2,
        PLACE_HOLDER_ABILITY_3,
        PLACE_HOLDER_ABILITY_4,
        PLACE_HOLDER_ABILITY_5,
        PLACE_HOLDER_ABILITY_6,
    }
}
