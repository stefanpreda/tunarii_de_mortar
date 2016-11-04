using UnityEngine;
using System.Collections.Generic;


//This is a support class for NetworkManager
//Attach this to NetworkManager only
public class NetworkManagerData : MonoBehaviour {

    private List<GameObject> player_list;

    //Initialization function
	void Start ()
    {
        player_list = new List<GameObject>();
        var players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject p in players)
        {
            player_list.Add(p);
        }
    }

    //Add GameObject to list
    void OnPlayerConnected()
    {
        player_list.Clear();
        var players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject p in players)
        {
            player_list.Add(p);
        }
    }

    //Remove GameObject from list
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        GameObject player_object = GameObject.FindGameObjectWithTag("Character_" + player.guid);
        if (player_object != null)
            Destroy(player_object);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    public List<GameObject> getPlayerList()
    {
        return player_list;
    }

    void Update()
    {
        //TODO: Switch between attacker and runner every X seconds
    }
}
