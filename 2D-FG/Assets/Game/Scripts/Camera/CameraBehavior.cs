using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //referencias
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Rigidbody2D p1Rb;
    [SerializeField] private Rigidbody2D p2Rb;
            
    void Update()
    {
        //colis�o manual no fim da camera
        CameraColision();
        //essa colis�o foi implementada para usar o cinemachine 
        //se por fim n�o for usar o cinemachine � melhor colocar a colis�o no objeto que detem a camera

        //comportamento da camera
        CBehavior();
    }

    private void CBehavior()
    {
        //zerando a velocidade antes de tudo
        rb.velocity = Vector2.zero;

        //p1 na esquerda e p2 na direita
        if (p1Rb.position.x < p2Rb.position.x)
        {
            //p1 depois do ponto 1 da camera e a velocidade de p1 � diferente de 0
            if (p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5  &&
                p1Rb.velocity.x != 0)
            {
                //p1 no corner a esquerda e p2 n�o est� no corner a direita
                if (p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1.1 &&
                    p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1)
                {
                    //movimenta a camera
                    rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                }

                //p2 est� depois do ponto 2 da camera
                else if (p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5)
                {
                    //p1 est� se movendo para a direita
                    if (p1Rb.velocity.x > 0)
                    {
                        //movimenta a camera para a direita at� p2 passar do ponto 2 da camera
                        rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                    }
                }

                //p2 est� antes do ponto 2 da camera
                else if (p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5)
                {
                    //p1 est� se movendo para a esquerda
                    if (p1Rb.velocity.x < 0)
                    {
                        //movimenta a camera para a esquerda at� p2 chegar ao ponto 2 da camera
                        rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                    }
                }
            }

            //p2 depois do ponto 2 da camera e a velocidade de p2 � diferente de 0
            if (p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5 &&
                p2Rb.velocity.x != 0)
            {
                //p2 no corner a direita e p1 n�o est� no corner a esquerda
                if (p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1.1 &&
                    p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1 )
                {
                    //movimenta a camera
                    rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                }

                //p1 est� depois do ponto 1 da camera
                else if (p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5)
                {
                    //p1 est� se movendo para a esquerda
                    if (p2Rb.velocity.x < 0)
                    {
                        //movimenta a camera para a esquerda at� p2 passar do ponto 1 da camera
                        rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                    }
                }

                //p1 n�o est� no ponto 1 da camera
                else if (p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5)
                {
                    //a camera n�o chegou ao fim do cenario e p1 est� se movendo para a direita
                    if (p2Rb.velocity.x > 0)
                    {
                        //movimenta a camera para a direita at� p2 chegar ao ponto 1 da camera
                        rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                    }
                }
            }
        }

        //p1 na direita e p2 na esquerda
        else if (p1Rb.position.x > p2Rb.position.x)
        {
            //p1 depois do ponto 2 da camera
            if(p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5)
            {
                //p1 no corner a direita e p2 n�o est� no corner a esquerda
                if(p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1.1 &&
                   p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1)
                {
                    //movimenta a camera
                    rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                }

                //p2 est� depois do ponto 1 da camera
                else if (p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5)
                {
                    //p1 est� se movendo para a esquerda
                    if (p1Rb.velocity.x < 0)
                    {
                        //movimenta a camera para a esquerda at� p2 passar do ponto 1 da camera
                        rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                    }
                }

                //p2 n�o est� no ponto 1 da camera
                else if(p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5)
                {
                    //a camera n�o chegou ao fim do cenario e p1 est� se movendo para a direita
                    if (p1Rb.velocity.x > 0)
                    {
                        //movimenta a camera para a direita at� p2 chegar ao ponto 1 da camera
                        rb.velocity = new Vector2(p1Rb.velocity.x, 0);
                    }
                }
            }

            //p2 depois do ponto 1 da camera e a velocidade de p2 � diferente de 0
            if (p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 5 &&
                p2Rb.velocity.x != 0)
            {
                //p2 no corner a esquerda e p1 n�o est� no corner a direita
                if (p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1.1 &&
                    p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1)
                {
                    //movimenta a camera
                    rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                }

                //p1 est� depois do ponto 2 da camera
                else if (p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5)
                {
                    //p1 est� se movendo para a direita
                    if (p2Rb.velocity.x > 0)
                    {
                        //movimenta a camera para a direita at� p2 passar do ponto 2 da camera
                        rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                    }
                }

                //p1 est� antes do ponto 2 da camera
                else if (p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 5)
                {
                    //p2 est� se movendo para a esquerda
                    if (p2Rb.velocity.x < 0)
                    {
                        //movimenta a camera para a esquerda at� p2 chegar ao ponto 2 da camera
                        rb.velocity = new Vector2(p2Rb.velocity.x, 0);
                    }
                }
            }
        }

        //se a camera passar do fim do cenario
        if (rb.position.x < -8.3)
        {
            //setando a camera na posi��o maxima
            rb.position = new Vector2(-8.3f, 0);

            //se a camera estiver se movendo para a direita
            if (rb.velocity.x < 0)
                rb.velocity = Vector2.zero;
        }

        //se a camera passar do fim do cenario
        else if (rb.position.x > 8.3)
        {
            //setando a camera na posi��o maxima
            rb.position = new Vector2(8.3f, 0);

            //se a camera estiver se movendo para a esquerda
            if (rb.velocity.x > 0)
                rb.velocity = Vector2.zero;
        }
    }
    
    private void CameraColision()
    {
        //colis�o no fim da camera a direita do p1
        if (p1Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3 (0,0,10)).x +1 && p1Rb.velocity.x < 0)
        {
            p1Rb.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x +1, p1Rb.position.y);
        }
        //colis�o no fim da camera a esquerda do p1
        else if(p1Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x -1 && p1Rb.velocity.x > 0)
        {
            p1Rb.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1, p1Rb.position.y);
        }


        //colis�o no fim da camera a direita do p2
        if (p2Rb.position.x < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1 && p2Rb.velocity.x < 0)
        {
            p2Rb.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x + 1, p2Rb.position.y);
        }
        //colis�o no fim da camera a esquerda do p2
        else if (p2Rb.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1 && p2Rb.velocity.x > 0)
        {
            p2Rb.position = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x - 1, p2Rb.position.y);
        }
    }
}
