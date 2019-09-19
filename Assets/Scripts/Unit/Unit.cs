using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event Action onDeathEvent;
    public GameObject domain;
    public GameObject target;

    public float healthMax = 100.0f;
    public float health;

    public float healthRegen = 1.0f;
    private bool regenReady = false;
    private int regenCounter = 0;
    private int regenOnCount = 100;

    public float damageMin = 2.0f;
    public float damageMax = 4.0f;
    private bool attackReady = false;
    private int attackCounter = 0;
    [SerializeField]
    private int attackOnCount = 50;

    public float critChance = 0.0f;
    public float critDamageModifier = 2.0f;
    public Item item;

    //@Cameron this is the reference to the node the unit is on top of
    private Node nodeUnitOnTopOf;
    //this is the reference to the grid - needs to be set in the unity inspector
    public GameObject grid;

    void Start() {
        health = healthMax;

        //TODO : Remove later when target can be found from code
        if (target != null) {
            SetTarget(target);
        }

        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        body.freezeRotation = true;
    }

    void FixedUpdate() {

        //start of the loop to update what node the unit is currently on top of
        Node tempNode;

        if (nodeUnitOnTopOf == null)
        {
            nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
            tempNode = nodeUnitOnTopOf;
        }
        else
        {
            tempNode = nodeUnitOnTopOf;
            nodeUnitOnTopOf = grid.GetComponent<NodeGrid>().getNodeFromWorld(gameObject.transform.position);
        }

        if (nodeUnitOnTopOf == tempNode)
        {
            tempNode.unitOnTop = false;
        }
        else
        {
            tempNode.unitOnTop = true;
            tempNode = nodeUnitOnTopOf;
            tempNode.unitOnTop = false;
        }
        //end of node update loop

        if (health <= 0) {
            if (onDeathEvent != null) {
                onDeathEvent();
            }    
            OnDeath();
        } else {
            if (target == null) {
                FindTarget();
            } else {
                TryAttack();
                TryRegen();
            }
        }       
    }

    private void FindTarget() {
        GameObject[] objects = null;
        if (CompareTag("EnemyTroop")) {
            objects = GameObject.FindGameObjectsWithTag("PlayerTroop");
        }
        if (CompareTag("PlayerTroop")) {
            objects = GameObject.FindGameObjectsWithTag("EnemyTroop");
        }

        if (objects != null) {
            int size = objects.Length;
            if (size == 0) {
                print("Victory!");
            } else {
                SetTarget(objects[UnityEngine.Random.Range(0, size)]);
            }
            
        }
    }

    private void TryRegen() {
        if (regenCounter >= regenOnCount) {
            regenReady = true;
        } else {
            regenCounter++;
        }
        if (regenReady) {
            Regen();
        }
    }

    private void Regen() {
        if (health + healthRegen > healthMax) {
            health = healthMax;
        } else {
            health += healthRegen;
        }
        regenReady = false;
        regenCounter = 0;
        
    }

    private void TryAttack() {
        if (!attackReady) {
            if (attackCounter >= attackOnCount) {
                attackReady = true;
            } else {
                attackCounter++;
            }
        }
        if (attackReady) {
            Attack();
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            Attack attack = new Attack(this);
            Unit other;
            if (target.TryGetComponent<Unit>(out other))
            {
                other.RecieveAttack(attack);
                attackCounter = 0;
                attackReady = false;
                Rigidbody body = gameObject.GetComponent<Rigidbody>();
                if (body != null) {
                    body.AddForce(new Vector3(0, 100, 0));
                }
            }

            
        }
    }

    public void RecieveAttack(Attack attack) {
        health -= attack.damage;
    }

    private void OnDeath() {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        float xForce = UnityEngine.Random.Range(200,-200);
        float yForce = UnityEngine.Random.Range(100, 200);
        float zForce = UnityEngine.Random.Range(200,-200);
        Vector3 randomForce = new Vector3(xForce, yForce, zForce);

        body.constraints = RigidbodyConstraints.None;
        body.freezeRotation = false;

        body.AddForce(randomForce);

        Destroy(gameObject.GetComponent<UnitPathing>());

        Destroy(this);

        Destroy(gameObject.GetComponent<Collider>(), 4.0f);

        Destroy(gameObject, 5.0f);
    }

    private void OnTargetDeath() {
        target = null;
    }

    public void SetTarget(GameObject target) {
        this.target = target;
        Unit unit;
        if (target.TryGetComponent<Unit>(out unit)) {
            unit.onDeathEvent += OnTargetDeath;
        }
    }

    public Item Equip(Item item) {
        Item itemHolder = item;
        this.item = item;
        
        return itemHolder;
    }
}
