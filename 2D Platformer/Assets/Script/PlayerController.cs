using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Shooting Settings")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;
    private Vector3 originalScale;
    private float originalFireRate;
    private int originalBulletDamage;
    private float originalKnockbackForce;
    private Vector3 originalBulletScale;

    [Header("Transformation Settings")]
    public float transformationDuration = 5f;
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 enlargedBulletScale = new Vector3(1.5f, 1.5f, 1.5f);
    public float enlargedFireRate = 0.3f;
    public int extraBulletDamage = 1;
    public float knockbackMultiplier = 2f;

    [Header("Cooldown Settings")]
    public float transformationCooldown = 10f;  // CD
    private float nextTransformationTime = 0f;  // Duration

    private bool isTransformed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        originalFireRate = fireRate;

        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            originalBulletDamage = bulletScript.damage;
            originalKnockbackForce = bulletScript.knockbackForce;
        }
        originalBulletScale = bulletPrefab.transform.localScale;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleShooting();

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextTransformationTime) // กด E ขยานร่าง
        {
            StartCoroutine(TransformTemporarily());
        }
    }

    void HandleMovement()
    {
        float moveX = 0;

        if (Input.GetKey(KeyCode.A)) moveX = -1;
        if (Input.GetKey(KeyCode.D)) moveX = 1;

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        rbBullet.velocity = direction * bulletSpeed;

        // กระสุนร่างใหญ่
        if (isTransformed)
        {
            bullet.transform.localScale = enlargedBulletScale;
            bulletScript.damage = originalBulletDamage + extraBulletDamage;
            bulletScript.knockbackForce = originalKnockbackForce * knockbackMultiplier;
        }
        else
        {
            bullet.transform.localScale = originalBulletScale;
            bulletScript.damage = originalBulletDamage;
            bulletScript.knockbackForce = originalKnockbackForce;
        }

        Destroy(bullet, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator TransformTemporarily()
    {
        isTransformed = true;
        transform.localScale = enlargedScale;
        fireRate = enlargedFireRate;
        nextTransformationTime = Time.time + transformationCooldown; //CD

        yield return new WaitForSeconds(transformationDuration);

        isTransformed = false;
        transform.localScale = originalScale;
        fireRate = originalFireRate;
    }

    public bool IsTransformed()
    {
        return isTransformed;
    }
}