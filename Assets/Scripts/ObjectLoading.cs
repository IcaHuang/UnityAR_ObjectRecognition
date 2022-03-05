using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoading : MonoBehaviour
{
    private Dictionary<string, GameObject> mPrefabs = new Dictionary<string, GameObject>();

    void Start()
    {
        mPrefabs.Add("plant", Resources.Load("PlantModel") as GameObject);
        mPrefabs.Add("dolly", Resources.Load("DollyModel") as GameObject);
        loadModel(mPrefabs);

    }

    void loadModel(Dictionary<string, GameObject> prefabs)
    {
        foreach(string key in prefabs.Keys)
        {
            Debug.Log(key);
            Instantiate(mPrefabs[key]);
        }
        
    }
}
