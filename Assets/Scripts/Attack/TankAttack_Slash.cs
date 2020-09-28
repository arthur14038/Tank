using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack_Slash : TankAttack_Base
{
    public TankAttack_Slash(Transform attackPoint) : base(attackPoint)
    {

    }

    protected override float mReloadTime => 3f;
    float mDuration = 4.2f;
    int mHitCount = 16;
    float mCurrentDuration;
    int mCurrentHit;
    float mDurationBetweenHits;
    float mNextTimeHit;

    public override void OnFire()
    {
        if (!CanUse())
            return;

        GameUIManager.Instance.SetCD(1f);
        mAttackWorking = true;
        mCurrentDuration = mDuration;
        mCurrentHit = 0;
        mDurationBetweenHits = mDuration / mHitCount;
        OnFireEvent?.Invoke();

        HitOnce();
    }

    public override void UpdateAttack()
    {
        if(mAttackWorking)
        {
            if(mCurrentDuration > 0f)
            {
                mCurrentDuration -= Time.deltaTime;
                if(mCurrentDuration <= mNextTimeHit)
                    HitOnce();
            }
            else
            {
                mAttackWorking = false;
                Reload();
            }
        }

        base.UpdateAttack();
    }

    void HitOnce()
    {
        Damage = mCurrentHit++;
        mNextTimeHit = mCurrentDuration - mDurationBetweenHits;
        var slash = ObjectPool.Instance.GetRecyclableObject(ObjectType.SlashEffect) as SlashBehavior;
        slash.Spawn(mAttackPoint.position, mAttackPoint.rotation, mAttackPoint);
        slash.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 359f));
        slash.Slash(mDurationBetweenHits, Damage);
    }
}
