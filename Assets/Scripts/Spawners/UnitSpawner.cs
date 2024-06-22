using System.Linq;
using UnityEngine;

public class UnitSpawner : Spawner<Unit>
{
    protected override Unit Spawn()
    {
        Unit unit = Instantiate(Prefab.First(), transform.position, Quaternion.identity);

        return unit;
    }
}
