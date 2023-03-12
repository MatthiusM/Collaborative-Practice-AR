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


    private readonly Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        arTrackedImagesManager = GetComponent<ARTrackedImageManager>();
        foreach(GameObject prefabData in ArPrefabs)
        {
            GameObject prefab = Instantiate(prefabData, Vector3.zero, Quaternion.identity);
            prefab.SetActive(false);
            prefab.name = prefabData.name;
            prefabDictionary.Add(prefab.name, prefab);   
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
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage){

        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = prefabDictionary[name];
        prefab.SetActive(true);
        prefab.transform.position = position;

        // Play audio when prefab is spawned
        ModelScript modelScript = prefab.GetComponent<ModelScript>();
        modelScript.PlayAudio();

        // Stop audio when tracked image is lost
        if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None)
        {
            modelScript.StopAudio();
        }

        foreach (GameObject go in prefabDictionary.Values)
        {
            if (go.name != name && GameObject.Find(name))
            {
                prefabDictionary[name].SetActive(false);
            }
        }
    }
}
