using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int yOffset4DeadPieces = -1;

    public bool playersTurnBool = true;

    public GameObject tempPiece = null;

    //Declare variable to store the previous position of the player
    public float tempPlayersPositionsX;
    public float tempPlayersPositionsY;
    //Declare variable to store the actual position of the player
    public float tempUpdatedPlayersPositionX;
    public float tempUpdatedPlayersPositionY;

}
