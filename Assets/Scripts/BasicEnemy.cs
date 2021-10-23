using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BasicEnemy : MonoBehaviour
{
    protected GameObject player;
    public float maxHealth = 10.0f;
    private float _curHealth;


    public float speed = 1.0f;
    public float attackRange = 1.0f;
    public float enemyAreaDetection = 10.0f;
    public float attackDelayInSeconds = 1.0f;
    private bool canAttack = true;

    private SpriteRenderer enemySR;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemySR = GetComponent<SpriteRenderer>();
        _curHealth = maxHealth;

        canAttack = false;
        StartCoroutine(AttackDelay());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= enemyAreaDetection)
            MoveToPlayer();
        else
            Rest();
    }

    public virtual void MoveToPlayer()
    {
        //rotate to look at player
        /*transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);*/

        //move towards player
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            if (player.transform.position.x < transform.position.x)
            {
                enemySR.flipX = true;
                transform.Translate(new Vector3(speed * Time.deltaTime * -1, 0, 0));
            }
            else if (player.transform.position.x > transform.position.x)
            {
                enemySR.flipX = false;
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }

        } else if (canAttack) {
            canAttack = false;
            Hit();
        } else
        {
            Rest();
        }
    }

    public virtual void Rest()
    {
        //animation
        return;
    }

    public abstract void Hit();

    public virtual void DamageEnemy(float damages)
    {
        _curHealth -= damages;
        if (_curHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>().AddKill();
            //animation of death
            Destroy(gameObject);
        }
        //Animation for damages
    }

    public IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelayInSeconds);
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Hit();
        }
    }
}