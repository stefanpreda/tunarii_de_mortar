using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WallControler :  NetworkBehaviour
{
    public GameObject brick_wall;
    public float height = 11.0f;
    public float width = 15.0f;

    public void GenerateMap()
    {


        for (int i = (int)-width; i < width; i++)
            for (int j = (int)-height; j < height; j++)
                if (i == -width || i == width - 1|| j == -height || j == height - 1)
                {
                    GameObject wall2 = (GameObject)Instantiate(brick_wall, new Vector3(i, j, 0), transform.rotation);
                    NetworkServer.Spawn(wall2);
                }

        int collisions = 0;
        for (int i = 0; i < Random.Range(125, 150); i++)
        {
            int random_x = (int)Random.Range(-width + 0.5f, width - 0.5f);
            int random_y = (int)Random.Range(-height + 0.5f, height - 0.5f);

            var colliders = Physics2D.OverlapArea(new Vector2(random_x - 0.25f, random_y + 0.25f), new Vector2(random_x + 0.25f, random_y - 0.25f));
            if (colliders == null) // daca nu exista overlap, seteaza peretele 
            {
                collisions++;
                GameObject wall2 = (GameObject)Instantiate(brick_wall, new Vector3(random_x, random_y, 0), transform.rotation);
                NetworkServer.Spawn(wall2);
            }
        }
        Debug.Log("Collisions when generating map:" + collisions + ".." + netId);
    }
}