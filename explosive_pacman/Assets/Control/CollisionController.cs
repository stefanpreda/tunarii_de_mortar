using UnityEngine;

//This class should be responsible for actions taken in case of collision
//Attach this to Character only
//TODO: This is called a lot of times, a timeout after taking dmg is needed
public class CollisionController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
 
        //Get object that was hit
        var hit = collision.gameObject;

        //Get the score controller for the hit object
        var score_controller_target = hit.GetComponent<ScoreController>();
        var score_controller_self = gameObject.GetComponent<ScoreController>();

        if (score_controller_target == null || score_controller_self == null)
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

        //Adjust the score accordingly
        if (score_controller_self.getStatus() == 0 && score_controller_target.getStatus() == 1)
        {
            score_controller_self.modifyScore(0);
            score_controller_target.modifyScore(1);
        }

        if (score_controller_self.getStatus() == 1 && score_controller_target.getStatus() == 0)
        {
            score_controller_self.modifyScore(1);
            score_controller_target.modifyScore(0);
        }

        //Destroy(gameObject);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        /* var hit = collision.gameObject;
        hit.GetComponent<Rigidbody2D>().isKinematic = false;
        */
    }
}
