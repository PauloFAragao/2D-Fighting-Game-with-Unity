using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StunBar : MonoBehaviour
{
    //referencias
    [SerializeField] private HealthSystem healthSystem;         //referencia ao healthSystem

    [SerializeField] private Image stunBar;                     //referencia a barra da interface

    //variaveis de indicação
    private const float RECOVER_TIMER_MAX = 1F;                 //tempo para o escudo se recuperar

    //variaveis de estado
    private float recoverTimer;                                 //timer

    //variaveis de comando
    private bool stunRecover;                                 //indicador de que tem que recuperar o stun

    private void Start()
    {
        healthSystem.OnStunDamage += HealthSystem_OnStunDamage;
        healthSystem.OnStunRecover += HealthSystem_OnStunRecover;
    }

    private void Update()
    {
        //testando se o timer está maior que zero
        if (recoverTimer > 0)
            recoverTimer -= Time.deltaTime;

        if (recoverTimer < 0 && stunRecover)
            RecoverStun();
    }

    private void HealthSystem_OnStunDamage(object sender, System.EventArgs e)
    {
        //iniciando o timer
        recoverTimer = RECOVER_TIMER_MAX;

        //para fazer a barra se mover
        stunRecover = true;

        //mudando na interface
        SetStun(healthSystem.GetStunPercent());
    }

    private void HealthSystem_OnStunRecover(object sender, System.EventArgs e)
    {
        //mudando na interface
        SetStun(healthSystem.GetStunPercent());
    }

    private void SetStun(float percent)
    {
        stunBar.fillAmount = (percent / 100);
    }

    private void RecoverStun()
    {
        if (healthSystem.GetStunAmount() < healthSystem.GetStunMax())
        {
            healthSystem.StunRecover(.1f);
        }
    }

}
