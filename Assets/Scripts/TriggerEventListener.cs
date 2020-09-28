using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventListener : MonoBehaviour
{
    public event Action<Collider> OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }
}
