using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarShrink : MonoBehaviour
{
    //referencias
    [SerializeField] private HealthSystem healthSystem;         //referencia ao healthSystem
    [SerializeField] private RectTransform lifeBar;             //referencia a barra de vida
    [SerializeField] private RectTransform damagedBar;          //referencia a barra vermelha

    //variaveis de indicação
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 1f;   //tempo que vai demorar para começar a mover a damaged bar

     private float size = 765, minPos = 505f;                  //posições

    //variaveis de estado
    private float damagedHealthShrinkTimer;                     //timer

    //variaveis de comando
    private bool shrinkBar;                                     //indicador de que a barra tem que se mexer

    private void Start()
    {
        //eventos
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    void Update()
    {
        //testando se o timer está maior que zero
        if(damagedHealthShrinkTimer > 0) 
            damagedHealthShrinkTimer -= Time.deltaTime;

        //se o timer estiver maior que zero
        if(damagedHealthShrinkTimer < 0 && shrinkBar)
            ShrinkDamagedBar();
        
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        //mudando na interface
        SetHealth(healthSystem.GetHealthPercent());

        //reposicionando a damagebar na interface
        damagedBar.anchoredPosition = lifeBar.anchoredPosition;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        //iniciando o timer
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;

        //para fazer a damaged bar se mover
        shrinkBar = true;

        //mudando na interface
        SetHealth(healthSystem.GetHealthPercent());
    }

    //método que vai alterar a barra na interface
    private void SetHealth(float healthPercent)
    {
        lifeBar.anchoredPosition = new Vector2( ( (size * healthPercent) / 100 ) - minPos, 0);
    }

    //método que vai mover a damaged bar
    private void ShrinkDamagedBar()
    {
        //verificando se a barra já chegou no ponto que deveria chegar
        if( damagedBar.anchoredPosition.x > lifeBar.anchoredPosition.x )
            damagedBar.anchoredPosition = new Vector2 ( damagedBar.anchoredPosition.x - 4f, 0);
        
        else//se já chegou
        {
            //posicionando corretamente a damaged bar
            damagedBar.anchoredPosition = lifeBar.anchoredPosition;

            //para não deixar ficar chamando esse método desnecessariamente
            shrinkBar = false;
        }
    }

}
