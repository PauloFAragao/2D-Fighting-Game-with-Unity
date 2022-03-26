using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //eventos
    public event EventHandler OnDamaged;                //evento de quando sofre dano
    public event EventHandler OnHealed;                 //evento de quando sofre cura
    public event EventHandler OnShieldDamaged;          //evento de quando sofre dano no escudo
    public event EventHandler OnShieldRecover;          //evento para quando o escudo é recuperado

    public event EventHandler OnStunDamage;             //evento para "causar dano" ao stun do personagem
    public event EventHandler OnStunRecover;            //evento para recuperar o stun do personagem

    //variaveis de indicação
    [SerializeField] private int healthAmountMax;       //quantidade de pontos de vida maxima
    private int shieldAmountMax;                        //quantidade de pontos de escudo maximo

    [SerializeField] private float stunMax;               //quantidade de pontos de stun maximo

    //variaveis de estado
    private int healthAmount;                           //quantidade de pontos de vida atual

    private int shieldAmount;                           //quantidade de pontos de escudo atual

    private float stunAmount;                             //quantidade de pontos de stun atual

    private void Start()
    {
        //inicialisando o valor dos pontos de vida
        healthAmount = healthAmountMax;

        //indicando o valor dos pontos de escudo maximo
        shieldAmountMax = healthAmountMax;

        //inicialisando o valor dos pontos de escudo
        shieldAmount = shieldAmountMax;

        //iniciando o valor dos pontos de stun
        stunAmount = stunMax;
    }

    //método que vai ser chamando quando personagem sofrer dano
    public void Damage(int amount)
    {
        healthAmount -= amount;

        if (healthAmount < 0)
            healthAmount = 0;

        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
    }

    //método que vai ser chamando quando o personagem sofrer cura
    public void Heal(int amount)
    {
        healthAmount += amount;

        if (healthAmount > healthAmountMax)
            healthAmount = healthAmountMax;

        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
    }

    //método que vai ser chamando quando o personagem sofrer dano no escudo
    public void ShieldDamage(int amount)
    {
        shieldAmount -= amount;

        if (shieldAmount < 0)
            shieldAmount = 0;

        if (OnShieldDamaged != null) OnShieldDamaged(this, EventArgs.Empty);
    }

    //método que vai ser usado para recuperar o escudo
    public void RecoverShield(int amount)
    {
        shieldAmount += amount;

        if (shieldAmount > shieldAmountMax)
            shieldAmount = shieldAmountMax;

        if (OnShieldRecover != null) OnShieldRecover(this, EventArgs.Empty);
    }

    //método que vai ser chamado para subtrair pontos de stun
    public void Stun(float amount)
    {
        stunAmount -= amount;

        Debug.Log("valor: "+amount+ " stunAmount: "+stunAmount);

        if (stunAmount < 0)
            stunAmount = 0;

        if (OnStunDamage != null) OnStunDamage(this, EventArgs.Empty);
    }

    //método que vai ser chamando para recuperar os pontos de stun
    public void StunRecover(float amount)
    {
        stunAmount += amount;

        if (stunAmount > stunMax)
            stunAmount = stunMax;

        if (OnStunRecover != null) OnStunRecover(this, EventArgs.Empty);
    }

    //retorna a percentagem de vida que o personagem está
    public float GetHealthPercent()
    {
        return (float)(healthAmount * 100) / healthAmountMax;
    }

    //retorna a percentage de escudo que o personagem está
    public float GetShieldPercent()
    {
        return (float)(shieldAmount * 100) / shieldAmountMax;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public int GetShieldAmount()
    {
        return shieldAmount;
    }

    public int GetShieldAmountMax()
    {
        return shieldAmountMax;
    }

    public float GetStunAmount()
    {
        return stunAmount;
    }

    public float GetStunPercent()
    {
        return (stunAmount * 100) / stunMax;
    }

    public float GetStunMax()
    {
        return stunMax;
    }

    public void SetStunFull()
    {
        StunRecover(stunMax);
    }

}
