using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float damages = 1f;

    private Vector2 shootDirection;
    public Transform player;

    //[SerializeField] GameObject destroyEffect;

    private void Start()
    {
        shootDirection = Input.mousePosition;
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        if (player.position.x < transform.position.x)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        else if (player.position.x > transform.position.x)
            transform.Translate(new Vector3(speed * Time.deltaTime * -1, 0, 0));
    }

    private void DestroyProjectile()
    {
        //Animation de destruction
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            BasicEnemy _enemy = collision.collider.GetComponent<BasicEnemy>();
            _enemy.DamageEnemy(damages);
            Destroy(gameObject);
        }
    }
}
