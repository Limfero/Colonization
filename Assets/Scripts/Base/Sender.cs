using UnityEngine;

public class Sender : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _finishPoint;

    public void SendUnit(Resource target, Unit unit)
    {
        unit.Init(target.transform.position, _finishPoint.position);
    }

    public void SendBuilder(UnitBuilder builder, Vector3 position)
    {
        UnitBuilder unit = Instantiate(builder, _spawnPoint.position, Quaternion.identity);
        unit.Init(position);
    }
}
