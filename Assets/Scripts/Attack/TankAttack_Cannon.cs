using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack_Cannon : TankAttack_Base
{
    public TankAttack_Cannon(Transform attackPoint) : base(attackPoint)
    {

    }

    float mShootForce = 5f;

    public override void OnFire()
    {
        if (!CanUse())
            return;

        var bullet = ObjectPool.Instance.GetRecyclableObject(ObjectType.Bullet) as BulletBehavior;
        bullet.Spawn(mAttackPoint.position, mAttackPoint.rotation, null);
        bullet.ShootIt(mAttackPoint.forward * mShootForce, Damage);

        Reload();
    }
}
