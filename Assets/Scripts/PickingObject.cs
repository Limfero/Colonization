using UnityEngine;

[RequireComponent (typeof(Collider))]
public class PickingObject : MonoBehaviour
{
    private Collider _collider;
    private Spawner<PickingObject> _spawner;
    private bool _isBusy = false;

    public bool IsBusy => _isBusy;

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
        transform.position = _spawner.transform.position;

        _collider.enabled = true;
    }

    public void Relese()
    {
        _isBusy = false;
        _spawner.Relese(this);
    }

    public void Init(Spawner<PickingObject> spawner) => _spawner = spawner;

    public void Hold() => _isBusy = true;
}
