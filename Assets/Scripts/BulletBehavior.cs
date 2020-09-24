using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : RecyclableObject
{
    [SerializeField]
    Rigidbody m_Rigidbody;
    [SerializeField]
    TrailRenderer m_TrailRenderer;

    float mDamage;

    public override void Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        m_TrailRenderer.Clear();
        base.Spawn(position, rotation, parent);
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    public void ShootIt(Vector3 force, float damage)
    {
        mDamage = damage;
        m_Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var unit = collision.collider.GetComponentInParent<Unit>();
        if(unit != null)
            unit.DealDamage(mDamage);

        var hitEffect = ObjectPool.Instance.GetRecyclableObject(ObjectType.HitEffect);
        hitEffect.Spawn(collision.contacts[0].point, Quaternion.identity, null);
        Recycle();
    }
}
