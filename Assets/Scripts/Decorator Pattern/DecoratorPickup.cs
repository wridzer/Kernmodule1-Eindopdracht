using System;
using System.Collections;
using System.Linq;
using UnityEngine;


public class DecoratorPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IGun gun = collision.gameObject.GetComponent<Player>()?.gun;

        if (gun != null)
        {
            
        }
    }
}