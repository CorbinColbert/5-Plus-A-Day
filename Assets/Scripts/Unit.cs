using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]                    
    protected int healthPointsMax;      //Maxiumum HP
    protected int healthPoints;         //Current HP

    [SerializeField]                    
    protected int abilityMeterMax;      //Maximum charge required to perform the ability
    protected int abilityMeter;         //Current charge
    protected bool hasAbility;          //Some units suck, and wont even have abilities
    protected bool abilityReady;        //Indicates if the unit can perform the special attack on the next frame

    [SerializeField]                    
    protected AOE_TYPE abilityAOE;          //Enum AOE linked to a static class specifying the area of the attack
    protected SpecialAttack areaOfEffect;   //The special attack and data related to it

    [SerializeField]                   
    protected int attackFrequency;      //How often this unit will perform a basic attack (e.g a value of 30 means once per 30 frames)
    private int attackFrameCounter;     //A basic attack deals the basicAttackDamage to the target
    private bool attackReady;           //Indicates availability

    [SerializeField]
    protected int basicAttackDamage = 10;   //How much damage a unit will do per attack
    [SerializeField]
    protected int basicAttackRange = 1;     //How far a unit can attack, TO DISCUSS DIAGONAL ATTACKS

    public Board board;
    public Pair position;

    protected Unit target;          

    protected Item heldItem;        //If null, the unit is not holding an item
    protected int level = 0;        //How many battles the unit has survived / won  TO DISCUSS

    void Start() {
        this.healthPoints = this.healthPointsMax;   //Start the unit on full health!

        this.hasAbility = (abilityMeterMax != 0);   //If the max ability meter is 0, the unit doesnt have one
        this.abilityMeter = 0;

        if (this.hasAbility) {  //Link the AOE enum to an actual special attack object with information about the position of the speical attack
            switch (abilityAOE) {
                case AOE_TYPE.BASH:
                    areaOfEffect = AOE_Factory.getBash();
                    break;
                case AOE_TYPE.SLASH:
                    areaOfEffect = AOE_Factory.getSlash();
                    break;
                case AOE_TYPE.STAB:
                    areaOfEffect = AOE_Factory.getStab();
                    break;
                case AOE_TYPE.SUPER_WHIRL:
                    areaOfEffect = AOE_Factory.getSuperWhirl();
                    break;
                case AOE_TYPE.WHIRL:
                    areaOfEffect = AOE_Factory.getWhirl();
                    break;
            }
        }
 
        //The unit can attack straight away, once it gets close enough of course
        this.attackFrameCounter = attackFrequency;
    }

    //Called once per frame
    void Update() {
        if (this.isAlive()) {
            tryAttackTarget();
        } else {
            //die?
        }
    }

 
    //Method to set the target to attack next, if no target can be found, this unit should sleep or stand still for a set amount of time
    public void findTarget() {

    }

    protected void tryAttackTarget() {
        if (hasTarget())
        {   if (targetIsInRange())
            {   if (abilityReady)
                {
                    performAbility(); 
                }
                else if (attackReady)
                {   
                    performAttack();      
                }
                else
                {   
                    incrementBasicAttackCounter();              
                }
            }
            else
            {   
                walkTo(target.getBoardPosition());    
            }
        }
        else
        {   
            findTarget();         
        }
    }

    public Pair getBoardPosition() {
        return this.position;
    }

    public bool hasTarget() {
        return (target != null);
    }

    //TODO
    public bool targetIsInRange() {
        return false;
    }

    //May return a null
    public Item getItem() {
        return heldItem;
    }

    public bool hasItem() {
        return (heldItem != null);
    }

    //Method to be called from inside this script only
    protected void walkTo(Pair tileCoordinate) {
        //walk closer to the tile specified by coordinate
        //update current coordinate
    }

    private void incrementAbilityAttackCounter(int damageDone) {
        if (!this.hasAbility) {
            return;
        }

        //At the moment, half of damage dealt is gained as ability charge
        abilityMeter += (damageDone / 2);
        if (abilityMeter >= abilityMeterMax) {
            abilityMeter = 0;
            abilityReady = true;
        }
    }
    
    //Method to increment the basic attack frame counter
    private void incrementBasicAttackCounter() {
        //If the unit is able to attack but hasn't attacked yet, do not modifiy the counter
        if (attackReady) {
            return;
        }

        //Test if this is meant to be the attack frame
        if (attackFrameCounter == attackFrequency) {
            attackFrameCounter = 1;
            attackReady = true;
        } else {
            attackFrameCounter++;
        }
    }

    protected void performAbility() {
        //do the ability
        abilityReady = false;
    }

    protected void performAttack() {
        //Code for attacking another unit;
        //One frame later check if that unit was killed ?
        if (hasAbility) {
            incrementAbilityAttackCounter(basicAttackDamage); //NOTE: in future may pass in the actual damage dealt, rather than the unit's attack damage
        }
        attackReady = false;
    }

    public void recieveAttack(Unit otherUnit) {
        //Code for receiving an attack from another unit;
        //E.g. health -= otherUnit.attackDamage
        //Check for death
    }

    //Applies any benefits or drawbacks of the item to this unit
    private void equipItem(Item item) {
        
        //Health
        this.healthPoints += item.getHealthModifier();
        this.healthPointsMax += item.getHealthModifier();

        //Ability
        if (this.hasAbility) {
            this.abilityMeter -= item.getAbilityMeterModifier();
            this.abilityMeterMax -= item.getAbilityMeterModifier();
        }

        //Attack Damage
        this.basicAttackDamage += heldItem.getDamageModifier();

        //Attack Speed
        this.attackFrequency -= heldItem.getAttackFrequencyModifier();

        //Limit attack speed to 10 frames / attack (this is approx. 6 attacks/second at 60 frames per second)
        if (attackFrequency < 10) {
            attackFrequency = 10;
        }

    }

    //Removes any benefits or drawbacks of the item to this unit
    //Basically reverses changes made in equipItem()
    private void unequipItem(Item item) {
        if (item == null) {
            return;
        }

        //Health
        this.healthPoints -= item.getHealthModifier();
        this.healthPointsMax -= item.getHealthModifier();

        //Ability
        if (this.hasAbility) {
            this.abilityMeter += item.getAbilityMeterModifier();
            this.abilityMeterMax += item.getAbilityMeterModifier();
        }

        //Attack Damage
        this.basicAttackDamage -= heldItem.getDamageModifier();

        //Attack Speed
        this.attackFrequency += heldItem.getAttackFrequencyModifier();

    }

    //Method which should be called when placing items on units
    //Returns the old item the unit was using
    //This can return null if no item was attatched before
    public Item attatchItem(Item item) {
        unequipItem(heldItem);
        Item removedItem = heldItem;
        equipItem(item);

        //Finally, return the item we stored in step 2
        return removedItem;
    }

    public bool isAlive() {
        return (healthPoints > 0);
    }
}
