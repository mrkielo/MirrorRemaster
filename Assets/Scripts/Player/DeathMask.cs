using System.Collections;
using UnityEngine;

public class DeathMask : MonoBehaviour
{
    [SerializeField] Vector3 endScale;
    public float time;
    [SerializeField] Transform mask1;
    [SerializeField] Transform mask2;
    [SerializeField] AudioSource audioSource;

    public void Play(Vector3 pos1, Vector3 pos2)
    {
        mask1.position = pos1;
        gameObject.SetActive(true);
        mask2.position = pos2;
        audioSource.Play();
        StartCoroutine(DeathMaskCoroutine());
    }

    IEnumerator DeathMaskCoroutine()
    {
        Vector3 startScale = mask1.localScale;
        float startTime = Time.time;
        while (startTime + time > Time.time)
        {
            mask1.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime) / time);
            mask2.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime) / time);
            yield return null;
        }
    }
}
