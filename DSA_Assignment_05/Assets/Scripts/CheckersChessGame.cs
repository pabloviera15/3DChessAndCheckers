using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersChessGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1_X_button;
    public GameObject player2_X_button;

    //Variables for width/height, and placements Offsets
    public int player1_height, player1_width, player1_xOffset, player1_yOffset, player1_zOffset;
    public int player2_height, player2_width, player2_xOffset, player2_yOffset, player2_zOffset;


    //Variable for Switching line placing of both players
    public int spawn8_player1 = 0;
    public int spawn8_player2 = 0;

    public int piecesCount = 1;



    void Start()
    {
        SpawnPlayer1();
        SpawnPlayer2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnPlayer1()
    {
        //Spawn n x n Cubes 
        for (int y = 0; y < player1_height; y++)
        {

            for (int x = 0; x < player1_width; x++)
            {
                player1_xOffset = x;
                player1_yOffset = y;
                player1_xOffset = player1_xOffset * 2;
                player1_yOffset = player1_yOffset * 2;

                spawn8_player1++;

                piecesCount++;

                if (spawn8_player1 <= 8 && spawn8_player1 % 2 == 0)
                {

                    Instantiate(player1_X_button, new Vector3(player1_xOffset, player1_yOffset, player1_zOffset), Quaternion.identity);
                    player1_X_button.name = "Player_1_0" + piecesCount;
                }

                else if (spawn8_player1 >= 8 && spawn8_player1 % 2 == 1)
                {
                    Instantiate(player1_X_button, new Vector3(player1_xOffset, player1_yOffset, player1_zOffset), Quaternion.identity);
                    player1_X_button.name = "Player_1_0" + piecesCount;
                }
            }
        }
    }

    public void SpawnPlayer2()
    {
        //Spawn n x n Cubes 
        for (int y = 0; y < player2_height; y++)
        {

            for (int x = 0; x < player2_width; x++)
            {
                player2_xOffset = x;
                player2_yOffset = y+6;
                player2_xOffset = player2_xOffset * 2;
                player2_yOffset = player2_yOffset * 2;

                spawn8_player2++;

                piecesCount++;

                if (spawn8_player2 <= 8 && spawn8_player2 % 2 == 0)
                {

                    Instantiate(player2_X_button, new Vector3(player2_xOffset, player2_yOffset, player2_zOffset), Quaternion.identity);

                    player2_X_button.name = "Player_2_0" + piecesCount;
                }

                else if (spawn8_player2 >= 8 && spawn8_player2 % 2 == 1)
                {
                    Instantiate(player2_X_button, new Vector3(player2_xOffset, player2_yOffset, player2_zOffset), Quaternion.identity);

                    player2_X_button.name = "Player_2_0" + piecesCount;
                }
            }
        }
    }
}
