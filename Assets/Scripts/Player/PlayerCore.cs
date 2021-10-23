using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{

    public float maxHealth = 10f;
    private float _curHealth;
    private bool playerDead;
    
    [SerializeField] float shootDelayInSeconds = 2f;

    private bool attackSpeedBoosted = false;
    private float shootDelayInSecondsBoosted;

    private bool canShoot = true;
    private float hitDelayInSeconds = 0.5f;
    private bool isInvincible = false;
    [SerializeField] float invincibleTimeInSeconds = 3f;
    [SerializeField] float AttachSpeedBoostTimeInSeconds = 3f;

    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] GameObject bullet;

    [SerializeField] public Transform shotpointRight;
    [SerializeField] public Transform shotpointLeft;

    public bool isLeft = false;
    public bool isRight = true;

    private SpriteRenderer playerSR;


    // Start is called before the first frame update
    void Start()
    {
        playerSR = GetComponent<SpriteRenderer>();
        shootDelayInSecondsBoosted = shootDelayInSeconds / 5;
        _curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.mousePosition.x <= gameObject.transform.position.x)
        {
            isLeft = true;
            isRight = false;
        }
        else if (Input.mousePosition.x > gameObject.transform.position.x)
        {
            isRight = true;
            isLeft = false;
        }

        if (canShoot && Input.GetKeyDown(KeyCode.Mouse0))
        {
            canShoot = false;
            Shoot();
        }
    }

    void Shoot()
    {
        //...instantiating the rocket
        GameObject bulletInstance;

        if (isLeft)
        {
            bulletInstance = Instantiate(bullet, shotpointLeft.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.GetComponent<PlayerBulletScript>().player = transform;
        }
        else if (isRight)
        {
            bulletInstance = Instantiate(bullet, shotpointRight.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.GetComponent<PlayerBulletScript>().player = transform;
        }

        StartCoroutine(ShootDelay(attackSpeedBoosted ? shootDelayInSecondsBoosted : shootDelayInSeconds));
    }

    public void DamagePlayer(float damages)
    {
        if (!isInvincible)
        {
            Debug.Log("Damages");
            _curHealth -= damages;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().UpdateLife(_curHealth);
            if (_curHealth <= 0)
            {
                //animation
                playerDead = true;
            }
        }
        else
            Debug.Log("nodamages");
    }

    public void Heal(float heal)
    {
        if (_curHealth + heal > maxHealth)
        {
            _curHealth = maxHealth;
        } else
        {
            _curHealth += heal;
        }
    }

    public void BecomeInvincible()
    {
        isInvincible = true;
        StartCoroutine(InvincibleDelay(invincibleTimeInSeconds));
    }

    public void AttackSpeedBoost()
    {
        attackSpeedBoosted = true;
        StartCoroutine(AttackSpeedBoostDelay(AttachSpeedBoostTimeInSeconds));
    }

    IEnumerator ShootDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    IEnumerator InvincibleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInvincible = false;
    }

    IEnumerator AttackSpeedBoostDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackSpeedBoosted = false;
    }

    public bool isPlayerDead()
    {
        return playerDead;
    }
}
