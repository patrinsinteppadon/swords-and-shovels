using UnityEngine;

public enum ItemTypeDefinitions
{
    HEALTH, WEALTH, MANA, WEAPON, ARMOR, BUFF, EMPTY,
}
public enum ItemArmorSubType
{
    NONE, WEAPON, HEAD, CHEST, HANDS, LEGS, FEET
}
[CreateAssetMenu(fileName = "NewItem", menuName = "Spawnable Item/New Pick-up", order = 1)]
public class ItemPickUp_SO : ScriptableObject
{
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public ItemArmorSubType armorType = ItemArmorSubType.NONE;
    public int itemAmount = 0;

}
