using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;

    public bool isGrounded;
    public PlayerState state = PlayerState.Idle;

    private bool _jumpCooldown;
    private ArduinoComms _arduino;
    private Animator _animator;
    private GameController _gameController;

    [SerializeField] private PolygonCollider2D _colliderRun;
    [SerializeField] private PolygonCollider2D _colliderDuck;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _arduino = ArduinoComms.instance;
        _animator = GetComponent<Animator>();
        _gameController = GameController.instance;
    }

    private void Update()
    {
        if (_rb.linearVelocityY == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (_arduino.duckButton)
        {
            Duck();
        } 
        else if (_arduino.jumpButton)
        {
            Jump();
            state = PlayerState.Running;
        }
        else if( GameController.instance.currentState == GameState.Running )
        {
            state = PlayerState.Running;
        }
        else if(_gameController.currentState == GameState.GameOver )
        {
            state = PlayerState.Hurt;
        }
        else
        {
            state = PlayerState.Idle;
        }

        if(state == PlayerState.Ducking)
        {
            _colliderRun.enabled = false;
            _colliderDuck.enabled = true;
        }
        else
        {
            _colliderRun.enabled = true;
            _colliderDuck.enabled = false;
        }

        _animator.SetInteger("PlayerState", (int) state);
    }

    public void Jump()
    {
        if (!isGrounded) return;
        if (_jumpCooldown) return;

        _jumpCooldown = true;
        StartCoroutine(ResetJumpCooldown());

        if (_gameController.currentState != GameState.Running)
        {
            _gameController.PlayerInput();
            return;
        }

        _rb.AddForce(new Vector2(0,400));
    }

    IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(0.7f);
        _jumpCooldown = false;
    }

    public void Duck()
    {
        if(!isGrounded) return;

        if(state != PlayerState.Ducking) state = PlayerState.Ducking;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision == null) return;
        if(collision.gameObject.tag == "Obstacle")
        {
            _gameController.currentState = GameState.GameOver;
        }
    }
}

public enum PlayerState
{
    Idle,
    Running,
    Ducking,
    Hurt,
    None
}
