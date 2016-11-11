using UnityEngine;
using UnityEngine.Networking;

public class PlayerRandomSpawner : NetworkBehaviour {

    public float min_x = -4;
    public float max_x = 4;
    public float min_y = -4;
    public float max_y = 4;
    public float check_radius = 0.5f; //same on x and y because it's a circle

    [Command]
    public void CmdRespawn()
    {
        RpcRespawn();
    }

    [ClientRpc]
    public void RpcRespawn()
    {
        if (!isLocalPlayer)
        {
            return;
        }
   
        bool spawned = false;

        while (!spawned)
        {
            var random_x = Random.Range(min_x, max_x);
            var random_y = Random.Range(min_y, max_y);
            var colliders = Physics2D.OverlapArea(new Vector2(random_x, random_y),
                new Vector2(check_radius, check_radius));
            if (colliders == null)
            {
                transform.position = new Vector3(random_x, random_y, 0);
                spawned = true;
            }
        }


    }
}
