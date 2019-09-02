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

public static class SP_Factory {
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
    
    public static SpecialAttack getSpecialAttack(SP_TYPE type) {
        switch (type) {
            case SP_TYPE.BASH:
                return SP_Factory.getBash();
            case SP_TYPE.SLASH:
                return  SP_Factory.getSlash();
            case SP_TYPE.STAB:
                return SP_Factory.getStab();
            case SP_TYPE.SUPER_WHIRL:
                return SP_Factory.getSuperWhirl();
            case SP_TYPE.WHIRL:
                return SP_Factory.getWhirl();
            default:
                return null;
        }
        }
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



