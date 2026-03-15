using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool isGrounded;
    private bool _jumpCooldown;
    private ArduinoComms _arduino;

    [SerializeField] GameObject _Head;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _arduino = ArduinoComms.instance;
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
        else
        {
            _Head.SetActive(true);
        } 
        
        if (_arduino.jumpButton)
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (!isGrounded) return;
        if (_jumpCooldown) return;
        if (!_Head.activeSelf) return;

        _jumpCooldown = true;
        StartCoroutine(ResetJumpCooldown());

        _rb.AddForce(new Vector2(0,700));
    }

    IEnumerator ResetJumpCooldown()
    {
        yield return new WaitForSeconds(0.7f);
        _jumpCooldown = false;
    }

    public void Duck()
    {
        if(!isGrounded) return;

        _Head.SetActive(false);
    }
}
