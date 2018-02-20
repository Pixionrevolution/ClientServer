using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
   //public static Player[] instanceP = new Player[Constants.MAX_PLAYERS];

    
/*
    private void Awake()
    {
        for (int i = 1; i< Constants.MAX_PLAYERS; i++)
        {
            instance[i] = new Player();
        }
    }*/

    //General
    public string Username;
    public int index;

    // position
    public float posX;
    public float posZ;
    public float posY;

    // Rotation
    public float rotX;
    public float rotY;
    public float rotZ;
    public float rotW;

}