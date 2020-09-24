using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TankType
{
    Green = 0,
    Blue,
    Red
}

public class GameUIManager : SingletonMonoBehavior<GameUIManager>
{
    [SerializeField]
    RectTransform m_GameUIRoot;
    [SerializeField]
    RectTransform m_ChooseTankUIRoot;
    [SerializeField]
    Button m_ChooseTankOK;
    [SerializeField]
    Button m_ChooseTank;
    [SerializeField]
    GameObject m_Showroom;
    [SerializeField]
    Image m_CD;

    TankType mCurrentTankType = TankType.Green;

    public Action<TankType> OnTankChanged;

    public void Init()
    {
        m_GameUIRoot.gameObject.SetActive(false);
        m_ChooseTankUIRoot.gameObject.SetActive(true);
        m_Showroom.SetActive(true);

        m_ChooseTankOK.onClick.AddListener(OnClickChooseOK);
        m_ChooseTank.onClick.AddListener(OnClickChooseTank);
    }

    public void SetCD(float amount)
    {
        m_CD.fillAmount = amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleTankChanged(int index)
    {
        mCurrentTankType = (TankType)index;
    }

    void OnClickChooseOK()
    {
        OnTankChanged?.Invoke(mCurrentTankType);
        m_GameUIRoot.gameObject.SetActive(true);
        m_ChooseTankUIRoot.gameObject.SetActive(false);
        m_Showroom.SetActive(false);
    }

    void OnClickChooseTank()
    {
        m_GameUIRoot.gameObject.SetActive(false);
        m_ChooseTankUIRoot.gameObject.SetActive(true);
        m_Showroom.SetActive(true);
    }
}
