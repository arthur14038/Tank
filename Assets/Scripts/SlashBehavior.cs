using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SlashBehavior : RecyclableObject
{
    [SerializeField]
    Transform m_ColliderRoot;
    [SerializeField]
    TriggerEventListener m_TriggerEventListener;

    public override void Init(Action<RecyclableObject> recycle)
    {
        m_TriggerEventListener.OnTriggerEnterEvent += OnTriggerEnter;
        base.Init(recycle);
    }

    private void OnDestroy()
    {
        m_TriggerEventListener.OnTriggerEnterEvent -= OnTriggerEnter;
    }

    float mDamage;

    public void Slash(float time, float damage)
    {
        m_ColliderRoot.DOLocalRotate(Vector3.right*179f, time);
        mDamage = damage;
    }

    public override void Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        base.Spawn(position, rotation, parent);

        m_ColliderRoot.transform.localEulerAngles = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponentInParent<Unit>();
        if (unit != null)
            unit.DealDamage(mDamage);

        var rigidbody = other.GetComponentInParent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.AddForce(transform.forward*5f, ForceMode.Impulse);
    }
}
