using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

[SelectionBase]
public class Hero : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("References")]
    [SerializeField] Collider2D groundCheck;
    public HeroAnimation heroAnimation;
    [SerializeField] HeroSounds sounds;

    [Header("Data")]
    [HideInInspector] public WorldData worldData;
    [HideInInspector] public PlayerData playerData;
    [HideInInspector] public LevelData levelData;

    [SerializeField] ParticleSystem deathParticles;
    public event Action OnDeath;

    float coyoteTimer;
    float jumpBufferTimer = -1;
    int facingDir;
    bool isDashing = false;
    bool isDashCharged = false;
    bool isDead = false;
    public bool isWon = false;
    float inputDir;

    void Start()
    {
        OnDeath += PlayDeathEffects;
        coyoteTimer = -playerData.coyoteTime;
    }

    void Update()
    {
        AnimationTree();

        if (CheckGround())
        {
            if (CheckJumpBuffer())
            {
                TryJump();
            }
            coyoteTimer = Time.time;
            isDashCharged = true;
        }
    }

    public void TryDash()
    {
        if (CanDash())
        {
            sounds.PlayClip(sounds.Dash);
            StartCoroutine(DashCoroutine());
            isDashCharged = false;
        }
    }

    IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        isDashing = true;
        int dashDir = facingDir;
        while (startTime + playerData.dashTime > Time.time)
        {
            rb.linearVelocity = new Vector2(playerData.dashPower * dashDir, 0);
            yield return null;
        }
        isDashing = false;
    }


    bool CheckGround()
    {
        return groundCheck.IsTouchingLayers(worldData.groundLayers);
    }

    bool CheckJumpBuffer()
    {
        return jumpBufferTimer + playerData.jumpBufferTime > Time.time;
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

    public bool CanDash()
    {
        return levelData.canDash && !CheckGround() && isDashCharged;
    }

    public void Move(float dir)
    {
        inputDir = dir;
        if (isDashing) return;

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
        if (inputDir != 0 && CheckGround()) sounds.PlayClipIfEmpty(sounds.Walk);
        if (inputDir == 0) sounds.StopClip(sounds.Walk);

    }

    public void TryJump()
    {
        if (CanJump())
        {
            rb.linearVelocityY = 0;
            sounds.PlayClip(sounds.Jump);
            rb.AddForceY(playerData.jumpForce);
            jumpBufferTimer = -1; // safe value to prevent multi jumping
        }
        else
        {
            jumpBufferTimer = Time.time;
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
        isDead = true;
        sounds.PlayClip(sounds.Die);
        deathParticles.Play();
        StartCoroutine(FadeOut());

    }

    void AnimationTree()
    {
        if (isWon)
        {
            heroAnimation.SetState(HeroAnimation.Win);
            return;
        }
        if (isDead)
        {
            heroAnimation.SetState(HeroAnimation.Death);
            return;
        }
        if (isDashing)
        {
            heroAnimation.SetState(HeroAnimation.Dash);
            return;
        }
        if (CheckGround())
        {
            if (inputDir != 0)
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

    public IEnumerator FadeOut()
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
