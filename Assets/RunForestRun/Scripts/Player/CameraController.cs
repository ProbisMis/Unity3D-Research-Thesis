using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject Sphere;       //Public variable to store a reference to the player game object


    private float offset;         //Private variable to store the offset distance between the player and camera

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 10f;
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position.z - Sphere.transform.position.z;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if(Sphere == null){
            
        }
        else{
            float newPos = Sphere.transform.position.z + offset;
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = Vector3.Slerp(transform.position, new Vector3(0, transform.position.y, newPos), SmoothFactor);
           // transform.position.z = newPos ;

        }


            }
}