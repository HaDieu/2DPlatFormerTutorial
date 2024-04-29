using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [Range(0, 10)]
    [SerializeField] int occurAfterVeclicity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;
    float counter;
    bool isOnGround;

    [Header("")]
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem tounchParticle;


    private void Start()
    {
        tounchParticle.transform.parent = null;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVeclicity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayTounchParticle(Vector2 pos)
    {
        tounchParticle.transform.position = pos;
        tounchParticle.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOnGround = false;
        }
    }
}
