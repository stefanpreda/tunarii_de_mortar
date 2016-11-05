using UnityEngine;

// WORK IN PROGRESS, this might be removed
public class PlayerRandomSpawner : MonoBehaviour {

	void Start () {
        int x = Random.Range(0, 3);
        int y = Random.Range(0, 3);
        transform.Translate(x, y, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
