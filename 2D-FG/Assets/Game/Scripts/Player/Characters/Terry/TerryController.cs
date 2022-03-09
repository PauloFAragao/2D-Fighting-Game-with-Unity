using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerryController : GenericCharacter
{
//referencias
    private PlayerController terry;                                 //referencia a classe PlayerController do personagem
    private InputController inputController;                        //referencia ao InputController do personagem
    private AttackControl attackControl;                            //referencia ao AttackControl do personagem
    private Rigidbody2D rb;                                         //referencia ao Rigidbody2D do personagem

    private PlayerController opponent;                              //referencia a classe PlayerController do oponente
    private Damageable oDamageable;                                 //referencia a classe Damageable do oponente
    public Transform EffectSpawner;                                 //referencia para o transform que vai ser usado como ponto de spawn do prefab
    
    //prefabs
    [SerializeField] private TerrySpellPool terrySpellPool;         //referencia para o poll de spells

    public GameObject EspecialEffectPrefab;                         //prefab do efeito de especial

    //variaveis de controle
    [SerializeField] private float roundWaveVelocity;               //velocidade de movimento durante o round wave

    [SerializeField] private float weakBurningKnuckleVelocity;      //velocidade de movimento durante o Burning Knuckle com soco fraco
    [SerializeField] private float strongBurningKnuckleVelocity;    //velocidade de movimento durante o Burning Knuckle com soco forte

    [SerializeField] private float weakCrackShootVelocity;          //velocidade de movimento durante o crack shoot fraco
    [SerializeField] private float strongCrackShootVelocity;        //velocidade de movimento durante o crack shoot fraco
    [SerializeField] private float weakCrackShootImpulse;           //impulso do crack Shoot fraco
    [SerializeField] private float strongCrackShootImpulse;         //impulso do crack Shoot forte

    [SerializeField] private float powerDunkVelocity;               //velocidade de movimento durante o power Dunk
    [SerializeField] private float weakPowerDunkImpulse;            //impulso do power dunk fraco
    [SerializeField] private float strongPowerDunkImpulse;          //impulso do power dunk forte

    [SerializeField] private float risingTackleHorizontalVelocity;  //velocidade de movimendo na horizontal durante o rising tackle
    [SerializeField] private float weakRisingTackleVerticalVelocity;//velocidade de movimento na vertical durante o rising tackle fraco
    [SerializeField] private float strongRisingTackleVerticalVelocity;//velocidade de movimento na vertical durante o rising tackle forte

    [SerializeField] private float busterWolfVelocity;              //velocidade de movimento durante o buster wolf

    private void Awake()
    {
        terry = GetComponent<PlayerController>();

        inputController = GetComponent<InputController>();
        attackControl = GetComponentInChildren<AttackControl>();
        rb = GetComponent<Rigidbody2D>();

        if (terry.GetPlayer1())
        {
            opponent = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
            oDamageable = GameObject.FindWithTag("Player2").GetComponentInChildren<Damageable>();
        }
        else
        {
            opponent = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
            oDamageable = GameObject.FindWithTag("Player1").GetComponentInChildren<Damageable>();
        }

    }

    //controle dos botões de ação
    public override void ActionButtons()
    {
        //verificar se precisa mudar para alguma ação do botão A
        if (ProcessAButton()) return;

        //verificar se precisa mudar para alguma ação do botão B
        if (ProcessBButton()) return;

        //verificar se precisa mudar para alguma ação do botão C
        if (ProcessCButton()) return;

        //verificar se precisa mudar para alguma ação do botão D
        if (ProcessDButton()) return;

    }

    public override void ProcessActions()
    {
        switch (terry.GetAction())
        {
            case 48://Falling For Recover
                ProcessFallingForRecover();
                break;

            case 300://Weak Punch Preparation
                ProcessWeakPunchPreparation();
                break;

            case 310://Strong Punch Preparation
                ProcessStrongPunchPreparation();
                break;

            case 320://Weak Kick Preparation
                ProcessWeakKickPreparation();
                break;

            case 330://Weak Kick Preparation
                ProcessStrongKickPreparation();
                break;

            case 331://Weak Kick 
                ProcessStrongKick();
                break;

            case 340://Crouched Weak Punch Preparation
                ProcessCrouchedWeakPunchPreparation();
                break;

            case 350://Crouched Strong Punch Preparation
                ProcessCrouchedStrongPunchPreparation();
                break;

            case 360://Crouched Weak kick Preparation
                ProcessCrouchedWeakKickPreparation();
                break;

            case 370://Crouched Strong kick Preparation
                ProcessCrouchedStrongKickPreparation();
                break;

            case 380://Jumping Weak Punch Preparation
                ProcessJumpingWeakPunchPreparation();
                break;

            case 381://Jumping Weak Punch
                ProcessJumpingWeakPunch();
                break;

            case 390://Jumping Strong Punch Preparation
                ProcessJumpingStrongPunchPreparation();
                break;

            case 391://Jumping Weak Punch
                ProcessJumpingStrongPunch();
                break;

            case 400://Jumping Weak Kick Preparation
                ProcessJumpingWeakKickPreparation();
                break;

            case 401://Jumping Weak Kick
                ProcessJumpingWeakKick();
                break;

            case 410://Jumping Strong Kick Preparation
                ProcessJumpingStrongKickPreparation();
                break;

            case 411://Jumping Strong Kick
                ProcessJumpingStrongKick();
                break;

            case 382://Jumping Weak Punch End
            case 392://Jumping Strong Punch End
            case 402://Jumping Weak Kick End
            case 412://Jumping Strong Kick End
            case 451://Jumping Special Attack End
                ProcessJumpingAttackEnd();
                break;

            case 420://Close Strong Punch Preparation
                ProcessCloseStrongPunchPreparation();
                break;

            case 430://Close Strong Kick Preparation
                ProcessCloseStrongKickPreparation();
                break;

            case 441://Special Attack
                ProcessSpecialAttack();
                break;

            case 450://Jumping Special Attack
                ProcessJumpingSpecialAttack();
                break;

            case 600://Weak Punch Forward Preparation
                ProcessWeakPunchForwardPreparation();
                break;

            case 610://Rising Upper
                ProcessRisingUpperPreparation();
                break;

            case 631://Round Wave Moving
                ProcessRoundWaveMoving();
                break;

            case 632://Round Wave End
                ProcessRoundWaveEnd();
                break;

            case 641://Weak Crack Shoot
            case 651://Strong Crack Shoot
                ProcessCrackShoot();
                break;

            case 661://Weak Power Dunk Start
            case 671://Strong Power Dunk Start
                ProcessPowerDunkJumping();
                break;

            case 662://Weak Power Dunk falling
            case 672://Strong Power Dunk falling
                ProcessPowerDunkFalling();
                break;

            case 681://Weak Burning Knuckle
                ProcessWeakBurningKnuckle();
                break;

            case 691://Strong Burning Knuckle
                ProcessStrongBurningKnuckle();
                break;

            case 701://Weak Rising Tackle
            case 711://Strong Rising Tackle
                ProcessRisingTackle();
                break;

            case 731://Buster Wolf
                ProcessBusterWolf();
                break;

        }
    }

    //////////////////// Processadores ////////////////////

    //método que vai processar a ação falling - 48
    private void ProcessFallingForRecover()
    {
        if (terry.GetGrounded())
        {
            terry.SetAction(48);
            terry.SetAnimation("Landing For Recover");
        }
    }

    //método que vai processar weak punch preparation - 300
    private void ProcessWeakPunchPreparation()
    {
        //verificando o comando de rolamento
        if (terry.Rolling()) return;

        //verificando se o comando para o soco fraco já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(301);
            terry.SetAnimation("Weak Punch");
        }
    }

    //método que vai processar Strong punch preparation - 310
    private void ProcessStrongPunchPreparation()
    {
        //verificando o comando de ataque especial comum
        if (terry.SpecialCommon()) return;

        //verificar o comando de pow
        if (terry.ActivePow()) return;

        //verificando se o comando para o soco forte já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(311);
            terry.SetAnimation("Strong Punch");
        }
    }

    //método que vai processar Weak Kick preparation - 320
    private void ProcessWeakKickPreparation()
    {
        //verificando o comando de ataque especial comum
        if (terry.SpecialCommon()) return;

        //verificar o comando de pow
        if (terry.ActivePow()) return;

        //verificando se o comando para o chute fraco já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(321);
            terry.SetAnimation("Weak kick");
        }
    }

    //método que vai processar Strong Kick preparation - 330
    private void ProcessStrongKickPreparation()
    {
        //verificando o comando de ataque especial comum
        if (terry.SpecialCommon()) return;

        //verificando se o comando para o chute forte já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(331);
            terry.SetAnimation("Strong Kick");
        }
    }

    //método que vai processar Strong Kick - 331
    private void ProcessStrongKick()
    {
        if (terry.GetFacingRight())
            rb.velocity = new Vector2(5, 0);//mudando a velocidade do personagem

        else
            rb.velocity = new Vector2(-5, 0);//mudando a velocidade do personagem
    }

    //método que vai processar Crouched Weak Punch preparation - 340
    private void ProcessCrouchedWeakPunchPreparation()
    {
        //verificando se o comando para o soco fraco agachado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(341);
            terry.SetAnimation("Crouched Weak Punch");
        }
    }

    //método que vai processar Crouched Strong Punch Preparation - 350
    private void ProcessCrouchedStrongPunchPreparation()
    {
        //verificando se o comando para o soco forte agachado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(351);
            terry.SetAnimation("Crouched Strong Punch");
        }
    }

    //método que vai processar Crouched Weak Kick Preparation - 360
    private void ProcessCrouchedWeakKickPreparation()
    {
        //verificando se o comando para o chute fraco agachado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(361);
            terry.SetAnimation("Crouched Weak Kick");
        }
    }

    //método que vai processar Crouched Strong Kick Preparation - 370
    private void ProcessCrouchedStrongKickPreparation()
    {
        //verificando se o comando para o chute forte agachado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(371);
            terry.SetAnimation("Crouched Strong Kick");
        }
    }

    //método que vai processar Jumping Weak Punch Preparation- 380
    private void ProcessJumpingWeakPunchPreparation()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }

        //verificando se o comando para o chute forte agachado já pode ser executado
        if (!terry.GetGrounded() && Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetPlayerLayer(7);  //resetando a layer do personagem

            terry.SetAction(381);
            terry.SetAnimation("Jumping Weak Punch");
        }
    }

    //método que vai processar Jumping Weak Punch - 381
    private void ProcessJumpingWeakPunch()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
        }
    }

    //método que vai processar Jumping Strong Punch Preparation - 390
    private void ProcessJumpingStrongPunchPreparation()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }

        //verificando se o comando para o chute forte agachado já pode ser executado
        if (!terry.GetGrounded() && Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetPlayerLayer(7);  //resetando a layer do personagem

            terry.SetAction(391);
            terry.SetAnimation("Jumping Strong Punch");
        }
    }

    //método que vai processar Jumping Strong Punch - 391
    private void ProcessJumpingStrongPunch()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
        }
    }

    //método que vai processar Jumping Weak Kick Preparation - 400
    private void ProcessJumpingWeakKickPreparation()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }

        //verificando se o comando para o chute forte agachado já pode ser executado
        if (!terry.GetGrounded() && Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetPlayerLayer(7);  //resetando a layer do personagem

            terry.SetAction(401);
            terry.SetAnimation("Jumping Weak Kick");
        }
    }

    //método que vai processar Jumping Weak Kick - 401
    private void ProcessJumpingWeakKick()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
        }
    }

    //método que vai processar Jumping Strong Kick Preparation - 410
    private void ProcessJumpingStrongKickPreparation()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }

        //verificando se o comando para o chute forte agachado já pode ser executado
        if (!terry.GetGrounded() && Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetPlayerLayer(7);  //resetando a layer do personagem

            terry.SetAction(411);
            terry.SetAnimation("Jumping Strong Kick");
        }
    }

    //método que vai processar Jumping Strong Kick - 411
    private void ProcessJumpingStrongKick()
    {
        if (terry.GetGrounded())
        
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
        }
    }

    //método que vai processar Jumping Attack End - 382/392/402/412/451
    private void ProcessJumpingAttackEnd()
    {
        if (rb.velocity.y > 2)//Rising After Attaque
        {
            terry.SetAction(45);
            terry.SetAnimation("Rising");
            return;
        }

        if (rb.velocity.y <= 2)//End of Climp After Attaque
        {
            terry.SetAction(46);
            terry.SetAnimation("End of Climp");
            return;
        }

        if (rb.velocity.y < -1)//Falling After Attaque
        {
            terry.SetAction(47);
            terry.SetAnimation("Falling");
            return;
        }

        if (terry.GetGrounded())//Landing
        {
            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }
    }

    //método que vai processar close Strong Punch Preparation- 420
    private void ProcessCloseStrongPunchPreparation()
    {
        //verificando se o comando para o soco forte colado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(421);
            terry.SetAnimation("Close Strong Punch");
        }
    }

    //método que vai processar close Kick Punch Preparation- 430
    private void ProcessCloseStrongKickPreparation()
    {
        //verificando se o comando para o chute forte colado já pode ser executado
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(431);
            terry.SetAnimation("Close Strong Kick");
        }
    }

    //método que vai processar Special Attack - 441
    private void ProcessSpecialAttack()
    {
        if (terry.GetFacingRight()) rb.velocity = new Vector2(terry.GetRunSpeed(), 0);
        else rb.velocity = new Vector2(terry.GetRunSpeed() * -1, 0);
    }

    //método que vai processar Jumping Special Attack - 450
    private void ProcessJumpingSpecialAttack()
    {
        if (terry.GetGrounded() && rb.velocity.y < 0)
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(44);
            terry.SetAnimation("Landing");
            return;
        }
    }

    //método que vai processar Weak Punch Forward Preparation - 600
    private void ProcessWeakPunchForwardPreparation()
    {
        if (Time.time - GetAttackCommandTime() > .1f)
        {
            terry.SetAction(601);
            terry.SetAnimation("Weak Punch Forward");
        }
    }

    //método que vai processar Weak Punch Forward Preparation - 610
    private void ProcessRisingUpperPreparation()
    {
        if (Time.time - GetAttackCommandTime() > .09f)
        {
            terry.SetAction(611);
            terry.SetAnimation("Rising Upper");
        }
    }

    //método que vai processar o Round Wave Moving - 631
    private void ProcessRoundWaveMoving()
    {
        rb.velocity = new Vector2(roundWaveVelocity, 0);
    }

    //método que vai processar o Round Wave End - 632
    private void ProcessRoundWaveEnd()
    {
        rb.velocity = Vector2.zero;
    }

    //método que vai processar CraCk Shoot Start - 640/650
    public void ProcessCraCkShootStart()
    {
        if (terry.GetAction() == 640)
        {
            if (terry.GetFacingRight())
                rb.AddForce(new Vector2(weakCrackShootVelocity, weakCrackShootImpulse), ForceMode2D.Impulse);

            else
                rb.AddForce(new Vector2(weakCrackShootVelocity * -1, weakCrackShootImpulse), ForceMode2D.Impulse);

            terry.SetAction(641);
            terry.SetAnimation("Crack Shoot");
        }
        else if (terry.GetAction() == 650)
        {
            if (terry.GetFacingRight())
                rb.AddForce(new Vector2(strongCrackShootVelocity, strongCrackShootImpulse), ForceMode2D.Impulse);

            else
                rb.AddForce(new Vector2(strongCrackShootVelocity * -1, strongCrackShootImpulse), ForceMode2D.Impulse);

            terry.SetAction(651);
            terry.SetAnimation("Crack Shoot");
        }
        AjustPosition(2f);
    }

    //método que vai processar Crack Shoot - 641/651
    private void ProcessCrackShoot()
    {
        if (rb.velocity.y <= 0)
        {
            if (terry.GetGrounded())
            {
                //o personagem não está mais atacando
                attackControl.SetAttacking(false);

                terry.SetAction(49);
                terry.SetAnimation("Landing For Recover");
            }
        }
    }

    //método que vai processar o power dunk start - 660/670
    public void ProcessPowerDunkStart()
    {
        if (terry.GetAction() == 660)
        {
            if (terry.GetFacingRight())
                rb.AddForce(new Vector2(powerDunkVelocity, weakPowerDunkImpulse), ForceMode2D.Impulse);

            else
                rb.AddForce(new Vector2(powerDunkVelocity * -1, weakPowerDunkImpulse), ForceMode2D.Impulse);

            terry.SetAction(661);
            terry.SetAnimation("Power Dunk Jumping");
        }
        else if (terry.GetAction() == 670)
        {
            if (terry.GetFacingRight())
                rb.AddForce(new Vector2(powerDunkVelocity, strongPowerDunkImpulse), ForceMode2D.Impulse);

            else
                rb.AddForce(new Vector2(powerDunkVelocity * -1, strongPowerDunkImpulse), ForceMode2D.Impulse);

            terry.SetAction(671);
            terry.SetAnimation("Power Dunk Jumping");
        }
    }

    //método que vai processar o power dunk Jumping - 661/671
    private void ProcessPowerDunkJumping()
    {
        if (rb.velocity.y < 2)
        {
            if (terry.GetAction() == 661)
                terry.SetAction(662);

            else if (terry.GetAction() == 671)
                terry.SetAction(672);

            terry.SetAnimation("Power Dunk Falling");
        }
    }

    //método que vai processar o power dunk falling - 662/672
    private void ProcessPowerDunkFalling()
    {
        if (terry.GetGrounded())
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(49);
            terry.SetAnimation("Landing For Recover");
        }
    }

    //método que vai processar o Weak Burning Knuckle - 681
    private void ProcessWeakBurningKnuckle()
    {
        if(terry.GetFacingRight())
            rb.velocity = new Vector2(weakBurningKnuckleVelocity, 0);

        else
            rb.velocity = new Vector2(weakBurningKnuckleVelocity * -1, 0);
    }

    //método que vai processar o Strong Burning Knuckle - 691
    private void ProcessStrongBurningKnuckle()
    {
        if (terry.GetFacingRight())
            rb.velocity = new Vector2(strongBurningKnuckleVelocity, 0);

        else
            rb.velocity = new Vector2(strongBurningKnuckleVelocity * -1, 0);
    }

    //método que vai processar Rising Tackle Start - 700/710
    public void RisingTackleStart()
    {
        if (terry.GetFacingRight())
            rb.AddForce(new Vector2(risingTackleHorizontalVelocity, 0), ForceMode2D.Impulse);

        else
            rb.AddForce(new Vector2(risingTackleHorizontalVelocity*-1, 0), ForceMode2D.Impulse);

        if (terry.GetAction() == 700)
        {
            terry.SetAction(701);
            terry.SetAnimation("Rising Tackle Weak");
        }
        else if (terry.GetAction() == 710)
        {
            terry.SetAction(711);
            terry.SetAnimation("Rising Tackle Strong");
        }
    }

    //método que vai processar Rising Tackle - 701/711
    private void ProcessRisingTackle()
    {
        if (terry.GetAction() == 701)
            rb.velocity = new Vector2(rb.velocity.x, weakRisingTackleVerticalVelocity);

        else if (terry.GetAction() == 711)
            rb.velocity = new Vector2(rb.velocity.x, strongRisingTackleVerticalVelocity);
    }

    //método que vai processar Rising Trackle Fall - 702/712
    public void ProcessRisingTrackleFall()
    {
        //o personagem não está mais atacando
        attackControl.SetAttacking(false);

        terry.SetAction(48);
        terry.SetAnimation("Falling");
    }

    //método que vai processar Buster Wolf - 731
    private void ProcessBusterWolf()
    {
        if (terry.GetFacingRight())
            rb.velocity = new Vector2(busterWolfVelocity ,0);
        else
            rb.velocity = new Vector2(busterWolfVelocity*-1, 0);
    }
    
    //método que vai verificar qual proxima ação tomar
    public void BusterWolf()
    {
        if(terry.GetAction() == 731)
        {
            //o personagem não está mais atacando
            attackControl.SetAttacking(false);

            terry.SetAction(734);
            terry.SetAnimation("Buster Wolf Fail");
        }
        else if(terry.GetAction() == 732)
        {
            terry.SetAction(733);
            terry.SetAnimation("Buster Wolf End");
        }
    }

   //////////////////// OUTROS ////////////////////

    //método que vai ser chamando para instanciar o projétil do power wave
    public void InstantiatePowerWeaveEffect()
    {
        terrySpellPool.GetSpell(1, EffectSpawner.position).GetComponent<PowerWaveEffectController>().SetData(terry.GetFacingRight(), terry.GetPlayer1());
    }

    //método que vai ser chamando para instanciar o efeito do round wave
    public void InstantiateRoundWeaveEffect()
    {
        terrySpellPool.GetSpell(2, EffectSpawner.position).GetComponent<RoundWaveEffectController>().SetData(terry.GetFacingRight(), terry.GetPlayer1());
    }

    //método que vai ser chamando para instanciar o efeito do Power Geyser
    public void InstantiatePowerGeyser()
    {
        terrySpellPool.GetSpell(0, EffectSpawner.position).GetComponent<PowerGeyserEffectController>().SetData(terry.GetFacingRight(), terry.GetPlayer1());
    }

    //método que vai ajustar a posição do personagem em X
    public void AjustPosition(float pos)
    {
        if (terry.GetFacingRight())
            transform.position = new Vector3(transform.position.x + pos, transform.position.y, 0);
        else
            transform.position = new Vector3(transform.position.x - pos, transform.position.y, 0);
    }

    //método que vai instanciar o efeito do especial
    public void InstantiateEspecialEffect()
    {
        Instantiate(EspecialEffectPrefab, EffectSpawner.position, EffectSpawner.rotation);
    }
    
    //////////////////// botões ////////////////////

    //processamento do botão A
    private bool ProcessAButton()
    {
        if (inputController.GetButtons(4) && 
           !inputController.GetButtons(5) && 
           !inputController.GetButtons(6) && 
           !inputController.GetButtons(7))
        {
            //controle do soco fraco
            if (!GetAvoidAttackLoop())
            {
                //guardando o momento do comando para o soco fraco
                SetAttackCommandTime(Time.time);

                //mudando o estado do personagem para atacando
                attackControl.SetAttacking(true);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(true);

                //verificar power geyser
                if(CheckIfCanSwitchTo720(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand5(4))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(720);
                    terry.SetAnimation("Power Geyser");

                    return true;
                }

                //verificar Power Wave
                if (CheckIfCanSwitchTo620(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand1(4))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(620);
                    terry.SetAnimation("Power Wave");
                    
                    return true;
                }

                //Burning Knuckle
                if (CheckIfCanSwitchTo680(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand2(4))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(680);
                    terry.SetAnimation("Weak Burning Knuckle");
                    
                    return true;
                }

                //verificar Rising Tackle
                if (CheckIfCanSwitchTo700(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand4(4))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(700);
                    terry.SetAnimation("Rising Tackle Start");

                    return true;
                }

                //verificando qual ação deve ser executada
                if (inputController.GetButtons(1) && 
                    CheckIfCanSwitchTo340(terry.GetAction(), terry.GetCanCancelAction()))//soco fraco agachado
                {
                    terry.SetAction(340);
                    terry.SetAnimation("Crouched Idle");
                    return true;
                }

                if(CheckIfCanSwitchTo380(terry.GetAction(), terry.GetCanCancelAction()))//soco fraco pulando
                {
                    terry.SetAction(380);

                    return true;
                }

                if ((terry.GetFacingRight() && inputController.GetButtons(2) && CheckIfCanSwitchTo600(terry.GetAction(), terry.GetCanCancelAction())) ||
                    (!terry.GetFacingRight() && inputController.GetButtons(0) && CheckIfCanSwitchTo600(terry.GetAction(), terry.GetCanCancelAction())))//soco fraco para frente
                {
                    terry.SetAction(600);

                    return true;
                }

                if(CheckIfCanSwitchTo300(terry.GetAction(), terry.GetCanCancelAction()))//soco fraco em pé
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(300);
                    terry.SetAnimation("Idle");

                    return true;
                }

                //mudando o estado do personagem para não atacando
                attackControl.SetAttacking(false);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(false);
            }
        }

        return false;
    }

    //processamento do botão B
    private bool ProcessBButton()
    {
        if (!inputController.GetButtons(4) &&
            inputController.GetButtons(5) &&
            !inputController.GetButtons(6) &&
            !inputController.GetButtons(7))
        {
            //controle do soco forte
            if (!GetAvoidAttackLoop())
            {
                //guardando o momento do comando para o soco forte
                SetAttackCommandTime(Time.time);

                //mudando o estado do personagem para atacando
                attackControl.SetAttacking(true);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(true);

                //verificar power geyser
                if (CheckIfCanSwitchTo720(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand5(5))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(720);
                    terry.SetAnimation("Power Geyser");

                    return true;
                }

                //verificar Round Wave
                if (CheckIfCanSwitchTo630(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand1(5))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(630);
                    terry.SetAnimation("Round Wave Start");

                    return true;
                }

                //verificar Rising Tackle
                if (CheckIfCanSwitchTo710(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand4(5))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(710);
                    terry.SetAnimation("Rising Tackle Start");

                    return true;
                }

                //Burning Knuckle
                if (CheckIfCanSwitchTo690(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand2(5))
                {
                    terry.SetAction(690);
                    terry.SetAnimation("Strong Burning Knuckle");

                    return true;
                }

                //rising upper
                if (CheckIfCanSwitchTo610(terry.GetAction(), terry.GetCanCancelAction()) &&
                    inputController.GetButtons(1) && inputController.GetButtons(2) && inputController.GetButtons(5))
                {
                    terry.SetAction(610);

                    return true;
                }

                if (inputController.GetButtons(1) &&
                    CheckIfCanSwitchTo350(terry.GetAction(), terry.GetCanCancelAction()))//soco forte agachado
                {
                    terry.SetAction(350);
                    terry.SetAnimation("Crouched Idle");

                    return true;
                }

                if (CheckIfCanSwitchTo390(terry.GetAction(), terry.GetCanCancelAction()))//soco forte pulando
                {
                    terry.SetAction(390);

                    return true;
                }

                //agarrão: direcional para frente e soco forte
                if (CheckIfCanSwitchTo460Or470(terry.GetAction()) &&
                    (terry.GetFacingRight() && inputController.GetButtons(2) || !terry.GetFacingRight() && inputController.GetButtons(0)) &&
                    opponent.GetGrounded() && Mathf.Abs(terry.GetOTransformX() - transform.position.x) < 3.5)
                {

                    //se o outro personagem também der um comando para agarrar
                    if (opponent.GetAction() == 460 || opponent.GetAction() == 470)
                    {
                        terry.SetAction(930);
                        terry.SetAnimation("Get Hit in the Chin");
                        terry.SetAttackingFalse();

                        //efeito de empurrão
                        if (terry.GetFacingRight())
                        {
                            //efeito de empurrão
                            rb.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);

                            //instanciando efeito na tela
                            attackControl.InstantiateHitEffect(transform.position.x + 1f, -1f);
                        }
                        else
                        {
                            //efeito de empurrão
                            rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);

                            //instanciando efeito na tela
                            attackControl.InstantiateHitEffect(terry.transform.position.x - 1f, -1f);
                        }

                        //oponente
                        oDamageable.SetDamage(4, 0, 0, 20);//mudando animação e empurrando o oponente
                        opponent.SetAttackingFalse();
                        
                        //mudando o estado do personagem para não atacando
                        attackControl.SetAttacking(false);

                        //mudando o controle que evita loop de ataque
                        SetAvoidAttackLoop(false);

                        return false;
                    }
                    
                    else//agarrão
                    {
                        terry.SetAction(460);

                        return true;
                    }
                }

                if (CheckIfCanSwitchTo310(terry.GetAction(), terry.GetCanCancelAction()))
                {
                    if (Mathf.Abs(terry.GetOTransformX() - transform.position.x) < 3.5)//soco forte colado
                    {
                        terry.SetAction(420);
                        terry.SetAnimation("Idle");

                        return true;
                    }
                    else//soco forte
                    {
                        //zerando a velocidade do personagem
                        rb.velocity = Vector2.zero;

                        terry.SetAction(310);
                        terry.SetAnimation("Idle");

                        return true;
                    }
                }

                //mudando o estado do personagem para não atacando
                attackControl.SetAttacking(false);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(false);
            }
        }

        return false;
    }

    //processamento do botão C
    private bool ProcessCButton()
    {
        if (!inputController.GetButtons(4) &&
            !inputController.GetButtons(5) &&
            inputController.GetButtons(6) &&
            !inputController.GetButtons(7))
        {
            //controle do chute fraco
            if (!GetAvoidAttackLoop())
            {
                //guardando o momento do comando para o chute fraco
                SetAttackCommandTime(Time.time);

                //mudando o estado do personagem para atacando
                attackControl.SetAttacking(true);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(true);
                
                //verificar Buster Wolf
                if (CheckIfCanSwitchTo730(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand6(6))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(730);
                    terry.SetAnimation("Buster Wolf Start");

                    return true;
                }

                //verificar Crack Shoot
                if (CheckIfCanSwitchTo640(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand2(6))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(640);
                    terry.SetAnimation("Crack Shoot Start");

                    return true;
                }

                //verificar power dunk 
                if (CheckIfCanSwitchTo660(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand3(6))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(660);
                    terry.SetAnimation("Power Dunk Start");
                    
                    return true;
                }
                
                //verificando qual ação deve ser executada
                if (inputController.GetButtons(1) && CheckIfCanSwitchTo360(terry.GetAction(), terry.GetCanCancelAction()))//chute fraco agachado
                {
                    terry.SetAction(360);
                    terry.SetAnimation("Crouched Idle");

                    return true;
                }

                if (CheckIfCanSwitchTo400(terry.GetAction(), terry.GetCanCancelAction()))//chute fraco pulando
                {
                    terry.SetAction(400);

                    return true;
                }

                if (CheckIfCanSwitchTo320(terry.GetAction(), terry.GetCanCancelAction()))//chute fraco em pé
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(320);
                    terry.SetAnimation("Idle");

                    return true;
                }

                //mudando o estado do personagem para não atacando
                attackControl.SetAttacking(false);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(false);
            }
        }

        return false;
    }

    //processamento do botão D
    private bool ProcessDButton()
    {
        if (!inputController.GetButtons(4) &&
            !inputController.GetButtons(5) &&
            !inputController.GetButtons(6) &&
            inputController.GetButtons(7))
        {
            //controle do chute forte
            if (!GetAvoidAttackLoop())
            {
                //guardando o momento do comando para o chute fraco
                SetAttackCommandTime(Time.time);

                //mudando o estado do personagem para atacando
                attackControl.SetAttacking(true);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(true);

                //verificar Buster Wolf
                if (CheckIfCanSwitchTo730(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand6(7))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(730);
                    terry.SetAnimation("Buster Wolf Start");

                    return true;
                }

                //verificar Crack Shoot
                if (CheckIfCanSwitchTo650(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand2(7))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(650);
                    terry.SetAnimation("Crack Shoot Start");

                    return true;
                }

                //verificar power dunk 
                if (CheckIfCanSwitchTo670(terry.GetAction(), terry.GetCanCancelAction()) && CheckCommand3(7))
                {
                    //zerando a velocidade do personagem
                    rb.velocity = Vector2.zero;

                    terry.SetAction(670);
                    terry.SetAnimation("Power Dunk Start");

                    return true;
                }
                
                if (inputController.GetButtons(1) && CheckIfCanSwitchTo370(terry.GetAction(), terry.GetCanCancelAction()))//chute forte agachado
                {
                    terry.SetAction(370);
                    terry.SetAnimation("Crouched Idle");

                    return true;
                }

                if (CheckIfCanSwitchTo410(terry.GetAction(), terry.GetCanCancelAction()))//chute forte pulando
                {
                    terry.SetAction(410);

                    return true;
                }

                //agarrão: direcional para frente e chute forte
                if (CheckIfCanSwitchTo460Or470(terry.GetAction()) &&
                    (terry.GetFacingRight() && inputController.GetButtons(2) || !terry.GetFacingRight() && inputController.GetButtons(0)) &&
                    opponent.GetGrounded() && Mathf.Abs(terry.GetOTransformX() - transform.position.x) < 3.5)
                {
                    //se o outro personagem também der um comando para agarrar
                    if (opponent.GetAction() == 460 || opponent.GetAction() == 470)
                    {
                        terry.SetAction(930);
                        terry.SetAnimation("Get Hit in the Chin");
                        terry.SetAttackingFalse();

                        //efeito de empurrão
                        if (terry.GetFacingRight())
                        {
                            //efeito de empurrão
                            rb.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);

                            //instanciando efeito na tela
                            attackControl.InstantiateHitEffect(transform.position.x + 1f, -1f);
                            
                        }
                        else
                        {
                            //efeito de empurrão
                            rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);

                            //instanciando efeito na tela
                            attackControl.InstantiateHitEffect(transform.position.x - 1f, -1f);
                            
                        }

                        //oponente
                        oDamageable.SetDamage(4, 0, 0, 20);//mudando animação e empurrando o oponente
                        opponent.SetAttackingFalse();

                        //mudando o estado do personagem para não atacando
                        attackControl.SetAttacking(false);

                        //mudando o controle que evita loop de ataque
                        SetAvoidAttackLoop(false);

                        return false;
                    }
                    else//agarrão
                    {
                        terry.SetAction(470);

                        return true;
                    }
                }

                if (CheckIfCanSwitchTo330(terry.GetAction(), terry.GetCanCancelAction()))
                {
                    if (Mathf.Abs(terry.GetOTransformX() - transform.position.x) < 3.5)//chute forte colado
                    {
                        terry.SetAction(430);
                        terry.SetAnimation("Idle");

                        return true;
                    }
                    else//chute forte
                    {
                        //zerando a velocidade do personagem
                        rb.velocity = Vector2.zero;

                        terry.SetAction(330);
                        terry.SetAnimation("Idle");

                        return true;
                    }
                }
                
                //mudando o estado do personagem para não atacando
                attackControl.SetAttacking(false);

                //mudando o controle que evita loop de ataque
                SetAvoidAttackLoop(false);
            }
        }

        return false;
    }
    
    /////////////////// Golpes de Comando ///////////////////

    //método que vai verificar se o player inseriu o comando de meia lua para frente e o botão de ação corretamente
    private bool CheckCommand1(int button)
    {
        if(terry.GetFacingRight())
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > -0.045f &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) > 0 &&
                    inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) < .15f)
                {
                    if (inputController.GetBtDownImputs(1, 4) < inputController.GetBtDownImputs(2, 4))
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > -0.045f &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) > 0 &&
                    inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) < .15f)
                {
                    if (inputController.GetBtDownImputs(1, 4) < inputController.GetBtDownImputs(0, 4))
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    //método que vai verificar se o player inseriu o comando de meia lua para trás e o botão de ação corretamente
    private bool CheckCommand2(int button)
    {
        if (terry.GetFacingRight())
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > -0.045f &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) > 0 &&
                    inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) < .15f)
                {
                    if (inputController.GetBtDownImputs(1, 4) < inputController.GetBtDownImputs(0, 4))
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > -0.045f &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) > 0 &&
                    inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) < .15f)
                {
                    if (inputController.GetBtDownImputs(1, 4) < inputController.GetBtDownImputs(2, 4))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //método que vai verificar se o player inseriu o comando c para frente e o botão de ação corretamente
    private bool CheckCommand3(int button)
    {
        if (terry.GetFacingRight())
        {
            if(inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(2, 4) > -0.045f &&
               inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(2, 4) < .15f)
            {
                if(inputController.GetBtDownImputs(2, 4) - inputController.GetBtDownImputs(1, 4) > 0 &&
                    inputController.GetBtDownImputs(2, 4) - inputController.GetBtDownImputs(1, 4) < .15f)
                {
                    if(
                        (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 4) > 0 &&
                         inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 4) < .15f) ||

                        (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 3) > 0 &&
                         inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 3) < .15f)
                        )
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(0, 4) > -0.045f &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(0, 4) < .15f)
            {
                if (inputController.GetBtDownImputs(0, 4) - inputController.GetBtDownImputs(1, 4) > 0 &&
                    inputController.GetBtDownImputs(0, 4) - inputController.GetBtDownImputs(1, 4) < .15f)
                {
                    if (
                        (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 4) > 0 &&
                         inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 4) < .15f) ||

                        (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 3) > 0 &&
                         inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 3) < .15f)
                        )
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    //método que vai verificar se o player inseriu o comando (segurar) baixo + cima + botão de ação corretamente
    private bool CheckCommand4(int button)
    {
        if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(3, 4) > -0.045f &&
            inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(3, 4) < .15f)
        {
            if(inputController.GetBtDownImputs(3, 4) - inputController.GetBtUpImputs(1, 4) > -0.045f &&
                inputController.GetBtDownImputs(3, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if( inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(1, 4) > .15f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    //método que vai verificar se o player inseriu o comando do power geyser corretamente
    private bool CheckCommand5(int button)
    {
        if (terry.GetFacingRight())
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(2, 4) > 0 &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(2, 4) < .15f)
            {
                if ( (inputController.GetBtDownImputs(2, 4) - inputController.GetBtUpImputs(1, 4) > 0 &&
                      inputController.GetBtDownImputs(2, 4) - inputController.GetBtUpImputs(1, 4) < .15f) ||
                     (inputController.GetBtDownImputs(2, 4) - inputController.GetBtUpImputs(1, 3) > 0 &&
                      inputController.GetBtDownImputs(2, 4) - inputController.GetBtUpImputs(1, 3) < .15f) )
                {
                    if( (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) > 0 &&
                         inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) < .15f) ||
                        (inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(0, 4) > 0 &&
                         inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(0, 4) < .15f) )
                    {
                        if(inputController.GetBtDownImputs(0, 3) < inputController.GetBtDownImputs(1, 4) ||
                           inputController.GetBtDownImputs(0, 3) < inputController.GetBtDownImputs(1, 3))
                        {
                            return true;
                        }
                    }
                }

            }
        }
        else
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(0, 4) > 0 &&
                inputController.GetBtDownImputs(button, 4) - inputController.GetBtDownImputs(0, 4) < .15f)
            {
                if ( (inputController.GetBtDownImputs(0, 4) - inputController.GetBtUpImputs(1, 4) > 0 &&
                      inputController.GetBtDownImputs(0, 4) - inputController.GetBtUpImputs(1, 4) < .15f) ||
                     (inputController.GetBtDownImputs(0, 4) - inputController.GetBtUpImputs(1, 3) > 0 &&
                      inputController.GetBtDownImputs(0, 4) - inputController.GetBtUpImputs(1, 3) < .15f))
                {
                    if ((inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) > 0 &&
                         inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) < .15f) ||
                        (inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(2, 4) > 0 &&
                         inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(2, 4) < .15f))
                    {
                        if (inputController.GetBtDownImputs(2, 3) < inputController.GetBtDownImputs(1, 4) ||
                           inputController.GetBtDownImputs(2, 3) < inputController.GetBtDownImputs(1, 3) )
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    //método que vai verificar se o player inseriu o comando de duas meia lua para frente e o botão de ação corretamente
    private bool CheckCommand6(int button)
    {
        if (terry.GetFacingRight())
        {
            if(inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > 0 &&
               inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if(inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) > 0 &&
                   inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(2, 4) < .15f)
                {
                    if(inputController.GetBtDownImputs(2, 4) - inputController.GetBtDownImputs(1, 4) > 0 &&
                       inputController.GetBtDownImputs(2, 4) - inputController.GetBtDownImputs(1, 4) < .15f)
                    {
                        if((inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 4) > 0 &&
                            inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 4) < .15f) ||
                           (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 3) > 0 &&
                            inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(2, 3) < .15f))
                        {
                            if((inputController.GetBtUpImputs(2, 4) - inputController.GetBtUpImputs(1, 3) > 0 &&
                                inputController.GetBtUpImputs(2, 4) - inputController.GetBtUpImputs(1, 3) < .15f)||
                               (inputController.GetBtUpImputs(2, 3) - inputController.GetBtUpImputs(1, 3) > 0 &&
                                inputController.GetBtUpImputs(2, 3) - inputController.GetBtUpImputs(1, 3) < .15f))
                            {
                                if(inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(2, 3) > 0 &&
                                   inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(2, 3) < .15f)
                                {
                                    if(inputController.GetBtDownImputs(2, 3) > inputController.GetBtDownImputs(1, 3))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) > 0 &&
               inputController.GetBtDownImputs(button, 4) - inputController.GetBtUpImputs(1, 4) < .15f)
            {
                if (inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) > 0 &&
                   inputController.GetBtUpImputs(1, 4) - inputController.GetBtDownImputs(0, 4) < .15f)
                {
                    if (inputController.GetBtDownImputs(0, 4) - inputController.GetBtDownImputs(1, 4) > 0 &&
                       inputController.GetBtDownImputs(0, 4) - inputController.GetBtDownImputs(1, 4) < .15f)
                    {
                        if ((inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 4) > 0 &&
                             inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 4) < .15f) ||
                            (inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 3) > 0 &&
                             inputController.GetBtDownImputs(1, 4) - inputController.GetBtUpImputs(0, 3) < .15f))
                        {
                            if ((inputController.GetBtUpImputs(0, 4) - inputController.GetBtUpImputs(1, 3) > 0 &&
                                 inputController.GetBtUpImputs(0, 4) - inputController.GetBtUpImputs(1, 3) < .15f) ||
                                (inputController.GetBtUpImputs(0, 3) - inputController.GetBtUpImputs(1, 3) > 0 &&
                                 inputController.GetBtUpImputs(0, 3) - inputController.GetBtUpImputs(1, 3) < .15f))
                            {
                                if (inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(0, 3) > 0 &&
                                   inputController.GetBtUpImputs(1, 3) - inputController.GetBtDownImputs(0, 3) < .15f)
                                {
                                    if (inputController.GetBtDownImputs(0, 3) > inputController.GetBtDownImputs(1, 3))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }


    //////////////////// VERIFICADORES ////////////////////

    //método responsável por verificar se o personagem pode mudar para a ação active pow
    private bool CheckIfCanSwitchTo170(int action, bool canCancelAction)
    {
        if (action == 0 || action == 310 || action == 320)
        {
            return true;
        }
        //if(canCancelAction || action == )//ações que podem ser canceladas
        //{
            //return true;
        //}

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação weak punch
    private bool CheckIfCanSwitchTo300(int action, bool canCancelAction)
    {
        if (action == 0 || action == 12 || action == 20 || action == 30 || action == 90 || action == 91 || action == 92)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação strong punch
    private bool CheckIfCanSwitchTo310(int action, bool canCancelAction)
    {
        if (action == 0 || action == 12 || action == 20 || action == 30 || action == 90 || action == 91 || action == 92)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação weak kick
    private bool CheckIfCanSwitchTo320(int action, bool canCancelAction)
    {
        if (action == 0 || action == 12 || action == 20 || action == 30 || action == 90 || action == 91 || action == 92)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação strong kick
    private bool CheckIfCanSwitchTo330(int action, bool canCancelAction)
    {
        if (action == 0 || action == 12 || action == 20 || action == 30 || action == 90 || action == 91 || action == 92)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Crouched weak punch
    private bool CheckIfCanSwitchTo340(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Crouched strong punch
    private bool CheckIfCanSwitchTo350(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Crouched weak kick
    private bool CheckIfCanSwitchTo360(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Crouched Strong kick
    private bool CheckIfCanSwitchTo370(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Jumping weak punch
    private bool CheckIfCanSwitchTo380(int action, bool canCancelAction)
    {
        if (action == 41 || action == 42 || action == 43 || action == 61 || action == 62 || action == 81 || action == 82)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Jumping Strong punch
    private bool CheckIfCanSwitchTo390(int action, bool canCancelAction)
    {
        if (action == 41 || action == 42 || action == 43 || action == 61 || action == 62 || action == 81 || action == 82)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Jumping weak kick
    private bool CheckIfCanSwitchTo400(int action, bool canCancelAction)
    {
        if (action == 41 || action == 42 || action == 43 || action == 61 || action == 62 || action == 81 || action == 82)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Jumping Strong kick
    private bool CheckIfCanSwitchTo410(int action, bool canCancelAction)
    {
        if (action == 41 || action == 42 || action == 43 || action == 61 || action == 62 || action == 81 || action == 82)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Special Attack
    public override bool CheckIfCanSwitchTo440(int action, bool canCancelAction)
    {
        if (action == 0 || action == 310 || action == 330)
        {
            return true;
        }

        return true;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Jumping Special Attack
    public override bool CheckIfCanSwitchTo450(int action, bool canCancelAction)
    {
        if (action == 41 || action == 42 || action == 43 || action == 61 || action == 62 || action == 81 || action == 82 || action == 390 || action == 410)
        {
            return true;
        }

        return false;
    }
    
    //método responsável por verificar se o personagem pode mudar para a ação Grab and throw forward
    private bool CheckIfCanSwitchTo460Or470(int action)
    {
        if ((action == 0 || action == 20) && opponent.GetAction() < 900)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Weak Punch Forward
    private bool CheckIfCanSwitchTo600(int action, bool canCancelAction)
    {
        if (action == 0 || action == 20)
        {
            return true;
        }

        return false;
    }
    
    //método responsável por verificar se o personagem pode mudar para a ação Rising Upper
    private bool CheckIfCanSwitchTo610(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11 || action == 12 || action == 20 || action == 310 || action == 350 || action == 460)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação power wave
    private bool CheckIfCanSwitchTo620(int action, bool canCancelAction)
    {
        //verifica se já tem um power wave ativo e se tiver não permite usar outro
        if(!terrySpellPool.VerifySpecificSpell(1))
        {
            if( action == 0 || action == 10 || action == 11 || action == 12 || action == 20 || action == 300 )
            {
                return true;
            }
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação round wave
    private bool CheckIfCanSwitchTo630(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 20 || action == 310)
        {
            return true;
        }

        return false;
    }


    //método responsável por verificar se o personagem pode mudar para crack shoot fraco
    private bool CheckIfCanSwitchTo640(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 30 || action == 320)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para crack shoot forte
    private bool CheckIfCanSwitchTo650(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 30 || action == 330)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Power Dunk
    private bool CheckIfCanSwitchTo660(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 20 || action == 300)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Power Dunk
    private bool CheckIfCanSwitchTo670(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 20 || action == 310)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Burning Knuckle
    private bool CheckIfCanSwitchTo680(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 30 || action == 300)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Burning Knuckle
    private bool CheckIfCanSwitchTo690(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 30 || action == 310)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Rising Tackle
    private bool CheckIfCanSwitchTo700(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 300)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Rising Tackle
    private bool CheckIfCanSwitchTo710(int action, bool canCancelAction)
    {
        if (action == 0 || action == 10 || action == 11 || action == 12 || action == 310)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Power Geyser
    private bool CheckIfCanSwitchTo720(int action, bool canCancelAction)
    {
        if ( action == 10 || action == 11 || action == 12 || action == 20 || action == 30 || action == 300 || action == 310)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem pode mudar para a ação Buster Wolf
    private bool CheckIfCanSwitchTo730(int action, bool canCancelAction)
    {
        if (action == 10 || action == 11 || action == 12 || action == 20 || action == 320 || action == 330)
        {
            return true;
        }

        return false;
    }

    //método responsável por verificar se o personagem tem alguma spell ativa
    public override bool CheckIfHaveActiveSpell()
    {
        return terrySpellPool.VerifyActiveSpell();
    }

}
