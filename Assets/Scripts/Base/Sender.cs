using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    [SerializeField] private Spawner<Unit> _spawner;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _finishPoint;

    public void SendUnit(Queue<PickingObject> targets)
    {
        Unit unit = _spawner.GetObject(_spawnPoint.position);
        PickingObject target = targets.Dequeue();

        target.Hold();

        unit.Init(target.transform.position, _finishPoint.position);
    }

    public void SendBuilder(UnitBuilder builder, Vector3 position)
    {
        UnitBuilder unit = Instantiate(builder, _spawnPoint.position, Quaternion.identity);
        unit.Init(position);
    }
}
