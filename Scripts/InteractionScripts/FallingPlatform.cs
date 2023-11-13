using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] private float fallDelay = 1f; 
    [SerializeField] private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb2D; // reference the falling platform's rigidbody2D
    [SerializeField] private PlayerCont playerCont; // reference to access the PlayerCont script so we can jump on it since the jump code is inside PlayerCont


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // only do this if collision is with a gameobject that is tagged as Player
        {
            StartCoroutine(Fall());
        }    
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }    
    }
    */
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay); // fall after a few seconds (fallDelay variable dictates the seconds)
        rb2D.bodyType = RigidbodyType2D.Dynamic; // we change the platform's rigidbody2D from Kinematic(can't fall because it isn't affected by gravity and will stay stationary) to Dynamic(is affected by gravity)
        Destroy(gameObject, destroyDelay); // destroy platform after a few seconds
    }

}
