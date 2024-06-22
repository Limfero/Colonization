using System;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Resource : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void PickUp(Transform parent, Transform hands)
    {
        transform.SetParent(parent);
        transform.position = hands.position;

        _collider.enabled = false;
    }

    public void BreakFree()
    {
        transform.SetParent(null);

        _collider.enabled = true;
    }
}
