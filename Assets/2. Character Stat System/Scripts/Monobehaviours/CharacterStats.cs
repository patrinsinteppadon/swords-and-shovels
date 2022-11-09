using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinition;
    public CharacterInventory charInventory;
    public GameObject weaponSlot;

    #region Constructors
    public CharacterStats()
    {
        charInventory = CharacterInventory.instance;
    }
    #endregion

    void Start()
    {
        if (!characterDefinition.setManually)
        {
            characterDefinition.maxHealth = 100;
            characterDefinition.currentHealth = 100;

            characterDefinition.maxMana = 10;
            characterDefinition.currentMana = 10;

            characterDefinition.maxWealth = 9999;
            characterDefinition.currentWealth = 50;

            characterDefinition.baseResistance = 0;
            characterDefinition.currentResistance = 0;

            characterDefinition.maxEncumbrance = 0f;
            characterDefinition.currentEncumbrance = 0f;

            characterDefinition.maxEncumbrance = 0f;
            characterDefinition.currentEncumbrance = 0f;

            characterDefinition.baseDamage = 0;
            characterDefinition.currentDamage = 0;

            characterDefinition.charExperience = 0;
            characterDefinition.charLevel = 1;
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(2)) // right mouse button
        //{
        //    characterDefinition.saveCharacterData();
        //}
    }

    #region Stat Increasers
    public void AddHealth(int healthAmount)
    {
        characterDefinition.AddHealth(healthAmount);
    }
    public void AddMana(int manaAmount)
    {
        characterDefinition.AddMana(manaAmount);
    }
    public void AddWealth(int wealthAmount)
    {
        characterDefinition.AddWealth(wealthAmount);
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int healthAmount)
    {
        characterDefinition.TakeDamage(healthAmount);
    }
    public void TakeMana(int manaAmount)
    {
        characterDefinition.TakeMana(manaAmount);
    }
    public bool TakeWealth(int wealthAmount)
    {
        return characterDefinition.TakeWealth(wealthAmount);
    }
    #endregion

    #region Weapon And Armor Change
    public void ChangeWeapon(ItemPickUp weaponPickUp)
    {
        if (!characterDefinition.UnEquipWeapon(weaponPickUp, charInventory, weaponSlot))
        {
            characterDefinition.EquipWeapon(weaponPickUp, charInventory, weaponSlot);
        }
    }

    public void ChangeArmor(ItemPickUp armorPickUp)
    {
        if (!characterDefinition.UnEquipArmor(armorPickUp, charInventory))
        {
            characterDefinition.EquipArmor(armorPickUp, charInventory);
        }
    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.currentHealth;
    }

    public ItemPickUp GetCurrentWeapon()
    {
        return characterDefinition.weapon;
    }
    #endregion
}
