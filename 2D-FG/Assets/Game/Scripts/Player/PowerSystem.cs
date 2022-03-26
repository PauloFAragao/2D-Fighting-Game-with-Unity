using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public event EventHandler ModifyPower;     //evento de quando a quantidade de poder é alterada

    public event EventHandler ModifyPow;       //evento de quanto estoura a barra

    public event EventHandler ActivePow;       //evento de quando ativa a barra

    //variaveis de indicação
    private float powTimerMax = 4.5f;          //quantidade de tempo que o pow fica ativo

    //variaveis de estado
    private int currentPower = 0;               //quantidade de power atual
    private float currentPowTime;               //quantidade de tempo restante do pow

    private void Update()
    {
        //verificando se o pow está ativo
        if (currentPowTime > 0)
        {
            SetPow(Time.deltaTime);
        }
    }

    public void SetPower(int amount)
    {
        currentPower += amount;

        //verificando se chegou ao maximo 
        if (currentPower > 300)
            currentPower = 300;

        if (ModifyPower != null) ModifyPower(this, EventArgs.Empty);
    }

    public void SetPow(float amount)
    {
        currentPowTime -= amount;

        if (ModifyPow != null) ModifyPow(this, EventArgs.Empty);
    }

    //esse método vai ser usado para iniciar o pow
    public void StartPow()
    {
        currentPowTime = powTimerMax;

        if (ActivePow != null) ActivePow(this, EventArgs.Empty);
    }

    public int GetCurrentPower()
    {
        return currentPower;
    }

    public float GetPowerPercent()
    {
        if (currentPower == 300)
            return 100f;

        if (currentPower >= 200)
            return (float)currentPower - 200;

        if (currentPower >= 100)
            return (float)currentPower - 100;

        return (float)currentPower;
    }

    public float GetPowPercent()
    {
        return (currentPowTime * 100) / powTimerMax;
    }

    public float GetPowTime()
    {
        return currentPowTime;
    }


}
