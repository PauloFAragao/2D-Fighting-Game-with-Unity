using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //privadas
     private PlayerController pController;

    //botões
    private bool[] buttons = new bool[8];			    //controle de botões pressionados	-- [botão]

    //histórico dos botões
    private float[,] btDownImputs = new float[8, 5];	//histórico de botões - pressionado -- [botão, posição no histórico]
    private float[,] btUpImputs = new float[8, 5];	    //histórico de botões - Solto 		-- [botão, posição no histórico]

    //histórico de ordem dos botões
    private int[] btHistory = new int[10];              //histórico de botões pressionados 	-- [botão]
    
    private void Awake()
    {
        //pegando referencia no objeto pai
        pController = GetComponentInParent<PlayerController>();

        //inicializando o array buttons com o valor -1 em todos os campos
        for (int a = 10; a < 10; a++)
        {
            btHistory[a] = -1;
        }
        
    }

    private void FixedUpdate()
    {
        if (pController.GetPlayer1())//controles do p1
        {
        //horizontal-----------------------------------------------------------------
            if (Input.GetAxisRaw("HorizontalP1") > 0)//direcional pressionado para direita
            {
                if (!buttons[2])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(2);
                    ButtonPressHistory(2);
                }

                if (buttons[0])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(0);
                    buttons[0] = false;
                }

                buttons[2] = true;
            }
            else if (Input.GetAxisRaw("HorizontalP1") < 0)//direcional pressionado para esquerda
            {
                if (!buttons[0])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(0);
                    ButtonPressHistory(0);
                }

                if (buttons[2])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(2);
                    buttons[2] = false;
                }

                buttons[0] = true;
            }
            else if (Input.GetAxisRaw("HorizontalP1") == 0)//nenhum direcional pressionado
            {
                //o botão acabou de ser solto
                if (buttons[2])
                {
                    WriteBtUpImputs(2);
                    buttons[2] = false;
                }
                if (buttons[0])
                {
                    WriteBtUpImputs(0);
                    buttons[0] = false;
                }
            }

        //vertical---------------------------------------------------------------
            if (Input.GetAxisRaw("VerticalP1") > 0)//direcional pressionado para cima
            {
                if (!buttons[3])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(3);
                    ButtonPressHistory(3);
                }

                if (buttons[1])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(1);
                    buttons[1] = false;
                }

                buttons[3] = true;
            }
            else if (Input.GetAxisRaw("VerticalP1") < 0)//direcional pressionado para baixo
            {
                if (!buttons[1])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(1);
                    ButtonPressHistory(1);
                }

                if (buttons[3])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(3);
                    buttons[3] = false;
                }

                buttons[1] = true;
            }
            else if (Input.GetAxisRaw("VerticalP1") == 0)//nenhum direcional pressionado
            {
                //o botão acabou de ser solto
                if (buttons[3])
                {
                    WriteBtUpImputs(3);
                    buttons[3] = false;
                }
                if (buttons[1])
                {
                    WriteBtUpImputs(1);
                    buttons[1] = false;
                }
            }

        //controle de input botões de ação----------------------------------------------

            //botão A-------------------------------------------------------------------
            if (Input.GetAxisRaw("AButtonP1") != 0)//botão A pressionado
             {
                 if (!buttons[4])
                 {
                     WriteBtDownImputs(4);
                     ButtonPressHistory(4);
                     buttons[4] = true;
                 }
             }
             else if (Input.GetAxisRaw("AButtonP1") == 0 && buttons[4])//botão A Solto
             {
                 WriteBtUpImputs(4);
                 buttons[4] = false;
             }

             //botão B-------------------------------------------------------------------
             if (Input.GetAxisRaw("BButtonP1") != 0)//botão B pressionado
             {
                 if (!buttons[5])
                 {
                     WriteBtDownImputs(5);
                     ButtonPressHistory(5);
                     buttons[5] = true;
                 }
             }
             else if (Input.GetAxisRaw("BButtonP1") == 0 && buttons[5])//botão B Solto
             {
                 WriteBtUpImputs(5);
                 buttons[5] = false;
             }

             //botão C-------------------------------------------------------------------
             if (Input.GetAxisRaw("CButtonP1") != 0)//botão C pressionado
             {
                 if (!buttons[6])
                 {
                     WriteBtDownImputs(6);
                     ButtonPressHistory(6);
                     buttons[6] = true;
                 }
             }
             else if (Input.GetAxisRaw("CButtonP1") == 0 && buttons[6])//botão C Solto
             {
                 WriteBtUpImputs(6);
                 buttons[6] = false;
             }

             //botão D-------------------------------------------------------------------
             if (Input.GetAxisRaw("DButtonP1") != 0)//botão D pressionado
             {
                 if (!buttons[7])
                 {
                     WriteBtDownImputs(7);
                     ButtonPressHistory(7);
                     buttons[7] = true;
                 }
             }
             else if (Input.GetAxisRaw("DButtonP1") == 0 && buttons[7])//botão D Solto
             {
                 WriteBtUpImputs(7);
                 buttons[7] = false;
             }
        }
        else//controles do p2
        {
        //horizontal-----------------------------------------------------------------
            if (Input.GetAxisRaw("HorizontalP2") > 0)//direcional pressionado para direita
            {
                if (!buttons[2])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(2);
                    ButtonPressHistory(2);
                }

                if (buttons[0])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(0);
                    buttons[0] = false;
                }

                buttons[2] = true;
            }
            else if (Input.GetAxisRaw("HorizontalP2") < 0)//direcional pressionado para esquerda
            {
                if (!buttons[0])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(0);
                    ButtonPressHistory(0);
                }

                if (buttons[2])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(2);
                    buttons[2] = false;
                }

                buttons[0] = true;
            }
            else if (Input.GetAxisRaw("HorizontalP2") == 0)//nenhum direcional pressionado
            {
                //o botão acabou de ser solto
                if (buttons[2])
                {
                    WriteBtUpImputs(2);
                    buttons[2] = false;
                }
                if (buttons[0])
                {
                    WriteBtUpImputs(0);
                    buttons[0] = false;
                }
            }

        //vertical---------------------------------------------------------------
            if (Input.GetAxisRaw("VerticalP2") > 0)//direcional pressionado para cima
            {
                if (!buttons[3])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(3);
                    ButtonPressHistory(3);
                }

                if (buttons[1])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(1);
                    buttons[1] = false;
                }

                buttons[3] = true;
            }
            else if (Input.GetAxisRaw("VerticalP2") < 0)//direcional pressionado para baixo
            {
                if (!buttons[1])//o botão acabou de ser pressionado
                {
                    WriteBtDownImputs(1);
                    ButtonPressHistory(1);
                }

                if (buttons[3])//o botão acabou de ser solto
                {
                    WriteBtUpImputs(3);
                    buttons[3] = false;
                }

                buttons[1] = true;
            }
            else if (Input.GetAxisRaw("VerticalP2") == 0)//nenhum direcional pressionado
            {
                //o botão acabou de ser solto
                if (buttons[3])
                {
                    WriteBtUpImputs(3);
                    buttons[3] = false;
                }
                if (buttons[1])
                {
                    WriteBtUpImputs(1);
                    buttons[1] = false;
                }
            }

        //controle de input botões de ação----------------------------------------------

            //botão A-------------------------------------------------------------------
            if (Input.GetAxisRaw("AButtonP2") != 0)//botão A pressionado
            {
                if (!buttons[4])
                {
                    WriteBtDownImputs(4);
                    ButtonPressHistory(4);
                    buttons[4] = true;
                }
            }
            else if (Input.GetAxisRaw("AButtonP2") == 0 && buttons[4])//botão A Solto
            {
                WriteBtUpImputs(4);
                buttons[4] = false;
            }

            //botão B-------------------------------------------------------------------
            if (Input.GetAxisRaw("BButtonP2") != 0)//botão B pressionado
            {
                if (!buttons[5])
                {
                    WriteBtDownImputs(5);
                    ButtonPressHistory(5);
                    buttons[5] = true;
                }
            }
            else if (Input.GetAxisRaw("BButtonP2") == 0 && buttons[5])//botão B Solto
            {
                WriteBtUpImputs(5);
                buttons[5] = false;
            }

            //botão C-------------------------------------------------------------------
            if (Input.GetAxisRaw("CButtonP2") != 0)//botão C pressionado
            {
                if (!buttons[6])
                {
                    WriteBtDownImputs(6);
                    ButtonPressHistory(6);
                    buttons[6] = true;
                }
            }
            else if (Input.GetAxisRaw("CButtonP2") == 0 && buttons[6])//botão C Solto
            {
                WriteBtUpImputs(6);
                buttons[6] = false;
            }

            //botão D-------------------------------------------------------------------
            if (Input.GetAxisRaw("DButtonP2") != 0)//botão D pressionado
            {
                if (!buttons[7])
                {
                    WriteBtDownImputs(7);
                    ButtonPressHistory(7);
                    buttons[7] = true;
                }
            }
            else if (Input.GetAxisRaw("DButtonP2") == 0 && buttons[7])//botão D Solto
            {
                WriteBtUpImputs(7);
                buttons[7] = false;
            }
        }
    }

    //esse método é responsável por escrever no histórico do botão que foi pressionado o tempo em que ele foi pressionado
    private void WriteBtDownImputs(int button)
    {
        for (int a = 0; a < 4; a++)
        {
            btDownImputs[button, a] = btDownImputs[button, a + 1];
        }
        btDownImputs[button, 4] = Time.time;
    }
    
    //esse método é responsável por escrever no histórico do botão que foi solto o tempo em que ele foi solto
    private void WriteBtUpImputs(int button)
    {
        for (int a = 0; a < 4; a++)
        {
            btUpImputs[button, a] = btUpImputs[button, a + 1];
        }
        btUpImputs[button, 4] =Time.time;
    }

    //esse método é responsável por escrever no histórico de botões pressionados qual foi o ultimo botão pressionado
    private void ButtonPressHistory(int button)
    {
        for (int a = 0; a < 9; a++)
        {
            btHistory[a] = btHistory[a + 1];
        }
        btHistory[9] = button;
    }

//metodos públicos--------------------------------------------------------------------------------------------

    //método get para a variavel button, retorna o valor que informa se o botão requisitado está pressionado
    public bool GetButtons(int value)
    {
        return buttons[value];
    }

    //método get para a variavel btDownImputs, retorna o tempo requisitado
    public float GetBtDownImputs(int button, int index)
    {
        return btDownImputs[button, index];
    }

    //método get para a variavel btUpImputs, retorna o tempo requisitado
    public float GetBtUpImputs(int button, int index)
    {
        return btUpImputs[button, index];
    }

    //método get para a variavel btHistory, retorna o botão que foi pressionado na posição requisitada
    public int GetBtHistory(int value)
    {
        return btHistory[value];
    }

}

/*

	buttons -- controle de quais botões estão pressionados e quais estão soltos -- [botão]

	btDownImputs -- histórico dos 5 últimos inputs (momento em que foi pressionado) do jogador por cada um dos 8 botões de ação -- [botão, posição no histórico]
	btUpImputs -- histórico dos 5 últimos inputs (momento em que foi solto) do jogador por cada um dos 8 botões de ação -- [botão, posição no histórico]
	
	btHistory -- histórico de quais foram os últimos 10 botões pressionados pelo jogador -- [botão]
	
	[botões]
	0 - direcional esquerdo
	1 - direcional baixo
	2 - direcional direito
	3 - direcional cima
	4 - botão A
	5 - botão B
	6 - botão C
	7 - botão D
	
	[posição no histórico]
	0 - tempo em que o botão foi pressionado anterior ao valor dentro da posição 1
	1 - tempo em que o botão foi pressionado anterior ao valor dentro da posição 2
	2 - tempo em que o botão foi pressionado anterior ao valor dentro da posição 3
	3 - tempo em que o botão foi pressionado anterior ao valor dentro da posição 4
	4 - tempo em que o botão foi pressionado pela ultima vez

*/
