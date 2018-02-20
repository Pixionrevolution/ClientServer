using UnityEngine;
using System.Collections;

public class Enumerations : MonoBehaviour
{

    public static Enumerations instance;
    
    private void Awake()
    {
        instance = this;
    }
    public enum ServerPackets
    {
        SAlertMsg = 1,
        SPlayerData,
        SPlayersMovement,
        SPlayerDisconnect

    }

    public enum ClientPackets
    {
        CNewAccount = 1,
        CHandleLogin,
        CHandleMovement,
        CHandleDisconnect
    }

}
