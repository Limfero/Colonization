using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    [SerializeField] private Transform _hands;
    [SerializeField] private Unit _unit;

    private void OnTriggerEnter(Collider other)
    {
        if (_unit.IsBusy == false && other.TryGetComponent(out Resource @object))
        {
            @object.PickUp(transform, _hands);
            _unit.Hold();
            _unit.SetTargetToBase();
        }
    }
}
