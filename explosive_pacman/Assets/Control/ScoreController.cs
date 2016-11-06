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

    //[SyncVar(hook = "modifyScore")]
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

        /*if (!isServer)
        {
            return;
        }*/

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
                print("Player " + netId + " score = " + current_score);
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
        }
    }

    IEnumerator JustHurt()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
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
