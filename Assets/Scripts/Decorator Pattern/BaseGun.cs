using System.Collections;
using UnityEngine;


public class BaseGun : MonoBehaviour, IGun
{
    public GameObject bulletPrefab, gunPoint;
    public float bulletSpeed;
    public int Damage { get; set; } = 5;
    public int BulletAmount { get; set; } = 1;
    public int GrowAmount { get; set; } = 1;
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }
    public BulletType BulletTypes { get; set; }
    public GameObject BulletPrefab { get; set; }

    private void Start()
    {
        BulletPrefab = bulletPrefab;
    }

    public void Shoot()
    {
        for(int i = 0; i < BulletAmount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, gunPoint.transform.position, new Quaternion()); // OBJECTPOOL HERE
            newBullet.transform.localScale = new Vector3(
                transform.localScale.x * GrowAmount,
                transform.localScale.y * GrowAmount,
                transform.localScale.z * GrowAmount);
            IBullet bulletScript = newBullet.GetComponent<IBullet>();
            bulletScript.Damage = Damage * GrowAmount;
            bulletScript.ExplodeRadius = ExplodeRadius;
            bulletScript.BounceAmount = BounceAmount;

            //yeet bullet in direction
            Vector3 forward = gunPoint.transform.forward;
            float shootRadius = -90 + (180 / (i + 2) * (i + 1));
            Vector3 shootVector = new Vector3(forward.x + shootRadius, forward.y, bulletSpeed);
            bulletScript.Shoot(shootVector);
        }
    }
}