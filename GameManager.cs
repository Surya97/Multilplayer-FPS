using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static string player_ID = "Player ";

    private static Dictionary<string, PlayerHealth> _players = new Dictionary<string, PlayerHealth>();

    public static void RegisterPlayer(string netId, PlayerHealth player)
    {
        _players.Add(player_ID + netId, player);
        player.transform.name = player_ID + netId;
    }

    public static void DeRegisterPlayer(string _playerID)
    {
        _players.Remove(_playerID);
    }

    public static PlayerHealth GetPlayer(string _playerID)
    {
        return _players[_playerID];
    }
}
