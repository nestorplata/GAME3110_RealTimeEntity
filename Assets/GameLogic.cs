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
        //testing
        if (Input.GetKeyDown(KeyCode.A))
            OnGettingScreen(0, 0);
    }

    public void OnSpawnedBallon(Vector2 BallonPos, List<int> RecieverIDs)
    {
        foreach(var ID in RecieverIDs)
        {
            SendMessageToClient(ServerToClientSignifiers.OtherBallonSpawned, BallonPos, ID);
        }
    }
    public void OnPoppedBallon(string BallonID, List<int> RecieverIDs)
    {
        foreach (var ID in RecieverIDs)
        {
            SendMessageToClient(ServerToClientSignifiers.OtherBallonPopped, BallonID, ID);
        }
    }

    //Toogle new player Connection
    public void OnScreenRecieved(string BallonsData, int RecieverID)
    {
        SendMessageToClient(ServerToClientSignifiers.SettingScreen, BallonsData, RecieverID);
    }

    public void OnGettingScreen(int dataID, int RecieverID)
    {
        SendMessageToClient(ServerToClientSignifiers.GettingScreen, dataID+"", RecieverID);
    }


    internal void SettingMainPlayer(int RecieverID)
    {
        SendMessageToClient(ServerToClientSignifiers.SettingMainPlayer, "", RecieverID);
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
