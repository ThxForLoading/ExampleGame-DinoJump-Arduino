using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject _cloudPrefab;
    [SerializeField] float _yMin;
    [SerializeField] float _yMax;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(0, _yMin, 0), 0.5f);
        Gizmos.DrawSphere(new Vector3(0, _yMax, 0), 0.5f);
    }
}
