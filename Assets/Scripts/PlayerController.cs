using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ======================MOVEMENT======================
    [SerializeField] float speed = 3.0f;

    // =========================AIM========================
    [SerializeField] Transform aim;
    [SerializeField] Camera aimCamera;
    Vector2 lookDirection;

    // ======================BULLET========================
    [SerializeField] Transform bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        MoveAim();

        if (Input.GetMouseButton(0))
        {
            Bullet();
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

        void Bullet()
        {
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Instantiate(bullet, transform.position, targetRotation);
        }
    }
}