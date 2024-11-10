using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCollection : MonoBehaviour
{
    public static InventoryCollection Instance { get; private set; }
    public List<GameObject> CollectedObjects { get; private set; } = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Ensures it persists across scenes
    }

    public void AddObject(GameObject obj)
    {
        if (obj != null && !CollectedObjects.Contains(obj))
        {
            CollectedObjects.Add(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (obj != null)
        {
            CollectedObjects.Remove(obj);
        }
    }
}
