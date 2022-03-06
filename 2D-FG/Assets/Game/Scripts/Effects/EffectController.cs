using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public void DestroyMe()
    {
        Destroy(gameObject, 0);
    }
}
