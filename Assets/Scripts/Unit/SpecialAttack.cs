using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SP_TYPE {
    // [] is a space on the board
    // ## is the caster of the ability

    /**
    Relative to [0,0]:
    [-1, 1][ 0, 1][ 1, 1]
    [-1, 0][ 0, 0][ 1, 0]
    [-1,-1][ 0,-1][ 1,-1]
    */

    SUPER_WHIRL, 
    // [][][]
    // []##[]
    // [][][]

    WHIRL,
    //   []  
    // []##[]
    //   []  

    STAB,
    //   []
    //   []
    //   ##

    SLASH,
    // [][][] 
    //   ##

    BASH,
    //   []
    //   ##

}

public class SpecialAttack : Attack {
    private List<Pair> ordinates;

    public SpecialAttack() : base() {}

    public SpecialAttack(int damage, double criticalStrikeChance, GameObject attacker) : base(damage, criticalStrikeChance, attacker) {
        
    }

    public void addOrdinate(int relativeX, int relativeY) {
        ordinates.Add(new Pair(relativeX, relativeY));
    }

    public List<Pair> getAOE() {
        return ordinates;
    }



}



