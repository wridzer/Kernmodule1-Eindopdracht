using System.Collections;
using UnityEngine;


public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private int ExplodeDamage = 10;

    public int Damage { get; set; }
    public int ExplodeRadius { get; set; }
    public int BounceAmount { get; set; }
    public BulletType BulletTypes { get; set; }

    private Rigidbody rb;

    public Bullet(int _damage)
    {
        Damage = _damage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //hit for enim and bounce and explode

    public void Hit(GameObject other)
    {
        other.GetComponent<IDamageble>()?.TakeDamage(Damage);
        Debug.Log("Damage done is: " + Damage + " to " + other.name);
    }

    public void Shoot(Vector3 _Dir)
    {
        rb.AddForce(_Dir);
    }

    private void Bounce(GameObject other)
    {
        rb.AddForce(Vector3.Reflect(transform.position, other.transform.forward));
    }

    private void Explode()
    {
        RaycastHit[] hit;

        hit = Physics.SphereCastAll(transform.position, ExplodeRadius, Vector3.zero);
        foreach(RaycastHit raycastHit in hit)
        {
            raycastHit.collider.GetComponent<IDamageble>()?.TakeDamage(ExplodeDamage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
                Destroy(gameObject);    // OBJECTPOOL HERE
            }
            else
            {
                Destroy(gameObject);    // OBJECTPOOL HERE
            }
        }
    }
}