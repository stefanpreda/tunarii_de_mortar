using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetupStart : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.FindGameObjectWithTag("Character");
        if (obj != null)
            obj.GetComponent<PlayerRandomSpawner>().RpcRespawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
