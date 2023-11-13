using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class JumpPad : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float bouncePower = 70f;
    [SerializeField] private PlayerCont playerCont; // reference to access the PlayerCont script 
    //[SerializeField] GameObject player; // reference to access Player's scripts/components attached to it
    

    private void Awake()
    {
        //playerCont = player.GetComponent<PlayerCont>(); // we get the PlayerCont
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bouncePower, ForceMode2D.Impulse);
            StartCoroutine(JumpPadAnimation());
            
            playerCont.canDash = true;
        }
    }

    private IEnumerator JumpPadAnimation()
    {
        animator.Play("Work");
        yield return new WaitForSeconds(0.5f) ;
        animator.Play("Idle");
    }
    
}
