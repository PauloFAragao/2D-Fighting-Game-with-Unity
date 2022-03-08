using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
    //referencias
    [SerializeField] private PlayerController pController;      //referencia ao PlayerController do proprio personagem
    public GameObject hitEffectPrefab;                          //referencia ao efeito de hit
    public Transform hitEffectSpaw;                             //referencia para o game object que vai apontar o local onde o hit effect deve ser instanciado

    [SerializeField] private MatchController Mc;                //referência ao MatchController

    //variaveis de indicação
    private int attackAction;   //indica qual é a ação que o personagem estava executando quando houve o acerto

    //variaveis de estado
    private bool attacking;     //indica que o personagem está atacando
    
    //método que vai ser chamado quando o personagem atingir algo
    public void OnHit(Collider2D collision)
    {
        //verificar se o que foi atingido foi o outro personagem - feito
        //verificar qual ação o personagem está realizando em cada caso
        //chamar a função que vai fazer o outro player mudar de ação e vai contabilizar o dano e o stun do outro personagem - feito
        //chamar o efeito de hit - feito
        //verificar se pode mover o outro personagem se não mover esse personagem para trás - parcialmente feito (vai precisar ser refeito)
        //adicionar ao contador de pontos de especial
        //mudar a Order In Layer para fazer o personagem que deu dano ficar na frente - feito

        //verificar qual é a ação que o personagem estava executando quando houve o acerto
        actionSearch();
        
        //verificando se o efeito de hit não foi do proprio player
        if (collision.CompareTag("Player2") && pController.GetPlayer1() || //se o player 1 acertou o player 2
            collision.CompareTag("Player1") && !pController.GetPlayer1() ) //se o player 2 acertou o player 1
        {
            if (attackAction == 301)//soco fraco em pé
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 20

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 20);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 311)//soco forte em pé
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 23

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 23);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 321)//chute fraco em pé
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 23

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 23);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 331 || attackAction == 332)//chute forte em pé
            {//tipo de dano: no queixo - 4 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(4, 0, 0, 25);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 341)//soco fraco agachado
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 20

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 20);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 351)//soco forte agachado
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 20

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 20);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 361)//chute fraco agachado
            {//tipo de dano: baixo - 1 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 20

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(1, 0, 0, 20);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 371)//chute forte agachado
            {//tipo de dano: queda - 6 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 4

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(6, 0, 0, 4);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 381)//soco fraco pulando
            {//tipo de dano: alto - 3 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 20
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(3, 0, 0, 20);
            }

            else if (attackAction == 391)//soco forte pulando
            {//tipo de dano: alto - 3 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 23
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(3, 0, 0, 23);
            }

            else if (attackAction == 401)//chute fraco pulando
            {//tipo de dano: alto - 3 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 23
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(3, 0, 0, 23);
            }

            else if (attackAction == 411)//chute forte pulando
            {//tipo de dano: alto - 3 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(3, 0, 0, 25);
            }

            else if (attackAction == 421)//soco forte colado
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 16

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 16);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 431)//chute forte colado
            {//tipo de dano: no queixo - 4 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(4, 0, 0, 25);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 441)//chute forte colado
            {//tipo de dano: jogado longe - 7 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 10

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(7, 0, 0, 10);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 450)//chute forte colado
            {//tipo de dano: jogado longe - 7 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 10

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(7, 0, 0, 10);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 601)//soco fraco para frente
            {//tipo de dano: médio - 2 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(2, 0, 0, 30);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 611)//soco forte para cima
            {//tipo de dano: no queixo - 4 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(4, 0, 0, 25);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 641)//Crack Shoot fraco
            {//tipo de dano: por cima - 5 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 15

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(5, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 651)//Crack Shoot forte
            {//tipo de dano: por cima - 5 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 15

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(5, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }


            else if (attackAction == 661)//Power Dunk
            {//tipo de dano:  / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 15

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(0, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 662)//Power Dunk
            {//tipo de dano:  / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 15

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(0, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }


            else if(attackAction == 680 || attackAction == 681)//Weak Burning Knuckle
            {//tipo de dano: Knock Down - 6 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(6, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if (attackAction == 690 || attackAction == 691)//Strong Burning Knuckle
            {//tipo de dano: Knock Down - 6 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25

                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(6, 0, 0, 15);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            else if(attackAction == 731)//buster wolf
            {//tipo de dano:Hard Knock Down Static - 9 / quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(9, 0, 0, 0);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);

                //mudando a ação do personagem
                pController.SetAction(732);
            }

            else if(attackAction == 733)//buster wolf hit
            {
                //tipo de dano: Hard Knock Down - 7/ quantidade de dano do golpe: x / quantidade de stun do golpe: x / força de empurrar do golpe: 25
                //Chamando o método do outro personagem e passando: tipo do ataque, quantidade de dano, quantidade de stun, força do empurrão
                collision.GetComponent<Damageable>().SetDamage(7, 0, 0, 10);

                //adicionando a quantidade de pontos de especial 
                pController.AddEspecialConter(10);
            }

            //colocando o player 1 na frente
            Mc.SetLastHit(true);

            //efeito de hit
            InstantiateHitEffect();
        }

    }

    //método que vai verificar qual é a ação que o personagem estava executando quando houve o acerto
    private void actionSearch()
    {
        if(pController.GetAction() == 44 )
        {
            attackAction = pController.GetLastAction();
        }
        else
        {
            attackAction = pController.GetAction();
        }
    }

    public void SetAttacking(bool value)
    {
        attacking = value;
    }
    public bool GetAttacking()
    {
        return attacking;
    }

    public void InstantiateHitEffect()
    {
        Instantiate(hitEffectPrefab, hitEffectSpaw.position, hitEffectSpaw.rotation);
    }

    public void InstantiateHitEffect(float x, float y)
    {
        Instantiate(hitEffectPrefab, new Vector3(x, y, 0), new Quaternion(0,0,0,0) );
    }


}
