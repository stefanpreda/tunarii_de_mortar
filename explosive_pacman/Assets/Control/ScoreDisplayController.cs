using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreDisplayController : NetworkBehaviour {

    public GameObject score_display;

    public void generateScoreOnClients()
    {
        score_display = (GameObject)Instantiate(score_display, new Vector3(0.90f, 0.95f, 0.0f), score_display.transform.rotation);
        score_display.GetComponent<ScoreDisplaySupport>().setScore("DEFAULT");
        NetworkServer.Spawn(score_display);
    }

    public void changeScore(string score)
    {
        score_display.GetComponent<ScoreDisplaySupport>().setScore(score);
    }
}
