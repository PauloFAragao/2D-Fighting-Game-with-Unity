using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWaveEffectController : SpellEffectController
{

    public override void SetData(bool direction, bool player)
    {
        SetRightDirection(direction);
        SetPlayer(player);

        SetStateCicle(true);

        if (direction)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }


    //método que vai ser chamado para causar dano
    public override void DoDamage(Collider2D collision)
    {
        //tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 0
        collision.GetComponentInChildren<Damageable>().SetDamage(2, 30, 0, 25);
    }

    //método que vai mudar o estado dessa magia
    public override void SetState()
    {
        SetStateCicle(false);
        SetAnimation("End");
        SetVelZero();
    }
}
