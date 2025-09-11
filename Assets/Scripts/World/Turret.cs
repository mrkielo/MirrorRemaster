using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float shootDelay;
    [SerializeField] float shootInterval;
    [SerializeField] float bulletSpeed;

    [Header("Other")]
    [SerializeField] Vector3 bulletSpawnOffset;
    [SerializeField] GameObject bulletPrefab;

    float timer;

    void Start()
    {
        timer = Time.time + shootDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer + shootInterval < Time.time)
        {
            Shoot();
            timer = Time.time;
        }

    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, (transform.position + bulletSpawnOffset).FlatZ(), Quaternion.identity).GetComponent<Bullet>();
        bullet.Launch(bulletSpeed);
    }
}
