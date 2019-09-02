using System.Collections;
using System.Collections.Generic;

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

public class SpecialAttack {
    private List<Pair> ordinates;

    public void addOrdinate(int relativeX, int relativeY) {
        ordinates.Add(new Pair(relativeX, relativeY));
    }

    public List<Pair> getAOE() {
        return ordinates;
    }



}



