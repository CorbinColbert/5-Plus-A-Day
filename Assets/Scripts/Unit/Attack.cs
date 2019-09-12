using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private bool criticalHit;
    private int damage;
    private double criticalHitModifier;

    public Attack(int damage) {
        this.damage = damage;
        this.criticalHit = false;
    }

    public Attack(int damage, double criticalHitModifier) {
        this.damage = damage;
        this.criticalHitModifier = criticalHitModifier;
        this.criticalHit = true;
    }

    public int calculateDamage() {
        return criticalHit ? (int)(damage * criticalHitModifier) : damage;
    }

    public bool isCrit() {
        return criticalHit;
    }
}
