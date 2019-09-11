using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpecialAttackFactory {
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
                return SpecialAttackFactory.getBash();
            case SP_TYPE.SLASH:
                return  SpecialAttackFactory.getSlash();
            case SP_TYPE.STAB:
                return SpecialAttackFactory.getStab();
            case SP_TYPE.SUPER_WHIRL:
                return SpecialAttackFactory.getSuperWhirl();
            case SP_TYPE.WHIRL:
                return SpecialAttackFactory.getWhirl();
            default:
                return null;
        }
        }
    }
    
