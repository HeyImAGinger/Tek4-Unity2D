using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BasicEnemy
{
    [SerializeField] GameObject projectile;

    public override void Hit()
    {
        GameObject bullet;
        Transform shotpointLeft = GameObject.FindGameObjectWithTag("pointleft").transform;
        Transform shotpointRight = GameObject.FindGameObjectWithTag("pointright").transform;
        if (player.transform.position.x < transform.position.x)
        {
            bullet = Instantiate(projectile, shotpointLeft.position, shotpointLeft.rotation);
            bullet.GetComponent<EnemyProjectile>().enemy = gameObject.transform;
        }
        else if (player.transform.position.x > transform.position.x)
        {
            bullet = Instantiate(projectile, shotpointRight.position, shotpointRight.rotation);
            bullet.GetComponent<EnemyProjectile>().enemy = gameObject.transform;
        }
        StartCoroutine(AttackDelay());

        StartCoroutine(AttackDelay());
    }

    public override void DamageEnemy(float damages)
    {
        base.DamageEnemy(damages);
    }

    public override void MoveToPlayer()
    {
        base.MoveToPlayer();
    }

    public override void Rest()
    {
        base.Rest();
    }
}
