using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Hay : Unit
{
    protected override void DeadEffect()
    {
        var ro =ObjectPool.Instance.GetRecyclableObject(ObjectType.HayCollapse);
        ro.Spawn(transform.position, Quaternion.identity, null);
    }
}
