using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool { 

    public List<GameObject> activePool = new();
    private List<GameObject> inactivePool = new();

    private GameObject prefab;

    public GameObjectPool(GameObject prefab) {
        this.prefab = prefab;
    }

    private GameObject AddNewItemToPool() {
        GameObject instance = GameObject.Instantiate(prefab);
        inactivePool.Add(instance);
        return instance;
    }

    public GameObject GetObjectFromPool() {
        if (inactivePool.Count > 0)
            return ActivateItem(inactivePool[0]);

        return ActivateItem(AddNewItemToPool());
    }

    public GameObject ActivateItem(GameObject _item) {
        _item.SetActive(true);

        if (inactivePool.Contains(_item))
            inactivePool.Remove(_item);

        activePool.Add(_item);
        return _item;
    }

    public void ReturnObjectToInactivePool(GameObject _item) {
        if (activePool.Contains(_item))
            activePool.Remove(_item);

        _item.SetActive(false);
        inactivePool.Add(_item);
    }
}