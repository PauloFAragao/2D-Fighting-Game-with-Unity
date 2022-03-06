using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectController : MonoBehaviour
{
    //referencias
    private Rigidbody2D rb;
    private Animator anim;

    //variaveis de controle
    [SerializeField] private float speed;   //velocidade de movimento

    //variaveis de indicação
    private bool player1;                   //indica qual player usou o ataque

    private bool rightDirection = true;     //direção do movimento

    //variaveis de estado
    private bool stateCicle = true;         //true = estado em que se move e está ativo para acerto / false = estado em que não se move e não pode acertar


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (stateCicle)
        {
            if (rightDirection)
                rb.velocity = new Vector2(speed, 0);

            else
                rb.velocity = new Vector2(speed * -1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //verifica se está acertando o jogador certo e se ainda está no estado ativo (se estiver executando a animação end não deve poder pegar)
        if ((collision.CompareTag("Player2") && player1) || (collision.CompareTag("Player1") && !player1))
        {
            //metodo que vai ser especifico de cada magia e vai causar dano
            DoDamage(collision);

            //mudando estado 
            SetState();
        }

        //acertou outra magia
        if (collision.CompareTag("Spell"))
        {
            //chamando o SetState da outra magia
            collision.GetComponent<SpellEffectController>().SetState();

            //mudando estado
            SetState();
        }
    }

    //recebendo dados de direção e qual personagem acertar
    public void SetData(bool diretion, bool player)
    {
        rightDirection = diretion;
        player1 = player;

        if (diretion)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    


    //metodo que vai mudar o estado dessa magia
    public virtual void SetState(){ }// <<<<<<<<<< Essemetodo precisa ser re-escrito na classe que herda

    //metodo que vai gerar dano no outro personagem
    public virtual void DoDamage(Collider2D collision) { }// <<<<<<<<<< Essemetodo precisa ser re-escrito na classe que herda



    public void SetStateCicle(bool value)
    {
        stateCicle = value;
    }

    public void SetAnimation(string animation)
    {
        anim.Play(animation);
    }

    public void SetVelZero()
    {
        rb.velocity = Vector2.zero;
    }
}
