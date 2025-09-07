using System;
using System.Collections;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] Collider2D groundCheck;
    [SerializeField] WorldData worldData;
    [SerializeField] ParticleSystem deathParticles;
    public event Action OnDeath;

    void Start()
    {
        OnDeath += PlayDeathEffects;
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
        //in case of more jump conditions
        return CheckGround();
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
