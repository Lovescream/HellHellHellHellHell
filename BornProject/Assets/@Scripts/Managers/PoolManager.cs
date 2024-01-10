using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool {
    private GameObject _prefab;
    private IObjectPool<GameObject> _pool;
    private Transform _root;

    private Transform Root {
        get {
            if (_root == null) {
                GameObject obj = new() { name = $"[Pool_Root] {_prefab.name}" };
                _root = obj.transform;
            }
            return _root;
        }
    }

    public Pool(GameObject prefab) {
        this._prefab = prefab;
        this._pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }

    public GameObject Pop() {
        return _pool.Get();
    }

    public void Push(GameObject obj) {
        _pool.Release(obj);
    }

    #region Callbacks

    private GameObject OnCreate() {
        GameObject obj = GameObject.Instantiate(_prefab);
        obj.transform.SetParent(Root);
        obj.name = _prefab.name;
        return obj;
    }
    private void OnGet(GameObject obj) {
        obj.SetActive(true);
    }

    private void OnRelease(GameObject obj) {
        obj.SetActive(false);
    }

    private void OnDestroy(GameObject obj) {
        GameObject.Destroy(obj);
    }

    #endregion

}


public class PoolManager {

    private Dictionary<string, Pool> _pools = new();

    public GameObject Pop(GameObject prefab) {
        // #1. Ǯ�� ������ ���� �����.
        if (_pools.ContainsKey(prefab.name) == false) {
            CreatePool(prefab);
        }

        // #2. �ش� Ǯ���� �ϳ� �����´�.
        return _pools[prefab.name].Pop();
    }

    public bool Push(GameObject obj) {
        // #1. Ǯ�� �ִ��� Ȯ���Ѵ�.
        if (_pools.ContainsKey(obj.name) == false) return false;

        // #2. Ǯ�� ���� ������Ʈ�� �ִ´�.
        _pools[obj.name].Push(obj);

        return true;
    }

    private void CreatePool(GameObject prefab) {
        Pool pool = new(prefab);
        _pools.Add(prefab.name, pool);
    }

    public void Clear() {
        _pools.Clear();
    }
}