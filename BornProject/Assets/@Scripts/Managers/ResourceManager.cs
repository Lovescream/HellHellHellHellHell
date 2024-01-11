using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager {

    private Dictionary<string, Sprite> _sprites = new();
    private Dictionary<string, GameObject> _prefabs = new();

    public void Initialize() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Items");
        foreach (Sprite sprite in sprites) {
            _sprites.Add(sprite.name, sprite);
        }
        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs");
        foreach (GameObject obj in objs) {
            _prefabs.Add(obj.name, obj);
        }
    }
    
    public GameObject LoadPrefab(string key) {
        if (!_prefabs.TryGetValue(key, out GameObject prefab)) {
            Debug.LogError($"[ResourceManager] LoadPrefab({key}): Failed to load prefab.");
            return null;
        }
        return prefab;
    }
    public Sprite LoadSprite(string key) {
        if (!_sprites.TryGetValue(key, out Sprite sprite)) {
            Debug.LogError($"[ResourceManager] LoadSprite({key}): Failed to load sprite.");
            return null;
        }
        return sprite;
    }
    
    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false) {
        GameObject prefab = LoadPrefab(key);
        if (prefab == null) {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }
        
        if (pooling) return Main.Pool.Pop(prefab);

        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }
    public void Destroy(GameObject obj) {
        if (obj == null) return;

        if (Main.Pool.Push(obj)) return;

        Object.Destroy(obj);
    }

}