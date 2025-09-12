using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Sprite openSprite;
    [SerializeField] Sprite closeSprite;

    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closeSprite;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = openSprite;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = closeSprite;
        }
    }
}
