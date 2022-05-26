using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable { 
    bool Active { get; set; }
    void Init();
    void OnEnableObject();
    void OnDisableObject();
}

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inactivePool = new List<T>();

    private T AddNewItemToPool() {
        T instance = (T)Activator.CreateInstance(typeof(T));
        instance.Init();
        inactivePool.Add(instance);
        return instance;
    }

    public T GetObjectFromPool() {
        if(inactivePool.Count > 0) 
            return ActivateItem(inactivePool[0]);

        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T _item) {
        _item.OnEnableObject();
        _item.Active = true;

        if (inactivePool.Contains(_item)) 
            inactivePool.Remove(_item);

        activePool.Add(_item);
        return _item;
    }

    public void ReturnObjectToInactivePool(T _item) {
        if (activePool.Contains(_item)) 
            activePool.Remove(_item);

        _item.OnDisableObject();
        _item.Active = false;
        inactivePool.Add(_item);
    }
}