using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : BasicEnemy
{
    [SerializeField] float AttackDamages = 1f;

    public override void Hit()
    {
        PlayerCore _playerCore = player.GetComponent<PlayerCore>();
        _playerCore.DamagePlayer(AttackDamages);

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
