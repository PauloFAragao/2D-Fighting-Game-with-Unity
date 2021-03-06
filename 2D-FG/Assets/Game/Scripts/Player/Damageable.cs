using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    //referencias
    [SerializeField] private PlayerController pController;    //referencia ao TerryController do proprio personagem
    [SerializeField] private Rigidbody2D rb;                  //referencia do Rigidbody2D
    [SerializeField] private HealthSystem healthSystem;       //referencia ao healthSystem
    [SerializeField] private PowerSystem powerSystem;           //referencia ao powersystem

    public Transform effectSpawn;                //referencia ao ponto de spawn

    public GameObject shieldEffectPrefab;        //referencia ao prefab do efeito do dano na defesa

    //variaveis de indicação
    private bool inCombo;                        //indica que o personagem sofreu dano enquanto estava em hitStun
    private int combo;                           //indica a quantidade de hits que o personagem sofreu no combo
    private int comboDamage;                     //indica a quantidade de dano que o personagem recebeu do combo

    //variaveis de controle
    private bool nextAnimationControl;          //variavel que vai indicar qual a proxima animação deve ser executada - reseta animação

    private void Update()
    {
        //reseta o combo
        if (inCombo && pController.GetAction() < 900)
        {
            inCombo = false;
        }
    }

    //método que vai recever o tipo de ataque sofrido e processar o que deve acontecer
    public void SetDamage(int action, int damageAmount, int stun, int strong)
    {

        //controle do combo sofrido
        if (pController.GetAction() >= 900 && pController.GetAction() != 990 && pController.GetAction() != 1000)//os casos 990 e 1000 devem ser ignorados para evitar bug
        {
            inCombo = true;//sinalisa que o personagem está sofrendo combo
            combo++;//soma o contador de combo

            //implementar mecanica de dano reduzido em combo longo<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

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

        //GATO PARA DAR DANO
        //se está se defendendo
        if (pController.GetAction() == 110 || pController.GetAction() == 111 || pController.GetAction() == 112 ||
           pController.GetAction() == 120 || pController.GetAction() == 121 || pController.GetAction() == 122)
            healthSystem.ShieldDamage(damageAmount);

        //se não está se defendendo
        else
            healthSystem.Damage(damageAmount);

        //stun - se não estiver defendendo
        if (pController.GetAction() != 110 && pController.GetAction() != 111 && pController.GetAction() != 112 &&
            pController.GetAction() != 120 && pController.GetAction() != 121 && pController.GetAction() != 122)
        {
            ProcessStun(stun);
        }

        switch (action)//switch das ações de dano que o personagem vai sofrer
        {
            case 1://golpe baixo 
                   //regras de defesa: defesa em pé: dano / defesa agachado: defende

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

                //enquanto está agachado e não está defendendo
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

                //enquanto está agachado e está defendendo
                else if (pController.GetAction() == 120 || pController.GetAction() == 121 ||
                         pController.GetAction() == 122 /*|| pController.GetAction() == 123*/)
                {
                    pController.SetAction(122);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHitCrouched");
                    else pController.SetAnimation("DTakingHitCrouched 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
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
                   //regras de defesa: defesa em pé: defende / defesa agachado: defende

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

                //enquanto está agachado e está defendendo
                else if (pController.GetAction() == 120 || pController.GetAction() == 121 ||
                         pController.GetAction() == 122 /*|| pController.GetAction() == 123*/)
                {
                    pController.SetAction(122);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHitCrouched");
                    else pController.SetAnimation("DTakingHitCrouched 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
                }

                //enquanto está se defendendo em pé
                else if (pController.GetAction() == 110 || pController.GetAction() == 111 ||
                        pController.GetAction() == 112 /*|| pController.GetAction() == 113*/)
                {
                    pController.SetAction(112);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHit");
                    else pController.SetAnimation("DTakingHit 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
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
                   //regras de defesa: defesa em pé: defende / defesa agachado: defende

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

                //enquanto está agachado e está defendendo
                else if (pController.GetAction() == 120 || pController.GetAction() == 121 ||
                         pController.GetAction() == 122 /*|| pController.GetAction() == 123*/)
                {
                    pController.SetAction(122);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHitCrouched");
                    else pController.SetAnimation("DTakingHitCrouched 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
                }

                //enquanto está se defendendo em pé
                else if (pController.GetAction() == 110 || pController.GetAction() == 111 ||
                        pController.GetAction() == 112 /*|| pController.GetAction() == 113*/)
                {
                    pController.SetAction(112);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHit");
                    else pController.SetAnimation("DTakingHit 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
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
                   //regras de defesa: defesa em pé: defende / defesa agachado: defende

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

                //enquanto está agachado e está defendendo
                else if (pController.GetAction() == 120 || pController.GetAction() == 121 ||
                         pController.GetAction() == 122 /*|| pController.GetAction() == 123*/)
                {
                    pController.SetAction(122);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHitCrouched");
                    else pController.SetAnimation("DTakingHitCrouched 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
                }

                //enquanto está se defendendo em pé
                else if (pController.GetAction() == 110 || pController.GetAction() == 111 ||
                        pController.GetAction() == 112 /*|| pController.GetAction() == 113*/)
                {
                    pController.SetAction(112);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHit");
                    else pController.SetAnimation("DTakingHit 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
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
                   //regras de defesa: defesa em pé: defende / defesa agachado: dano

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
                        pController.GetAction() == 371 || pController.GetAction() == 970 ||
                        //casos de se defendendo agachado
                        pController.GetAction() == 120 || pController.GetAction() == 121 || pController.GetAction() == 122 /*|| pController.GetAction() == 123*/ )
                {
                    pController.SetAction(970);

                    if (nextAnimationControl) pController.SetAnimation("Taking Damage Crouched");
                    else pController.SetAnimation("Taking Damage Crouched 2");

                    nextAnimationControl = !nextAnimationControl;
                }

                //enquanto está se defendendo em pé
                else if (pController.GetAction() == 110 || pController.GetAction() == 111 ||
                        pController.GetAction() == 112 /*|| pController.GetAction() == 113*/)
                {
                    pController.SetAction(112);

                    if (nextAnimationControl) pController.SetAnimation("DTakingHit");
                    else pController.SetAnimation("DTakingHit 2");

                    nextAnimationControl = !nextAnimationControl;

                    //instanciando o efeito de defesa
                    InstantiateDefenceEffect();
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
                   //regras de defesa: sem defesa

                pController.SetAction(950);

                pController.SetAnimation("Knock Down Rising");

                //verificando se o personagem está pulando para adicionar a força correta
                if (!pController.GetGrounded()) rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                else rb.AddForce(new Vector2(0, 1850), ForceMode2D.Force);

                break;

            case 7://golpeado e jogado longe
                   //regras de defesa: sem defesa

                pController.SetAction(960);

                pController.SetAnimation("Hard Knock Down Rising");

                //verificando se o personagem está pulando para adicionar a força correta
                if (!pController.GetGrounded()) rb.AddForce(new Vector2(0, 1000), ForceMode2D.Force);

                else rb.AddForce(new Vector2(0, 2050), ForceMode2D.Force);

                break;

            case 8://jogado longe
                   //regras de defesa: sem defesa

                pController.SetAction(991);
                pController.SetAnimation("Thrown Forward Rising");

                rb.AddForce(new Vector2(0, 1500), ForceMode2D.Force);

                break;

            case 9://primeiro frame de Hard Knock Down
                   //regras de defesa: sem defesa

                pController.SetAction(962);
                pController.SetAnimation("Hard Knock Down Static");

                rb.velocity = Vector2.zero;

                break;
        }

        //mudando o estado de acatando para falso
        pController.SetAttackingFalse();

        //chamando o método que vai empurrar o personagem
        PushEffect(strong);

        //adicionando a quantidade de pontos de especial
        powerSystem.SetPower(15);

    }

    //método que vai ser chamado para empurrar o personagem
    private void PushEffect(int strong)
    {
        if (pController.GetFacingRight())
        {
            rb.AddForce(new Vector2(strong * -1, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(strong, 0), ForceMode2D.Impulse);
        }
    }

    private void InstantiateDefenceEffect()
    {
        Instantiate(shieldEffectPrefab, effectSpawn.position, effectSpawn.rotation);
    }

    private void ProcessStun(int amount)
    {
        healthSystem.Stun(amount);

        if (healthSystem.GetStunAmount() == 0)
        {
            //comando para o personagem executar a ação de stun
            pController.SetStumCommand(true);

            //comando para resetar a quantidade de pontos de stun
            healthSystem.SetStunFull();
        }
    }

}
