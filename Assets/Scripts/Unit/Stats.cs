using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int maximumHP;
    public int currentHP;

    public int maximumMP;
    public int currentMP;

    public int attackDamage;
    public double criticalStrikeChance;

    public int specialAttackDamage;
    public double specialCriticalStrikeChance;

    public Stats(int maxHP, int maxMP) {
        this.maximumHP = maxHP;
        this.currentHP = maxHP;
        this.maximumMP = maxMP;
        this.currentMP = 0;
    }

    public void setAttackDamage(int attackDamage) {
        this.attackDamage = attackDamage;
    }

    public void setCriticalStrikeChance(double criticalStrikeChance) {
        this.criticalStrikeChance = criticalStrikeChance;
    }

    public void setSpecialAttackDamage(int specialAttackDamage) {
        this.specialAttackDamage = specialAttackDamage;
    }

    public void setSpecialCriticalStrikeChance(double specialCriticalStrikeChance) {
        this.specialCriticalStrikeChance = specialCriticalStrikeChance;
    }
}
