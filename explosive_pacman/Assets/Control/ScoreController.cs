using UnityEngine;
using UnityEngine.Networking;

//Class which controls the score for the character
//Attach this to Character only
public class ScoreController : NetworkBehaviour {

    public int start_score = 50;
    public int win_score = 100;
    public int score_delta = 5;

    [SyncVar(hook = "modifyScore")]
    private int current_score;

    //Attacker or Runner
    private int status;

	// Initialization function
	void Start () {
        current_score = start_score;
	}

    public void modifyScore(int way)
    {

        if (!isServer)
        {
            return;
        }

        if (way == 0)
        {
            current_score -= score_delta;
            if (current_score <= 0)
            {
                current_score = 0;
                Debug.Log("LOST");
            }
        }
        else if (way == 1)
        {
            current_score += score_delta;
            if (current_score >= win_score)
            {
                current_score = win_score;
                Debug.Log("WON");
            }
        }
    }

    public int getStatus()
    {
        return status;
    }

    public int getCurrentScore()
    {
        return current_score;
    }
}
