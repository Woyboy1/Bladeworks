using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Movement Variables
    private Rigidbody2D rb;

    [SerializeField] public float moveSpeed = 0f;
    private Vector2 moveDirection;

    // Getting mouse position variables:
    private Vector2 mousePosition;
    public Camera sceneCamera;

    // Shooting Variables:
    public GameObject bullet;
    public Transform firePoint;
    public float fireForce;
    public static bool canShoot = true;

    public GameObject revolverBullet;
    public GameObject shotgunBullet;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public static bool pistolEquipped = true;
    public static bool shotgunEquipped = false;
    public static bool revolverEquipped = false;

    private int ammoLimit = 0;
    public static int ammoCount = 60;

    // Player Stats Variables:
    public static int hitsTillDeath = 0;
    public static int currentHealth = 10;
    public int maxHealth = 20;

    // Misc Variables:
    public AudioSource audioSource;
    public AudioClip shootingSound;
    public AudioClip reloadSound;
    public AudioClip noAmmoSound;
    public AudioClip shotgunSound;
    public AudioClip revolverSound;
    public AudioClip switchWeaponSound;


    public static bool mileStoneComplete = false;
    public static bool mileStoneMapComplete = false;

    // Particle Variables
    public ParticleSystem injuredParticleSystem;
    public ParticleSystem muzzleFlashParticleSystem;

    public HealthBar healthBar;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        // Setting Health Bar:
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


    }

    void Update()
    {
        // Reset Spawn Point and stops spawns
        if (mileStoneMapComplete)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, 1000 * Time.deltaTime);
            mileStoneMapComplete = false;
        }

        // Switch Weapons:
        if (Input.GetKeyDown(KeyCode.F))
        {
            shotgunEquipped = true;
            pistolEquipped = false;

            audioSource.PlayOneShot(switchWeaponSound);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            shotgunEquipped = false;
            pistolEquipped = true;

            audioSource.PlayOneShot(switchWeaponSound);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            shotgunEquipped = false;
            pistolEquipped = false;
            revolverEquipped = true;

            audioSource.PlayOneShot(switchWeaponSound);
        }

        // Click to shoot Revolver:
        if (Input.GetMouseButtonDown(0) && canShoot == true && timeBtwShots <= 0 && pistolEquipped == false && shotgunEquipped == false && revolverEquipped == true)
        {
            ShootPistol("revolver");
            ammoCount -= 7;
            audioSource.PlayOneShot(revolverSound);
            timeBtwShots = startTimeBtwShots;
            Instantiate(muzzleFlashParticleSystem, firePoint.position, Quaternion.identity);

            CameraShake.Instance.ShakeCamera(5f, 0.1f);
        }

            // Click to shoot pistol: 
        if (Input.GetMouseButton(0) && canShoot == true && timeBtwShots <= 0 && pistolEquipped == true && shotgunEquipped == false)
        {
            ShootPistol("pistol");
            ammoCount--;
            audioSource.PlayOneShot(shootingSound);
            timeBtwShots = startTimeBtwShots;
            Instantiate(muzzleFlashParticleSystem, firePoint.position, Quaternion.identity);

            CameraShake.Instance.ShakeCamera(1f, 0.1f);

        } else if (Input.GetMouseButtonDown(0) && canShoot == false ) // Out of ammo:
        {
            audioSource.PlayOneShot(noAmmoSound);
        } else
        {
            timeBtwShots -= Time.deltaTime;
        }

        // Click to shoot Shotgun: 
        if ((Input.GetMouseButtonDown(0) && canShoot == true && timeBtwShots <= 0 && pistolEquipped == false && shotgunEquipped == true))
        {
            ShootPistol("shotgun");
            ammoCount -= 9;
            timeBtwShots = startTimeBtwShots;
            Instantiate(muzzleFlashParticleSystem, firePoint.position, Quaternion.identity);
            audioSource.PlayOneShot(shotgunSound);

            CameraShake.Instance.ShakeCamera(5f, 0.1f);
        }
        else if (Input.GetMouseButtonDown(0) && canShoot == false) // Out of ammo:
        {
            audioSource.PlayOneShot(noAmmoSound);
        }
        


        // Checking if ammo limit is reached:
        if (ammoCount <= ammoLimit)
        {
            canShoot = false; 
        }


        // Always Update HealthBar:
        healthBar.setHealth(currentHealth);

        // Movement:
        ProcessInputs();

        // Checking if player dies:
        if (currentHealth <= PlayerController.hitsTillDeath)
        {
            StartCoroutine(PlayerDeath());
        }

    }

    IEnumerator PlayerDeath()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        canShoot = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        ScoreManager.displayDeathText = true;
        yield return new WaitForSeconds(0.15f);
        ScoreManager.displayDeathText = false;
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("DeathScene");
    }

    // Shoot Method:
    public void ShootPistol(string weapon)
    {
        if (weapon == "pistol")
        {
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        else if (weapon == "shotgun")
        {
            for (int i = 0; i <= 2; i++)
            {
                GameObject bulletProjectile = Instantiate(shotgunBullet, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bulletProjectile.GetComponent<Rigidbody2D>();
                
                switch (i)
                {
                    case 0:
                        rb.AddForce(firePoint.up * fireForce + new Vector3(0f, -20f, 0f), ForceMode2D.Impulse);
                        break;
                    case 1:
                        rb.AddForce(firePoint.up * fireForce + new Vector3(0f, 0f, 0f), ForceMode2D.Impulse);
                        break;
                    case 2:
                        rb.AddForce(firePoint.up * fireForce + new Vector3(0f, 20f, 0f), ForceMode2D.Impulse);
                        break;
                }

            }
        } 
        else if (weapon == "revolver")
        {
            GameObject projectile = Instantiate(revolverBullet, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        // rotate player to follow mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Play Particles when enemy hits:
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "EnemyBullet")
        {
            // play particles
            Instantiate(injuredParticleSystem, transform.position, Quaternion.identity);
            CameraShake.Instance.ShakeCamera(5f, 0.1f);
            healthBar.setHealth(currentHealth);
        }
    }
}
