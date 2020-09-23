using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_TankRigidbody;
    [SerializeField]
    Transform m_Camera;

    Vector3 mCurrentInput = Vector3.zero;
    float mRotateSenstivity = 0.25f;
    float mSpeed = 3f;

    private void Awake()
    {
        InputManager.Instance.MoveDataUpdate += OnMoveData;
        InputManager.Instance.OnJumpDown += RestTank;
    }

    private void OnDestroy()
    {
        InputManager.Instance.MoveDataUpdate -= OnMoveData;
        InputManager.Instance.OnJumpDown -= RestTank;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMoveData(float horizontal, float vertical)
    {
        mCurrentInput.z = vertical;

        if(vertical != 0f)
        {
            var velocity = transform.TransformDirection(mCurrentInput) * mSpeed;
            velocity.y = 0f;
            m_TankRigidbody.velocity = velocity;
        }

        var eulerAngle = transform.eulerAngles;
        eulerAngle.y += horizontal* mRotateSenstivity;
        transform.eulerAngles = eulerAngle;
    }

    void RestTank()
    {
        transform.position += Vector3.up*0.5f;
        transform.eulerAngles = Vector3.zero;
    }
}