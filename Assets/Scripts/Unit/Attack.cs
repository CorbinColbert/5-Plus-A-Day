using UnityEngine;

public class Attack
{
    public float damage {get; private set;}

    public Attack(Unit attacker)
    {
        damage = Random.Range(attacker.damageMin, attacker.damageMax);

        if (attacker.critChance <= Random.Range(0.0f, 1.0f))
        {
            damage *= attacker.critDamageModifier;
        }
    }
}
