using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSystem : MonoBehaviour
{
    public event EventHandler ModifyPower;                //evento de quando a quantidade de poder é alterada

    //variaveis de indicação
    private int currentPower = 0;

    public void SetPower(int amount)
    {
        currentPower += amount;

        //verificando se chegou ao maximo 
        if (currentPower > 300)
            currentPower = 300;

        if( ModifyPower != null ) ModifyPower(this, EventArgs.Empty);
    }

    public int GetCurrentPower()
    {
        return currentPower;
    }

    public float GetPowerPercent()
    {
        if(currentPower == 300)
            return 100f;

        if(currentPower >= 200)
            return (float) currentPower-200;

        if(currentPower >= 100)
            return (float) currentPower-100;

        return (float) currentPower;
    }

}
