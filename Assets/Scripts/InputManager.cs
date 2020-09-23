using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehavior<InputManager>
{
    public float Horizontal;
    public float Vertical;

    public Action<float, float> MoveDataUpdate;
    public Action OnJumpDown;
    public Action<float> OnVerticalChanged;
    public Action<float> OnHorizontalChanged;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        Vertical = Input.GetAxis("Vertical");

        MoveDataUpdate?.Invoke(Horizontal, Vertical);

        if (Input.GetButtonDown("Jump"))
            OnJumpDown?.Invoke();
    }

    //public void OnVerticalChanged()
    //{

    //}    
}
