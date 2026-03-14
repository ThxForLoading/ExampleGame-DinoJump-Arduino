using NUnit.Framework.Constraints;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Ground Config")]
    [SerializeField] Transform[] _visualGroundSegments;
    [SerializeField] Vector2 _offset;
    [SerializeField] float _startSpeed;
    [SerializeField] float _speedIncrement;

    private GameController _gameController;
    private float _speed;

    private void Start()
    {
        if(_visualGroundSegments.Length != 3)
        {
            Debug.Log("Ground segments not correctly assigned");
        }
        else
        {
            _visualGroundSegments[0].position = new Vector2(_offset.x * - 1, _offset.y);
            _visualGroundSegments[1].position = new Vector2(0, _offset.y);
            _visualGroundSegments[2].position = new Vector2(_offset.x, _offset.y);
        }

        if(_gameController == null)
        {
            _gameController = GameController.instance;
        }
    }

    private void FixedUpdate()
    {
        if(_gameController.currentState == GameState.Running)
        {
            _speed = _startSpeed + _gameController.gameTimer * _speedIncrement;
            foreach (Transform t in _visualGroundSegments)
            {
                t.position = Vector3.Lerp(t.position, t.position + new Vector3(_speed * -1, 0, 0), Time.deltaTime);
                if (t.position.x < _offset.x * -1.5f)
                {
                    t.position += new Vector3(_offset.x * _visualGroundSegments.Length, 0, 0);
                    if(t.gameObject.TryGetComponent<ObstacleSpawner>(out ObstacleSpawner os))
                    {
                        os.SpawnObstacleInSegment();
                    }
                }
            }
        }
    }
}
