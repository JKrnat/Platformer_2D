using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoofPlatforms : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteren;
    private BoxCollider2D boxCollider;
    [SerializeField] private float poofwait;
    [SerializeField] private float pooftimer;
    
    private bool appear = true;

    private void Start()
    {
        spriteren = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private  IEnumerator Poof()
    {
        yield return new WaitForSeconds(pooftimer);   
        if(appear == true) // if it's on the screen
        {
            spriteren.enabled = true;
            boxCollider.enabled = true;
            yield return new WaitForSeconds(poofwait); // wait for poofwait
        }
        
        appear = false;
        spriteren.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(poofwait);
        appear = true;
      
    }
    private void FixedUpdate() // fixed update for rigidbody because of physics
    {
        StartCoroutine(Poof());
    }
}
