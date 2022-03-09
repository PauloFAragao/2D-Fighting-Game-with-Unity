using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBar : MonoBehaviour
{
    //referencias
    [SerializeField] private HealthSystem healthSystem;         //referencia ao healthSystem
    [SerializeField] private RectTransform shieldBar;           //referencia a barra de vida

    //variaveis de indicação
    private const float RECOVER_TIMER_MAX = 1F;                 //tempo para o escudo se recuperar

    //-105 a -502
    private float size = 397f, minPos = 502f;                  //posições

    //variaveis de estado
    private float recoverTimer;                                 //timer

    //variaveis de comando
    private bool shieldRecover;                                 //indicador de que a barra tem que se mexer

    private void Start() 
    {
        //eventos
        healthSystem.OnShieldDamaged += HealthSystem_OnShieldDamaged;
        healthSystem.OnShieldRecover += HealthSystem_OnShieldRecover;
    }
    
    private void Update()
    {

        //testando se o timer está maior que zero
        if( recoverTimer > 0 ) 
            recoverTimer -= Time.deltaTime;

        if( recoverTimer < 0 && shieldRecover)
            RecoverShield();
    }

    private void HealthSystem_OnShieldDamaged(object sender, System.EventArgs e)
    {
        //iniciando o timer
        recoverTimer = RECOVER_TIMER_MAX;

        //para fazer a barra se mover
        shieldRecover = true;

        //mudando na interface
        SetShield(healthSystem.GetShieldPercent());
    }

    private void HealthSystem_OnShieldRecover(object sender, System.EventArgs e)
    {
        //mudando na interface
        SetShield(healthSystem.GetShieldPercent());
    }

    //método que vai alterar a barra na interface  
    private void SetShield(float shieldPercent)
    {
        shieldBar.anchoredPosition = new Vector2( ( ( size * shieldPercent ) / 100 ) -  minPos, 0);
    }

    //método que vai recuperar o escudo
    private void RecoverShield()
    {
        if( healthSystem.GetShieldAmount() < healthSystem.GetShieldAmountMax() )
        {
            healthSystem.RecoverShield(1);
        }
    }

}
