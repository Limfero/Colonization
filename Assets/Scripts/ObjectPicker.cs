using UnityEditor.iOS;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    [SerializeField] private Transform _hands;
    [SerializeField] private Unit _unit;

    private Resource _resource;

    private void OnTriggerEnter(Collider other)
    {
        if (_unit.IsBusy == false && other.TryGetComponent(out Resource resource))
        {
            _resource = resource;
            resource.PickUp(transform, _hands);
            _unit.Hold();
            _unit.SetTargetToBase();
        }
    }

    public void BreakFree(Spawner<Resource> resourceSpawnner, ResourceBase resourceBase)
    {
        if (_resource != null)
        {
            _resource.BreakFree();
            resourceSpawnner.Relese(_resource);
            resourceBase.RemoveResource(_resource);
        }
    }
}
