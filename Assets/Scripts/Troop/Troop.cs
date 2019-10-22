using System;
using System.Collections;
using System.Collections.Generic;
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
     * Attack Range (how far away a troop can attack another troop from)
     * Movement Speed
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
    public float attackRange;
    public float moveSpeed;
    public float regenAmount;
    public float regenSpeed;
    public bool regenEnabled;
    public TroopType troopType;
    public bool hasTarget = false;

    // Attackers will subscribe to this event and are notified when this troop dies
    public event Action OnDeathEvent;

    public GameObject target; // GameObject containing a Troop script
    private BattleManager battleManager; // Utility class used while troops fight
    public TroopState state;

    void Start()
    {
        Rigidbody body = gameObject.AddComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotationY;

        CheckStatsValid(); // To ensure correct use of the troop class's statistics
        battleManager = FindObjectOfType<BattleManager>();
        if (battleManager == null)
        {
            throw new MissingComponentException("Could not find BattleManager script");
        }
        Activate();
    }

    // Called 50 times per second
    void FixedUpdate()
    {
        if (healthCurrent <= 0.0f)
        {
            onDeath();
            return;
        }

        if (hasTarget == false)
        {
            target = FindTarget();
            if (hasTarget == false) {
                return;
            }
            target.GetComponent<Troop>().OnDeathEvent += TargetKilled;

            if (!gameObject.CompareTag("EnemyTroop"))
            {
                // Get a path to the target
                UnitPathing pathing = GetComponent<UnitPathing>();
                pathing.GetPathing(target);
            }
        }
        else
        {
            switch (state)
            {
                case TroopState.MOVING:
                    float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                    bool targetInRange = distance <= attackRange;

                    if (targetInRange)
                    {
                        state = TroopState.LOCKING;
                    }
                    break;
                case TroopState.LOCKING:
                        AttackTarget();
                    break;
                default:
                    // Do nothing
                    break;
            }
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
        hasTarget = false;
        state = TroopState.MOVING;
    }

    public void AttackTarget()
    {
        print("Attacking");
        if (target == null)
        {
            return;
        }

        Troop targetTroop = target.GetComponent<Troop>();
        float damage = UnityEngine.Random.Range(damageMin, damageMax);
        targetTroop.RecieveAttack(damage);

        Invoke("AttackTarget", 1.0f / attackSpeed);
    }

    public void RecieveAttack(float damage)
    {
        if (alive)
        {
            healthCurrent -= damage;
        }
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
        List<GameObject> enemies = battleManager.GetOpponents(gameObject.CompareTag("PlayerTroop"));
        float closestDistance = float.MaxValue;

        print(enemies.Count + ":Count");

        if (enemies.Count > 0)
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
                    print("Assigned chosen enemy");
                }
            }
        }

        if (chosenEnemy != null)
        {
            hasTarget = true;
        }

        // May be null
        return chosenEnemy;
    }

    // Call this when this troop dies
    private void onDeath()
    {
        print("On death called");
        alive = false;
        healthCurrent = 0;

        if (OnDeathEvent != null)
        {
            OnDeathEvent.Invoke(); // Invoke the on death event
        }
 
        Rigidbody body = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(UnityEngine.Random.Range(-100.0f,100.0f),
            UnityEngine.Random.Range(100.0f, 400.0f), 
            UnityEngine.Random.Range(-100.0f, 100.0f));
        body.AddForce(force);

        Destroy(GetComponent<UnitPathing>());
        Destroy(this);
    }
}
