using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameLogic : MonoBehaviour
{
    
    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
    }

    public void OnPoppedBallon(Vector2 BallonPos, int RecieverID)
    {
        SendMessageToClient(ServerToClientSignifiers.OtherBallonPopped, BallonPos);
    }

    public void OnSpawnedBallon(Vector2 BallonPos, int RecieverID)
    {
        SendMessageToClient(ServerToClientSignifiers.OtherBallonSpawned, BallonPos);
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

    public void SendMessageToClient(int signifier, Vector2 pos, int ID=0)
    {
        NetworkServerProcessing.SendMessageToClient(signifier + "," + pos.x + "_" + pos.y,ID,  TransportPipeline.ReliableAndInOrder);
    }



}
