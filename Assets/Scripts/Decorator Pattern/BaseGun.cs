using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseGun : MonoBehaviour, IGun
{
    public int Damage { get; set; } = 5;
    public float BulletAmount { get; set; } = 1;
    public int GrowAmount { get; set; } = 1;
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }
    public BulletType BulletTypes { get; set; }
    public GameObject BulletPrefab { get; set; }

    public GameObject bulletPrefab, gunPoint;
    public float bulletSpeed, growMultiplier;

    private GameObjectPool bulletPool;

    private void Start()
    {
        BulletPrefab = bulletPrefab;
        bulletPool = new GameObjectPool(bulletPrefab);
    }

    public void Shoot()
    {
        for(float i = 0; i < BulletAmount; i++)
        {
            GameObject newBullet = bulletPool.GetObjectFromPool();
            newBullet.transform.position = gunPoint.transform.position;
            newBullet.transform.localScale = new Vector3(
                transform.localScale.x * (GrowAmount * growMultiplier),
                transform.localScale.y * (GrowAmount * growMultiplier),
                transform.localScale.z * (GrowAmount * growMultiplier));
            IBullet bulletScript = newBullet.GetComponent<IBullet>();
            bulletScript.pool = bulletPool;
            bulletScript.Damage = Damage * GrowAmount;
            bulletScript.ExplodeRadius = ExplodeRadius;
            bulletScript.BounceAmount = BounceAmount;

            //yeet bullet in direction
            Vector3 curRot = gunPoint.transform.eulerAngles;
            float shootRotation = -90f + (180f * ((i + 1f) / (BulletAmount + 1f)));
            Vector3 shootDir = new Vector3(curRot.x, curRot.y + shootRotation, curRot.z);
            bulletScript.Shoot(shootDir, bulletSpeed);
        }
    }
}