using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    Animator anim;
    public float speed = 11.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer || anim == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("left", true);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", true);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", true);
            anim.SetBool("down", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
            anim.SetBool("up", false);
            anim.SetBool("down", true);
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(x, y, 0);
    }
}
