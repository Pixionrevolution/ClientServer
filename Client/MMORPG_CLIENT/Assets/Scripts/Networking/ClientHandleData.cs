using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

public class ClientHandleData : MonoBehaviour
{

    public static ClientHandleData instance;

    private delegate void Packet_(byte[] Data);
    private Dictionary<int, Packet_> Packets;


    public GameObject _LoginWindow;

    [Header("Player prefs")]
    public GameObject _playerPrefs;

    public void InitMessages()
    {
        Packets = new Dictionary<int, Packet_>();
        Packets.Add((int)Enumerations.ServerPackets.SAlertMsg, HandleAlertMsg);
        Packets.Add((int)Enumerations.ServerPackets.SPlayerData, HandlePlayerData);
        Packets.Add((int)Enumerations.ServerPackets.SPlayersMovement, HandleMovement);
        Packets.Add((int)Enumerations.ServerPackets.SPlayerDisconnect, HandleDisconnect);

    }

    private void Awake()
    {
        instance = this;
        InitMessages();
    }

    public void HandleData(byte[] data)
    {
        int packetnum;
        Packet_ Packet;
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();

        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();
        buffer = null;
        Debug.Log(packetnum);

        if (packetnum == 0)
            return;
        if (Packets.TryGetValue(packetnum, out Packet))
        {
            Packet.Invoke(data);
        }
    }

    void HandleAlertMsg(byte[]data)
    {
        int packetnum;
        
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();

        string AlertMsg = buffer.ReadString();

        Debug.Log(AlertMsg);
    }

    void HandleMovement(byte[] _data)
    {
        Debug.Log("Data is player movement.");
        int packetnum;
        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        //buffer.WriteBytes(data);
        buffer.WriteBytes(_data);

        packetnum = buffer.ReadInteger();

        // PLayer Info
        int index = buffer.ReadInteger();
        // PLayer Position
        float x = buffer.ReadFloat();
        float y = buffer.ReadFloat();
        float z = buffer.ReadFloat();

        // Player Rotation
        float rotX = buffer.ReadFloat();
        float rotY = buffer.ReadFloat();
        float rotZ = buffer.ReadFloat();
        float rotW = buffer.ReadFloat();

        GameObject _netPlayer;
        if(_netPlayer = GameObject.Find("Player "  + index + " : " + Network.instanceP[index].Username))
        {
            _netPlayer.GetComponent<Transform>().position = new Vector3(x, y, z);
            _netPlayer.GetComponent<Transform>().rotation = new Quaternion(rotX, rotY, rotZ, rotW);
        }

        else
        {
        }
    }

    void HandlePlayerData(byte[]data)
    {
        int packetnum;

        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();

        int i = buffer.ReadInteger();

        if (Globals.instance.MyIndex < 1)
        {
            Globals.instance.MyIndex = i;
            ClientSendData.instance._index = Globals.instance.MyIndex;

        }

        _LoginWindow.SetActive(false);

       
        Network.instanceP[i].index = i;
        General.instance.SetPlayerX(i, buffer.ReadFloat());
        General.instance.SetPlayerY(i, buffer.ReadFloat());
        General.instance.SetPlayerZ(i, buffer.ReadFloat());

        Network.instanceP[i].Username = buffer.ReadString();

        _playerPrefs = Instantiate(_playerPrefs, new Vector3(General.instance.GetPlayerX(i), General.instance.GetPlayerY(i), General.instance.GetPlayerZ(i)), Quaternion.identity);
        _playerPrefs.GetComponent<NetPlayer>().Index = i;
        _playerPrefs.name = "Player " + i + " : " + Network.instanceP[i].Username;

        
        

    }

    void HandleDisconnect(byte[] data)
    {
        int packetnum;

        KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
        buffer.WriteBytes(data);
        packetnum = buffer.ReadInteger();

        int index = buffer.ReadInteger();
        int connected = buffer.ReadInteger();
        if (connected == 0)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if(index != i)
                {
                    
                    Destroy(Network.instanceP[index]);
                }
                
            }
        }

        
      
   


    }

}
