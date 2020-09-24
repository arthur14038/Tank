using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : SingletonMonoBehavior<GameLogic>
{
    [SerializeField]
    TankBehavior m_Tank;

    // Start is called before the first frame update
    void Start()
    {
        GameUIManager.Instance.Init();
        GameUIManager.Instance.OnTankChanged += OnChangeTank;

        m_Tank.Init();

        ObjectPool.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameUIManager.Instance.OnTankChanged -= OnChangeTank;
    }

    void OnChangeTank(TankType tankType)
    {
        m_Tank.SetTankType(tankType);
    }
}
