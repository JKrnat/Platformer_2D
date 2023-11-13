using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    // camera reference to drag into
    public GameObject virtualcam;
    //public GameObject background;


    // on trigger enter -> compare the tag and if the collision has the tag "Player" and it isn't a trigger then set the camera to be active
    // activates the first camera
    private void OnTriggerEnter2D(Collider2D other)
       {

        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(true);
            //background.SetActive(true);
        }

       }

    // on trigger exit -> compare the tag and if the collision has the tag "Player" and it isn't a trigger then set the camera to be false
    // dezactivates the first camera and activates the second camera that it collides with
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualcam.SetActive(false);
            //background.SetActive(false);
        }

    }

}
