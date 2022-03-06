using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //objetos de referencia
    private InputController ic;
    private Rigidbody2D rb;
    private Animator anim;
    
    private GenericCharacter characterController;       //objeto da classe que vai fazer os processamentos específicos do personagem

    private AttackControl attackControl;                //referencia ao objeto da classe AttackControl atrelado a esse personagem

    [SerializeField] private Transform renderTrasnform; //transform do objeto render

    [SerializeField] private MatchController mc;        //referencia ao MatchController

    private AttackControl oAC;                          //referencia ao objeto da classe AttackControl do oponente
    private Transform oTrasnform;                       //referencia ao transforno do oponente
    private PlayerController oPC;                       //referencia ao objeto da classe PlayerController no oponente
    private Animator oAnim;                             //referencia ao animator do oponente
    private Damageable oDamageable;                     //referencia ao Damageable do oponente

    public Transform dustEffectSpaw;                    //referencia ao ponto de spaw

    //prefabs
    public GameObject dustEffectPrefab;                 //prefab da poeria do pulo
    
    public GameObject hitOnFlorEffectPrefab;            //prefab do efeito de ser jogado no chão
    
    //variaveis de indicação
    [SerializeField] private bool player1;

    private float[] changedActionMoment = new float[5];     //guarda o tempo em que mudou a ultima ação
    private int[] lastActions = new int[5];                 //guarda as ultimas ações do personagem

    //private int attackCommandTime;                        //guarda o tempo em que foi feito o comando de ataque

    public int characterIndex;                              //qual é o personagem que vai começar
    
    //variaveis de controle do personagem
    public float walkSpeed;             //velocidade do personagem ao andar
    public float runSpeed;              //velocidade do personagem ao correr
    public float rollinSpeed;           //velocidade do rolamento
    public float jumpRollingSpeed;      //velocidade durante o pulo
    public float weakJumpForce;         //força do pulo fraco
    public float strongJumpForce;       //força do pulo forte
    
    private bool canCancelAction;       //variavel para indicar que a ação atual pode ser cancelada por outra ação especifica
    
    //variaveis de estado do personagem
    [SerializeField]private int action = 0;             //guarda a ação que o prersonagem está executando no momento
    
    private bool grounded = true;       //para verificar se o personagem está no chão
    public Transform groundCheck;       //transform do objeto que verificar se o personagem está ou não no chão
    public LayerMask groundLayer;       //guarda a layer do terreno

    private bool facingRight;           //indica para qual lado o personagem está virado

    private int especialConter = 0;     //indica a quantidade atual de pontos de especial que o personagem tem

    private bool powActived;            //indica que o pow está ativo
    private int powActiveTime;          //indica o momento que o pow foi ativo

    //variaveis de comando
    private bool commandToChangeSide;   //indica que o personagem tem que mudar de lado assim que possivel
    private bool stunCommand;           //indica que o personagem sofreu um stum e tem que mudar para a ação de stun assim q possivel
    private bool jump;                  //sinaliza que a animação de preparação para o pulo já terminou


    private void Awake ()
	{
        ic = GetComponent<InputController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        attackControl = GetComponentInChildren<AttackControl>();

        //pegando referencia do outro personagem
        if (player1)
        {
            oAC = GameObject.FindWithTag("Player2").GetComponentInChildren<AttackControl>();
            oPC = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
            oTrasnform = GameObject.FindWithTag("Player2").GetComponent<Transform>();
            oAnim = GameObject.FindWithTag("Player2").GetComponent<Animator>();
            oDamageable  = GameObject.FindWithTag("Player2").GetComponentInChildren<Damageable>();
        }
        else
        {
            oAC = GameObject.FindWithTag("Player1").GetComponentInChildren<AttackControl>();
            oPC = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
            oTrasnform = GameObject.FindWithTag("Player1").GetComponent<Transform>();
            oAnim = GameObject.FindWithTag("Player1").GetComponent<Animator>();
            oDamageable = GameObject.FindWithTag("Player1").GetComponentInChildren<Damageable>();
        }

        //tipo de personagem
        if(characterIndex == 1)//terry bogard
        {
            characterController = GetComponent<TerryController>();
        }

        //controle de posições e lado do personagem
        if ( player1 )
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
            //transform.position = new Vector3(-7f, -1.965184f, 0);
        }
        else
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
            //transform.position = new Vector3(7f, -1.965184f, 0);
        }
    }

    private void Update()
    {
        //verificando se o personagem está no chão
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);

        //controle do avoidAttackLoop
        if (characterController.GetAvoidAttackLoop() && !ic.GetButtons(4) && !ic.GetButtons(5) && !ic.GetButtons(6) && !ic.GetButtons(7))
        {
            characterController.SetAvoidAttackLoop(false);
        }
    }

    private void FixedUpdate()
	{
        //chama o metodo que vai verificar e processar as ações que são comuns a todos os personagens
        ProcessActions();
        
        //chama o metodo que vai verificar e processar as ações especificas do personagem
        characterController.ProcessActions();
        
    }
    
    //metodo que vai verificar qual ação deve ser processada
    private void ProcessActions()
    {
        switch (action)
        {
            case 0://Idle
                ProcessIdle();
                break;

            case 10://Crouching
                ProcessCrouching();
                break;

            case 11://Crouched Idle
                ProcessCrouchedIdle();
                break;

            case 12://Getting up from the Crouch
                ProcessGettingUpFromTheCrouch();
                break;

            case 20://Walk
                ProcessWalk();
                break;

            case 30://Walk Backwards
                ProcessWalkBackwards();
                break;

            case 40://preparation to Jump
                ProcessPreparationToJump();
                break;

            case 41://Rising
                ProcessRising();
                break;

            case 42://End of Climp
                ProcessEndOfClimp();
                break;

            case 43://Falling
                ProcessFalling();
                break;

            case 44://Landing
                ProcessLanding();
                break;

            case 45://Rising After Attaque
                ProcessRisingAfterAttaque();
                break;

            case 46://End of Climp After Attaque
                ProcessEndOfClimpAfterAttaque();
                break;

            case 47://Falling After Attaque
                ProcessFallingAfterAttaque();
                break;

            case 50://preparation to Frontal Jump
                ProcessPreparationToFrontalJump();
                break;

            case 61://Rising
                ProcessRisingStrongFrontalJump();
                break;
                
            case 63://Falling
                ProcessFrontalJumpFalling();
                break;

            case 70://preparation to Backwards Jump
                ProcessePreparationToBackwardsJump();
                break;

            case 81://Rising
                ProcessRisingStrongBackwardsJump();
                break;

            case 83://Falling
                ProcessBackWardsJumpFalling();
                break;

            case 62://Frontal Rolling 
            case 82://Backwards Rolling
                ActionButtons();
                break;

            case 90://start run
                ProcessStartOfRun();
                break;

            case 91://Running
                ProcessRun();
                break;

            case 92://slide
                ProcessSlide();
                break;

            case 100://Dash Back
                ProcessDashBack();
                break;

            case 102://Dash BackRecover
                ProcessDashBackRecover();
                break;

            case 111://defence
                ProcessDefence();
                break;

            case 121://defence crouched
                ProcessDefenceCrouched();
                break;
                
            case 131://front rolling
                ProcessFrontRolling();
                break;

            case 132://front rolling end
                ProcessFrontRollingEnd();
                break;

            case 141://back rolling
                ProcessBackRolling();
                break;

            case 142://back rolling end 
                ProcessBackRollingEnd();
                break;

            case 151://Change Side end
                ProcessChangeSideEnd();
                break;

            case 161://Change Side Crouched
                ProcessChanceSideCrouched();
                break;
                
            case 460://Grab And Throw For Ward Preparation
                ProcessGrabAndThrowForWardPreparation();
                break;

            case 470://Grab and Throw For Backward
                ProcessGrabAndThrowForBackwardPreparation();
                break;

            case 950://knock down rising
                ProcessKnockDownRising();
                break;
                
            case 951://Knock Down Falling
                ProcessKnockDownFalling();
                break;

            case 960://Hard knock down rising
                ProcessHardKnockDownRising();
                break;

            case 961://Hard knock down Falling
                ProcessHardKnockDownFalling();
                break;

            case 980://Taking Damage Jumping - Rising
                ProcessTakingDamageJumpingRising();
                break;

            case 981://Taking Damage Jumping - Falling
                ProcessTakingDamageJumpingFalling();
                break;

            case 991://Thrown Forward Rising
                ProcessThrownForwardRising();
                break;

            case 992://Thrown Forward Falling
                ProcessThrownForwardFalling();
                break;

        }
    }

    //metodo que vai ajustar o lado que o personagem deve estar virado
    public void AjustRotation()
    {
        renderTrasnform.eulerAngles = new Vector3(0, 0, 0);
    }
    

    //metodo que vai chamar os metodos de processamento dos botões de ação
    private void ActionButtons()
    {

        //o controle dos botões de ação é feito especificamente pela classe do personagem
        characterController.ActionButtons();
        
        //verificando o comando de rolamento
        if (Rolling())
        {
            //resetando
            attackControl.SetAttacking(false);

            return;
        }

        //verificando o comando de ataque especial comum
        if (SpecialCommon())
        {
            //resetando
            attackControl.SetAttacking(false);

            return;
        }

        //verificar o comando de pow
        if (ActivePow())
        {
            //resetando
            attackControl.SetAttacking(false);

            return;
        }
    }

    //metodo que vai resolver o comando para mudar de lado
    private bool ChangeSide()
    {
        if (commandToChangeSide)//comando para virar para o outro lado
        {
            commandToChangeSide = false;//tirando o comando para mudar de lado

            if ( action == 10 || action == 11 || action == 970 || action == 980)//verificando se o personagem está agachado
            {
                SetAction(160);
                anim.Play("Change Side Crouched");
            }
            else
            {
                SetAction(150);
                anim.Play("Change Side");
            }

            return true;
        }

        else return false;
    }

    //metodo que vai resolver o comando de stum
    private bool ResolveStun()
    {
        if (stunCommand)//comando para executar a ação de stum
        {
            SetAction(200);
            anim.Play("Stun");

            stunCommand = false;

            return true;
        }
        else return false;
    }

    //metodo que vai verificar o comando de rolamento
    public bool Rolling()
    {
        //soco fraco + chute fraco
        if (ic.GetButtons(4) && ic.GetButtons(6) && CheckIfCanSwitchTo130or140())
        {
            if ( (ic.GetButtons(0) && facingRight) || (ic.GetButtons(2) && !facingRight) )//botão para tras pressionado
            {
                SetAction(140);     //mudando a ação para rolamento para tras
                anim.Play("BackRollingCommand");
            }
            else
            {
                SetAction(130);     //mudando a ação para rolamento para tras
                anim.Play("FrontRollingCommand");
            }

            SetPlayerLayer(8);  //mudando a layer do personagem para passar por tras do outro personagem
            return true;
        }

        return false;
    }
    
    //metodo que vai verificar o comando de ataque especial comum
    public bool SpecialCommon()
    {
        //soco forte + chute forte
        if ( ic.GetButtons(5) && ic.GetButtons(7) )
        {
            if(characterController.CheckIfCanSwitchTo450(action, canCancelAction))
            {
                SetAction(450);
                anim.Play("Jumping Special Attack");
            }
            else if (characterController.CheckIfCanSwitchTo440(action, canCancelAction))
            {
                SetAction(440);
                anim.Play("Special Attack Start");
            }

            return true;
        }
        return false;
    }

    //metodo que vai verificar o comando de ativar o pow
    public bool ActivePow()
    {
        //soco forte + chute fraco
        if ( ic.GetButtons(5) && ic.GetButtons(6) )
        {
            SetAction(170);
            anim.Play("Recover");

            return true;
        }

        return false;
    }

    //////////////////// Metodos de processamento de ação ////////////////////

    //metodo que deve ser chamado na animação para trocar a ação para idle 
    public void ChangeToIdle()
    {
        //metodo que vai verificar se precisa mudar para a ação de stum
        if (ResolveStun()) return;

        //metodo que vai verificar se precisa mudar para a ação de virar para o outro lado
        if (ChangeSide()) return;

        SetAction(0);
        anim.Play("Idle");
    }

    //metodo que deve ser chamado na animação para trocar a ação para idle 
    public void ChangeToCrouchedIdle()
    {
        //metodo que vai verificar se precisa mudar para a ação de stum
        if (ResolveStun()) return;

        //metodo que vai verificar se precisa mudar para a ação de virar para o outro lado
        if (ChangeSide()) return;

        SetAction(11);
        anim.Play("Crouched Idle");
    }

    //metodo que vai verificar se o personagem deve correr ou pular para tras
    private bool Run()
    {
        bool changeAction = false, running = false;

        //para direita
        if (ic.GetButtons(2) &&
           ic.GetBtDownImputs(2, 4) - ic.GetBtUpImputs(2, 4) < .3f &&
            CheckIfCanSwitchTo90or100(ic.GetBtUpImputs(2, 4)))
        {
            changeAction = true;
            if (facingRight)//correr
            {
                running = true;
            }
            else//pulo para tras
            {
                running = false;
            }
        }
        //para esquerda
        else if (ic.GetButtons(0) &&
            ic.GetBtDownImputs(0, 4) - ic.GetBtUpImputs(0, 4) < .3f &&
            CheckIfCanSwitchTo90or100(ic.GetBtUpImputs(0, 4)))
        {
            changeAction = true;
            if (facingRight)//pulo para tras
            {
                running = false;
            }
            else//correr
            {
                running = true;
            }
        }
        else changeAction = false;

        //o personagem tem que mudar de ação
        if (changeAction)
        {
            if(running)
            {
                SetAction(90);
                anim.Play("Start of Run");
            }
            else
            {
                rb.velocity = Vector2.zero;//zera a velocidade do personagem

                rb.AddForce(new Vector2(0, 850));//gera a força para o pulo

                SetAction(100);//mudando para a ação de dash back
                anim.Play("Dash Back");
            }
            return true;
        }

        return false;//o personagem não está nem pulando para tras nem correndo
    }

    //metodo que vai verificar se o personagem deve se agaichar
    private bool Crouche()
    {
        if (ic.GetButtons(1))
        {
            SetAction(10);
            anim.Play("Crouching");

            return true;
        }
        return false;
    }

    //metodo que vai processar a ação Idle - 0
    private void ProcessIdle()
    {
        //metodo que vai verificar se precisa mudar para a ação de virar para o outro lado
        if(ChangeSide() ) return;
        
        //metodo que vai processar os botões de ação
        ActionButtons();

        //se a ação tiver sido mudada sai do metodo que processa a ação idle
        if (action != 0) return;


//controle walk/walk back / jump to front/ jump to back
        //direcional para esquerda pressionado
        if (ic.GetButtons(2))
        {
            if (ic.GetButtons(3))//botão de pulo pressionado
            {
                if ( facingRight )//pulando para frente
                {
                    SetAction(50);//mudando para a ação de pulo para frente
                    anim.Play("Preparation to Jump");
                }
                else//pulando para tras
                {
                    SetAction(70);//mudando para a ação de pulo para tras
                    anim.Play("Preparation to Jump");
                }

                rb.velocity = Vector2.zero;//zerando a velocidade do personagem

                SetJumpFalse();//muda o estado do verificador que vai sinalizar quando a animação de preparação para o pulo terminar

                return;
            }
            else//movimentação horizoltal
            {
                if (facingRight)//andar
                {
                    if(!Run())
                    {
                        SetAction(20);
                        anim.Play("Walk");
                        return;
                    }
                }
                else
                {
                    if(oAC.GetAttacking())//se defendendo
                    {
                        SetAction(110);
                        anim.Play("Defending");
                        return;
                    }
                    else if(!Run())//andar de costas
                    {
                        SetAction(30);
                        anim.Play("Walk Backwards");
                        return;
                    }
                }
            }
        }

        //direcional para esquerda pressionado
        if (ic.GetButtons(0))
        {
            if ( ic.GetButtons(3) )//verifica se o botão para cima está pressionado
            {
                if (facingRight)//pulando para tras
                {
                    SetAction(70);//ação de pulo para tras
                    anim.Play("Preparation to Jump");
                }
                else//pulando para frente
                {
                    SetAction(50);//ação de pulo para frente
                    anim.Play("Preparation to Jump");
                }

                rb.velocity = Vector2.zero;//zerando a velocidade do personagem

                SetJumpFalse();//muda o estado do verificador que vai sinalizar quando a animação de preparação para o pulo terminar

                return;
            }
            else//o botão de pulo não foi pressionado
            {
                if (facingRight)//verifica se o personagem está virado para a direita
                {
                    if(oAC.GetAttacking())//se defendendo
                    {
                        SetAction(110);
                        anim.Play("Defending");
                        return;
                    }
                    else if (!Run())//andando de costas
                    {
                        SetAction(30);
                        anim.Play("Walk Backwards");
                        return;
                    }
                }
                else//se o personagem não está virado para a direta
                {
                    if (!Run())//andando
                    {
                        SetAction(20);
                        anim.Play("Walk");
                        return;
                    }
                }
            }
        }


        //se abaixando
        if (Crouche())  return; 

        //pulo na vertical sem força horizoltal
        if (ic.GetButtons(3) && !ic.GetButtons(0) && !ic.GetButtons(2))
        {
            SetAction(40);//ação de pulo forte ou fraco 
            anim.Play("Preparation to Jump");

            rb.velocity = Vector2.zero;//zerando a velocidade do personagem

            SetJumpFalse();//muda o estado do verificador que vai sinalizar quando a animação de preparação para o pulo terminar

            return;
        }
    }

    //metodo que vai processar a ação Crouching - 10
    private void ProcessCrouching()
    {
        //metodo que vai verificar se precisa mudar para a ação de virar para o outro lado
        if (ChangeSide()) return;
        
        ActionButtons();

        return;
    }

    //metodo que vai processar a ação CrouchedIdle - 11
    private void ProcessCrouchedIdle()
    {
        //metodo que vai verificar se precisa mudar para a ação de virar para o outro lado
        if (ChangeSide()) return;

        ActionButtons();

        if (action != 11) return;
        
        if ( !ic.GetButtons(1) )//se o player parou de pressionar o botão para permanecer abaixado
        {
            SetAction(12);
            anim.Play("Getting up From the Crouch");
        }
    }

    //metodo que vai processar a ação Getting Up From The Crouch - 12
    private void ProcessGettingUpFromTheCrouch()
    {
        ActionButtons();
    }

    //metodo que vai processar a ação Walk - 20
    private void ProcessWalk()
    {
        //metodo que vai processar os botões de ação
        ActionButtons();

        //se a ação tiver sido mudada sai do metodo que processa a ação idle
        if (action != 20) return;

        //se abaixando
        if (Crouche()) return;


        if (ic.GetButtons(3))//pulo
        {
            SetAction(50);//mudando para pulo para frente
            anim.Play("Preparation to Jump");

            rb.velocity = Vector2.zero;//zerando a velocidade do personagem

            SetJumpFalse();//muda o estado do verificador que vai sinalizar quando a animação de preparação para o pulo terminar

            return;
        }
        
        if ( ( ic.GetButtons(2) && !facingRight) ||
            ( ic.GetButtons(0) && facingRight) ||
            ( !ic.GetButtons(0) && !ic.GetButtons(2) ) )//parou de andar
        {
            rb.velocity = Vector2.zero;//zerando a velocidade do personagem

            SetAction(0);//mudando para idle
            anim.Play("Idle");
        }
        else if(!Run())
        {
             if (ic.GetButtons(2) && facingRight)//andando para a direita
            {
                rb.velocity = new Vector2(walkSpeed, 0);//mudando a velocidade do personagem
            }
            else if (ic.GetButtons(0) && !facingRight)//andando para a esquerda
            {
                rb.velocity = new Vector2(walkSpeed * -1, 0);//mudando a velocidade do personagem
            }
        }
    }

    //metodo que vai processar a ação WalkBackwards - 30
    private void ProcessWalkBackwards()
    {
        //metodo que vai processar os botões de ação
        ActionButtons();
        
        //se a ação tiver sido mudada sai do metodo que processa a ação idle
        if (action != 30) return;

        //se abaixando
        if (Crouche()) return;


        if ( ic.GetButtons(3) )
        {
            SetAction(70);
            anim.Play("Preparation to Jump");

            rb.velocity = Vector2.zero;
            SetJumpFalse();//muda o estado do verificador que vai sinalizar quando a animação de preparação para o pulo terminar
            return;
        }

        if ((ic.GetButtons(0) && !facingRight) ||
            (ic.GetButtons(2) && facingRight) ||
            (!ic.GetButtons(0) && !ic.GetButtons(2)))
        {
            rb.velocity = Vector2.zero;

            SetAction(0);
            anim.Play("Idle");
        }
        else if (!Run())
        {
            if (ic.GetButtons(0) && facingRight)
            {
                rb.velocity = new Vector2(walkSpeed * -1, rb.velocity.y);
            }
            else if (ic.GetButtons(2) && !facingRight)
            {
                rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
            }
        }
    }

    //metodo que vai processar a ação WeakJump - 40
    private void ProcessPreparationToJump()
    {
        if (jump)
        {
            if (ic.GetBtDownImputs(3, 4) - ic.GetBtUpImputs(3, 4) > .12f)//pulo alto
            {
                rb.AddForce(new Vector2(0, strongJumpForce), ForceMode2D.Force);
            }
            else//pulo baixo
            {
                rb.AddForce(new Vector2(0, weakJumpForce), ForceMode2D.Force);
            }

            SetAction(41);
            anim.Play("Rising");

        }
    }

    //metodo que vai processar a ação rising - 41
    private void ProcessRising()
    {
        ActionButtons();

        if (action != 41) return;

        if ( rb.velocity.y < 2 )
        {
            SetAction(42);
            anim.Play("End of Climp");
        }
    }

    //metodo que vai processar a ação end of climp - 42
    private void ProcessEndOfClimp()
    {
        ActionButtons();

        if (action != 42) return;
        
        if (rb.velocity.y < -1)
        {
            SetAction(43);
            anim.Play("Falling");
        }
    }

    //metodo que vai processar a ação falling - 43
    private void ProcessFalling()
    {
        ActionButtons();

        if (action != 43) return;

        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
        }
    }

    //metodo que vai processar a ação landing - 44
    private void ProcessLanding()
    {
        SetPlayerLayer(7);  //resetando a layer do personagem
    }

    //metodo que vai processar a ação Rising After Attaque - 45
    private void ProcessRisingAfterAttaque()
    {
        if (rb.velocity.y < 2)
        {
            SetAction(46);
            anim.Play("End of Climp");
        }
    }

    //metodo que vai processar a ação End Of Climp After Attaque - 46
    private void ProcessEndOfClimpAfterAttaque()
    {
        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
            return;
        }

        if (rb.velocity.y < -1)
        {
            SetAction(47);
            anim.Play("Falling");
        }
    }

    //metodo que vai processar a ação Falling After Attaque - 47
    private void ProcessFallingAfterAttaque()
    {
        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
        }
    }

    //metodo que vai processar a ação Preparation To Frontal Jump - 50
    private void ProcessPreparationToFrontalJump()
    {
        if (jump)
        {
            if (ic.GetBtDownImputs(3, 4) - ic.GetBtUpImputs(3, 4) > .12f)//pulo alto
            {
                if (facingRight)//se o personagem estiver virado para a direita
                {
                    rb.velocity = new Vector2(jumpRollingSpeed, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }
                else
                {
                    rb.velocity = new Vector2(jumpRollingSpeed * -1, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }

                rb.AddForce(new Vector2(0, strongJumpForce));//adicionando força ao pulo
                SetAction(61);//mudando a ação
                anim.Play("Rising full");
            }
            else
            {
                if (facingRight)//se o personagem estiver virado para a direita
                {
                    rb.velocity = new Vector2(jumpRollingSpeed, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }
                else
                {
                    rb.velocity = new Vector2(jumpRollingSpeed * -1, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }

                rb.AddForce(new Vector2(0, weakJumpForce));//adicionando força ao pulo

                SetAction(41);//mudando a ação
                anim.Play("Rising");
            }
        }
    }

    //metodo que vai processar a ação Rising do pulo forte para frente - 61
    private void ProcessRisingStrongFrontalJump()
    {
        ActionButtons();

        if (action != 61) return;

        if (rb.velocity.y < 4)
        {
            SetAction(62);
            anim.Play("Frontal Rolling Jump");
        }
    }

    //metodo que vai processar a ação de falling do pulo forte para frente - 63
    private void ProcessFrontalJumpFalling()
    {
        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
        }
    }

    //metodo que vai processar a ação Preparation To Backwards Jump - 70
    private void ProcessePreparationToBackwardsJump()
    {
        if (jump)
        {
            if (ic.GetBtDownImputs(3, 4) - ic.GetBtUpImputs(3, 4) > .12f)//pulo alto
            {
                if (facingRight)//se o personagem estiver virado para a direita
                {
                    rb.velocity = new Vector2(jumpRollingSpeed * -1, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }
                else
                {
                    rb.velocity = new Vector2(jumpRollingSpeed, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }

                rb.AddForce(new Vector2(0, strongJumpForce));

                SetAction(81);
                anim.Play("Rising full");
            }
            else
            {
                if (facingRight)//se o personagem estiver virado para a direita
                {
                    rb.velocity = new Vector2(jumpRollingSpeed * -1, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }
                else
                {
                    rb.velocity = new Vector2(jumpRollingSpeed, 0);//adicionando velocidade para frente

                    SetPlayerLayer(9);  //mudando a layer do personagem para passar por tras do outro personagem
                }

                rb.AddForce(new Vector2(0, weakJumpForce));

                SetAction(41);
                anim.Play("Rising");
            }
        }
    }

    //metodo que vai processar a ação Rising do pulo forte para tras - 81 
    private void ProcessRisingStrongBackwardsJump()
    {
        ActionButtons();

        if (action != 81) return;

        if (rb.velocity.y < 4)
        {
            SetAction(82);
            anim.Play("Backward Rolling Jump");
        }
    }

    //metodo que vai processar a ação de falling do pulo forte para tras - 83
    private void ProcessBackWardsJumpFalling()
    {
        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
        }
    }

    //metodo que vai processar a ação de Start Of Run - 90
    private void ProcessStartOfRun()
    {
        //metodo que vai processar os botões de ação
        ActionButtons();

        //se a ação tiver sido mudada sai do metodo que processa a ação rum
        if (action != 90) return;


        if(facingRight)
            rb.velocity = new Vector2(runSpeed, 0);
        else
            rb.velocity = new Vector2(runSpeed * -1, 0);
    }

    //metodo que vai processar a ação run - 91
    private void ProcessRun()
    {
        //metodo que vai processar os botões de ação
        ActionButtons();

        //se a ação tiver sido mudada sai do metodo que processa a ação rum
        if (action != 91) return;


        if (facingRight && !ic.GetButtons(2) )
        {
            SetAction(92);
            anim.Play("Slide");
            return;
        }
        else if (!facingRight && !ic.GetButtons(0))
        {
            SetAction(92);
            anim.Play("Slide");
            return;
        }
        
        if(facingRight)
            rb.velocity = new Vector2(runSpeed, 0);

        else
            rb.velocity = new Vector2(runSpeed * -1, 0);
        
        if ( ic.GetButtons(3) )//pulo
        {
            SetAction(50);
            anim.Play("Preparation to Jump");

            rb.velocity = Vector2.zero;
            SetJumpFalse();
        }

    }

    //metodo que vai processar a ação slide - 92
    private void ProcessSlide()
    {
        //metodo que vai processar os botões de ação
        ActionButtons();

        //se a ação tiver sido mudada sai do metodo que processa a ação rum
        if (action != 92) return;


        rb.velocity = Vector2.zero;
    }

    //metodo que vai processar a ação Dash Back - 100
    private void ProcessDashBack()
    {
        if( facingRight )
            rb.velocity = new Vector2(runSpeed * -1, rb.velocity.y);//adicionando velocidade para tras

        else
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);//adicionando velocidade para tras

        if (grounded)
        {
            SetAction(101);
            anim.Play("Dash Back Fall");

            rb.velocity = Vector2.zero;
        }
    }

    //metodo que vai processar a ação recover - 102
    private void ProcessDashBackRecover()
    {
        if ( ic.GetButtons(2) && !facingRight)
        {
            SetAction(30);
            anim.Play("Walk Backwards");
        }
        else if ( ic.GetButtons(0) && facingRight)
        {
            SetAction(30);
            anim.Play("Walk Backwards");
        }
        else
        {
            rb.velocity = Vector2.zero;

            SetAction(0);
            anim.Play("Idle");
        }
    }

    //metodo que vai processar a ação defence - 111
    private void ProcessDefence()
    {
        if(!ic.GetButtons(0) && !oAC.GetAttacking())
        {
            SetAction(113);
            anim.Play("DefenceExit");
        }
    }

    //metodo que vai processar a ação defence crouched - 121
    private void ProcessDefenceCrouched()
    {
        if (!ic.GetButtons(0) && !oAC.GetAttacking())
        {
            SetAction(123);
            anim.Play("DefenceCrouchedExit");
        }
    }
    
    //metodo que vai processar a ação Front Rolling - 131
    private void ProcessFrontRolling()
    {
        if(facingRight) rb.velocity = new Vector2(rollinSpeed-1, 0);
        else rb.velocity = new Vector2((rollinSpeed-1) * -1, 0);
    }

    //metodo que vai processar a ação Front Rolling End - 132
    private void ProcessFrontRollingEnd()
    { 
        rb.velocity = new Vector2(0, 0);
    }

    //metodo que vai processar a ação back rolling - 141
    private void ProcessBackRolling()
    {
        if (facingRight) rb.velocity = new Vector2(rollinSpeed * -1, 0);
        else rb.velocity = new Vector2(rollinSpeed, 0);
    }

    //metodo que vai processar a ação back rolling end - 142
    private void ProcessBackRollingEnd()
    {
        rb.velocity = new Vector2(0, 0);
    }

    //metodo que vai processar a ação Chance Side - 151
    private void ProcessChangeSideEnd()
    {
        
        facingRight = !facingRight;
            
        transform.Rotate(new Vector3(0, 180, 0));

        SetAction(0);
        anim.Play("Idle");
        
    }

    //metodo que vai processar Chance Side Crouched - 161
    private void ProcessChanceSideCrouched()
    {
        if (action == 161)
        {
            facingRight = !facingRight;
            
            transform.Rotate(new Vector3(0, 180, 0));
            
            if ( ic.GetButtons(1) )
            {
                SetAction(11);
                anim.Play("Crouched Idle");
            }
            else
            {
                SetAction(12);
                anim.Play("Getting up From the Crouch");
            }
        }
    }
    
    //metodo que vai processar Grab and throw forward Preparation - 460
    private void ProcessGrabAndThrowForWardPreparation()
    {
        //se o outro personagem tambem der um comando para agarrar
        if (oPC.GetAction() == 460 || oPC.GetAction() == 470)
        {
            SetAction(930);
            anim.Play("Get Hit in the Chin");
            SetAttackingFalse();

            //efeito de empurrão
            if (facingRight)
            {
                //efeito de empurrão
                rb.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);

                //instanciando efeito na tela
                attackControl.InstantiateHitEffect((transform.position.x) + 1f, -1f);
            }
            else
            {
                //efeito de empurrão
                rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);

                //instanciando efeito na tela
                attackControl.InstantiateHitEffect((transform.position.x) - 1f, -1f);
            }

            //oponente
            oDamageable.SetDamage(4, 0, 0, 20);//mudando animação e empurrando o oponente
            oPC.SetAttackingFalse();
        }
        //verificando se o comando para o agarrar e jogar para frente pode ser executado
        else if (Time.time - characterController.GetAttackCommandTime() > .1f)
        {
            mc.SetLastHit(player1);//mudando o personagem para frente

            rb.velocity = Vector2.zero;
            SetAction(461);
            anim.Play("Grab and Throw Forward");

            oPC.SetSpeedZero();
            oPC.SetAction(990);
            oAnim.Play("Grabbed and Thrown Forward Frame 0");
        }
    }

    //metodo que vai processar Grab and throw forward - 461
    public void ProcessGrabAndThrowForward(int frame)
    {
        switch(frame)//verificando o frame que recebe e processando de acordo
        {
            case 0://instanciando o prefab do efeito do hit
                attackControl.InstantiateHitEffect();//instanciando o prefab do efeito do hit

                //mudando a posição do outro personagem
                if (facingRight)
                    oTrasnform.position = new Vector3(transform.position.x+0.1f, transform.position.y, transform.position.z);
                else
                    oTrasnform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                break;

            case 1:
                oAnim.Play("Grabbed and Thrown Forward Frame 1");//pariando animação
                break;

            case 2:
                oAnim.Play("Grabbed and Thrown Forward Frame 2");//pariando animação
                break;

            case 3:
                oAnim.Play("Grabbed and Thrown Forward Frame 3");//pariando animação
                break;

            case 4:
                oAnim.Play("Grabbed and Thrown Forward Frame 4");//pariando animação
                break;

            case 5:
                oAnim.Play("Grabbed and Thrown Forward Frame 5");//pariando animação
                break;

            case 6:
                oAnim.Play("Grabbed and Thrown Forward Frame 6");//pariando animação
                break;

            case 7:

                oDamageable.SetDamage(8, 0, 0, 15);//gerando dano


                if (facingRight)//verificando o lado e mudando a posição do outro personagem
                    oTrasnform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);

                else
                    oTrasnform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);

                break;
        }
    }

    //metodo que vai processar Grab And Throw For Backward Preparation - 470
    private void ProcessGrabAndThrowForBackwardPreparation()
    {
        //se o outro personagem tambem der um comando para agarrar
        if (oPC.GetAction() == 460 || oPC.GetAction() == 470)
        {
            SetAction(930);
            anim.Play("Get Hit in the Chin");
            SetAttackingFalse();

            //efeito de empurrão
            if (facingRight)
            {
                //efeito de empurrão
                rb.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);

                //instanciando efeito na tela
                attackControl.InstantiateHitEffect((transform.position.x) + 1f, -1f);
            }
            else
            {
                //efeito de empurrão
                rb.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);

                //instanciando efeito na tela
                attackControl.InstantiateHitEffect((transform.position.x) - 1f, -1f);
            }

            //oponente
            oDamageable.SetDamage(4, 0, 0, 20);//mudando animação e empurrando o oponente
            oPC.SetAttackingFalse();
        }
        //verificando se o comando para o agarrar e jogar para frente pode ser executado
        else if (Time.time - characterController.GetAttackCommandTime() > .1f)
        {
            mc.SetLastHit(player1);//mudando o personagem para frente

            rb.velocity = Vector2.zero;
            SetAction(471);
            anim.Play("Grab and Throw For Backward");

            oPC.SetSpeedZero();
            oPC.SetAction(1000);
            oAnim.Play("Grabbed and Thrown For Backward Frame 0");

            //movendo o outro personagem
            if (facingRight)
                oTrasnform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            else
                oTrasnform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            
        }
    }

    //metodo que vai processar Grab And Throw For Backward - 471
    public void ProcessGrabAndThrowForBackward(int frame)
    {
        switch (frame)//verificando o frame que recebe e processando de acordo
        {
            case 0:
                attackControl.InstantiateHitEffect();//instanciando o prefab do efeito do hit
                break;

            case 1:
                oAnim.Play("Grabbed and Thrown For Backward Frame 1");//pariando animação
                break;

            case 2:
                oAnim.Play("Grabbed and Thrown For Backward Frame 2");//pariando animação
                break;

            case 3:
                oAnim.Play("Grabbed and Thrown For Backward Frame 3");//pariando animação
                break;

            case 4:
                oAnim.Play("Grabbed and Thrown For Backward Frame 4");//pariando animação
                break;

            case 5:
                oAnim.Play("Grabbed and Thrown For Backward Frame 5");//pariando animação
                break;

            case 6:
                oDamageable.SetDamage(8, 0, 0, -15);//gerando dano

                //mudando de lado
                oPC.JustChangeSide();

                if (facingRight)//verificando o lado e mudando a posição do outro personagem
                    oTrasnform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);

                else
                    oTrasnform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);

                break;
        }
    }

    //metodo que vai processar Knock Down Rising - 950
        private void ProcessKnockDownRising()
    {
        if (rb.velocity.y < 0)
        {
            SetAction(951);
            anim.Play("Knock Down Falling");
        }
    }

    //metodo que vai processar Knock Down Falling - 951
    private void ProcessKnockDownFalling()
    {
        if (grounded)
        {
            SetAction(952);
            anim.Play("Knock Down Landing");
        }
    }

    //metodo que vai processar Hard Knock Down Rising - 960
    private void ProcessHardKnockDownRising()
    {
        if (rb.velocity.y < 2)
        {
            SetAction(961);
            anim.Play("Hard Knock Down Falling");
        }
    }

    //metodo que vai processar Hard Knock Down Falling - 961
    private void ProcessHardKnockDownFalling()
    {
        if (grounded)
        {
            SetAction(952);
            anim.Play("Knock Down Landing");
        }
    }

    //metodo que vai processar Taking Damage Jumping Rising - 980
    private void ProcessTakingDamageJumpingRising()
    {
        if (rb.velocity.y < 0)
        {
            SetAction(981);
            anim.Play("Taking Damage Jumping - Falling");
        }
    }

    //metodo que vai processar Taking Damage Jumping Falling - 981
    private void ProcessTakingDamageJumpingFalling()
    {
        if (grounded)
        {
            SetAction(44);
            anim.Play("Landing");
        }
    }

    //metodo que vai processar Thrown Forward Rising - 991
    private void ProcessThrownForwardRising()
    {
        if (rb.velocity.y < 0)
        {
            SetAction(992);
        }
    }

    //metodo que vai processar Thrown Forward Falling - 992
    private void ProcessThrownForwardFalling()
    {
        if(grounded)
        {
            SetAction(952);
            anim.Play("Knock Down Landing");
        }
    }

    
    //////////////////// VERIFICADORES PARA TODOS OS PERSONAGENS ////////////////////

    //metodo responsavel por verificar se o personagem pode mudar para a ação Run/DashBack - 90/100
    private bool CheckIfCanSwitchTo90or100(float value)
    {
        int a = -1;//variavel para verificar se o personagem fez alguma das ações 

        for (int i = 0; i < 5; i++)
        {
            //verificando lista de ações
            if (lastActions[i] == 43 || lastActions[i] == 63 || lastActions[i] == 83 || lastActions[i] == 12 || lastActions[i] == 101 || lastActions[i] == 102
                || lastActions[i] == 301 || lastActions[i] == 311 || lastActions[i] == 321 || lastActions[i] == 331 || lastActions[i] == 332 
                || lastActions[i] == 421 || lastActions[i] == 431)
                a = i;
        }

        //se não tiver encontrado nenhuma das ações que vai precisar ser analisada
        if (a == -1) return true;

        //se dentro das ultimas 5 ações houve uma das ações que precisa ser verificada
        else if (changedActionMoment[a] < value) return true;

        //se o personagem não pode usar a ação de correr
        else return false;
    }

    //metodo responsavel por verificar se o personagem pode mudar para a ação front rolling ou back rolling
    private bool CheckIfCanSwitchTo130or140()
    {
        if (action==0 || action == 300 || action == 320)
        {
            return true;
        }
        return false;
    }
    
    //metodo responsavel por verificar se o personagem pode mudar para a ação Grab and throw forward
    private bool CheckIfCanSwitchTo460()
    {
        if ( (action == 0 || action == 20) && oPC.GetAction() < 900)
        {
            return true;
        }

        return false;
    }

    //metodo responsavel por verificar se o personagem pode mudar para a ação Grab and Throw For Backward
    private bool CheckIfCanSwitchTo470()
    {
        if ((action == 0 || action == 20) && oPC.GetAction() < 900)
        {
            return true;
        }

        return false;
    }
    
    //////////////////// Outros ////////////////////

    //metodo que vai instanciar a poeira do pulo
    public void InstantiateDust()
    {
        Instantiate(dustEffectPrefab, dustEffectSpaw.position, dustEffectSpaw.rotation);
    }

    //metodo que vai instanciar o efeito de bater no chão
    public void InstantiateHitOnFloorEffect()
    {
        Instantiate(hitOnFlorEffectPrefab, dustEffectSpaw.position, dustEffectSpaw.rotation);
    }

    //metodo que vai mudar o personagem de lado sem fazer animação para as ações que jogam o personagem pro outro lado
    public void JustChangeSide()
    {
        facingRight = !facingRight;

        transform.Rotate(new Vector3(0, 180, 0));

        commandToChangeSide = false;
    }

    //////////////////// Gets & Sets ////////////////////

    //metodo responsavel por trocar a ação e gerar um historico de ações e tempos
    public void SetAction(int value)
    {
        //atualizando o historico
        for( int a = 0; a < 4; a++ )
        {
            changedActionMoment[a] = changedActionMoment[a + 1];
            lastActions[a] = lastActions[a + 1];
        }

        changedActionMoment[4] = (int)(Time.time * 1000);//guardando o tempo em que a ação foi mudada em milesegundos
        lastActions[4] = action;//guardando a ultima ação executada

        canCancelAction = false;//mudando o estado da booleana que indica que uma ação pode ser cancelada

        action = value;//trocando a ação
    }
    public int GetAction()
    {
        return action;
    }

    public int GetLastAction()
    {
        return lastActions[4];
    }

    public void SetJumpTrue()
    {
        jump = true;
    }
    public void SetJumpFalse()
    {
        jump = false;
    }
    public bool GetJump()
    {
        return jump;
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }

    public void SetCommandToChangeSide()
    {
        commandToChangeSide = true;
    }

    public void SetAttackingFalse()
    {
        attackControl.SetAttacking(false);
    }

    public bool GetPlayer1()
    {
        return player1;
    }

    public void SetPlayerLayer(int value)
    {
        gameObject.layer = value;
    }

    public int GetEspecialConter()
    {
        return especialConter;
    }
    public void AddEspecialConter(int value)
    {
        especialConter += value;
    }

    public void SetAnimation(string name)
    {
        anim.Play(name);
    }

    public void SetStumCommand(bool command)
    {
        stunCommand = command;
    }
    public bool GetStunCommand()
    {
        return stunCommand;
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void SetAnimationOnOpponent(string name)
    {
        oAnim.Play(name);
    }

    public void SetSpeedZero()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void SetCanCancelAction()
    {
        canCancelAction = true;
    }
    
    public bool GetCanCancelAction()
    {
        return canCancelAction;
    }
    
    public float GetOTransformX()
    {
        return oTrasnform.position.x;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }
    
}


/*          //anim.Play("Base Layer.Walk", 0, 0.25f);
*           //anim.Play("nomeAnimacao", 0, 0.5f);
*           O primeiro parâmetro é o nome da animação. O segundo é a camada, o terceiro é o tempo. O tempo é tipo uma porcentagem. 0.5 vai rodar no meio da animação. 0 no início e 1 no final
*       
*       -IMPORTANTE-
*       
*       Ações que precisam processar passagem para outras ações
*           slide -> run
*           slide -> golpes
*           slide -> jump
*           change cide -> run
*           change cide -> golpes
*           change cide -> jump
*           walk -> crouch
*           wakkbackwards -> crouch
*           
*       
*       BUGS E PROBLEMAS:
*       
*       *   CheckIfCanSwitchTo90or100 está com um erro que não deixa o personagem correr depois de realizar uma das ações da lista
*       provavelmente só deixa correr depois das ações da lista terem saido da memoria - aparentemente não tem esse bug
*   
*   -De vez enquando o peronsagem se comporta incorretamente na colisão caindo depois do pulo - NÂO VI MAIS ACONTECER
*   
*   -depois da mudança do animator a ação run tbm está com bug de corrida infinita - NÂO VI MAIS ACONTECER
*       
*   -O rolamento para tras não funciona com o comando correto mais funciona de o comando for alterado para 2 botões pressionados ao mesmo tempo
*       
*   -a força aplicada pelos golpes para empurrar não está funcionando corretamente, as vezes mais forte as vezes mais fraco
* 
*   -um personagem consegue empurar o outro no ar (só de acontecer colisão) aplicando força o suficiente para joga-lo longe 
*       se um personagem estiver andando e o ourto pular vai acontecer isso
* 
*   -quando o personagem sofre um hard knock down ele voa longe com distancia inconstante
*       tambem acontece quando o personagem sofre dano enquanto pula
*       
*   -o efeito de hit no chão pode instanciar no lugar errado (depois do agarrão)
*   
*   -é necessario tirar as hurtboxes das animações no chão e se recuperando
*   -as vezes o jogo calcula hit, ativa o efeito mais o oponente não executa uma animação de sofrendo dano
*       Resolvendo um deve resolver o outro
*   
*   -As hurt boxes não estão servindo para reconhecer colisão com as hit boxes
*       possivel solução: criar uma nova layer só para as boxes que estão em triger e deixar elas como boxes normais talvez se as trigger 
*       boxes tambem estiverem nessa layer funcione
*       
*   -Quando o Crack shoot é usado muito proximo o personagem colide com o outro e perde toda a velocidade em X
*   
*   -As ações que usam alguma forma de reposicionamento não estão funcionando como o esperado
*       o ultimo frame da animação que chama a função para reposicionar é renderisado na posição errada pos ao inves de reposicionar e passar
*       para a proxima animação ele reposiciona, imprime o frame atual na nova posição e então troca de animação
*       uma solução é chamar o metodo de reposicionar no primeiro frame da nova animação e não no ultimo frame da animação anterior
*           -burning knocle
*       
*   -Existem alguns ataques que não estão alterando o status de atacando corretamente
*   
*   -no especial Buster Wolf as vezes o personagem muda para a ação 730 mais fica executando a animação crouched idle
*   -O buster wolf pode pegar e passar direto pelo oponente e deixar ele bugado parado
*   
*/
