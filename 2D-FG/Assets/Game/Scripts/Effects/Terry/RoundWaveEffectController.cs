using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundWaveEffectController : SpellEffectController
{
    private void Start()
    {
        SetStateCicle(false);
    }

    //método que vai ser chamado para causar dano
    public override void DoDamage(Collider2D collision)
    {
        //tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / for�a de empurrar do golpe: 0
        collision.GetComponentInChildren<Damageable>().SetDamage(2, 40, 12, 25);
    }
}
