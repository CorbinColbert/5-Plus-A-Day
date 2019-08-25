using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    [SerializeField]
    private string itemName;
    [SerializeField]
    public int damageModifier;          //Adds to the damage of the unit's basic attacks
    [SerializeField]
    public int abilityMeterModifier;    //Subtracts from the total meter so that the unit has less to charge up
    [SerializeField]
    public int healthModifier;          //Adds to the unit's max health
    [SerializeField]
    public int attackFrequencyModifier; //Subracts from the number of frames between each basic attack

    public int getDamageModifier() {
        return damageModifier;
    }

    public int getAbilityMeterModifier() {
        return abilityMeterModifier;
    }

    public int getHealthModifier() {
        return healthModifier;
    }

    public int getAttackFrequencyModifier() {
        return attackFrequencyModifier;
    }

    public string getName() {
        return itemName;
    }
}

//May use this to produce items
public static class ItemFactory {

}
