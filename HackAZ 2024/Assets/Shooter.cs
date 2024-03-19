using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    // Interval between shots
    public float shootingInterval = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the shooting coroutine
        StartCoroutine(ShootPeriodically());
    }

    IEnumerator ShootPeriodically()
    {
        // Infinite loop to keep shooting
        while (true)
        {
            // Shoot a bullet
            ShootBullet();

            // Wait for the specified interval before shooting again
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    void ShootBullet()
    {
        var bulletNew = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletNew.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
    }
}
