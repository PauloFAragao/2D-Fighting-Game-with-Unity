using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    //referencias
    [SerializeField]private AttackControl attackControl;
    
    //metodo que detecta o hit trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackControl.OnHit(collision);
    }
}
