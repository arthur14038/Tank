using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Tank_Green,
    Tank_Blue,
    Tank_Red,
    Bullet,
    HitEffect,
    HayCollapse,
    SlashEffect,
    End
}

public class ObjectPool : SingletonMonoBehavior<ObjectPool>
{
    [SerializeField]
    RecyclableObjectData[] m_Objects;

    Dictionary<ObjectType, Queue<RecyclableObject>> m_PoolContents;
    
    public void Init()
    {
        m_PoolContents = new Dictionary<ObjectType, Queue<RecyclableObject>>();
        for(int i = 0; i < (int)ObjectType.End; ++i)
            m_PoolContents[(ObjectType)i] = new Queue<RecyclableObject>();

        for (int i = 0; i < m_Objects.Length; ++i)
        {
            LoadMore(m_Objects[i].RecyclableObject, m_Objects[i].Count);
        }
    }

    public RecyclableObject GetRecyclableObject(ObjectType objectType)
    {
        var ro = m_PoolContents[objectType].Dequeue();
        if (m_PoolContents[objectType].Count == 0)
            LoadMore(ro, 1);
        return ro;
    }

    void LoadMore(RecyclableObject recyclableObject , int count)
    {
        for(int i = 0; i < count ; ++i)
        {
            var ro = Instantiate(recyclableObject, transform);
            ro.Init(Recycle);
            m_PoolContents[recyclableObject.ObjectType].Enqueue(ro);
        }
    }

    void Recycle(RecyclableObject recyclableObject)
    {
        recyclableObject.transform.parent = transform;
        m_PoolContents[recyclableObject.ObjectType].Enqueue(recyclableObject);
    }
}

[Serializable]
public class RecyclableObjectData
{
    public RecyclableObject RecyclableObject;
    public int Count;
}