using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;
    GameObject[] _obstaclesToSpawn;

    private void Start()
    {
        _obstaclesToSpawn = Resources.LoadAll<GameObject>("Obstacles");
    }

    public void SpawnObstacleInSegment()
    {
        int obs = Random.Range(0, _obstaclesToSpawn.Length);
        int spawn = Random.Range(0, _spawnPoints.Length);

        Instantiate(_obstaclesToSpawn[obs], _spawnPoints[spawn]);
    }
}
