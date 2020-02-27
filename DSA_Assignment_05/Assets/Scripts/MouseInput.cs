using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseInput : MonoBehaviour
{
    //Declare an Object to a local variable
    Board BoardObj;
    //Declare variable to store the mouse position
    Vector3 tempMousePosition;
    //Declare variable to store the previous value of the player
    Vector3 tempPlayersPositions;
    float tempUpdatedPlayersPositionX;
    float tempUpdatedPlayersPositionY;
    //Declare the array to store all the Players Pieces
    GameObject[] player1Array;
    GameObject[] player2Array;
    //Declare and initilize the lists to store all the Player Pieces
    List<GameObject> player1List = new List<GameObject>();
    List<GameObject> player2List = new List<GameObject>();
    //Declare a variable to check wich player turn is
    Board playersTurnBoolObj;


    // Start is called before the first frame update
    void Start()
    {
        //BoardObj.tempPiece = null;
        playersTurnBoolObj = FindObjectOfType<Board>();
        BoardObj = FindObjectOfType<Board>();
        //Store all the player 1 Objects in an array and convert it into a list
        player1Array = GameObject.FindGameObjectsWithTag("Player1");
        player1List = player1Array.ToList();
        //Store all the player 2 Objects in an array and convert it into a list
        player2Array = GameObject.FindGameObjectsWithTag("Player2");
        player2List = player2Array.ToList();


        print("Return Player1" + playersTurnBoolObj.playersTurnBool);


    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse position every frame
        tempMousePosition = Input.mousePosition;

        if (Mathf.Round(this.transform.position.x) % 2 == 0 || Mathf.Round(this.transform.position.y) % 2 == 0)
        {
            tempUpdatedPlayersPositionX = Mathf.Round(this.transform.position.x);
            tempUpdatedPlayersPositionY = Mathf.Round(this.transform.position.y);
        }


        

    }

    void OnMouseDown()
    {
        tempPlayersPositions = this.transform.position;
       
    }

    //Update  dragging with mouse
    void OnMouseDrag()
    {
        tempMousePosition.z = 16.5f; // Set this to be the distance you want the object to be placed in front of the camera.
        //Get relative position mapped between the camera and thedistance
        this.transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);

        BoardObj.tempPiece = null;
        BoardObj.tempPiece = this.gameObject;
        print(BoardObj.tempPiece.name);

    }

    //Calls when Finish Click
    void OnMouseUp()
    {
        // (If not white or black Tile)
        //If the position coordenades are odd
        //If a piece is dropped on the middle of any board line return to the place where it was dragged
        if (tempUpdatedPlayersPositionX % 2 != 0 || tempUpdatedPlayersPositionY % 2 != 0)
        {
            this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);

        }

        // (If Black Tile)
        //If the position coordinades are even move the piece to the center of the new tile
        if (tempUpdatedPlayersPositionY % 2 == 0 || tempUpdatedPlayersPositionY % 2 == 0)
        {
            

            
            if (this.gameObject.tag == "Player1")
            {
                DeagonalLimitMovementPlayer1();
            }
            else if(this.gameObject.tag == "Player2")
            {
                DeagonalLimitMovementPlayer2();
            }

            //ReturnToPreviousPlaceIfDropedOnAnotherPiecePlayer1();


            //Limits the movement for 1 tile for forward left or right deagonal     
            //if (playersTurnBoolObj.playersTurnBool)
            //{
            //DeagonalLimitMovementPlayer1();
            //print("Player1" + playersTurnBoolObj.playersTurnBool);

            //if (this.tag == "Player1")
            //{
            //    DeagonalLimitMovementPlayer1();

            //    print("Player1" + BoardObj.playersTurnBool);
            //}
            //else
            //{
            //    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
            //    print("Return Player2" + BoardObj.playersTurnBool);
            //}

            //}
            //if (playersTurnBoolObj.playersTurnBool)
            //{
            //DeagonalLimitMovementPlayer2();
            //print("Return Player1" + playersTurnBoolObj.playersTurnBool);

            //if (this.tag == "Player2")
            //{
            //    print("Player2" + BoardObj.playersTurnBool);
            //    DeagonalLimitMovementPlayer2();

            //}
            //else
            //{
            //    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
            //    print("Return Player1" + BoardObj.playersTurnBool);
            //}
            //}


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BoardObj.tempPiece.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
        BoardObj.tempPiece = null;
    }

    void ReturnToPreviousPlaceIfDropedOnAnotherPiecePlayer1()
    {
        //--------------------------------------------------------------PLAYER 1-----------------------------------------------------------------------------------
        
            //if a piece is on the next place from Player 1 return to the previus place
            for (int i = 0; i < player1List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player1List[i].transform.position.x && tempUpdatedPlayersPositionY == player1List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    print("Return");
                    break;
                }

            }
            //if a piece is on the next place from Player 2 return to the previus place
            for (int i = 0; i < player2List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player2List[i].transform.position.x && tempUpdatedPlayersPositionY == player2List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    print("Return");
                    break;
                }

            }
        /*
        //--------------------------------------------------------------PLAYER 2-----------------------------------------------------------------------------------
        if (this.gameObject.tag == "Player2")
        {
            //if a piece is on the next place from Player 1 return to the previus place
            for (int i = 0; i < player1List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player1List[i].transform.position.x && tempUpdatedPlayersPositionY == player1List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                }

            }
            //if a piece is on the next place from Player 2 return to the previus place
            for (int i = 0; i < player2List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player2List[i].transform.position.x && (tempUpdatedPlayersPositionY == player2List[i].transform.position.y))
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                }

            }
        }*/
    }

    //Limit themovements to checker account
    void DeagonalLimitMovementPlayer1()
    {
        //ReturnToPreviousPlaceIfDropedOnAnotherPiecePlayer1();
        //if player 1 move Down to Up --------------------------PLAYER 1---------------------------------------------------------------------------------------------------------------- 
                 /*   
            //if a piece is on the next place from Player 1 return to the previus place
            for (int i = 0; i < player1List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player1List[i].transform.position.x && tempUpdatedPlayersPositionY == player1List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    print("Return");
                    break;
                }

            }
            //if a piece is on the next place from Player 2 return to the previus place
            for (int i = 0; i < player2List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player2List[i].transform.position.x && tempUpdatedPlayersPositionY == player2List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    print("Return");
                    break;
                }

            }
            */

            
            //JUMP Right
            //If a piece is on the next place JUMP
            if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x + 4) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y + 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {
                    //if player 1 piece is on the next move, jump
                    if ((player1List[i].transform.position.x == (tempPlayersPositions.x + 2) && player1List[i].transform.position.y == (tempPlayersPositions.y + 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x + 4, tempPlayersPositions.y + 4, 6);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool1();

                        break;
                    }
                    //if player 2 piece is on the next move, jump and delete it
                    if ((player2List[i].transform.position.x == (tempPlayersPositions.x + 2) && player2List[i].transform.position.y == (tempPlayersPositions.y + 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x + 4, tempPlayersPositions.y + 4, 6);

                        MoveDeadPlayer2ToTheSide(i);

                        print("YOffset " + BoardObj.yOffset4DeadPieces);

                        BoardObj.yOffset4DeadPieces++;                        

                        //Remove from the list of pieces
                        player2List.Remove(player2List[i]);

                        print(player2List.Count);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool1();

                        break;
                    }
                    //if bad movement return to place
                    else
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    }
                }


            }
            //JUMP Left
            //If a piece is on the next place JUMP
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x - 4) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y + 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {

                    //if player 1 piece is on the next move, jump
                    if ((player1List[i].transform.position.x == (tempPlayersPositions.x - 2) && player1List[i].transform.position.y == (tempPlayersPositions.y + 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x - 4, tempPlayersPositions.y + 4, 6);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool1();

                        break;
                    }
                    //if player 2 piece is on the next move, jump and delete it
                    if ((player2List[i].transform.position.x == (tempPlayersPositions.x - 2) && player2List[i].transform.position.y == (tempPlayersPositions.y + 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x - 4, tempPlayersPositions.y + 4, 6);


                        MoveDeadPlayer2ToTheSide(i);

                        print("YOffset " + BoardObj.yOffset4DeadPieces);

                        BoardObj.yOffset4DeadPieces++;


                        //Remove from the list of pieces
                        player2List.Remove(player1List[i]);


                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool1();
                        
                        break;
                    }
                    //if bad movement return to place
                    else
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    }
                }

            }

            //Deagonal Right
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x + 2) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y + 2))
            {
                this.transform.position = new Vector3(tempUpdatedPlayersPositionX, tempUpdatedPlayersPositionY, 6);

                //Toogle Players turn Boolean
                TooglePlayersTurnBool1();

            }
            //Deagonal Left
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x - 2) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y + 2))
            {
                this.transform.position = new Vector3(tempUpdatedPlayersPositionX, tempUpdatedPlayersPositionY, 6);
                //Toogle Players turn Boolean
                TooglePlayersTurnBool1();
            }
            //Return to place if not moving correctly
            else
            {
                this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
            }
           
      
       




    }



    void DeagonalLimitMovementPlayer2()
    {
        //if player 2 move Up to Down ------------------------------------------------------------PLAYER 2------------------------------------------------------------------------------
                    //if a piece is on the next place from Player 1 return to the previus place
            for (int i = 0; i < player1List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player1List[i].transform.position.x && tempUpdatedPlayersPositionY == player1List[i].transform.position.y)
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                }

            }
            //if a piece is on the next place from Player 2 return to the previus place
            for (int i = 0; i < player2List.Count; i++)
            {
                if (tempUpdatedPlayersPositionX == player2List[i].transform.position.x && (tempUpdatedPlayersPositionY == player2List[i].transform.position.y))
                {
                    this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                }

            }


            //JUMP Right
            //If a piece is on the next place JUMP
            if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x + 4) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y - 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {
                    //if player 1 piece is on the next move, jump and delete it
                    if ((player1List[i].transform.position.x == (tempPlayersPositions.x + 2) && player1List[i].transform.position.y == (tempPlayersPositions.y - 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x + 4, tempPlayersPositions.y - 4, 6);

                        MoveDeadPlayer1ToTheSide(i);
                        print("YOffset " + BoardObj.yOffset4DeadPieces);
                        BoardObj.yOffset4DeadPieces++;

                        //Remove from the list of pieces
                        player1List.Remove(player1List[i]);


                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool2();

                        break;



                    }
                    //if player 2 piece is on the next move, jump
                    if (player2List[i].transform.position.x == (tempPlayersPositions.x + 2) && player2List[i].transform.position.y == (tempPlayersPositions.y - 2))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x + 4, tempPlayersPositions.y - 4, 6);

                        break;
                    }
                    //if bad movement return to place
                    else
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    }
                }
            }
            //JUMP Left
            //If a piece is on the next place JUMP
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x - 4) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y - 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {
                    //if player 1 piece is on the next move, jump and delete it
                    if ((player1List[i].transform.position.x == (tempPlayersPositions.x - 2) && player1List[i].transform.position.y == (tempPlayersPositions.y - 2)))

                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x - 4, tempPlayersPositions.y - 4, 6);

                        MoveDeadPlayer1ToTheSide(i);
                        print("YOffset " + BoardObj.yOffset4DeadPieces);

                        BoardObj.yOffset4DeadPieces++;

                        //Remove from the list of pieces
                        player1List.Remove(player1List[i]);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool2();

                        break;
                    }
                    //if player 2 piece is on the next move, jump
                    if ((player2List[i].transform.position.x == (tempPlayersPositions.x - 2) && player2List[i].transform.position.y == (tempPlayersPositions.y - 2)))
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x - 4, tempPlayersPositions.y - 4, 6);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool2();

                        break;
                    }
                    //if bad movement return to place
                    else
                    {
                        this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
                    }
                }

            }

            //Deagonal Right
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x + 2) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y - 2))
            {
                this.transform.position = new Vector3(tempUpdatedPlayersPositionX, tempUpdatedPlayersPositionY, 6);

                //Toogle Players turn Boolean
                TooglePlayersTurnBool2();

            }
            //Deagonal Left
            else if (tempUpdatedPlayersPositionX == (tempPlayersPositions.x - 2) && tempUpdatedPlayersPositionY == (tempPlayersPositions.y - 2))
            {
                this.transform.position = new Vector3(tempUpdatedPlayersPositionX, tempUpdatedPlayersPositionY, 6);

                //Toogle Players turn Boolean
                TooglePlayersTurnBool2();
            }
            //Return to place if not moving correctly
            else
            {
                this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
            }
        
    }

    //Limit the movement for an specific tile
    void LimitMovement(float a, float b)
    {
        if (this.transform.position.x == a && this.transform.position.y == b)
        {
            this.transform.position = new Vector3(tempPlayersPositions.x, tempPlayersPositions.y, 6);
        }
    }

    void MoveDeadPlayer2ToTheSide(int i)
    {
        //Move the dead piece to the side
        
        player2List[i].transform.position = new Vector3(16, BoardObj.yOffset4DeadPieces, 6);
    }
    void MoveDeadPlayer1ToTheSide(int i)
    {

        //Move the dead piece to the side
        player1List[i].transform.position = new Vector3(16, BoardObj.yOffset4DeadPieces, 6);
    }

    //Toogle the boolean
    void TooglePlayersTurnBool1()
    {
        if (playersTurnBoolObj.playersTurnBool)
        {
            playersTurnBoolObj.playersTurnBool = true;
        }
    }
    void TooglePlayersTurnBool2()
    {
        if (!playersTurnBoolObj.playersTurnBool)
        {
            playersTurnBoolObj.playersTurnBool = true;
        }
    }


}
