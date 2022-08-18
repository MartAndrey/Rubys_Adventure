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
    [SerializeField] Transform bullet;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;
    bool powerShotEnable;

    // ====================INVULNERABLE=====================
    [SerializeField] float invulnerableTime = 3;
    bool invulnerable;

    // ====================ANIMATOR=====================
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 moveNormalize = move.normalized;

        Move(horizontal, vertical);
        MoveAim();

        // ======================BULLET========================
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            Bullet();
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
    void Move(float x, float y)
    {
        Vector2 position = transform.position;

        position.x += speed * x * Time.deltaTime;
        position.y += speed * y * Time.deltaTime;

        transform.position = position;
    }

    // =========================AIM========================
    void MoveAim()
    {
        lookDirection = aimCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)lookDirection.normalized;
    }

    // ======================BULLET========================
    void Bullet()
    {
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform bulletClone = Instantiate(bullet, transform.position, targetRotation);

        if (powerShotEnable)
        {
            bulletClone.GetComponent<Bullet>().powerShot = true;
        }
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