using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEditor.Build.Content;
using UnityEngine;

static public class NetworkServerProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);
        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if(signifier != ClientToServerSignifiers.SendingScreen)
        {
            string[] pos = csv[1].Split('_');
            Vector2 Ballonpos = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
            switch (signifier)
            {
                case ClientToServerSignifiers.BallonSpawned:
                    gameLogic.OnSpawnedBallon(Ballonpos, clientConnectionID);
                    break;
                case ClientToServerSignifiers.BallonPopped:
                    gameLogic.OnPoppedBallon(Ballonpos, clientConnectionID);
                    break;

            }
        }
        else
        {

            gameLogic.OnScreenRecieved(csv[2], int.Parse(csv[1]));

        }




    }
    static public void SendMessageToClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        networkServer.SendMessageToClient(msg, clientConnectionID, pipeline);
    }

    #endregion

    #region Connection Events

    static public void ConnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client connection, ID == " + clientConnectionID);
        int MainPlayer = networkServer.FindMainPlayer();
        if (clientConnectionID != MainPlayer)
        {
            gameLogic.OnGettingScreen(clientConnectionID, MainPlayer);
        }
        else
        {
            gameLogic.SettingMainPlayer(clientConnectionID);
        }
    }
    static public void DisconnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client disconnection, ID == " + clientConnectionID);

        gameLogic.SettingMainPlayer(networkServer.FindMainPlayer());

    }

    #endregion

    #region Setup
    static NetworkServer networkServer;
    static GameLogic gameLogic;

    static public void SetNetworkServer(NetworkServer NetworkServer)
    {
        networkServer = NetworkServer;
    }
    static public NetworkServer GetNetworkServer()
    {
        return networkServer;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion
}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int BallonSpawned = 0;
    public const int BallonPopped = 1;
    public const int SendingScreen = 2;
}

static public class ServerToClientSignifiers
{
    public const int SettingMainPlayer = -1;
    public const int OtherBallonSpawned = 0;
    public const int OtherBallonPopped = 1;
    public const int GettingScreen = 2;
    public const int SettingScreen = 3;
}

#endregion

