using UnityEngine;
using UnityEngine.Networking;

public class RoleTransfom : NetworkBehaviour {

    public float attacker_speed = 8;
    public float runner_speed = 5;
    public float attacker_scale = 7.0f;
    public float runner_scale = 3.82f;

    [ClientRpc]
    public void RpcSetAttacker()
    {
        if (gameObject != null)
        {
            transform.localScale = new Vector3(attacker_scale, attacker_scale, 0.0f);
            gameObject.GetComponent<PlayerController>().setSpeed(attacker_speed);
        }
    }

    [ClientRpc]
    public void RpcSetRunner()
    {
        if (gameObject != null)
        {
            transform.localScale = new Vector3(runner_scale, runner_scale, 0.0f);
            gameObject.GetComponent<PlayerController>().setSpeed(runner_speed);
        }
    }
}
