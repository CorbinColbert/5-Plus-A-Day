using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event Action onDeathEvent;
    public Loyalty loyalty;
    public Grid domain;
    public Unit target;

    public float healthMax = 100.0f;
    public float health;
    public float healthRegen = 1.0f;
    public float damageMin = 2.0f;
    public float damageMax = 4.0f;

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

    void Update() {
        if (health <= 0) {
            onDeathEvent();
        } else {
            Attack();
        }
    }

    private void Attack() {
        if (target != null) {
            Attack();
        }
    }

    public void RecieveAttack(Attack attack) {
        health -= attack.damage;
    }

    private void OnTargetDeath() {
        print("Triggered death event on unit "+gameObject.name);
    }

    public void SetTarget(Unit target) {
        this.target = target;
        target.onDeathEvent += OnTargetDeath;
    }

    public Item Equip(Item item) {
        Item itemHolder = item;
        this.item = item;
        
        return itemHolder;
    }
}

public enum Loyalty {
    PLAYER,
    ENEMY
}
