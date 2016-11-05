using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

//This class is used to sync data across clients
public class DataIntegrity : NetworkBehaviour
{

    private List<GameObject> player_list;

    //Initialization function
    void Start()
    {
        if (Network.isServer)
        {
            player_list = new List<GameObject>();
            if (gameObject != null)
                player_list.Add(gameObject);
            return;
        }

        if (Network.isClient)
        {
            CmdAddToList(gameObject);
        }
    }

    void OnDestroy()
    {

        if (Network.isServer)
        {
            player_list = null;
        }

        if (Network.isClient)
        {
            CmdRemoveFromList(gameObject);
        }
    }

    [Command]
    void CmdAddToList(GameObject obj)
    {
        // this code is only executed on the server
        RpcAddToList(obj); // invoke Rpc on all clients
    }

    [Command]
    void CmdRemoveFromList(GameObject obj)
    {
        RpcRemoveFromList(obj);
    }

    [ClientRpc]
    void RpcAddToList(GameObject obj)
    {
        // this code is executed on all clients
        player_list.Add(obj);
    }

    [ClientRpc]
    void RpcRemoveFromList(GameObject obj)
    {
        // this code is executed on all clients
        player_list.Remove(obj);
    }

    public List<GameObject> getPlayerList()
    {
        return player_list;
    }
}
