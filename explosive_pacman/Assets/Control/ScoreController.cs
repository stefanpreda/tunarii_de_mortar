using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//Class which controls the score for the character
//Attach this to Character only
public class ScoreController : NetworkBehaviour {

    public int start_score = 50;
    public int win_score = 100;
    public int score_delta = 5;
    public int invulTime = 3;

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
                    current_score = 0;
                    Debug.Log("Player " + netId + " LOST");
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
                current_score = win_score;
                Debug.Log("Player " + netId + " WON");
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

    /*TODO: Score must be requested regularly from the server to be accurate(maybe use void update())
     * Also get a reference to LobbyManager somewhat like this
    */
    public void displayScore()
    {
        var scores = GameObject.FindGameObjectWithTag("Server").GetComponent<Prototype.NetworkLobby.LobbyManager>().getScores();
        string scores_string = "";
        print(scores.Count);
        for (int i = 0; i < scores.Count; i++)
            scores_string = scores_string + scores[i] + " ";

        print("Player " + netId + " " + scores_string);
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
