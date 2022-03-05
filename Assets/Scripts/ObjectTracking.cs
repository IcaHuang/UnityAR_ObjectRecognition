using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//To make sure if a ARTrackedObjectManager exists. While there is no one, create one.
[RequireComponent(typeof(ARTrackedObjectManager))]

public class ObjectTracking : MonoBehaviour
{
    //Store all the animations
    [SerializeField]
    private GameObject[] placeablePrefabs;

    //Use a Dictionary to store animations. It will be easy to search based on a name.
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private ARTrackedObjectManager TrackedObjectManager;

    private void Awake()
    {
        TrackedObjectManager = FindObjectOfType<ARTrackedObjectManager>();

        //Instantiate all animations
        foreach(GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);  //Create each animation as GameObject
            newPrefab.SetActive(false);                                         //Keep all animations disabled while not triggered
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(newPrefab.name, newPrefab);
        }
    }

    private void OnEnable() => TrackedObjectManager.trackedObjectsChanged += ObjectChanged;

    private void OnDisable() => TrackedObjectManager.trackedObjectsChanged -= ObjectChanged;

    private void ObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {
        foreach(ARTrackedObject trackedObject in eventArgs.added)       //eventArgs.added: The list of ARTrackedObjects added since the last event.
        {
            UpdateObject(trackedObject);
        }
        foreach (ARTrackedObject trackedObject in eventArgs.updated)    //eventArgs.updated: The list of ARTrackedObjects updated since the last event.
        {
            UpdateObject(trackedObject);
        }
        foreach (ARTrackedObject trackedObject in eventArgs.removed)    //eventArgs.removed: The list of ARTrackedObjects removed since the last event.
        {
            spawnedPrefabs[trackedObject.name].SetActive(false);
        }
    }

    private void UpdateObject(ARTrackedObject trackedObject)
    {
        //store the name of the tracked object
        string name = trackedObject.referenceObject.name;

        //store the position of the tracked object
        Vector3 position = trackedObject.transform.position;

        //Enable the animation related to the tracked object(Previous designed). And put it at the position of the object.
        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        //Make sure that the other animations are disabled
        foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
}