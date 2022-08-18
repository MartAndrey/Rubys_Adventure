using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // =======================HEALTH=======================
    int maxHealth = 5;
    int currentHealth;
    public int Health { get { return currentHealth; } }

    // ======================MOVEMENT======================
    public float speed = 3.0f;
    float direction;

    // =========================AIM========================
    [SerializeField] Transform aim;
    [SerializeField] Camera aimCamera;
    Vector2 lookDirection;

    // ======================BULLET========================
    [SerializeField] GameObject bulletPrefab;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    bool powerShotEnable;

    // ====================INVULNERABLE=====================
    [SerializeField] float invulnerableTime = 3;
    bool invulnerable;

    // ====================ANIMATOR=====================
    Animator animator;

    Rigidbody2D rb;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 moveNormalize = move.normalized;

        MoveAim();

        // ======================BULLET========================
        if (Input.GetMouseButton(0) && gunLoaded && gunLoaded)
        {
            Launch();
            gunLoaded = false;
            StartCoroutine(ReloadGun());
        }

        // ====================ANIMATOR=====================
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed X", moveNormalize.x);
        animator.SetFloat("Speed Y", moveNormalize.y);

        if (move.magnitude != 0.0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    // ======================MOVEMENT======================
    void FixedUpdate()
    {
        Vector2 position = rb.position;

        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        rb.MovePosition(position);
        Debug.Log(rb.velocity); 
    }

    // =========================AIM========================
    void MoveAim()
    {
        lookDirection = aimCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection.Normalize();
        aim.position = (transform.position + Vector3.up * 0.5f) + (Vector3)lookDirection.normalized;
    }

    // ======================BULLET========================
    void Launch()
    {
        GameObject projectileObject = Instantiate(bulletPrefab , rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Bullet projectile = projectileObject.GetComponent<Bullet>();
        projectile.Launch(lookDirection, 300);
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    // =======================HEALTH=======================
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (invulnerable) return;

            invulnerable = true;
            StartCoroutine(MakeVulnerableAgain());

            if (currentHealth <= 0)
            {
                //TODO:
            }
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            switch (other.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;

                case PowerUp.PowerUpType.PowerShort:
                    powerShotEnable = true;
                    break;
            }
            Destroy(other.gameObject, 0.1f);
        }
    }
}