using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Class which controls the score for the character
//Attach this to Character only
public class ScoreController : NetworkBehaviour {

    public int start_score = 10;
    public int win_score = 20;
    public int score_delta = 5;
    public int invulTime = 2;

    private int current_score;

    //1 = Attacker or 0 = Runner
    private int status;
    private bool invulnerable;

    // Initialization function
    void Start () {
        current_score = start_score;
        status = 0;
        invulnerable = false;
    }

    public void modifyScore(int way)
    {

        if (way == 0)
        {
            if (!invulnerable)
            {
                current_score -= score_delta;
                if (current_score <= 0)
                {
                    loseGame();
                }
                else
                {
                    // called on the Server, invoked on the Clients
                    gameObject.GetComponent<PlayerRandomSpawner>().RpcRespawn();
                        
                }
                //print("Player " + netId + " score = " + current_score);  
                StartCoroutine(JustHurt());
            }

        }
        else if (way == 1)
        {
            current_score += score_delta;
            if (current_score >= win_score)
            {
                winGame();
            }
            //print("Player " + netId + " score = " + current_score);
        }
    }

    IEnumerator JustHurt()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
    }

    public void displayScore()
    {
        var scores = GameObject.FindGameObjectWithTag("Server").GetComponent<Prototype.NetworkLobby.LobbyManager>().getScores();
        string scores_string = "";
        print(scores.Count);
        for (int i = 0; i < scores.Count; i++)
            scores_string = scores_string + scores[i] + " ";

        print("Player " + netId + " " + scores_string);
    }

    public void winGame()
    {
        current_score = win_score;
        Debug.Log("Player " + netId + " WON");
        Cmd_DestroyAllExceptOne(netId);
    }

    public void loseGame()
    {
        current_score = 0;
        Debug.Log("Player " + netId + " LOST");
        GameObject.FindGameObjectWithTag("Server").GetComponent<Prototype.NetworkLobby.LobbyManager>().removePlayer(gameObject);
        Cmd_DestroyThis(netId);
    }

    [Command]
    void Cmd_DestroyThis(NetworkInstanceId netID)
    {
        GameObject theObject = NetworkServer.FindLocalObject(netID);
        NetworkServer.Destroy(theObject);
    }

    [Command]
    public void Cmd_DestroyAllExceptOne(NetworkInstanceId netID)
    {
        Dictionary<NetworkInstanceId, NetworkIdentity> map = new Dictionary<NetworkInstanceId, NetworkIdentity>();

        foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> entry in NetworkServer.objects)
        {
            map.Add(entry.Key, entry.Value);
        }

        foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> entry in map)
        {
            if (entry.Key.Value != netID.Value)
            {
                GameObject theObject = NetworkServer.FindLocalObject(entry.Key);
                NetworkServer.Destroy(theObject);
            }
        }
    }

    public int getStatus()
    {
        return status;
    }

    public void setStatus(int status)
    {
        this.status = status;
    }

    public bool getInvulnerable()
    {
        return invulnerable;
    }

    public void setInvulnerable(bool invulnerable)
    {
        this.invulnerable = invulnerable;
    }

    public int getCurrentScore()
    {
        return current_score;
    }
}
