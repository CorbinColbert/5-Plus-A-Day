using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event Action onDeathEvent;
    public Loyalty loyalty;
    public GameObject domain;
    public GameObject target;

    public float healthMax = 100.0f;
    public float health;
    public float healthRegen = 1.0f;
    public float damageMin = 2.0f;
    public float damageMax = 4.0f;

    private int attackCounter = 0;
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
                onDeathEvent.Invoke();
            }    
            OnDeath();
        } else {
            Attack();
            Heal();
        }       
    }

    void Awake() {
        
    }

    private void Heal() {
        if ()
    }

    // void Metod() {
    //     domain.GetComponent<PathHelper>().RequestAPath(target.gameObject, gameObject, MetodTwo)

    // }

    // void MetodTwo(List<Node> path, bool successful) {
    //     domain.GetComponent<PathHelper>().PathRequestFinished(path, successful);
    // }

    private void Attack() {
        if (target != null) {
            Attack attack = new Attack(this);
            target.GetComponent<Unit>().RecieveAttack(attack);
        }
    }

    public void RecieveAttack(Attack attack) {
        health -= attack.damage;
    }

    private void OnDeath() {
        Destroy(this, 3.0f);
    }

    private void OnTargetDeath() {
        print("Triggered death event on unit "+gameObject.name);
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

public enum Loyalty {
    PLAYER,
    ENEMY
}
