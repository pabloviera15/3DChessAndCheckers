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
    }
    //Update  dragging with mouse
    void OnMouseDrag()
    {
        // Set this to be the distance you want the object to be placed in front of the camera.
        tempMousePosition.z = 17; 
        //Get relative position mapped between the camera and thedistance
        this.transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);

        if (Mathf.Round(this.transform.position.x) % 2 == 0 || Mathf.Round(this.transform.position.y) % 2 == 0)
        {
            BoardObj.tempUpdatedPlayersPositionX = Mathf.Round(this.transform.position.x);
            BoardObj.tempUpdatedPlayersPositionY = Mathf.Round(this.transform.position.y);
        }
    }

    //Calls when Finish Click
    void OnMouseUp()
    {
        //Limit the board
        if (BoardObj.tempUpdatedPlayersPositionX >= 0 && BoardObj.tempUpdatedPlayersPositionX <= 14 && BoardObj.tempUpdatedPlayersPositionY <= 14 && BoardObj.tempUpdatedPlayersPositionY >= 0)
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
            if (BoardObj.tempUpdatedPlayersPositionX % 2 == 0 || BoardObj.tempUpdatedPlayersPositionY % 2 == 0)
            {
                //TO-DO Do the double jump
                //--------------------------------------------------------------------------PLAYER 1----------------------------------------------------------------------------------------------------------------
                if (playersTurnBoolObj.playersTurnBool == true)
                {
                    //If Player 1 turn and move a Player1 piece move according to logic
                    if (this.gameObject.tag == "Player1")
                    {
                        //Calculate next one square space movement
                        DeagonalShortMovePlayer1();
                        //Calculate jump if there is an enemy or ally piece                        
                        while(playersTurnBoolObj.playersTurnBool == true)
                        {
                            DeagonalJUMPPlayer1();

                            break;
                        }
                        print(BoardObj.playersTurnBool);
                    }
                    //If Player 1 turn and move a Player2 piece return to place
                    else if (this.gameObject.tag == "Player2")
                    {
                        this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
                    }
                }
                //--------------------------------------------------------------------------PLAYER 2----------------------------------------------------------------------------------------------------------------
                else if (playersTurnBoolObj.playersTurnBool == false)
                {
                    if (this.gameObject.tag == "Player2")
                    {

                        //Calculate next one square space movement
                        DeagonalShortMovePlayer2();
                        //Calculate jump if there is an enemy or ally piece
                        while (playersTurnBoolObj.playersTurnBool == true)
                        {
                            DeagonalJUMPPlayer2();
                            break;
                        }
                        print(BoardObj.playersTurnBool);
                    }
                    else if (this.gameObject.tag == "Player1")
                    {
                        this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
                    }
                }
            }
            //If any player touches the last tile of the opposite side they will WIN!
            if (BoardObj.tempUpdatedPlayersPositionY == 14 && this.gameObject.tag == "Player1")
            {
                print("You WON!!");
            }
            else if (BoardObj.tempUpdatedPlayersPositionY == 0 && this.gameObject.tag == "Player2")
            {
                print("You WON!!");
            }
        }
        //If movement outside the board return to the place it was grabbed
        else
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }

    }

    //If two pieces are in the same place collide and return the piece to where it was dragged
    private void OnTriggerEnter(Collider other)
    {
        //--------------------------------------------------------------------------PLAYER 1----------------------------------------------------------------------------------------------------------------  
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 4))
        { }
        //--------------------------------------------------------------------------PLAYER 2----------------------------------------------------------------------------------------------------------------  
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 4))
        { }
        //if they are in the same place return to the previous move
        else
        {
            BoardObj.tempPiece.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }
    }

    //Limit themovements to checker account
    void DeagonalJUMPPlayer1() 
    {
        //--------------------------------------------------------------------------PLAYER 1----------------------------------------------------------------------------------------------------------------                 
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
                }
                //if player 2 piece is on the next move, jump and delete it
                else if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2))
                    && player1List[i].transform.position.x != BoardObj.tempPlayersPositionsX + 4 && player1List[i].transform.position.x != BoardObj.tempPlayersPositionsX + 4)
                {
                    this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

                    MoveDeadPlayer2ToTheSide(i);

                    BoardObj.yOffset4DeadPieces++;

                    DeagonalJUMPPlayer1();
                }
            }
        }

        //JUMP Left
        //If a piece is on the next place JUMP
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 4) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 4))
        {
            for (int i = 0; i < player1List.Count; i++)
            {

                //if player 1 piece is on the next move, jump
                if ((player1List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player1List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX - 4, BoardObj.tempPlayersPositionsY + 4, 6);
                }
                //if player 2 piece is on the next move, jump and delete it
                else if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY + 2))
                       && player1List[i].transform.position.x == BoardObj.tempPlayersPositionsX - 4 && player1List[i].transform.position.x == BoardObj.tempPlayersPositionsX + 4)
                {
                    this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

                    MoveDeadPlayer2ToTheSide(i);

                    BoardObj.yOffset4DeadPieces++;

                    DeagonalJUMPPlayer1();
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

                    BoardObj.yOffset4DeadPieces++;


                }
                //if player 2 piece is on the next move, jump
                if (player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX + 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2))
                {
                    this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX + 4, BoardObj.tempPlayersPositionsY - 4, 6);

                }
            }
            TooglePlayersTurnBool2();
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

                    BoardObj.yOffset4DeadPieces++;
                }
                //if player 2 piece is on the next move, jump
                if ((player2List[i].transform.position.x == (BoardObj.tempPlayersPositionsX - 2) && player2List[i].transform.position.y == (BoardObj.tempPlayersPositionsY - 2)))
                {
                    this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX - 4, BoardObj.tempPlayersPositionsY - 4, 6);

                }

            }
            TooglePlayersTurnBool2();
        }
    }

    //Calculate next deagonal move for the Player 1
    void DeagonalShortMovePlayer1()
    {
        //Deagonal Right
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            TooglePlayersTurnBool1();
        }
        //Deagonal Left
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY + 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            TooglePlayersTurnBool1();
        }
        else
        {
            this.transform.position = new Vector3(BoardObj.tempPlayersPositionsX, BoardObj.tempPlayersPositionsY, 6);
        }
    }

    //Calculate next deagonal move for the Player 2
    void DeagonalShortMovePlayer2()
    {
        //Deagonal Right move if there is not another piece
        if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX + 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            TooglePlayersTurnBool2();
        }
        //Deagonal Left move if there is not another piece
        else if (BoardObj.tempUpdatedPlayersPositionX == (BoardObj.tempPlayersPositionsX - 2) && BoardObj.tempUpdatedPlayersPositionY == (BoardObj.tempPlayersPositionsY - 2))
        {
            this.transform.position = new Vector3(BoardObj.tempUpdatedPlayersPositionX, BoardObj.tempUpdatedPlayersPositionY, 6);

            TooglePlayersTurnBool2();
        }
        //If there is not a possible move return to previous place
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

    //Toogle the boolean make it false
    void TooglePlayersTurnBool1()
    {
        playersTurnBoolObj.playersTurnBool = false;
    }
    //Toogle the boolean make it true
    void TooglePlayersTurnBool2()
    {
        playersTurnBoolObj.playersTurnBool = true;
    }


}