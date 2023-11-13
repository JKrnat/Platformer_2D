using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBackgroundParallaxEffect : MonoBehaviour
{
    // Parallax Effect = when the camera is moving compared to the camera's position, we want to move the background's elements

    private float lenght; // lenght of the sprite
    private float startPos; // starting position of this current object that has this script attached to it (one of the background's elements for example)
    [SerializeField] private CinemachineVirtualCamera cam;  // cam reference
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float border;
    // Start is called before the first frame update
    void Start()
    {
        border = lenght;
        startPos = transform.position.x; // get current object we want to modify; we get only x position because we move on x axis
        lenght = GetComponent<SpriteRenderer>().bounds.size.x; // gets the border of the sprites on the x axis
    }

    // Update is called once per frame
    void Update()
    {
        float temporary = (cam.transform.position.x * (1 - parallaxEffect)); // looks where the object at this point in time
        float distance = (cam.transform.position.x * parallaxEffect); // get camera position and multiply with parallax effect
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z); // move the object the script is attached to; 

        if (temporary > startPos + lenght) // if the object is at the right side of the border 
        {
            startPos = startPos + lenght; // shift the background
        }
        else if (temporary < startPos - lenght) // if the object is at the left side of the border 
        {
            startPos = startPos - lenght; // shift the background
        }

    }

}
