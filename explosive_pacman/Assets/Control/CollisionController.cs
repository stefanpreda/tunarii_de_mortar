using UnityEngine;

//This class should be responsible for actions taken in case of collision
//Attach this to Character only
//TODO: Collision must be active in Unity
public class CollisionController : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        //Get object that was hit
        var hit = collision.gameObject;

        //Get the score controller for the hit object
        var score_controller = hit.GetComponent<ScoreController>();

        if (score_controller == null)
            return;
   
        //Get common network data
        NetworkManagerData network_data =
            GameObject.FindGameObjectWithTag("NetworkManagerData").GetComponent<NetworkManagerData>();

        if (network_data == null)
            return;

        //Get players as GameObjects
        var player_list = network_data.getPlayerList();

        //TODO: Check which player was hit, get the corresponding player status, adjust score accordingly

        Destroy(gameObject);
    }
}
