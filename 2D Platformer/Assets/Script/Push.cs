using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float pushForce = 5f;
    private Rigidbody2D boxRb;

    void Start()
    {
        boxRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null && player.IsTransformed())
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null && boxRb != null)
            {
                Vector2 pushDirection = new Vector2(playerRb.velocity.x, 0);

                if (Mathf.Abs(playerRb.velocity.x) > 0.1f)
                {
                    boxRb.velocity = pushDirection * pushForce;
                }
            }
        }
        else
        {
            boxRb.velocity = Vector2.zero;
        }
    }
}