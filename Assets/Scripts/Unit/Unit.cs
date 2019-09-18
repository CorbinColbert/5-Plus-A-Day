﻿using System.Collections;
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

    void Start() {
        health = healthMax;

        //TODO : Remove later when target can be found from code
        if (target != null) {
            SetTarget(target);
        }
    }

    void FixedUpdate() {
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

    void Awake() {
        
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
                SetTarget(objects[UnityEngine.Random.Range(0, size + 1)]);
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
            target.GetComponent<Unit>().RecieveAttack(attack);
            attackCounter = 0;
            attackReady = false;
            Rigidbody body = gameObject.GetComponent<Rigidbody>();
            body.AddForce(new Vector3(0, 100, 0));
        }
    }

    public void RecieveAttack(Attack attack) {
        health -= attack.damage;
    }

    private void OnDeath() {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        float xForce = UnityEngine.Random.Range(200,-200);
        float yForce = UnityEngine.Random.Range(100, 400);
        float zForce = UnityEngine.Random.Range(200,-200);
        Vector3 randomForce = new Vector3(xForce, yForce, zForce);

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
        target.GetComponent<Unit>().onDeathEvent += OnTargetDeath;
    }

    public Item Equip(Item item) {
        Item itemHolder = item;
        this.item = item;
        
        return itemHolder;
    }
}
