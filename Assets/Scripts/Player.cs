using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//no monobehaviour because they're for things that get placed in a scene. This is just data. 
public class Player
{
    public int PlayerNumber { get; private set; }
    public bool IsJoined { get; set; }
    //constructor
    public Player(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }
}