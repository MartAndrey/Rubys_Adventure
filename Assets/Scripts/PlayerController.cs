using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // =======================HEALTH=======================
    int maxHealth = 5;
    int currentHealth;

    // ======================MOVEMENT======================
    public float speed = 3.0f;

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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        MoveAim();

        // ======================BULLET========================
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            Bullet();
            StartCoroutine(ReloadGun());
        }
    }

    // ======================MOVEMENT======================
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;

        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

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
    public void TakeDamage()
    {
        if (invulnerable) return;

        currentHealth--;
        invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());

        if (currentHealth <= 0)
        {
            //TODO:
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
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