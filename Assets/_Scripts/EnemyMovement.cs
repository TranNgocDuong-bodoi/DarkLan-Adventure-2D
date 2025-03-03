using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float enemySpeed = 1f;
    private Rigidbody2D enemyRb;
    private bool FlipEnemy = false;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float EnemiesSpeed = 1f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        this.transform.position = pointA.transform.position;
        this.gameObject.tag = "danger";

    }

    void Update()
    {
       EnemiesMoving2();
    }
    void EnemiesMoving2()
    {
        if(this.transform.position.x > pointB.transform.position.x)
        {
            this.enemySpeed = -this.enemySpeed;
            this.transform.position = new Vector2(pointB.transform.position.x,this.transform.position.y);
            this.transform.localScale = new Vector3(-1,1,1);
        }
        if(this.transform.position.x < pointA.transform.position.x)
        {
            this.enemySpeed = -this.enemySpeed;
            this.transform.position = new Vector2(pointA.transform.position.x, this.transform.position.y);
            this.transform.localScale = new Vector3(1,1,1);
        }
        enemyRb.velocity = new Vector2(this.enemySpeed, 0f);
    }
    void EnemiesMoving1()
    {
         if(FlipEnemy)
        {
            enemySpeed = -enemySpeed;
            if(enemySpeed < 0)
            {
                this.transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                this.transform.localScale = new Vector3(1,1,1);
            }
            
        }
        enemyRb.velocity = new Vector2(enemySpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            this.FlipEnemy = false;
            
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            this.FlipEnemy = true;
            
        }
    }
}
