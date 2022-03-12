using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowBars : MonoBehaviour
{
    //referencias
    [SerializeField] PowerSystem powerSystem;               //referencia ao powerSystem

    [SerializeField] private GameObject powBar;             //power bar 1

    [SerializeField] private RectTransform powerSlider;     //barra que vai se mover

    //variaveis de indicação
    private float size = 221, minPos = 25;                  //posições

    private void Start()
    {
        //eventos
        powerSystem.ModifyPow += PowerSystem_ModifyPow;
        powerSystem.ActivePow += PowerSystem_ActivePow;

        //desativando a barra de pow
        powBar.SetActive(false);
    }

    private void PowerSystem_ModifyPow(object sender, System.EventArgs e)
    {
        if (powerSystem.GetPowTime() <= 0)
        {
            //desativando a barra de pow
            powBar.SetActive(false);
        }

        SetPow(powerSystem.GetPowPercent());
    }

    private void PowerSystem_ActivePow(object sender, System.EventArgs e)
    {
        //ativando a barra de pow
        powBar.SetActive(true);

        SetPow(powerSystem.GetPowPercent());
    }

    private void SetPow(float powPercent)
    {
        powerSlider.anchoredPosition = new Vector2(((size * powPercent) / 100) + minPos, 0);
    }

}
