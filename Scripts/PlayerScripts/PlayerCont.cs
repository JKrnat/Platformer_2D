using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCont : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuUI;
    public bool isDying; // bool for death because we don't want to be able to pause/resume the game while the IEnumerator for RestartLevel() is iterating
    public bool isSitting; // bool for sitting at campfire and going to the next level

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform walljumpCheckLeft;
    [SerializeField] private Transform walljumpCheckRight;
    [SerializeField] private float walljumpCheck_number;
    
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer tr;

    private float horizontal;
    private float vertical;
    private bool idle;
    public bool playerdead = false;
    public bool canDash = true; // public so we can access it in JumpPad.cs script so dash resets everytime we touch the JumpPad like it would happen with ground
    private bool isDashing;
    [SerializeField] private float dashingPowerX = 24f;
    [SerializeField] private float dashingPowerY = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    private float dashingCooldown = 0f;
    

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float jumpingPowerHold = 0.7f; // variable for jumping power if we press the jump key longer
 
    [SerializeField] private float walljumpForceY = 20f;

    [SerializeField]private float coyoteTime = 0.2f; // variable for coyoteTime; the higher the value, the later the player can jump after leaving the ground
    private float coyoteTimeHelp; // variable to help the player by allowing him to jump 0.2 seconds late even if he's not grounded 
    [SerializeField]private float jumpBuffer = 0.3f; // variable for jumpBuffer; same as coyoteTime
    private float jumpBufferHelp; // variable to help the player by allowing him to jump 0.2 seconds faster even if he's not grounded at the exact moment he wants to jump

    private bool isFreezePos;




    // Start is called before the first frame update
    void Start()
    {
       
        //playerCont = player.GetComponent<PlayerCont>(); // we get the PlayerCont
    }

    // Update is called once per frame
    void Update()
    {


        if (IsGrounded())
        {
            coyoteTimeHelp = coyoteTime;
        }
        else
        {
            coyoteTimeHelp = coyoteTimeHelp - Time.deltaTime; // Time.deltaTime = 1/framerate; ex: 1/20 (20 frames) = 0.05 (Time.deltaTime = 0.05) 
                                                              // is simply how long the last frame took to render
                                                              // if we multiply the Time.deltaTime with anything, we'll get the same result everytime even if we have different frame rates
        }

        if (IsWallJumpFakeLeft() || IsWallJumpFakeRight())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, vertical * dashingPowerY);
        }
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBufferHelp = jumpBuffer;
        }
        else
        {
            jumpBufferHelp = jumpBufferHelp - Time.deltaTime;
        }

        DashCD();

        // no output/returns nothing; stops player from doing anything while dashing
        if (isDashing)
        {
            return;
        }

        if (isDying)
        {
            return;
        }
        if (isSitting)
        {
            return;
        }

        // we take the input of our horizontal movement (can be -1 = left or 1 = right)
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // if we press the key and the player is grounded
        if (jumpBufferHelp > 0f && coyoteTimeHelp > 0f)
        {
            // modify the speed of the player in y direction( = velocity) of our rigidbody2D only on y axis (with the float jumpingPower) ; the x axis isn't influenced by anything here
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);
            jumpBufferHelp = 0f;
           

        }


        


        // if we release the jump key and our player is moving upwards 
        if (Input.GetKeyUp(KeyCode.UpArrow) && rb2D.velocity.y > 0f) 
        {
            // mulitply y velocity by 0.5f; this allows us to jump higher by pressing the jump Key longer or jump lower by just tapping the jump button 
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * jumpingPowerHold);
            coyoteTimeHelp = 0f; // prevents player from jumping infinitely by spamming the jump key
            
        }

        if (Input.GetKeyDown(KeyCode.X) && canDash == true)
        {
            
            StartCoroutine(Dash());
        }

        

    }

    private void FixedUpdate()
    {
        if(isSitting)
        {
            return;
        }

        if (isDying)
        {
            return;
        }

        // no output; stops player from doing anything while dashing
        if (isDashing)
        {
            return;
        }

         

        // move horizontal ; move on x axis multiplied by speed
        rb2D.velocity = new Vector2(horizontal * speed,rb2D.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal)); // always positive value; 

        if (horizontal <0f)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1); // player flip left
        }
        else if(horizontal > 0f)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); // player flip right
        }
        // animation for right (1)
        /*if (horizontal > 0f)
        {
            animator.Play("MordredRun");
        }
        //animation for left (-1)
        else if (horizontal < 0f)
        {
            animator.Play("MordredRunFlip");
        }
        // animation for nothing (0)
        if (horizontal == 0 && IsGrounded())
        {

            IdleStatus();

        }*/

        
    }

    // creates an invisible circle at our player's feet and when it collides with the groundLayer("Ground") it will allow us to jump
    private bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }
    
    // simualtes a wall jump; is used to check left side of the player
    private bool IsWallJumpFakeLeft()
    {
        return Physics2D.OverlapCircle(walljumpCheckLeft.position, walljumpCheck_number, groundLayer);
    }

    // simualtes a wall jump; is used to check right side of the player
    private bool IsWallJumpFakeRight()
    {
        return Physics2D.OverlapCircle(walljumpCheckRight.position, walljumpCheck_number, groundLayer);
    }

   






    /*private void IdleStatus()
    {
        idle = true;
        animator.Play("MordredIdleNoDirection");

    }
    */
    private void DashCD()
        {
        if (IsGrounded()) // we use IsGrounded() instead of coyoteTimeHelp > 0f && jumpBufferHelp > 0f because we don't want the player to be able to use 2 dashes in a row if he's on the ground
        {                 // he'll be able to use 2 dashes in a row because coyoteTimeHelp and jumpBufferHelp allows the player to simulate touching the ground sooner by 0.2f
                          // that's why we use IsGrounded()
            canDash = true;
        }

    }

    private IEnumerator Dash()
    {
        
        canDash = false;
        isDashing = true;
        
        
        // we don't want that the player's gravity be affected while dashing so we store it in a variable, then set the rb2D gravity to 0f
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;
        
        // dashing horizontal right (1) or left (-1)
        // using dashingPowerX variable for dashing on X axis
        if (horizontal > 0f || horizontal < 0f)
        {
            rb2D.velocity = new Vector2(horizontal * dashingPowerX, 0f);
        }

        //dashing vertical up(1) or down(-1) + dashing horizontal at the same time
        // using dashingPowerY variable for dashing on Y axis
        if (vertical > 0f || vertical < 0f)
        {
            rb2D.velocity = new Vector2(horizontal * dashingPowerX, vertical * dashingPowerY);
        }


        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2D.gravityScale = originalGravity;
        isDashing = false;

        
        
    }

    

   
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JumpReset"))
        {
            canDash = true;
        }

        if (collision.gameObject.CompareTag("LoadPoint"))
        {
            Debug.Log("Load Point here!");
            
                StartCoroutine(NextLevel());
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Spike"))
        {


            StartCoroutine(RestartLevel());
            Debug.Log("Game Over");
        }
        

        if (collision.gameObject.tag == "WallJump")
        {

            rb2D.AddForce(new Vector2(0f, walljumpForceY), ForceMode2D.Impulse);

        }


    }

    

    /*private void Death()
    {
       
        animator.SetTrigger("death");

        
        
    }*/

   private IEnumerator RestartLevel()
    {

        isDying = true;
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        rb2D.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(10f);
        deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isDying = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator NextLevel()
    {
        
        
            animator.SetFloat("Speed", 0);
            isSitting = true;
            
            
            yield return new WaitForSeconds(1f);
            animator.Play("MordredSleep");
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // load the next level in the queue (in the index from Build in Unity; Files > Build Settings > order the scenes in the index)
            isSitting = false;
        
    }





}

