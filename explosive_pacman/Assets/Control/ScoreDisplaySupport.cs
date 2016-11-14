using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreDisplaySupport : NetworkBehaviour {

    [SyncVar]
    string score = "";
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<GUIText>().text = score;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!gameObject.GetComponent<GUIText>().text.Equals(score))
            gameObject.GetComponent<GUIText>().text = score;
    }

    public void setScore(string score)
    {
        this.score = score;
    }
}
