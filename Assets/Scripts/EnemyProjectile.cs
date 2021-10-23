using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float damages = 1f;

    public Transform enemy;
    private SpriteRenderer projectileSR;

    //[SerializeField] GameObject destroyEffect;

    private void Start()
    {
        projectileSR = GetComponent<SpriteRenderer>();
        Invoke("DestroyProjectile", lifeTime);
        if (enemy.transform.position.x < transform.position.x)
            projectileSR.flipX = false;
        else if (enemy.transform.position.x > transform.position.x)
            projectileSR.flipX = true;
    }

    private void Update()
    {
        if (enemy.position.x < transform.position.x)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        else if (enemy.position.x > transform.position.x)
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
        if (collision.gameObject.tag == "Player")
        {
            PlayerCore _playerCore = collision.collider.GetComponent<PlayerCore>();
            _playerCore.DamagePlayer(damages);
            DestroyProjectile();
        }
    }
}
