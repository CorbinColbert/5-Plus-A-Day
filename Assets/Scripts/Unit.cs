using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Board board;
    public Pair position;

    [SerializeField]                    
    protected int healthPointsMax;  
    protected int healthPoints; 

    [SerializeField]                    //Basic attacks will charge the ability
    protected int abilityMeterMax;      //Performing an ability should have priority over a basic attack
    protected int abilityMeter;         //When the meter is full the unit should 
    protected bool hasAbility;          //Some units suck, and wont even have abilities
    protected bool abilityReady;

    [SerializeField]                    //Enum AOE linked to a static class specifying the area of the attack
    protected AOE_TYPE abilityType;     //[0,0] is the tile this unit is standing on, [-1,0] is one left, [0, 1] is one up etc.
    protected AOE areaOfEffect;         

    [SerializeField]                    //How often this unit will perform a basic attack (e.g a value of 30 means once per 30 frames)
    protected int attackFrequency;      //A basic attack deals the basicAttackDamage to the target
    private int attackFrameCounter;     
    private bool attackReady;

    [SerializeField]
    protected int basicAttackDamage = 10;   
    [SerializeField]
    protected int basicAttackRange = 1; 

    protected Unit target;

    protected Item heldItem;        //If null, the unit is not holding an item
    protected int level = 0;        //How many battles the unit has survived

    // Start is called before the first frame update
    void Start()
    {
        //Start the unit on full health!
        this.healthPoints = this.healthPointsMax;
        
        //If the max ability meter is 0, the unit doesnt have one
        this.hasAbility = (abilityMeterMax != 0);
        this.abilityMeter = 0;

        //Link the AOE enum to an actual area of effect object with information about the position of the speical attack
        if (this.hasAbility) {
            switch (abilityType) {
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

    // Update is called once per frame
    void Update()
    {
        if (this.isAlive()) {
            tryAttackTarget();
        }
    }

 
    //Method to set the target to attack next, if no target can be found, this unit should sleep or stand still for a set amount of time
    public void findTarget() {

    }

    protected void tryAttackTarget() {
        if (hasTarget()) {
            if (targetIsInRange()) {
                if (abilityReady) {
                    performAbility();
                } else if (attackReady) {
                    performAttack();
                } else {
                    incrementBasicAttackCounter();
                }
            } else {
                walkTo(target.getBoardPosition());
            }
        }
    }

    public Pair getBoardPosition() {
        return this.position;
    }

    public bool hasTarget() {
        return (target != null);
    }

    public bool targetIsInRange() {

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
        //walk to the tile specified by coordinate
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
        //First, take the current item off
        unequipItem(heldItem);

        //Second, store it in removedItem
        Item removedItem = heldItem;

        //Third, put the new item on
        equipItem(item);

        //Finally, return the item we stored in step 2
        return removedItem;
    }

    public bool isAlive() {
        return (healthPoints > 0);
    }
}
