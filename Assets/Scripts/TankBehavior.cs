using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankBehavior : MonoBehaviour
{
    [SerializeField]
    Transform m_Turret;
    [SerializeField]
    AudioClip m_IdleSound;
    [SerializeField]
    AudioClip m_DrivingSound;
    [SerializeField]
    AudioClip m_ReloadSound;
    [SerializeField]
    AudioSource m_AudioSource;

    Rigidbody mTankRigidbody;
    Vector3 mCurrentInput = Vector3.zero;
    float mRotateSenstivity = 50f;
    float mSpeed = 6f;
    private float mMovementInputValue;
    private float mTurnInputValue;
    Vector3 mMovement = Vector3.forward;
    RecyclableObject mTankModel = null;
    float mShootForce = 5f;
    float mCurrentDamage = 25;
    float mCurrentReloadTime = 0f;
    float mReloadTime = 1.5f;

    public void Init()
    {
        mTankRigidbody = GetComponent<Rigidbody>();
        mTankRigidbody.isKinematic = true;
    }

    public void SetTankType(TankType tankType)
    {
        ObjectType objectType = ObjectType.Tank_Green;
        mCurrentDamage = 25f;
        switch (tankType)
        {
            case TankType.Blue:
                mCurrentDamage = 20f;
                objectType = ObjectType.Tank_Blue;
                break;
            case TankType.Red:
                mCurrentDamage = 10f;
                objectType = ObjectType.Tank_Red;
                break;
        }

        if (mTankModel != null)
            mTankModel.Recycle();

        mTankModel = ObjectPool.Instance.GetRecyclableObject(objectType);
        mTankModel.Spawn(transform.position, transform.rotation, transform);
        m_AudioSource.clip = m_IdleSound;
        if (!m_AudioSource.isPlaying)
            m_AudioSource.Play();

        mTankRigidbody.isKinematic = false;
    }

    private void Update()
    {
        if (mCurrentReloadTime > 0f)
        {
            mCurrentReloadTime -= Time.deltaTime;
            GameUIManager.Instance.SetCD(mCurrentReloadTime / mReloadTime);
            if (mCurrentReloadTime <= 0f)
                ReloadComplete();
        }
    }

    private void FixedUpdate()
    {
        if (mTankRigidbody == null)
            return;

        Move();
        Rotate();
    }

    void Move()
    {
        mMovement = transform.forward * mMovementInputValue * mSpeed * Time.fixedDeltaTime;
        mTankRigidbody.MovePosition(mTankRigidbody.position + mMovement);
    }

    void Rotate()
    {
        float rotateAmount = mTurnInputValue * mRotateSenstivity * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotateAmount, 0f);
        mTankRigidbody.MoveRotation(mTankRigidbody.rotation * turnRotation);
    }

    private void OnMove(InputValue value)
    {
        mCurrentInput = value.Get<Vector2>();

        mMovementInputValue = mCurrentInput.y;
        mTurnInputValue = mCurrentInput.x;

        if (mMovementInputValue == 0)
            m_AudioSource.clip = m_IdleSound;
        else
            m_AudioSource.clip = m_DrivingSound;

        if (!m_AudioSource.isPlaying)
            m_AudioSource.Play();
    }

    private void OnFire(InputValue value)
    {
        Fire();
    }

    void Fire()
    {
        if (mCurrentReloadTime > 0f)
            return;

        var bullet = ObjectPool.Instance.GetRecyclableObject(ObjectType.Bullet) as BulletBehavior;
        bullet.Spawn(m_Turret.position, transform.rotation, null);
        bullet.ShootIt(transform.forward * mShootForce, mCurrentDamage);

        Reload();
    }

    void Reload()
    {
        mCurrentReloadTime = mReloadTime;
        GameUIManager.Instance.SetCD(mCurrentReloadTime / mReloadTime);
    }

    void ReloadComplete()
    {
        GameUIManager.Instance.SetCD(0f);
        m_AudioSource.PlayOneShot(m_ReloadSound);
    }
}
