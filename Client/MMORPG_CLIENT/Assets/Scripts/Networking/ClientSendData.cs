using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClientSendData : MonoBehaviour
{
    public static ClientSendData instance;
    public Network network;


    [Header("Registration")]
    public Text _username;
    public Text _password;

    [Header("Login")]
    public Text _loginUser;
    public Text _loginPass;

    [Header("Connecté")]
    public int _index;
    public int _connected =1;

    // Use this for initialization
    void Awake()
    {
        instance = this;
       
    }

   
    public void SendDataToServer(byte[] data)
    {
        Network._client.Send(data);
    }


    public void SendNewAccount()
    {
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteInteger((int)Enumerations.ClientPackets.CNewAccount);
        buffer.WriteString(_username.text);
        buffer.WriteString(_password.text);
        SendDataToServer(buffer.ToArray());
        buffer = null;
    }

    public void SendLogin()
    {
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteInteger((int)Enumerations.ClientPackets.CHandleLogin);
        buffer.WriteString(_loginUser.text);
        buffer.WriteString(_loginPass.text);
        SendDataToServer(buffer.ToArray());
        buffer = null;
    }

    public void SendDisconnect()
    {
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteInteger((int)Enumerations.ClientPackets.CHandleDisconnect);
        buffer.WriteInteger(_index);
        buffer.WriteInteger(_connected);
        SendDataToServer(buffer.ToArray());
        buffer = null;
    }

    public void SendMovement(float x, float y, float z, float rotX, float rotY, float rotZ, float rotW)
    {
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteInteger((int)Enumerations.ClientPackets.CHandleMovement);

        // Send position and rotation

        buffer.WriteFloat(x);
        buffer.WriteFloat(y);
        buffer.WriteFloat(z);

        buffer.WriteFloat(rotX);
        buffer.WriteFloat(rotY);
        buffer.WriteFloat(rotZ);
        buffer.WriteFloat(rotW);

        SendDataToServer(buffer.ToArray());
        buffer = null;

    }
    
}
