using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private static float BULLET_SIZE = 0.3f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] PlayerController playerController;
    [SerializeField] LayerMask enemiesLayer;
    private Rigidbody2D bulletRb;
    private PolygonCollider2D bulletCollider;
    private float flipBullet;
    private float bulletFacing;
    private float countTime = 0f;
    void Start()
    {
        flipBullet = BULLET_SIZE;
        bulletFacing = BULLET_SIZE;

        bulletCollider = GetComponent<PolygonCollider2D>();
        bulletRb = GetComponent<Rigidbody2D>();
        playerController = FindAnyObjectByType<PlayerController>();

        flipBullet = (playerController.transform.localScale.x > 0) ? BULLET_SIZE : -BULLET_SIZE;
        bulletFacing = (playerController.transform.localScale.x > 0) ? BULLET_SIZE : -BULLET_SIZE;
    }

 
    void Update()
    {
        CountTime();
        FireABullet();

        
    }
    void FireABullet()
    {
        this.transform.localScale = new Vector3(flipBullet,BULLET_SIZE,BULLET_SIZE);
        bulletRb.velocity = new Vector2(bulletFacing * bulletSpeed, 0f);
    }
    IEnumerator DelayDestroyBullet()
    {
        Debug.Log("bawts dau");
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        Debug.Log($"Thoi gian= {this.countTime}");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            this.bulletSpeed = -bulletSpeed;
            this.flipBullet = -this.flipBullet;
            StartCoroutine(DelayDestroyBullet());
            
        } 
        if(collision.gameObject.CompareTag("danger"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void CountTime()
    {
        this.countTime += Time.deltaTime;
    }
}
