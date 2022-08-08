using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // =======================HEALTH=======================
    int health = 10;

    // ======================MOVEMENT======================
    [SerializeField] float speed = 3.0f;

    // =========================AIM========================
    [SerializeField] Transform aim;
    [SerializeField] Camera aimCamera;
    Vector2 lookDirection;

    // ======================BULLET========================
    [SerializeField] Transform bullet;
    bool gunLoaded = true;
    [SerializeField] float fireRate = 1;

    // Start is called before the first frame update
    void Start()
    {

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

        Instantiate(bullet, transform.position, targetRotation);
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    // =======================HEALTH=======================
    public void TakeDamage()
    {
        health --;

        if (health <= 0)
        {
            //TODO:
        }
    }
}