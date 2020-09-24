using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclableObject : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Root;
    [SerializeField]
    public ObjectType ObjectType;
    [SerializeField]
    protected float m_AutoDestroyedTime = -1;

    protected Action<RecyclableObject> OnRecycle;
    float mCurrentLifeTime;

    public virtual void Init(Action<RecyclableObject> recycle)
    {
        m_Root.SetActive(false);
        OnRecycle = recycle;
    }

    public virtual void Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        m_Root.SetActive(true);
        m_Root.transform.position = position;
        m_Root.transform.rotation = rotation;
        m_Root.transform.parent = parent;

        mCurrentLifeTime = 0f;
    }

    private void Update()
    {
        if(m_AutoDestroyedTime > 0)
        {
            mCurrentLifeTime += Time.deltaTime;
            if (mCurrentLifeTime >= m_AutoDestroyedTime)
                Recycle();
        }
    }

    public virtual void Recycle()
    {
        m_Root.SetActive(false);
        OnRecycle(this);
    }
}
