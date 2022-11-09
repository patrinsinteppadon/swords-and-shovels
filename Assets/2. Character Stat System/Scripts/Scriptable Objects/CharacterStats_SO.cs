using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    [System.Serializable]
    public class CharLevelUps
    {
        public int maxHealth;
        public int maxMana;
        public int maxWealth;
        public int baseDamage;
        public float baseResistance;
        public float maxEncumbrance;
    }

    #region Fields
    public bool setManually = false;
    public bool saveDataOnClose = false;

    public ItemPickUp weapon { get; private set; }
    public ItemPickUp headArmor { get; private set; }
    public ItemPickUp chestArmor { get; private set; }
    public ItemPickUp handArmor { get; private set; }
    public ItemPickUp legArmor { get; private set; }
    public ItemPickUp footArmor { get; private set; }
    public ItemPickUp misc1 { get; private set; }
    public ItemPickUp misc2 { get; private set; }

    [Space, Header("Character Stats")]
    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxMana = 0;
    public int currentMana = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0f;
    public float currentResistance = 0f;

    public float maxEncumbrance = 0f;
    public float currentEncumbrance = 0f;

    public int charExperience = 0;
    public int charLevel = 0;

    public CharLevelUps[] charLevelUps;
    #endregion

    #region Stat Increasers
    public bool AddHealth(int healthAmount)
    {
        if (currentHealth == maxHealth)
        {
            return false;
        }

        currentHealth = Mathf.Min(currentHealth + healthAmount, maxHealth);
        return true;
    }

    public bool AddMana(int manaAmount)
    {
        if (currentMana == maxMana)
        {
            return false;
        }

        currentMana = Mathf.Min(currentMana + manaAmount, maxMana);
        return true;
    }

    public void AddWealth(int wealthAmount)
    {
        currentWealth = Mathf.Min(currentWealth + wealthAmount, maxWealth);
    }

    public void EquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot)
    {
        weapon = weaponPickUp;
        currentDamage = baseDamage + weapon.itemDefinition.itemAmount;
    }

    public void EquipArmor(ItemPickUp armorPickup, CharacterInventory charInventory)
    {
        switch (armorPickup.itemDefinition.armorType)
        {
            case ItemArmorSubType.HEAD:
                headArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.CHEST:
                chestArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.HANDS:
                handArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.LEGS:
                legArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            case ItemArmorSubType.FEET:
                footArmor = armorPickup;
                currentResistance += armorPickup.itemDefinition.itemAmount;
                break;
            default:
                Debug.Log("Error: armorPickup has no assigned ItemArmorSubType");
                break;
        }
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            // kill
        }
    }

    public void TakeMana(int amount)
    {
        if (currentMana - amount >= 0)
        {
            currentMana -= amount;
        }
    }
    public bool TakeWealth(int amount)
    {
        if (currentWealth - amount >= 0)
        {
            currentWealth -= amount;
            return true;
        }

        return false;
    }

    public bool UnEquipWeapon(ItemPickUp weaponPickUp, CharacterInventory charInventory, GameObject weaponSlot)
    {
        bool previousWeaponIsSame = false;
        if (weapon != null)
        {
            if (weapon == weaponPickUp)
            {
                previousWeaponIsSame = true;
            }

            Object.Destroy(weaponSlot.transform.GetChild(0).gameObject);
            weapon = null;
            UpdateDamage();
        }
        return previousWeaponIsSame;
    }

    public bool UnEquipArmor(ItemPickUp armorPickup, CharacterInventory charInventory)
    {
        bool previousArmorSame = false;
        switch (armorPickup.itemDefinition.armorType)
        {
            case ItemArmorSubType.HEAD:
                if (headArmor != null)
                {
                    if (headArmor == armorPickup)
                    {
                        previousArmorSame = true;
                    }
                    headArmor = null;
                }
                break;
            case ItemArmorSubType.CHEST:
                if (chestArmor != null)
                {
                    if (chestArmor == armorPickup)
                    {
                        previousArmorSame = true;
                    }
                    chestArmor = null;
                }
                break;
            case ItemArmorSubType.HANDS:
                if (handArmor != null)
                {
                    if (handArmor == armorPickup)
                    {
                        previousArmorSame = true;
                    }
                    handArmor = null;
                }
                break;
            case ItemArmorSubType.LEGS:
                if (legArmor != null)
                {
                    if (legArmor == armorPickup)
                    {
                        previousArmorSame = true;
                    }
                    legArmor = null;
                }
                break;
            case ItemArmorSubType.FEET:
                if (footArmor != null)
                {
                    if (footArmor == armorPickup)
                    {
                        previousArmorSame = true;
                    }
                    footArmor = null;
                }
                break;
            default:
                Debug.Log("Error: armorPickup has no assigned ItemArmorSubType");
                break;
        }

        UpdateResistance();
        return previousArmorSame;
    }

    #endregion

    #region Character Level Up And Death
    private void Death()
    {
        Debug.Log("You're Dead");
    }

    private void LevelUp()
    {
        charLevel++;
        // show level up visualization
        var newStats = charLevelUps[charLevel - 1];
        maxHealth = newStats.maxHealth;
        maxMana = newStats.maxMana;
        maxWealth = newStats.maxWealth;
        baseDamage = newStats.baseDamage;
        baseResistance = newStats.baseResistance;
        maxEncumbrance = newStats.maxEncumbrance;
    }
    #endregion

    #region SaveCharacterData
    public void saveCharacterData()
    {
        //saveDataOnClose = true;
        //EditorUtility.SetDirty(this);
    }
    #endregion
    /**
     * updates currentDamage after re-equipping gear, as well as after applying buffs
     */
    private void UpdateDamage()
    {
        currentDamage = baseDamage + (weapon != null ? weapon.itemDefinition.itemAmount : 0);
    }

    private void UpdateResistance()
    {
        currentResistance = baseResistance
                              + (headArmor != null ? headArmor.itemDefinition.itemAmount : 0)
                              + (chestArmor != null ? chestArmor.itemDefinition.itemAmount : 0)
                              + (handArmor != null ? handArmor.itemDefinition.itemAmount : 0)
                              + (legArmor != null ? legArmor.itemDefinition.itemAmount : 0)
                              + (footArmor != null ? footArmor.itemDefinition.itemAmount : 0);
    }
}
