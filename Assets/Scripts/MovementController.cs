using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed;
    float speedMultiplier;
    bool btnPressed;

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    [Range(1, 10)]
    [SerializeField] float accelaration;

    Vector2 relateTranform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    private void Start()
    {
        UpdateRelateTranform();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier * relateTranform.x;
         

        if (isOnPlatform )
        {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.04f, 0.35f), 0, wallLayer);

        if (isWallTouch )
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        UpdateRelateTranform();
    }

    protected void UpdateRelateTranform()
    {
        relateTranform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
        }
        else if (value.canceled)
        {
            btnPressed = false;
        }
    }

    private void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime * accelaration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime * accelaration;
            if (speedMultiplier < 0) speedMultiplier = 0;
        }
    }
}
