using System.Collections;
using UnityEngine;


public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private float ExplodeDamage, lifetime;
    [SerializeField] private GameObject ExplodePrefab;
    private Vector3 hitPos;

    public int Damage { get; set; }
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }
    public BulletType BulletTypes { get; set; }
    public GameObjectPool pool { get; set; }

    private Rigidbody rb;

    public Bullet(int _damage)
    {
        Damage = _damage;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            //Destroy(gameObject);    // OBJECTPOOL HERE
            pool.ReturnObjectToInactivePool(this.gameObject);
        }
    }

    //hit for enim and bounce and explode

    public void Hit(GameObject other)
    {
        other.GetComponent<IDamageble>()?.TakeDamage(Damage);
        Debug.Log("Damage done is: " + Damage + " to " + other.name);
    }

    public void Shoot(Vector3 _Dir, float _BulletForce)
    {
        rb = GetComponent<Rigidbody>();
        rb.transform.eulerAngles = _Dir;
        rb.AddForce(rb.transform.forward * _BulletForce);
    }

    private void Bounce(GameObject other)
    {
        BounceAmount -= 1;
        rb.AddForce(Vector3.Reflect(transform.position, other.transform.forward));
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(ExplodePrefab);
        explosion.transform.position = hitPos;
        explosion.transform.localScale = new Vector3(ExplodeRadius, ExplodeRadius, ExplodeRadius);

        RaycastHit[] hit;

        hit = Physics.SphereCastAll(hitPos, ExplodeRadius, Vector3.zero);
        foreach(RaycastHit raycastHit in hit)
        {
            raycastHit.collider.GetComponent<IDamageble>()?.TakeDamage(ExplodeDamage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitPos = transform.position;
        if (collision.gameObject.GetComponent<IDamageble>() != null)
        {
            Hit(collision.gameObject);
            if (ExplodeRadius > 0)
                Explode();
        }
        else
        {
            if (BounceAmount > 0)
            {
                Bounce(collision.gameObject);
            }
            else if (BounceAmount <= 0 && ExplodeRadius > 0)
            {
                Explode();
                //Destroy(gameObject);    // OBJECTPOOL HERE
                pool.ReturnObjectToInactivePool(this.gameObject);
            }
            else
            {
                //Destroy(gameObject);    // OBJECTPOOL HERE
                pool.ReturnObjectToInactivePool(this.gameObject);
            }
        }
    }
}