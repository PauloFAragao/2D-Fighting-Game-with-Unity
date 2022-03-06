using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGeyserEffectController : SpellEffectController
{
    private void Start()
    {
        SetStateCicle(false);
    }

    //metodo que vai ser chamado para causar dano
    public override void DoDamage(Collider2D collision)
    {
        //tipo de dano: medio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 0
        collision.GetComponentInChildren<Damageable>().SetDamage(7, 0, 0, 5);
    }
}
