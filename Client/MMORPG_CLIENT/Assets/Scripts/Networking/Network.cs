using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class Network : MonoBehaviour
{

    public static Network instance;
    public static Player[] instanceP = new Player[Constants.MAX_PLAYERS];

    [Header("Network Settings")]
    public string ServerIP ="10.53.50.104";
    public int ServerPort = 5500;
    public bool isConnected;
    public static Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] _buffer = new byte[1024];
    public bool shouldHandleData;
    public byte[] data;

    public Player[] Players = instanceP;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        ConnectGameServer();
    
        for (int i = 1; i < Constants.MAX_PLAYERS; i++)
        {
            instanceP[i] = new Player();
        }   
    }

    void ConnectGameServer()
    {      
        _client.BeginConnect(ServerIP, ServerPort, new AsyncCallback(ConnectCallback), _client);
    }

    void ConnectCallback(IAsyncResult result)
    {
        _client.EndConnect(result);
        isConnected = true;
        while(isConnected)
        {
            OnReceive();
        }
    }

    private void Update()
    {
        if (shouldHandleData)
        {          
            ClientHandleData.instance.HandleData(data);
            shouldHandleData = false;
        }
    }

    private void OnApplicationQuit()
    {
       
        isConnected = false;
        ClientSendData.instance.SendDisconnect();
        _client.Disconnect(true);
      
    }

    void OnReceive(/*IAsyncResult result*/)
    {    
        byte[] _sizeInfo = new byte[4];
        byte[] _receivedbuffer = new byte[1024];

        int totalread = 0, currentread = 0;
        try
        {
            currentread = totalread = _client.Receive(_sizeInfo);
            if(totalread <= 0 )
            {
                isConnected = false;
                Debug.Log("Your are not connected to the server ");
            }
            else
            {
                while(totalread< _sizeInfo.Length && currentread >0)
                {
                    currentread = _client.Receive(_sizeInfo, totalread, _sizeInfo.Length - totalread, SocketFlags.None);
                    totalread += currentread;
                }

                int messagesize = 0;
                messagesize |=  _sizeInfo[0];
                messagesize |= (_sizeInfo[1] << 8);
                messagesize |= (_sizeInfo[2] << 16);
                messagesize |= (_sizeInfo[3] << 24);

                data = new byte[messagesize];
                totalread = 0;
                currentread =  _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                totalread = currentread;

                while (totalread < messagesize && currentread >0)
                {
                    currentread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                    totalread += currentread;
                }
                Debug.Log(data[0]);
                shouldHandleData = true;
                
            }
        }
        catch
        {
            isConnected = false;
            Debug.Log("You are not connected to the server anymore");
            
        }
    }


}
