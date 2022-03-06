using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWaveEffectController : SpellEffectController
{
    //metodo que vai ser chamado para causar dano
    public override void DoDamage(Collider2D collision)
    {
        //tipo de dano: medio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 0
        collision.GetComponentInChildren<Damageable>().SetDamage(2, 0, 0, 25);
    }

    //metodo que vai mudar o estado dessa magia
    public override void SetState()
    {
        SetStateCicle(false);
        SetAnimation("End");
        SetVelZero();
    }
}
