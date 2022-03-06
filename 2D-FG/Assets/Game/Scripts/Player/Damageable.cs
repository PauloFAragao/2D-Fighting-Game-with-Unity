using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    //referencias
    [SerializeField] private PlayerController pController;    //referencia ao TerryController do proprio personagem
    [SerializeField] private Rigidbody2D rb;                  //referencia do Rigidbody2D

    //variaveis de indicação
    [SerializeField] private int maxLifePoints; //quantidade inicial de pontos de vida do personagem
    private int maxDefencePoints;               //quantidade inicial de pontos de escudo do personagem

    private bool inCombo;                        //indica que o personagem sofreu dano enquanto estava em hitStun
    private int combo;                           //indica a quantidade de hits que o personagem sofreu no combo
    private int comboDamage;                     //indica a quantidade de dano que o personagem recebeu do combo

    //variaveis de controle

    private bool nextAnimationControl;          //variavel que vai indicar qual a proxima animação deve ser executada - reseta animação

    //variaveis de estado
    private int currentLifePoints;              //quantidade de pontos de vida atual do personagem
    private int currentDefencePoints;           //quantidade de pontos de escudo atual do personagem
    private int stunPoints;                     //quantidade de pontos de stun atual do personagem

    private void Start()
    {
        currentLifePoints = maxLifePoints;
        currentDefencePoints = maxDefencePoints;
        stunPoints = 100;
    }

    private void Update()
    {
        //reseta o combo
        if(inCombo && pController.GetAction() < 900)
        {
            inCombo = false;
        }
    }

    //método que vai recever o tipo de ataque sofrido e processar o que deve acontecer
    public void SetDamage(int action, int damageAmount, int stun, int strong)
    {
        //verificar se o ataque teve efeito 
            //-caso 1: o personagem está se defendendo e não sofreu dano
            //subitrair da defesa atual o dano causado
            //mudar a ação para onhit na defesa - block stun

        //-caso 2: o personagem não está se defendendo e sofreu dano
            //subitrair da vida atual o dano causado
            //mudar para a ação de sofrer dano correspondente - hit stun
            //subtrair do contador de stun atual os pontos de stun causados

        //-em todos os casos
        //adicionar ao contador de pontos de especial (x pontos ao sofrer dano e y pontos ao defender)


        //controle do combo sofrido
        if (pController.GetAction() >= 900 && pController.GetAction() != 990 && pController.GetAction() != 1000)//os casos 990 e 1000 devem ser ignorados para evitar bug
        {
            inCombo = true;//sinalisa que o personagem está sofrendo combo
            combo++;//soma o contador de combo

            //implementar mecanica de dano reduzido em combo longo

            comboDamage += damageAmount;//soma o dano no contador de combo

            //chamar função Damage(int damageAmount) passando dano reduzido pelo combo
        }
        else if (pController.GetAction() < 900)
        {
            combo = 1;//muda o contador de combo para 1
            comboDamage = damageAmount;//inicialisa o contador de combo
        }

        //GATO PARA MOSTRAR O COMBO
        if (inCombo)
        {
            Debug.Log("Contador de combo: " + combo + " Dano do combo: " + comboDamage);
        }
        
        switch (action)//switch das ações de dano que o personagem vai sofrer
        {
            case 1://golpe baixo
                
                //enquanto está pulando
                if (!pController.GetGrounded())
                {
                    pController.SetAction(980);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Jumping - Rising");
                    else pController.SetAnimation("Taking Damage Jumping - Rising 2");

                    rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                    strong /= 2;//mudando a força do empurrão

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está agachado
                else if (pController.GetAction() == 10 || pController.GetAction() == 11 || pController.GetAction() == 340 ||
                        pController.GetAction() == 341 || pController.GetAction() == 350 || pController.GetAction() == 351 ||
                        pController.GetAction() == 360 || pController.GetAction() == 361 || pController.GetAction() == 370 ||
                        pController.GetAction() == 371 || pController.GetAction() == 970)
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //em pé
                else
                {
                    pController.SetAction(900);

                    if (nextAnimationControl) pController.SetAnimation("Taking Low Attack Damage");
                    else pController.SetAnimation("Taking Low Attack Damage 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                break;

            case 2://golpe médio
                
                //enquanto está pulando
                if (!pController.GetGrounded())
                {
                    pController.SetAction(980);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Jumping - Rising");
                    else pController.SetAnimation("Taking Damage Jumping - Rising 2");

                    rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                    strong /= 2;//mudando a força do empurrão

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está agachado
                else if(pController.GetAction() == 10 || pController.GetAction() == 11 || pController.GetAction() == 340 ||
                        pController.GetAction() == 341 || pController.GetAction() == 350 || pController.GetAction() == 351 ||
                        pController.GetAction() == 360 || pController.GetAction() == 361 || pController.GetAction() == 370 ||
                        pController.GetAction() == 371 || pController.GetAction() == 970)
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //em pé
                else
                {
                    pController.SetAction(910);

                    if (nextAnimationControl) pController.SetAnimation("Taking Medium Attack Damage");
                    else pController.SetAnimation("Taking Medium Attack Damage 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                break;

            case 3://golpe alto

                //enquanto está pulando
                if (!pController.GetGrounded())
                {
                    pController.SetAction(980);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Jumping - Rising");
                    else pController.SetAnimation("Taking Damage Jumping - Rising 2");

                    rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                    strong /= 2;//mudando a força do empurrão

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está agachado
                else if (pController.GetAction() == 10 || pController.GetAction() == 11 || pController.GetAction() == 340 ||
                        pController.GetAction() == 341 || pController.GetAction() == 350 || pController.GetAction() == 351 ||
                        pController.GetAction() == 360 || pController.GetAction() == 361 || pController.GetAction() == 370 ||
                        pController.GetAction() == 371 || pController.GetAction() == 970)
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //em pé
                else
                {
                    pController.SetAction(920);

                    if (nextAnimationControl) pController.SetAnimation("Taking High Attack Damage");
                    else pController.SetAnimation("Taking High Attack Damage 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                break;

            case 4://Golpe do queixo

                //enquanto está pulando
                if (!pController.GetGrounded())
                {
                    pController.SetAction(980);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Jumping - Rising");
                    else pController.SetAnimation("Taking Damage Jumping - Rising 2");

                    rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                    strong /= 2;//mudando a força do empurrão

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está agachado
                else if (pController.GetAction() == 10 || pController.GetAction() == 11 || pController.GetAction() == 340 ||
                        pController.GetAction() == 341 || pController.GetAction() == 350 || pController.GetAction() == 351 ||
                        pController.GetAction() == 360 || pController.GetAction() == 361 || pController.GetAction() == 370 ||
                        pController.GetAction() == 371 || pController.GetAction() == 970)
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //em pé
                else
                {
                    pController.SetAction(930);

                    if (nextAnimationControl) pController.SetAnimation("Get Hit in the Chin");
                    else pController.SetAnimation("Get Hit in the Chin");

                    nextAnimationControl = !nextAnimationControl;
                }
                break;

            case 5://Golpeado por cima

                //enquanto está pulando
                if (!pController.GetGrounded())
                {
                    pController.SetAction(980);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Jumping - Rising");
                    else pController.SetAnimation("Taking Damage Jumping - Rising 2");

                    rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                    strong /= 2;//mudando a força do empurrão

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está agachado
                else if (pController.GetAction() == 10 || pController.GetAction() == 11 || pController.GetAction() == 340 ||
                        pController.GetAction() == 341 || pController.GetAction() == 350 || pController.GetAction() == 351 ||
                        pController.GetAction() == 360 || pController.GetAction() == 361 || pController.GetAction() == 370 ||
                        pController.GetAction() == 371 || pController.GetAction() == 970)
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //em pé
                else
                {
                    pController.SetAction(940);

                    if (nextAnimationControl) pController.SetAnimation("Get Hit From Above");
                    else pController.SetAnimation("Get Hit From Above");

                    nextAnimationControl = !nextAnimationControl;
                }

                break;

            case 6://golpeado e caindo

                pController.SetAction(950);

                pController.SetAnimation("Knock Down Rising");

                //verificando se o personagem está pulando para adicionar a força correta
                if(!pController.GetGrounded()) rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                else rb.AddForce(new Vector2(0, 1850), ForceMode2D.Force);
                
                break;

            case 7://golpeado e jogado longe

                pController.SetAction(960);

                pController.SetAnimation("Hard Knock Down Rising");

                //verificando se o personagem está pulando para adicionar a força correta
                if (!pController.GetGrounded()) rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                else rb.AddForce(new Vector2(0, 2050), ForceMode2D.Force);
                
                break;

            case 8:

                pController.SetAction(991);
                pController.SetAnimation("Thrown Forward Rising");

                rb.AddForce(new Vector2(0, 1500), ForceMode2D.Force);

                break;

            case 9://primeiro frame de Hard Knock Down
                
                pController.SetAction(962);
                pController.SetAnimation("Hard Knock Down Static");

                rb.velocity = Vector2.zero;

                break;
        }

        //mudando o estado de acatando para falso
        pController.SetAttackingFalse();
        
        //chamando o método que vai empurrar o personagem
        PushEffect(strong);

        //chamando o método que vai calcular o stun
        StunEffect(stun);
        
    }

    //método que vai ser chamado para empurrar o personagem
    private void PushEffect(int strong)
    {
        if(pController.GetFacingRight())
        {
            rb.AddForce(new Vector2(strong * -1, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(strong, 0), ForceMode2D.Impulse);
        }
    }

    //método que vai ser chamado para causar dano ao player
    private void Damage(int damageAmount)
    {
        if (currentLifePoints - damageAmount > 0)
        {
            currentLifePoints -= damageAmount;
        }
        else currentLifePoints = 0;
    }

    //método que vai ser chamado para causar dano a defesa do player
    private void DamageOnDefence(int damageAmount)
    {
        if (currentDefencePoints - damageAmount > 0)
        {
            currentDefencePoints -= damageAmount;
        }
        else
        {
            currentDefencePoints = 0;

            //chamar a ação de quebrar defesa

        }
    }

    //método que vai ser chamado para gerar stun no player
    private void StunEffect(int stun)
    {
        if (stunPoints - stun > 0)
        {
            stunPoints -= stun;
        }
        else
        {
            stunPoints = 0;
            pController.SetStumCommand(true);
        }
    }

    //método que vai regenerar os pontos de defesa do player
    private void DefenceRegeneration()
    {

    }

    //método que vai regenerar os pontos de stun do player
    private void StunRegeneration()
    {

    }

}
