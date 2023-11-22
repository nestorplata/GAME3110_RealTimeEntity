using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameLogic : MonoBehaviour
{
    
    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            OnGettingScreen(0, 0);
    }

    public void OnPoppedBallon(string BallonID, List<int> RecieverIDs)
    {
        foreach (var ID in RecieverIDs)
        {
            SendMessageToClient(ServerToClientSignifiers.OtherBallonSpawned, BallonID, ID);
        }
    }

    public void OnSpawnedBallon(Vector2 BallonPos, List<int> RecieverIDs)
    {
        foreach(var ID in RecieverIDs)
        {
            SendMessageToClient(ServerToClientSignifiers.OtherBallonSpawned, BallonPos, ID);
        }
    }

    public void OnGettingScreen(int dataID, int RecieverID)
    {
        NetworkServerProcessing.SendMessageToClient(ServerToClientSignifiers.GettingScreen+","+dataID, RecieverID, TransportPipeline.ReliableAndInOrder);

    }

    public void OnScreenRecieved(string BallonsData, int RecieverID)
    {
        NetworkServerProcessing.SendMessageToClient(ServerToClientSignifiers.SettingScreen + "," + BallonsData, RecieverID, TransportPipeline.ReliableAndInOrder);
    }
    internal void SettingMainPlayer(int RecieverID)
    {
        NetworkServerProcessing.SendMessageToClient(ServerToClientSignifiers.SettingMainPlayer+"", RecieverID, TransportPipeline.ReliableAndInOrder);
    }

    public void SendMessageToClient(int signifier, Vector2 pos, int ID)
    {
        NetworkServerProcessing.SendMessageToClient(signifier + "," + pos.x + "_" + pos.y,ID,  TransportPipeline.ReliableAndInOrder);
    }

    public void SendMessageToClient(int signifier, string message, int ID = 0)
    {
        NetworkServerProcessing.SendMessageToClient(signifier + "," + message, ID, TransportPipeline.ReliableAndInOrder);
    }



}
