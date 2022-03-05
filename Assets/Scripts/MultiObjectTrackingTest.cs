using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedObjectManager))]
public class MultiObjectTrackingTest : MonoBehaviour
{
    ARTrackedObjectManager ObjTrackedManager;
    private Dictionary<string, GameObject> mPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        ObjTrackedManager = GetComponent<ARTrackedObjectManager>();
    }

    void Start()
    {
        mPrefabs.Add("plant", Resources.Load("PlantModel") as GameObject);
        mPrefabs.Add("dolly", Resources.Load("DollyModel") as GameObject);
    }

    private void OnEnable()
    {
        ObjTrackedManager.trackedObjectsChanged += OnTrackedObjectsChanged;
    }
    void OnDisable()
    {
        ObjTrackedManager.trackedObjectsChanged -= OnTrackedObjectsChanged;
    }
    void OnTrackedObjectsChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {
        foreach (var trackedObject in eventArgs.added)
        {
            OnImagesChanged(trackedObject);
        }
        // foreach (var trackedImage in eventArgs.updated)
        // {
        //     OnImagesChanged(trackedImage.referenceImage.name);
        // }
    }

    private void OnImagesChanged(ARTrackedObject refObject)
    {
        FindObjectOfType<SceneChange>().LoadNextLevel();
    }

}
