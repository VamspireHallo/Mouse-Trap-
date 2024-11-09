using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUI : MonoBehaviour
{
    //public List<GameObject> collectedObjects = new List<GameObject>();
    //public List<InventoryItem> collectedItems = new List<InventoryItem>();

    public static PermanentUI perm;

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
        //Singelton
        if(!perm)
        {
            perm = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
