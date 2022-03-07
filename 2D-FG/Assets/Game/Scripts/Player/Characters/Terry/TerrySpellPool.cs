using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrySpellPool : MonoBehaviour
{
//referencias
    [SerializeField] private GameObject powerGeyserPrefab;  //prefab do power geyser
    [SerializeField] private GameObject powerWavePrefab;    //prefab do power wave
    [SerializeField] private GameObject roundWavePrefab;    //prefab do round wave

    private void Start() 
    {
        //instanciando o power geyser    indice 0
        GameObject powerGeyserEffect = Instantiate(powerGeyserPrefab, transform);
        powerGeyserEffect.SetActive(false);

        //instanciando o power wave      indice 1
        GameObject powerWaveEffect = Instantiate(powerWavePrefab, transform);
        powerWaveEffect.SetActive(false);

        //instanciando o round wave      indice 2
        GameObject roundWaveEffect = Instantiate(roundWavePrefab, transform);
        roundWaveEffect.SetActive(false);

    }

    //método que ativa uma spell
    public GameObject GetSpell(int index, Vector2 position)
    {
        //ativando o objeto
        transform.GetChild(index).gameObject.SetActive(true);

        //colocando o objeto na posição
        transform.GetChild(index).gameObject.transform.position = new Vector3(position.x, position.y, 0);

        //pegando o filho desse objeto na posição de index
        return transform.GetChild(index).gameObject;
    }

    //método que retorna se tem alguma spell ativa
    public bool VerifyActiveSpell()
    {
        //verificando se algum filho está ativo
        for( int i = 0; i<3; i++ )
        {
            if( transform.GetChild(i).gameObject.activeInHierarchy )
            {
                return true;
            }
        }
        return false;
    }

    //método que retorna se a spell específica está ativa
    public bool VerifySpecificSpell(int index)
    {
        //verificando se o objeto especifico está ativo
        if( transform.GetChild(index).gameObject.activeInHierarchy )
        {
            return true;
        }

        return false;
    }

}
