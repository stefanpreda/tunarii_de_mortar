using UnityEngine;

//This class should be responsible for actions taken in case of collision
//Attach this to Character only
//TODO: This is called a lot of times because of the push-back
public class CollisionController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
 
        //Get object that was hit
        var hit = collision.gameObject;

        //hit.GetComponent<Rigidbody2D>().isKinematic = true;

        //Get the score controller for the hit object
        var score_controller = hit.GetComponent<ScoreController>();

        if (score_controller == null)
        {
            Debug.Log("NULL score controller");
            return;
        }

        //Get common network data
        DataIntegrity network_data = gameObject.GetComponent<DataIntegrity>();

        if (network_data == null)
        {
            Debug.Log("NULL player data");
            return;
        }

        //Get players as GameObjects
        var player_list = network_data.getPlayerList();

        //TODO: Check which player was hit, get the corresponding player status, adjust score accordingly

        //Destroy(gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        /* var hit = collision.gameObject;
        hit.GetComponent<Rigidbody2D>().isKinematic = false;
        */
    }
}
