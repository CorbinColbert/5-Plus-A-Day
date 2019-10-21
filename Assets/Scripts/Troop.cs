using System;
using UnityEngine;

/*
 * Troop Class - Used in scene GameScene
 *********************************************************
 * Class which manages all functionality of a troop
 * while it is fighting or dying
 */
public class Troop : MonoBehaviour
{
    /* Troop Statistics, to be modified in editor
     ********************************************
     * Alive (default true)
     * Current Health Points
     * Max Health Points
     * Min Attack Damage
     * Attack Speed (attacks per second)
     * Regeneration
     * Regeneration Speed (regens per second)
     * Regen Enabled (if true, regeneration occurs)
     * Troop Type (enum of what fruit/vege)
     */
    public bool alive = true;
    public float healthCurrent;
    public float healthMax;
    public float damageMin;
    public float damageMax;
    public float attackSpeed;
    public float regenAmount;
    public float regenSpeed;
    public bool regenEnabled;
    public TroopType troopType;

    // Attackers will subscribe to this event and are notified when this troop dies
    public event Action OnDeathEvent;

    private GameObject target; // GameObject containing a Troop script
    private BattleManager battleManager; // Utility class used while troops fight

    void Start()
    {
        CheckStatsValid(); // To ensure correct use of the troop class's statistics

        battleManager = FindObjectOfType<BattleManager>();

        if (battleManager == null)
        {
            throw new MissingComponentException("Could not find BattleManager script");
        }
    }

    // Called 50 times per second
    void FixedUpdate()
    {
        if (healthCurrent <= 0.0f)
        {
            onDeath();
            return;
        }

        if (target == null)
        {
            target = FindTarget();
            target.GetComponent<Troop>().OnDeathEvent += TargetKilled;
        }
    }

    // Call this at the start of the fight
    public void Activate()
    {
        Regenerate();
    }

    // Called through events when the target is killed
    public void TargetKilled()
    {
        target = null;
    }

    // Throws exception if stats are out of expected ranges
    private void CheckStatsValid()
    {
        if (healthMax <= 0)
        {
            throw new FormatException("Max Health below 0");
        }
        if (healthMax < healthCurrent)
        {
            throw new FormatException("Max Health smaller than Current Health");
        }
        if (damageMax < 0 || damageMin < 0)
        {
            throw new FormatException("Damage cannot be negative");
        }
        if (damageMax < damageMin)
        {
            throw new FormatException("Max Damage smaller than Min Damage");
        }
        if (attackSpeed <= 0)
        {
            throw new FormatException("Attack Speed must be larger than 0");
        }
        if (regenEnabled) // Do not check these if regen is disabled on this troop
        {
            if (regenAmount <= 0)
            {
                throw new FormatException("Regeneration must be larger than 0 if regen is enabled");
            }
            if (regenSpeed <= 0)
            {
                throw new FormatException("Regeneration Speed must be larger than 0 if regen is enabled");
            }
        }
    }

    // Heals the unit if regen is enabled
    // Invokes itself repeatedly until troop death
    private void Regenerate()
    {
        // Dont regenerate dead units! Thats spooky
        if (!alive)
        {
            return;
        }

        if (regenEnabled || !alive)
        {
            // Test for overheal
            if (regenAmount + healthCurrent > healthMax)
            {
                // Apply healing equal to difference between current and max health
                healthCurrent += (healthMax - healthCurrent);
            }
            else
            {
                healthCurrent += regenAmount;
            }

            // Regenerate again
            Invoke("Regenerate", (1.0f / regenSpeed));
        }
        else
        {
            // Do not invoke Regenerate again
            return;
        }
    }

    // Uses the BattleManager class to find a suitable opponent
    // Returns null if no suitable opponent can be found
    private GameObject FindTarget()
    {
        GameObject chosenEnemy = null;
        GameObject[] enemies = battleManager.GetOpponents(gameObject.CompareTag("PlayerTroop"));
        float closestDistance = float.MaxValue;

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                // Calculate the distance to that enemy
                float distance = Vector3.Distance(enemy.transform.position, transform.position);

                // If it is smaller than the so far closest enemy, become the target
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    chosenEnemy = enemy;
                }
            }
        }

        // May be null
        return chosenEnemy;
    }

    // Call this when this troop dies
    private void onDeath()
    {
        if (OnDeathEvent != null)
        {
            OnDeathEvent.Invoke(); // Invoke the on death event
        }

        alive = false;
        healthCurrent = 0;



        gameObject.SetActive(false);
    }
}
