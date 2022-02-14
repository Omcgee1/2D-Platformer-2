using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float projectileSpeed = 10f;
    float xSpeed;

    Rigidbody2D rb;
    Player player;
    Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.transform.localScale.x * projectileSpeed;
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
