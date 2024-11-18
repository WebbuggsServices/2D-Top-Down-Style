using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    

    public float speedBoost;
    public float jumpForce;
 
    float minX, maxX;
    public float padding = 0.6f;

    bool isJumping, canDoubleJump;
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;

    public GameObject hammer;


    Rigidbody2D rb;
    Animator anim;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        FindBoundries();

      
    }

    void FindBoundries()
    {
        //for fix to player in camera scene
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        // maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        maxX = CameraCtrl.instance.maxValue.x;

    }

    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y);
        isGrounded = Physics2D.OverlapCircle(feet.position, feetRadius, whatIsGround);
        float playerSpeed = Input.GetAxisRaw("Horizontal");
        playerSpeed *= speedBoost;

        if (playerSpeed != 0)
        {
            MoveHorizontal(playerSpeed);
        }
        else
        {
            StopMoving();
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            hammer.SetActive(true);
            anim.SetTrigger("Hammer");
            Invoke(nameof(HideHammer), 0.8f);
        }
    }

    void HideHammer()
    {
        hammer.SetActive(false);
    }
    void MoveHorizontal(float playerSpeed)
    {
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        if (playerSpeed < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (playerSpeed > 0)
            transform.localScale = Vector3.one;
        if (!isJumping)
        {
           anim.SetInteger("State", 1);
        }

    }

    public void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("State", 0);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
            isJumping = true;
            rb.velocity = new Vector2(0, jumpForce);
            anim.SetInteger("State", 2);
        }
        else
        {
            canDoubleJump = false;
            isJumping = true;
            anim.SetInteger("State", 2);
            rb.velocity = new Vector2(0, jumpForce);
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feet.position, feetRadius);
    }
}
