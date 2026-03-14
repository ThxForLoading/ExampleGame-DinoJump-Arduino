using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool _isGrounded;
    public float timer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (_rb.linearVelocityY == 0)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        timer += Time.deltaTime;

        if(timer > 5)
        {
            timer = 0;
            Jump();
        }
    }

    public void Jump()
    {
        if (!_isGrounded) return;

        _rb.AddForce(new Vector2(0,700));
    }

    public void Duck()
    {

    }
}
