using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCharacter : MonoBehaviour
{
    //variaveis de controle
    private bool avoidAttackLoop;       //variavel para evitar que um ataque fique fazendo loop por um botão estar pressionado

    //variaveis de indicação
    private float attackCommandTime;   //guarda o tempo em que foi feito o comando de ataque
    
    
    //controle dos botões de ação
    public virtual void ActionButtons() {}

    //metodo que vai verificar qual ação deve ser processada
    public virtual void ProcessActions() {}
    
    //////////////////// VERIFICADORES ////////////////////
    

    public virtual bool CheckIfCanSwitchTo440(int action, bool canCancelAction)// <<<<<<<<<< Essemetodo precisa ser re-escrito na classe que herda
    {  return true; }
    
    public virtual bool CheckIfCanSwitchTo450(int action, bool canCancelAction)// <<<<<<<<<< Essemetodo precisa ser re-escrito na classe que herda
    { return false; }

    //////////////////// Gets & Sets ////////////////////
    
    public void SetAvoidAttackLoop(bool value)
    {
        avoidAttackLoop = value;
    }
    public bool GetAvoidAttackLoop()
    {
        return avoidAttackLoop;
    }

    public void SetAttackCommandTime(float value)
    {
        attackCommandTime = value;
    }
    public float GetAttackCommandTime()
    {
        return attackCommandTime;
    }

}
