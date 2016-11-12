using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class WallControler : NetworkBehaviour
{
    public GameObject brick_wall;
    float height, width;
    int marginX, marginY;
    bool[,] bitMatrix;
    // preia dimensiunea cadrului vizualizat de camera
    public Vector2 get_world_dimensions()
    {
        var cam = Camera.main;
        var p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        var p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float width = (p2 - p1).magnitude;
        float height = (p3 - p2).magnitude;

        Vector2 dimensions = new Vector2(width, height);

        return dimensions;
    }

    void Start()
    {
        GameObject wall = (GameObject)Instantiate(brick_wall, new Vector3(0, 0, 0), transform.rotation);

        // am nevoie de dimensiuni pentru a vedea cate cuburi(pereti) incap in scena camerei
        Vector2 dimensions = get_world_dimensions();
        height = dimensions.y;
        width = dimensions.x;
        marginX = (int)(width / 2);  // marginX si marginY sunt utile cand schimbam pozitia camerei
        marginY = (int)(height / 2);
        Debug.Log("Abosolute value of marginX = " + marginX + "; Abosolute value of marginY = " + marginY);

        bitMatrix = new bool[marginX * 2, marginY * 2];

        for (int i = 0; i < marginX * 2; i++)
            for (int j = 0; j < marginY * 2; j++)
                if (i == 0 || i == marginX * 2 - 1 || j == 0 || j == marginY * 2 - 1)
                {   // afiseaza peretii de pe margine, add boxcollider, Rigidbody,isKinematick
                    bitMatrix[i, j] = true;
                    //GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject wall2 = (GameObject)Instantiate(brick_wall, new Vector3(i - marginX, j - marginY, 0), transform.rotation);
                    //Network.Instantiate(brick_wall, new Vector3(i - marginX, j - marginY, 0), transform.rotation,0);
                    //BoxCollider boxCollider = (BoxCollider)cube2.gameObject.AddComponent(typeof(BoxCollider));
                    //Rigidbody gameObjectsRigidBody = (Rigidbody)cube2.AddComponent<Rigidbody>(); // Add the rigidbody.
                    //boxCollider.center = new Vector3(i, j, 0);
                    //gameObjectsRigidBody.mass = 0;
                    //gameObjectsRigidBody.isKinematic = true;
                }

        // adauga maxim 40 pereti in interior
        int Nucoliziuni = 0;
        for (int i = 0; i < Random.Range(290, 350); i++)
        {
            int random_x = (int)Random.Range(1, marginX * 2 - 1);
            int random_y = (int)Random.Range(1, marginY * 2 - 1);
            float marireX = Random.Range(0, 0.8f);
            float marireY = Random.Range(0, 0.8f);
            var colliders = Physics2D.OverlapArea(new Vector2(random_x - 0.25f, random_y - 0.25f), new Vector2(random_x + 0.1f, random_y + 0.1f));
            if (colliders == null) // daca nu exista overlap, seteaza peretele 
            {
                Nucoliziuni++;
                bitMatrix[random_x, random_y] = true;
                GameObject wall2 = (GameObject)Instantiate(brick_wall, new Vector3(random_x - marginX, random_y - marginY, 0), transform.rotation);
                //Network.Instantiate(brick_wall, new Vector3(random_x - marginX, random_y - marginY, 0), transform.rotation,0);
                //wall.transform.localScale += new Vector3(marireX, marireY, 0);
                //wall.transform.Rotate(transform.rotation.x, transform.rotation.y + 45, transform.rotation.z);

            }
        }
        Debug.Log("NUColiziuni :" + Nucoliziuni + "..");

    }
}