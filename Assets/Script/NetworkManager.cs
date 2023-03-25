using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Colyseus;


public class NetworkManager : MonoBehaviour
{
    private static ColyseusRoom<MyRoomState> _room = null;
    private static ColyseusClient _client = null;
    private static string HOST_ADDRESS = "ws://localhost:2567";
    private static string GAME_NAME = "my_room";
    public void Init()
    {
        _client = new ColyseusClient(HOST_ADDRESS);
    }

    public async Task JoinOrCreateGame()
    {
        _room = await Client.JoinOrCreate<MyRoomState>(GAME_NAME);
    }
    public ColyseusClient Client
    {
        get
        {
            // Initialize Colyseus client, if the client has not been initiated yet or input values from the Menu have been changed.
            if (_client == null )
            {
                Init();
            }
            return _client;
        }
    }
    public ColyseusRoom<MyRoomState> GameRoom
    {
        get
        {
            if (_room == null)
            {
                Debug.LogError("Room hasn't been initialized yet!");
            }
            return _room;
        }
    }

    public void SendPlayerPosition(Vector3 position)
    {
        GameRoom.Send("position", new { x = position.x, y = position.y , z = position.z});
    }
}
