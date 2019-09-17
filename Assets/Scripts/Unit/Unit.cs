using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Loyalty loyalty;
    private Tile tile;
    public Unit target;

    public int maximumHP;
    public int currentHP;
    public int maximumMP;
    public int currentMP;
    public int attackDamage;
    public double criticalStrikeChance;
    public int specialAttackDamage;
    public double specialCriticalStrikeChance;

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

    void Start() {
        this.currentHP = maximumHP;
        this.currentMP = maximumMP;
    }

    //Called once per frame
    void Update() {
        AttackTarget();
    }

    public void RecieveAttack(Attack attack) {
        currentHP -= attack.calculateDamage();
        if (currentHP <= 0) {
            OnDeath(attack);
        }
    }

    private void AttackTarget() {
        if (target != null) {
            Attack attack = new Attack(attackDamage, criticalStrikeChance, gameObject);
            target.RecieveAttack(attack);
        }
    }

    //Call this when the target dies
    public void RemoveTarget() {
        target = null;
    }

    public Tile GetTile() {
        return tile;
    }

    public void placeOnBoard(Tile tile) {
        this.tile = tile;
        tile.SetUnit(this);
    }

    private void OnDeath(Attack finalBlow) {        
        Unit unit = null;
        UnitPathing pathing = null;
        finalBlow.attacker.TryGetComponent<Unit>(out unit);

        gameObject.TryGetComponent<UnitPathing>(out pathing);
        if (pathing != null) {
            print("Destroying pathing from "+name);
            Destroy(pathing, 0.0f);
        }

        if (unit != null) {
            unit.RemoveTarget();
        }

        Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.AddForce(new Vector3(Random.Range(-200.0f, 200.0f), Random.Range(100.0f, 200.0f), Random.Range(-200.0f, 200.0f)));
        Collider collider;
        if (gameObject.TryGetComponent<Collider>(out collider)) {
            Destroy(collider, 2.0f);
        }
        Destroy(this, 0.0f);
    }

}

public enum Loyalty {
    ALLY,
    ENEMY
}
