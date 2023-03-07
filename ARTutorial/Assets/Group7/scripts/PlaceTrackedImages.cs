using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImagesManager;

    [SerializeField]
    private GameObject[] ArPrefabs;


    private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        arTrackedImagesManager = GetComponent<ARTrackedImageManager>();
        for(int i = 0; i < ArPrefabs.Length; i++)
        {
            prefabDictionary[ArPrefabs[i].name] = ArPrefabs[i];
        }
    }
    private void OnEnable()
    {
        arTrackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable()
    {
        arTrackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.updated)
        {
            prefabDictionary[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }


        foreach (var trackedImage in eventArgs.removed)
        {
            // Destroy its prefab
            Destroy(prefabDictionary[trackedImage.referenceImage.name]);
            // Also remove the instance from our array
            //prefabDictionary.Remove(trackedImage.referenceImage.name);
        }
    }
}
