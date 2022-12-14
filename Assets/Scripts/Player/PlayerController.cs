using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // =======================HEALTH=======================
    int maxHealth = 5;
    int currentHealth;
    public int Health { get { return currentHealth; } }
    [SerializeField] ParticleSystem hitParticle;

    // ======================MOVEMENT======================
    float direction;
    public float speed = 3.0f;

    // =========================AIM========================
    Vector2 lookDirection;
    [SerializeField] Transform aim;
    [SerializeField] Camera aimCamera;

    // ======================BULLET========================
    public int AmmoBullet { get; set; }
    public bool HasAmmo { get; set; }
    public bool GunLoaded { get; set; }
    [SerializeField] float fireRate = 1;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject OutOfAmmoText;
    [SerializeField] TMP_Text textBullet;

    // ====================INVULNERABLE=====================
    public bool invulnerable;
    [SerializeField] float invulnerableTime = 3;

    // ====================ANIMATOR=====================
    Animator animator;

    // ====================PHYSICAL=====================
    float horizontal;
    float vertical;
    Rigidbody2D rb;

    SpriteRenderer spriteRenderer;
    [SerializeField] float blinkRate = 0.1f;

    // =====================CAMERA=====================
    CameraController camController;

    // =====================AUDIO======================
    AudioSource audioSource;
    [SerializeField] AudioClip audioHit;
    [SerializeField] AudioClip audioGameOver;

    void Start()
    {
        currentHealth = maxHealth;
        GunLoaded = true;
        HasAmmo = false;
        AmmoBullet = 0;

        UpdateUIAmmoBullet();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        camController = FindObjectOfType<CameraController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector2 move, moveNormalize;
        ReadInput(out move, out moveNormalize);

        MoveAim();

        // ======================BULLET========================
        if (Input.GetMouseButton(0) && GunLoaded && GameManager.Instance.currentScene == Scenes.Game)
        {
            if (HasAmmo)
                Launch();
            else
                if (OutOfAmmoText.activeSelf == false)
                    StartCoroutine(OutOfAmmoTextRutiner());
        }

        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentScene == Scenes.Game)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up, lookDirection, 3, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();

                if (npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }

        // ====================ANIMATOR=====================
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed X", moveNormalize.x);
        animator.SetFloat("Speed Y", moveNormalize.y);

        if (move.magnitude != 0.0)
        {
            animator.SetBool("IsMoving", true);
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else
        {
            animator.SetBool("IsMoving", false);
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }

    // ======================MOVEMENT======================
    void FixedUpdate()
    {
        Vector2 position = rb.position;

        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        rb.MovePosition(position);
    }

    void ReadInput(out Vector2 move, out Vector2 moveNormalize)
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        move = new Vector2(horizontal, vertical);
        moveNormalize = move.normalized;
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
        GunLoaded = false;
        GameObject projectileObject = Instantiate(bulletPrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        animator.SetTrigger("Lauch");
        Bullet projectile = projectileObject.GetComponent<Bullet>();
        projectile.Launch(lookDirection, 300);
        AmmoBullet += -1;

        UpdateUIAmmoBullet();
        StartCoroutine(ReloadGun());

        if (AmmoBullet <= 0)
            HasAmmo = false;
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        GunLoaded = true;
    }

    public void UpdateUIAmmoBullet()
    {
        textBullet.text = AmmoBullet.ToString();
    }

    IEnumerator OutOfAmmoTextRutiner()
    {
        OutOfAmmoText.SetActive(true);
        yield return new WaitForSeconds(2);
        OutOfAmmoText.SetActive(false);
    }

    // =======================HEALTH=======================
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (invulnerable) return;

            invulnerable = true;
            camController.Shake();
            animator.SetTrigger("Hit");
            PlayAudio(audioHit);
            Instantiate(hitParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            StartCoroutine(MakeVulnerableAgain());
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth <= 0)
        {
            GameOver();
            GameManager.Instance.GameOverScene();
        }
    }

    IEnumerator MakeVulnerableAgain()
    {
        StartCoroutine(BlinkRountine());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator BlinkRountine(float t = 5)
    {
        while (t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkRate);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkRate);
            t--;
        }
    }

    public void GameOver()
    {
        PlayAudio(audioGameOver);
        rb.simulated = false;
        animator.enabled = false;
        StartCoroutine(BlinkRountine(10));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collectible collectible = other.GetComponent<Collectible>();

            switch (other.GetComponent<Collectible>().typeCollectible)
            {
                case Collectible.TypeCollectible.HealthCollectible:
                    if (Health >= maxHealth)
                        return;

                    collectible.HealthCollectible();
                    break;

                case Collectible.TypeCollectible.AmmoCollectible:
                    collectible.AmmoCollectible();
                    break;
            }
            Destroy(other.gameObject, 0.6f);
        }
    }

    void PlayAudio(AudioClip audio)
    {
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }
}