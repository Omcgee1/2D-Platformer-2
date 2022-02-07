using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is CapsuleCollider2D)
        {
            FindObjectOfType<GameSession>().AddToScore(points);
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
