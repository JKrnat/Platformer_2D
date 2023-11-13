using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cam;
   

    private void Start()
    {


        cam = cam.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraflip();
                                            
        }

    }

    private void cameraflip()
    {
        cam.gameObject.transform.rotation = Quaternion.Euler(new Vector3(190, 0, 0));
    }

}
