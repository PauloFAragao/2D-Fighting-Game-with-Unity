using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    //referencias
    [SerializeField] private PlayerController p1Controler;
    [SerializeField] private Transform p1Trasnform;

    [SerializeField] private PlayerController p2Controler;
    [SerializeField] private Transform p2Trasnform;

    [SerializeField] private SpriteRenderer p1SpriteRenderer;
    [SerializeField] private SpriteRenderer p2SpriteRenderer;

    //variaveis de controle
    private bool p1LastHit = false;

    private void Start()
    {
        //setando para o jogo rodar a 60 fps
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        //controle do comando de virar para o outro lado 
        ChangeSideController();
    }

    private void OrderLayerController()
    {
        if(p1LastHit)//p1 foi o ultimo a acertar o oponente
        {
            p1SpriteRenderer.sortingOrder = 5;
            p2SpriteRenderer.sortingOrder = 4;
        }
        else
        {
            p1SpriteRenderer.sortingOrder = 4;
            p2SpriteRenderer.sortingOrder = 5;
        }
    }

    private void ChangeSideController()
    {
        //verifica se o player 1 precisa virar para o outro lado
        if (p1Controler.GetFacingRight() && //esta virado para a direita
            p1Trasnform.position.x > p2Trasnform.position.x && //está a direita do p2
            p1Controler.GetAction() != 150 && p1Controler.GetAction() != 151 && //a ação não é mudando de lado
            p1Controler.GetAction() != 160 && p1Controler.GetAction() != 161)
        {
            p1Controler.SetCommandToChangeSide();//gerando comando para mudar de lado
        }
        if (!p1Controler.GetFacingRight() && //esta virado para a direita
             p1Trasnform.position.x < p2Trasnform.position.x && //está a esquerda do p2
             p1Controler.GetAction() != 150 && p1Controler.GetAction() != 151 && //a ação não é mudando de lado
             p1Controler.GetAction() != 160 && p1Controler.GetAction() != 161)
        {
            p1Controler.SetCommandToChangeSide();//gerando comando para mudar de lado
        }

        //verifica se o player 2 precisa virar para o outro lado
        if (p2Controler.GetFacingRight() && //esta virado para a direita
            p2Trasnform.position.x > p1Trasnform.position.x && //está a direita do p2
            p2Controler.GetAction() != 150 && p2Controler.GetAction() != 151 && //a ação não é mudando de lado
            p2Controler.GetAction() != 160 && p2Controler.GetAction() != 161)
        {
            p2Controler.SetCommandToChangeSide();//gerando comando para mudar de lado
        }
        if (!p2Controler.GetFacingRight() && //esta virado para a direita
             p2Trasnform.position.x < p1Trasnform.position.x && //está a esquerda do p2
             p2Controler.GetAction() != 150 && p2Controler.GetAction() != 151 && //a ação não é mudando de lado
             p2Controler.GetAction() != 160 && p2Controler.GetAction() != 161)
        {
            p2Controler.SetCommandToChangeSide();//gerando comando para mudar de lado
        }
    }
    
    //metodo que vai alterar a ordem das cadas
    public void SetLastHit(bool value)
    {
        p1LastHit = value;
        
        //controle de qual personagem vai ser renderisado na frente
        OrderLayerController();
    }
    
}
