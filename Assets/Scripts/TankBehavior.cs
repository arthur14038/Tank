using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankBehavior : MonoBehaviour
{
    [SerializeField]
    Transform m_Turret;
    [SerializeField]
    AudioClip m_ReloadSound;
    [SerializeField]
    AudioClip m_SlashSound;
    [SerializeField]
    AudioClip m_SlashReloadSound;
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
    TankAttack_Base mTankAttack;
    AttackType mCurrentAttackType = AttackType.Cannon;

    public void Init()
    {
        mTankRigidbody = GetComponent<Rigidbody>();
        mTankRigidbody.isKinematic = true;

        SetAttackType(AttackType.Cannon);
    }

    public void SetAttackType(AttackType attackType)
    {
        mCurrentAttackType = attackType;
        if (mTankAttack != null)
        {
            mTankAttack.OnReloadComplete -= ReloadComplete;
            mTankAttack.OnFireEvent -= OnFire;
        }

        switch (attackType)
        {
            case AttackType.Cannon:
                mTankAttack = new TankAttack_Cannon(m_Turret);
                break;
            case AttackType.Slash:
                mTankAttack = new TankAttack_Slash(m_Turret);
                break;
        }
        mTankAttack.OnReloadComplete += ReloadComplete;
        mTankAttack.OnFireEvent += OnFire;
    }

    public void SetTankType(TankType tankType)
    {
        ObjectType objectType = ObjectType.Tank_Green;
        mTankAttack.Damage = 25f;
        switch (tankType)
        {
            case TankType.Blue:
                mTankAttack.Damage = 20f;
                objectType = ObjectType.Tank_Blue;
                break;
            case TankType.Red:
                mTankAttack.Damage = 10f;
                objectType = ObjectType.Tank_Red;
                break;
        }

        if (mTankModel != null)
            mTankModel.Recycle();

        mTankModel = ObjectPool.Instance.GetRecyclableObject(objectType);
        mTankModel.Spawn(transform.position, transform.rotation, transform);

        mTankRigidbody.isKinematic = false;
    }

    private void Update()
    {
        mTankAttack.UpdateAttack();        
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
    }

    private void OnFire(InputValue value)
    {
        mTankAttack.OnFire();
    }

    void ReloadComplete()
    {
        switch (mCurrentAttackType)
        {
            case AttackType.Slash:
                m_AudioSource.PlayOneShot(m_SlashReloadSound);
                break;
            case AttackType.Cannon:
                m_AudioSource.PlayOneShot(m_ReloadSound);
                break;
        }
    }

    void OnFire()
    {
        switch(mCurrentAttackType)
        {
            case AttackType.Slash:
                m_AudioSource.PlayOneShot(m_SlashSound);
                break;
        }
    }
}
