using UnityEngine;

//This class should be responsible for actions taken in case of collision
//Attach this to Character only
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

        //Adjust the score accordingly
        if (score_controller_self.getStatus() == 0 && score_controller_target.getStatus() == 1)
        {
            if (!score_controller_self.getInvulnerable())
            {
                if (score_controller_self != null)
                    score_controller_self.modifyScore(0);
                score_controller_target.modifyScore(1);
                score_controller_self.displayScore();
            }
        }

        if (score_controller_self.getStatus() == 1 && score_controller_target.getStatus() == 0)
        {
            if (!score_controller_target.getInvulnerable())
            {
                score_controller_self.modifyScore(1);

                if (score_controller_target != null)
                score_controller_target.modifyScore(0);
                score_controller_self.displayScore();

            }
        }

    }

}
