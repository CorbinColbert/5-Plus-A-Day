using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    private string itemName;
    public int damageModifier;          //Adds to the damage of the unit's basic attacks
    public int abilityMeterModifier;    //Subtracts from the total meter so that the unit has less to charge up
    public int healthModifier;          //Adds to the unit's max health

    public int getDamageModifier() {
        return damageModifier;
    }

    public int getAbilityMeterModifier() {
        return abilityMeterModifier;
    }

    public int getHealthModifier() {
        return healthModifier;
    }

    public string getName() {
        return itemName;
    }
}
