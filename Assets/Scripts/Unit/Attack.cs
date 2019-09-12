using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public GameObject attacker;
    private bool criticalHit;
    private int damage;
    private double criticalHitModifier;

    protected Attack() { }

    public Attack(int damage, double criticalHitModifier, GameObject attacker) {
        this.damage = damage;
        this.criticalHitModifier = criticalHitModifier;
        this.criticalHit = (Random.value <= 0.5f);
        this.attacker = attacker;
    }

    public int calculateDamage() {
        return criticalHit ? (int)(damage * criticalHitModifier) : damage;
    }

    public bool isCrit() {
        return criticalHit;
    }
}
