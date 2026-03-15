using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject _cloudPrefab;
    [SerializeField] float _yMin;
    [SerializeField] float _yMax;
    [SerializeField] float _xOffset;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(_xOffset, _yMin, 0), 0.1f);
        Gizmos.DrawSphere(new Vector3(_xOffset, _yMax, 0), 0.1f);
    }
}
