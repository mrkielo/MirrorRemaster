using System;
using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] Collider2D groundCheck;
    [SerializeField] WorldData worldData;
    [SerializeField] PlayerData playerData;
    [SerializeField] HeroAnimation heroAnimation;

    [SerializeField] ParticleSystem deathParticles;
    public event Action OnDeath;

    float coyoteTimer;
    int facingDir;
    int inputDir;

    void Start()
    {
        OnDeath += PlayDeathEffects;
        coyoteTimer = -playerData.coyoteTime;
    }

    public void TryDash()
    {
        if (!CheckGround())
        {
            rb.linearVelocityY = 0;
            rb.AddForceX(facingDir * playerData.dashPower);
        }
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

        if (rb.linearVelocityX > 0)
        {
            facingDir = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.linearVelocityX < 0)
        {
            facingDir = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        rb.AddForceY(playerData.jumpForce);
    }

    void Update()
    {
        AnimationTree();

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

    void AnimationTree()
    {
        if (CheckGround())
        {
            if (rb.linearVelocityX != 0)
            {
                heroAnimation.SetState(HeroAnimation.Run);
            }
            else
            {
                heroAnimation.SetState(HeroAnimation.Idle);
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                heroAnimation.SetState(HeroAnimation.Jump);
            }
            else if (rb.linearVelocityY < 0)
            {
                heroAnimation.SetState(HeroAnimation.Fall);
            }
        }
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
