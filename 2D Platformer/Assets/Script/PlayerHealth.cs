using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Player Dead!");
            Destroy(gameObject); // เปลี่ยนเป็น Game Over ได้
        }
    }
}