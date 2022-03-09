using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBars : MonoBehaviour
{
    //referencias
    [SerializeField] PowerSystem powerSystem;

    [SerializeField] private GameObject powerBar1;              //power bar 1
    [SerializeField] private GameObject powerBar2;              //power bar 2
    [SerializeField] private GameObject powerBar3;              //power bar 3

    [SerializeField] private GameObject powerBarFull1;          //power bar full 1
    [SerializeField] private GameObject powerBarFull2;          //power bar full 2
    [SerializeField] private GameObject powerBarFull3;          //power bar full 3

    //variaveis de indicação
    private float size = 140, minPos = 1090;//-1090 -950

    private void Start()
    {
        //evento
        powerSystem.ModifyPower += PowerSystem_ModifyPower;

        //desativando as barras de especial
        powerBar1.SetActive(false);
        powerBar2.SetActive(false);
        powerBar3.SetActive(false);
        powerBarFull1.SetActive(false);
        powerBarFull2.SetActive(false);
        powerBarFull3.SetActive(false);
    }

    private void PowerSystem_ModifyPower(object sender, System.EventArgs e)
    {
        SetPower(powerSystem.GetPowerPercent());
    }

    //método que vai ser chamando para atualizar a interface
    private void SetPower(float powerPercent)
    {
        //caso tenha 3 barras de poder cheias
        if (powerSystem.GetCurrentPower() == 300)
        {
            if (!powerBarFull1.activeSelf)//verificando se a barra está ativa
                powerBarFull1.SetActive(true);

            if (!powerBarFull2.activeSelf)//verificando se a barra está ativa
                powerBarFull2.SetActive(true);

            if (!powerBarFull3.activeSelf)//verificando se a barra está ativa
                powerBarFull3.SetActive(true);


            if (powerBar1.activeSelf)//verificando se a barra está ativa
                powerBar1.SetActive(false);

            if (powerBar2.activeSelf)//verificando se a barra está ativa
                powerBar2.SetActive(false);

            if (powerBar3.activeSelf)//verificando se a barra está ativa
                powerBar3.SetActive(false);
        }
        //caso tenha 2 barras de poder cheias
        else if (powerSystem.GetCurrentPower() >= 200)
        {
            if (!powerBarFull1.activeSelf)//verificando se a barra está ativa
                powerBarFull1.SetActive(true);

            if (!powerBarFull2.activeSelf)//verificando se a barra está ativa
                powerBarFull2.SetActive(true);

            if (powerBarFull3.activeSelf)//verificando se a barra está ativa
                powerBarFull3.SetActive(false);


            if (powerBar1.activeSelf)//verificando se a barra está ativa
                powerBar1.SetActive(false);

            if (powerBar2.activeSelf)//verificando se a barra está ativa
                powerBar2.SetActive(false);

            if (!powerBar3.activeSelf)//verificando se a barra está ativa
                powerBar3.SetActive(true);

            powerBar3.GetComponent<RectTransform>().anchoredPosition = new Vector2(((size * powerPercent) / 100) - minPos, 1.7f);

        }
        //caso tenha 1 barra de poder cheia
        else if (powerSystem.GetCurrentPower() >= 100)
        {
            if (!powerBarFull1.activeSelf)//verificando se a barra está ativa
                powerBarFull1.SetActive(true);

            if (powerBarFull2.activeSelf)//verificando se a barra está ativa
                powerBarFull2.SetActive(false);

            if (powerBarFull3.activeSelf)//verificando se a barra está ativa
                powerBarFull3.SetActive(false);


            if (powerBar1.activeSelf)//verificando se a barra está ativa
                powerBar1.SetActive(false);

            if (!powerBar2.activeSelf)//verificando se a barra está ativa
                powerBar2.SetActive(true);

            if (powerBar3.activeSelf)//verificando se a barra está ativa
                powerBar3.SetActive(false);

            powerBar2.GetComponent<RectTransform>().anchoredPosition = new Vector2(((size * powerPercent) / 100) - minPos, 1.7f);

        }
        //caso não tenha nenhuma barra de power cheia
        else
        {
            if (powerBarFull1.activeSelf)//verificando se a barra está ativa
                powerBarFull1.SetActive(false);

            if (powerBarFull2.activeSelf)//verificando se a barra está ativa
                powerBarFull2.SetActive(false);

            if (powerBarFull3.activeSelf)//verificando se a barra está ativa
                powerBarFull3.SetActive(false);


            if (!powerBar1.activeSelf)//verificando se a barra está ativa
                powerBar1.SetActive(true);

            if (powerBar2.activeSelf)//verificando se a barra está ativa
                powerBar2.SetActive(false);

            if (powerBar3.activeSelf)//verificando se a barra está ativa
                powerBar3.SetActive(false);

            powerBar1.GetComponent<RectTransform>().anchoredPosition = new Vector2(((size * powerPercent) / 100) - minPos, 1.7f);
        }

    }

}
