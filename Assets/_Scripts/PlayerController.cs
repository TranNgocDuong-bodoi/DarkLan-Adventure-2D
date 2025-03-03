using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // temp 
    private float tempDisTance;
    // Start is called before the first frame update
    private static float BASE_SPEED = 5f;
    private static int GO_TO_RIGHT = 1;
    private static int NOT_CLIMBING = 0;
    private static int GO_TO_LEFT = -1;
    private static Vector3 FACING_RIGHT = new Vector3(1,1,1);
    private static Vector3 FACING_LEFT = new Vector3(-1,1,1);
    //
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private int _gravityScale;
    [SerializeField] private float _climbSpeed;
    // sprite apear when player die
    [SerializeField] private Sprite playerSprite;
    Vector2 moving;
    // bool type
    private bool isFacingRight = true;
    private bool onGround = true;
    private bool canClimb = false;
    private bool isDead = false;
    private bool canGetCoin = false;
    // object variable
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulluet;
    private CapsuleCollider2D playerCollier;
    private CircleCollider2D circleCollider2D;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public LayerMask bulletLayer;
    private GameObject coin;
    private Rigidbody2D rb;
    private PlayerInput input;
    private PlayerAnimationController animationController;
    void Start()
    {

        playerCollier = GetComponent<CapsuleCollider2D>();
        circleCollider2D =GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animationController = GetComponent<PlayerAnimationController>();
    }
    void Update()
    {
        if(isDead)
        {
            return;
        }
        OnMove();
        OnJump();
        OnClimb();
        OnFire();
        OnCollectCoin();
        OnDead();
        CalculateTempDistance();
    }
    private void OnMove()
    {
        // di chuyen
        moving = new Vector2(0,0);
        if(isDead)
        {
            return;
        }
        // variable
        moving = new Vector2(input.MoveInput, rb.velocity.y);
        if(moving.x == GO_TO_LEFT && isFacingRight){
            isFacingRight = false;
            this.transform.localScale = FACING_LEFT;
        }
        if(moving.x == GO_TO_RIGHT && !isFacingRight)
        {
            isFacingRight = true;

            this.transform.localScale = FACING_RIGHT;
        }
        moving.x = moving.x * this._speed;
        animationController.GetMovingAnimation(moving.x);
        rb.velocity = moving; 
    }
    private void OnJump()
    {
        if(isDead)
        {
            return;
        }
        if(playerCollier.IsTouchingLayers(groundLayer)){
            onGround = true;
        }
        else{
            onGround = false;
        }
        if(input.JumpInput && onGround)
        {
            rb.AddForce(Vector2.up * this._jumpSpeed, ForceMode2D.Impulse);
        }

    }
    private void OnClimb()
    {
        if(isDead)
        {
            return;
        }
        rb.gravityScale = 3;
        if(canClimb)
        {
            
            if(canClimb && onGround){
                rb.velocity = new Vector2(rb.velocity.x, input.ClimbInput * this._climbSpeed);
                animationController.GetClimbingAction(input.ClimbInput);
                Debug.Log("on ground not climbing");
            }
            if(this.CalculateTempDistance() >= 0.3f)
            {
                this._speed = 1f;
            }
            else{
                this._speed =BASE_SPEED;
            }
            if(canClimb && !onGround)
            {
                Debug.Log($"Distance = {this.CalculateTempDistance()}");
                rb.velocity = new Vector2(rb.velocity.x, input.ClimbInput * this._climbSpeed);
                animationController.GetClimbingAction(input.ClimbInput);
                Debug.Log("not on ground is climbing");
            }
            
            rb.gravityScale = 0;    
        }
        else{
            animationController.GetClimbingAction(NOT_CLIMBING);
            this._speed = BASE_SPEED;
        }
    }
    private void OnFire()
    {
        if(isDead)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(bulluet,gun.GetChild(0).transform.position,transform.rotation);
        }

    }
    private void OnDead()
    {
        if(circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Danger")) || circleCollider2D.IsTouchingLayers(bulletLayer))
        {
            this.isDead = true;
            this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hidden";
            GameObject deadPlayer = new GameObject("deadPlayer");
            deadPlayer.transform.position = this.transform.position;
            deadPlayer.AddComponent<SpriteRenderer>();
            deadPlayer.AddComponent<Rigidbody2D>();

            SpriteRenderer deadSprite = deadPlayer.GetComponent<SpriteRenderer>();
            Rigidbody2D deadRb = deadPlayer.GetComponent<Rigidbody2D>();

            deadSprite.sortingLayerName = "Character";
            deadSprite.sprite = playerSprite;

            deadRb.bodyType = RigidbodyType2D.Dynamic;
            deadRb.velocity = new Vector2(1f,5f);
            deadRb.AddTorque(20f);
            StartCoroutine(DelayAction(deadPlayer));     
        }
    }
    private void OnCollectCoin()
    {
        if(canGetCoin)
        {
            Destroy(coin);
        }
    }
    IEnumerator DelayAction(GameObject O)
    {
        Debug.Log("Bắt đầu delay...");
        yield return new WaitForSeconds(2f);
        
        Debug.Log("Hết thời gian delay!");
        Destroy(O.gameObject);
        FindObjectOfType<GameController>().PlayerDieController();

    }
    private void DestroyInstanceGameObject(GameObject o)
    {
        Destroy(o);
    }
    private float CalculateTempDistance()
    {
        if(playerCollier.IsTouchingLayers(groundLayer))
        {
            this.tempDisTance = this.transform.position.y;
        }
        return this.transform.position.y - this.tempDisTance;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ladder"))
        {
            canClimb = true;
        }
        if(collision.gameObject.CompareTag("coin"))
        {
            this.canGetCoin = true;
            coin = collision.gameObject;
            
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ladder"))
        {
            canClimb = false;
            
        }
        if(collision.gameObject.CompareTag("coin"))
        {
            this.canGetCoin = false;
        }
        
    }
    
}
