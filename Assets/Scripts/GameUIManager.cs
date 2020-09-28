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

public enum AttackType
{
    Cannon = 0,
    Slash = 1
}

public class GameUIManager : SingletonMonoBehavior<GameUIManager>
{
    [SerializeField]
    RectTransform m_GameUIRoot;
    [SerializeField]
    RectTransform m_ChooseTankUIRoot;
    [SerializeField]
    RectTransform m_ChangeAttackUIRoot;
    [SerializeField]
    Button m_ChooseTankOK;
    [SerializeField]
    Button m_ChooseTank;
    [SerializeField]
    Button m_ChangeAttack;
    [SerializeField]
    Button m_ChangeAttackOK;
    [SerializeField]
    GameObject m_Showroom;
    [SerializeField]
    Image m_CD;

    TankType mCurrentTankType = TankType.Green;
    AttackType mCurrentAttackType = AttackType.Cannon;

    public event Action<TankType> OnTankChanged;
    public event Action<AttackType> OnAttackChanged;

    public void Init()
    {
        m_ChangeAttackUIRoot.gameObject.SetActive(false);
        m_GameUIRoot.gameObject.SetActive(false);
        m_ChooseTankUIRoot.gameObject.SetActive(true);
        m_Showroom.SetActive(true);

        m_ChooseTankOK.onClick.AddListener(OnClickChooseOK);
        m_ChooseTank.onClick.AddListener(OnClickChooseTank);
        m_ChangeAttack.onClick.AddListener(OnClickChangeAttack);
        m_ChangeAttackOK.onClick.AddListener(OnClickAttackOK);
    }

    public void SetCD(float amount)
    {
        m_CD.fillAmount = amount;
    }

    public void OnToggleTankChanged(int index)
    {
        mCurrentTankType = (TankType)index;
    }

    public void OnToggleAttackChanged(int index)
    {
        mCurrentAttackType = (AttackType)index;
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

    void OnClickChangeAttack()
    {
        m_ChangeAttackUIRoot.gameObject.SetActive(true);
        m_GameUIRoot.gameObject.SetActive(false);
    }

    void OnClickAttackOK()
    {
        OnAttackChanged?.Invoke(mCurrentAttackType);
        m_ChangeAttackUIRoot.gameObject.SetActive(false);
        m_GameUIRoot.gameObject.SetActive(true);
    }
}
