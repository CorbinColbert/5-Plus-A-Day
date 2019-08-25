using System.Collections;
using System.Collections.Generic;

public enum AOE_TYPE {
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

public static class AOE_Factory {
    public static SpecialAttack getSuperWhirl() {
        SpecialAttack areaOfEffect = new SpecialAttack();

        areaOfEffect.addOrdinate(-1, 0);
        areaOfEffect.addOrdinate( 1, 0);
        areaOfEffect.addOrdinate( 0, 1);
        areaOfEffect.addOrdinate( 0,-1);

        return areaOfEffect;
    }
    public static SpecialAttack getWhirl() {
        SpecialAttack areaOfEffect = new SpecialAttack();

        areaOfEffect.addOrdinate(-1, 1);
        areaOfEffect.addOrdinate(-1, 0);
        areaOfEffect.addOrdinate(-1,-1);
        areaOfEffect.addOrdinate( 1, 1);
        areaOfEffect.addOrdinate( 1, 0);
        areaOfEffect.addOrdinate( 1,-1);
        areaOfEffect.addOrdinate( 0, 1);
        areaOfEffect.addOrdinate( 0,-1);

        return areaOfEffect;
    }
    public static SpecialAttack getStab() {
        SpecialAttack areaOfEffect = new SpecialAttack();

        areaOfEffect.addOrdinate( 0, 1);
        areaOfEffect.addOrdinate( 0, 2);

        return areaOfEffect;
    }
    public static SpecialAttack getSlash() {
        SpecialAttack areaOfEffect = new SpecialAttack();

        areaOfEffect.addOrdinate( 0, 1);
        areaOfEffect.addOrdinate( 1, 1);
        areaOfEffect.addOrdinate( -1, 1);

        return areaOfEffect;
    }
    public static SpecialAttack getBash() {
        SpecialAttack areaOfEffect = new SpecialAttack();

        areaOfEffect.addOrdinate( 0, 1);

        return areaOfEffect;
    }
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

public class Pair {
    private int x;
    private int y;

    public Pair(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }
}
