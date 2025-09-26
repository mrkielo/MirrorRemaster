using System;
using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] Collider2D groundCheck;
    [SerializeField] WorldData worldData;
    [SerializeField] PlayerData playerData;

    [SerializeField] ParticleSystem deathParticles;
    public event Action OnDeath;

    float coyoteTimer;
    int direction;

    void Start()
    {
        OnDeath += PlayDeathEffects;
        coyoteTimer = -playerData.coyoteTime;
    }

    void Dash()
    {
        rb.AddForceX(direction * playerData.dashPower);
    }


    bool CheckGround()
    {
        return groundCheck.IsTouchingLayers(worldData.groundLayers);
    }

    public bool CheckPortal()
    {
        return groundCheck.IsTouchingLayers(worldData.portalLayers);
    }

    public bool CanJump()
    {

        if (CheckGround())
        {
            return true;
        }
        else
        {
            if (coyoteTimer + playerData.coyoteTime > Time.time) return true;
            else return false;
        }
    }

    public void Move(float dir)
    {
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, dir * playerData.mSpeed, playerData.mSmoothing);
        if (rb.linearVelocityX > 0) direction = 1;
        else if (rb.linearVelocityX < 0) direction = -1;
    }
    public void Jump()
    {
        rb.AddForceY(playerData.jumpForce);
    }

    void Update()
    {
        if (CheckGround())
        {
            coyoteTimer = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & worldData.lethalLayers) != 0)
        {
            OnDeath?.Invoke();
        }
    }

    void PlayDeathEffects()
    {
        deathParticles.Play();
        StartCoroutine(FadeOut());

    }

    IEnumerator FadeOut()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float startTime = Time.time;
        float duration = 1;
        while (startTime + duration > Time.time)
        {
            sprite.color = Color.Lerp(Color.white, Color.black, (Time.time - startTime) / duration);
            yield return null;
        }
    }
}
