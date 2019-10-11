﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event Action onDeathEvent;
    public GameObject grid;
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
    public int attackRange = 14;
    private bool inRange;
    public float critChance = 0.0f;
    public float critDamageModifier = 2.0f;
    public Item item;
    public Node nodeUnitOnTopOf;

    // Start is called just before any of the Update methods is called the first time.
    void Start() 
    {
        health = healthMax;
        AddRigidBody();
    }

    // This function .
    void AddRigidBody() 
    {
        Rigidbody body;
        if (gameObject.TryGetComponent<Rigidbody>(out body)) {
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            body.freezeRotation = true;
        } else { 
            body = gameObject.AddComponent<Rigidbody>();
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            body.freezeRotation = true;
        }
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate() 
    {
        UnitNodeUpdate();

        // Get a path to the target.
        UnitPathing pathing;
        if (gameObject.TryGetComponent<UnitPathing>(out pathing)) {
            try {
                if (target != null && !pathing.hasPathToFollow && !inRange) {
                    pathing.GetPathing(target);
                }
            } catch (InvalidOperationException e) {
                print("Path already present: "+e);
            }
        }
        
        // Calls OnDeath when health reaches 0.
        // Else tries to find a target
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
    // This function updates the node a unit is currently on top of.
    private void UnitNodeUpdate()
    {
        Node tempNode;
        NodeGrid nodeGrid;

        if (grid.TryGetComponent<NodeGrid>(out nodeGrid))
        {
            if (nodeUnitOnTopOf == null)
            {
                nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
                tempNode = nodeUnitOnTopOf;
            }
            else
            {
                tempNode = nodeUnitOnTopOf;
                nodeUnitOnTopOf = nodeGrid.getNodeFromWorld(gameObject.transform.position);
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
        }
    }

    // This function finds an enemy target.
    private void FindTarget() 
    {
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

    // This function.
    private void TryRegen() 
    {
        if (regenCounter >= regenOnCount) {
            regenReady = true;
        } else {
            regenCounter++;
        }
        if (regenReady) {
            Regen();
        }
    }

    // This function regenerates the units health.
    private void Regen() 
    {
        if (health + healthRegen > healthMax) {
            health = healthMax;
        } else {
            health += healthRegen;
        }
        regenReady = false;
        regenCounter = 0;
        
    }

    // This function.
    private void TryAttack()
    {
        Unit targetUnit;
        if (!target.TryGetComponent<Unit>(out targetUnit)) {
            return;
        }

        if (nodeUnitOnTopOf != null && targetUnit.nodeUnitOnTopOf != null) {
            int distanceX = Mathf.Abs(nodeUnitOnTopOf.gridX - targetUnit.nodeUnitOnTopOf.gridX);
            int distanceY = Mathf.Abs(nodeUnitOnTopOf.gridY - targetUnit.nodeUnitOnTopOf.gridY);

            int distance = 0;
            if (distanceX > distanceY)
            {
            distance = 14 * distanceY + 10 * (distanceX - distanceY);
            } else {
            distance = 14 * distanceX + 10 * (distanceY - distanceX);
            }
            inRange = distance <= attackRange;
        }

        if (!attackReady) {
            if (attackCounter >= attackOnCount) {
                attackReady = true;
            } else {
                attackCounter++;
            }
        }

        if (attackReady && inRange) {
            Attack();
        }
    }

    // This function.
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

    // This function.
    public void RecieveAttack(Attack attack) 
    {
        health -= attack.damage;
    }

    // This function.
    public void FlingUnit() 
    {
        Rigidbody body;
        if (gameObject.TryGetComponent<Rigidbody>(out body)) {
            float xForce = UnityEngine.Random.Range(-200,200);
            float yForce = UnityEngine.Random.Range(-100,200);
            float zForce = UnityEngine.Random.Range(-200,200);

            Vector3 randomForce = new Vector3(xForce, yForce, zForce);

            float xRot = UnityEngine.Random.Range(-50, 50);
            float yRot = UnityEngine.Random.Range(-50, 50);
            float zRot = UnityEngine.Random.Range(-50, 50);

            Vector3 randomRotation = new Vector3(xRot, yRot, zRot);

            body.constraints = RigidbodyConstraints.None;
            body.freezeRotation = false;

            body.AddRelativeTorque(randomRotation);
            body.AddForce(randomForce);
        }
    }

    // This function.
    public void OnDeath() 
    {
        if (nodeUnitOnTopOf != null) {
            nodeUnitOnTopOf.unitOnTop = false;
        }

        FlingUnit();

        if (gameObject.TryGetComponent<UnitPathing>(out var pathing)) {
            Destroy(pathing);
        }
        if (gameObject.TryGetComponent<Unit>(out var unit)) {
            Destroy(unit);
        }
        if (gameObject.TryGetComponent<Collider>(out var collider)) {
            Destroy(collider, 2.0f);
        }
        if (CompareTag("EnemyTroop")) {
            GameManager.currency += 10;
        }
    }

    // This function.
    private void OnTargetDeath() 
    {
        target = null;
    }

    // This function.
    public void SetTarget(GameObject target) 
    {
        this.target = target;
        Unit unit;
        if (target.TryGetComponent<Unit>(out unit)) {
            unit.onDeathEvent += OnTargetDeath;
        }
    }

    // This function.
    public Item Equip(Item item) 
    {
        Item itemHolder = item;
        this.item = item;
        
        return itemHolder;
    }
}
