using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankAttack_Base
{
    public TankAttack_Base(Transform attackPoint)
    {
        mAttackPoint = attackPoint;
    }

    public float Damage;
    public Action OnReloadComplete;
    public Action OnFireEvent;

    protected virtual float mReloadTime => 1.5f;
    protected float mCurrentReloadTime;
    protected Transform mAttackPoint;
    protected bool mAttackWorking = false;
    protected bool mReloading = false;

    public abstract void OnFire();

    protected virtual bool CanUse()
    {
        if (mAttackWorking)
            return false;
        if (mReloading)
            return false;
        return true;
    }

    protected virtual void Reload()
    {
        mReloading = true;
        mCurrentReloadTime = mReloadTime;
        GameUIManager.Instance.SetCD(mCurrentReloadTime / mReloadTime);
    }

    public virtual void UpdateAttack()
    {
        if (mCurrentReloadTime > 0f)
        {
            mCurrentReloadTime -= Time.deltaTime;
            GameUIManager.Instance.SetCD(mCurrentReloadTime / mReloadTime);
            if (mCurrentReloadTime <= 0f)
                ReloadComplete();
        }
    }

    protected virtual void ReloadComplete()
    {
        mReloading = false;
        GameUIManager.Instance.SetCD(0f);
        OnReloadComplete?.Invoke();
    }
}
