using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_Player;

    Vector3 mFollowDistance;

    void Start()
    {
        mFollowDistance = new Vector3(0f, 3f, -7.2f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = m_Player.transform.forward;
        this.transform.position = m_Player.transform.position + m_Player.transform.TransformDirection(mFollowDistance);
    }
}
