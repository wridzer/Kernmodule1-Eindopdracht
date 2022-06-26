using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableGameobject : MonoBehaviour, IPoolable
{
    public GameObject prefab;
    private GameObject gameObject;

    public bool Active { get; set; }

    public void Init()
    {
        gameObject = Instantiate(prefab, this.transform);
    }

    public void OnDisableObject()
    {
        gameObject.SetActive(false);
    }

    public void OnEnableObject()
    {
        gameObject.SetActive(true);
    }
}
