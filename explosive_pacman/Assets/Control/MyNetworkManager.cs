using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// This is the server (Thanks 1livv for help)
public class MyNetworkManager : NetworkManager {

    List<NetworkConnection> players = new List<NetworkConnection>();
    int once = 0;
    public float startup_time = 3.0f;
    public float switch_time = 10.0f;

    override
    public void OnServerConnect(NetworkConnection conn)
    {
        players.Add(conn);
        if (once == 0)
        {
            InvokeRepeating("SwitchRole", startup_time, switch_time);
            once = 1;
        }
        print("Connected");
        base.OnServerConnect(conn);
    }

    override
    public void OnServerDisconnect(NetworkConnection conn)
    {
        players.Remove(conn);
        print("Disconnected");
        base.OnServerDisconnect(conn);
    }

    private void SwitchRole()
    {
        if (players.Count == 1)
        {
            players[0].playerControllers[0].gameObject.GetComponent<ScoreController>().setStatus(1);
            return;
        }
        while (true)
        {
            int index = Random.Range(0, players.Count);
            var obj = players[index].playerControllers[0].gameObject;
            if (obj.GetComponent<ScoreController>().getStatus() == 0)
            {
                for (int i = 0; i < players.Count; i++)
                    players[i].playerControllers[0].gameObject.GetComponent<ScoreController>().setStatus(0);
                obj.GetComponent<ScoreController>().setStatus(1);
                Debug.Log("Attacker index= " + index);
                break;
            }
        }

    }
}
