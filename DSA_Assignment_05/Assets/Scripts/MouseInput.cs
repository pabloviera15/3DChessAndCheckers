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

    }

    void OnMouseDown()
    {
        //Store the Previous Position of the piece
        BoardObj.tempPlayersPositionsX = Mathf.Round(this.transform.position.x);
        BoardObj.tempPlayersPositionsY = Mathf.Round(this.transform.position.y);
        //Store current clicked piece in a variable
        //BoardObj.tempPiece = null;
        BoardObj.tempPiece = this.gameObject;
        print(BoardObj.tempPiece.name);

    }
    //Update  dragging with mouse
    void OnMouseDrag()
    {
        tempMousePosition.z = 17; // Set this to be the distance you want the object to be placed in front of the camera.
        //Get relative position mapped between the camera and thedistance
        this.transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);

        if (Mathf.Round(this.transform.position.x) % 2 == 0 || Mathf.Round(this.transform.position.y) % 2 == 0)
        {
            BoardObj.tempUpdatedPlayersPositionX = Mathf.Round(this.transform.position.x);
            BoardObj.tempUpdatedPlayersPositionY = Mathf.Round(this.transform.position.y);
        }

        print("X Pos " + BoardObj.tempUpdatedPlayersPositionX + "Y Pos " + BoardObj.tempUpdatedPlayersPositionY);

    }

    //Calls when Finish Click
    void OnMouseUp()
    {
        // (If not white or black Tile)
        //If the position coordenades are odd
        //If a piece is dropped on the middle of any board line return to the place where it was dragged
        if (BoardObj.tempUpdatedPlayersPositionX % 2 != 0 || BoardObj.tempUpdatedPlayersPositionY % 2 != 0)
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);

        }

        // (If Black Tile)
        //If the position coordinades are even move the piece to the center of the new tile
        if (BoardObj.tempUpdatedPlayersPositionY % 2 == 0 || BoardObj.tempUpdatedPlayersPositionY % 2 == 0)
        {



            if (this.gameObject.tag == "Player1")
            {
                DeagonalShortMovePlayer1();
<<<<<<< HEAD
<<<<<<< HEAD
                //DeagonalJUMPPlayer1();
=======
                DeagonalJUMPPlayer1();
>>>>>>> parent of 9daa4bc... ScrewedVersion
=======
                DeagonalJUMPPlayer1();
>>>>>>> parent of 9daa4bc... ScrewedVersion
            }
            else if (this.gameObject.tag == "Player2")
            {
                DeagonalShortMovePlayer2();
<<<<<<< HEAD
<<<<<<< HEAD
               //DeagonalJUMPPlayer2();
=======
                DeagonalJUMPPlayer2();
>>>>>>> parent of 9daa4bc... ScrewedVersion
=======
                DeagonalJUMPPlayer2();
>>>>>>> parent of 9daa4bc... ScrewedVersion
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        BoardObj.tempPiece.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
=======
=======
>>>>>>> parent of 9daa4bc... ScrewedVersion
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 4))
        {
        }
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 4))
        {
        }
        else
        {
            BoardObj.tempPiece.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }
<<<<<<< HEAD
>>>>>>> parent of 9daa4bc... ScrewedVersion
=======
>>>>>>> parent of 9daa4bc... ScrewedVersion
        print("OUCH!!!");
    }


    //Limit themovements to checker account
    void DeagonalJUMPPlayer1()
    {
        //ReturnToPreviousPlaceIfDropedOnAnotherPiecePlayer1();
        //if player 1 move Down to Up --------------------------PLAYER 1----------------------------------------------------------------------------------------------------------------                 

        //JUMP Right
        //If a piece is on the next place JUMP
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 4))
        {
            for (int i = 0; i < player1List.Count; i++)
            {
                //if player 1 piece is on the next move, jump
                if ((player1List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player1List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX + 4, BoardObj.tempPlayersPositionsY + 4, 6);

                    //Toogle Players turn Boolean
                    TooglePlayersTurnBool1();

                    break;
                }
                //if player 2 piece is on the next move, jump and delete it
                if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

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

            }


        }

        //JUMP Left
        //If a piece is on the next place JUMP
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 4))
        {
            for (int i = 0; i < player1List.Count; i++)
            {

                //if player 1 piece is on the next move, jump
                if ((player1List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player1List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX - 4, BoardObj.tempPlayersPositionsY + 4, 6);

                    //Toogle Players turn Boolean
                    TooglePlayersTurnBool1();

                    break;
                }
                //if player 2 piece is on the next move, jump and delete it
                if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);


                    MoveDeadPlayer2ToTheSide(i);

                    print("YOffset " + BoardObj.yOffset4DeadPieces);

                    BoardObj.yOffset4DeadPieces++;


                    //Remove from the list of pieces
                    player2List.Remove(player1List[i]);


                    //Toogle Players turn Boolean
                    TooglePlayersTurnBool1();

                    break;
                }

            }

        }

    } 

    void DeagonalJUMPPlayer2()
    {
        //if player 2 move Up to Down ------------------------------------------------------------PLAYER 2------------------------------------------------------------------------------
         
            //JUMP Right
            //If a piece is on the next place JUMP
            if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {
                    //if player 1 piece is on the next move, jump and delete it
                    if ((player1List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player1List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2)))
                    {
                        this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

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
                    if (player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2))
                    {
                        this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX + 4, BoardObj.tempPlayersPositionsY - 4, 6);

                        break;
                    }
                    
                }
            }
            //JUMP Left
            //If a piece is on the next place JUMP
            else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 4))
            {
                for (int i = 0; i < player1List.Count; i++)
                {
                    //if player 1 piece is on the next move, jump and delete it
                    if ((player1List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player1List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2)))

                    {
                        this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

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
                    if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2)))
                    {
                        this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX - 4, BoardObj.tempPlayersPositionsY - 4, 6);

                        //Toogle Players turn Boolean
                        TooglePlayersTurnBool2();

                        break;
                    }
                    
                }
<<<<<<< HEAD
<<<<<<< HEAD

            }           
            
        
=======
=======
>>>>>>> parent of 9daa4bc... ScrewedVersion
            }     
>>>>>>> parent of 9daa4bc... ScrewedVersion
    }

    void DeagonalShortMovePlayer1()
    {
        //Deagonal Right
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            //Toogle Players turn Boolean
            TooglePlayersTurnBool1();

        }
        //Deagonal Left
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);
            //Toogle Players turn Boolean
            TooglePlayersTurnBool1();
        }
        else
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }
    }

    void DeagonalShortMovePlayer2()
    {
        //Deagonal Right
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            //Toogle Players turn Boolean
            TooglePlayersTurnBool2();

        }
        //Deagonal Left
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            //Toogle Players turn Boolean
            TooglePlayersTurnBool2();
        }
        else
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }
    }

    //Limit the movement for an specific tile
    void LimitMovement(float a, float b)
    {
        if (this.transform.position.x == a && this.transform.position.y == b)
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
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
