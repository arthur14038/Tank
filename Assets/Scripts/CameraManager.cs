using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_FollowPoint;

    private void FixedUpdate()
    {
        transform.forward = Vector3.Lerp(transform.forward, m_FollowPoint.transform.forward, 0.5f);
        transform.position = Vector3.Lerp(transform.position, m_FollowPoint.transform.position, 0.5f);
    }
}
