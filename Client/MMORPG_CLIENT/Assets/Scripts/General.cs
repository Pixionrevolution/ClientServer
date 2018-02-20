using UnityEngine;
using System.Collections;

public class General : MonoBehaviour
{
    public static General instance;

    private void Awake()
    {
        instance = this;
    }

    public float GetPlayerX(int index)
    {
        //prevent out of bounds
        if (index <= 0 || index > Constants.MAX_PLAYERS)
        {
            return 0;
        }
        
        return Network.instanceP[index].posX;
    }
    public float GetPlayerY(int index)
    {
        //prevent out of bounds
        if (index <= 0 || index > Constants.MAX_PLAYERS)
        {
            return 0;
        }
        
        return Network.instanceP[index].posY;
    }
    public float GetPlayerZ(int index)
    {
        //prevent out of bounds
        if (index <= 0 || index > Constants.MAX_PLAYERS)
        {
            return 0;
        }
        
        return Network.instanceP[index].posZ;
    }
    public void SetPlayerX(int index, float x)
    {
        if (index <= 0 || index > Constants.MAX_PLAYERS)
            return;

        
         Network.instanceP[index].posX = x;
    }
    public void SetPlayerY(int index, float y)
    {
        if (index <= 0 || index > Constants.MAX_PLAYERS)
            return;

        
        Network.instanceP[index].posY = y;
    }
    public void SetPlayerZ(int index, float z)
    {
        if (index <= 0 || index > Constants.MAX_PLAYERS)
            return;

        
        Network.instanceP[index].posZ = z;
    }

}
