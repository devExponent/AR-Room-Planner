using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;


public class ImageTracking : MonoBehaviour
{
    public ARTrackedImageManager TrackedImageManager;
    [SerializeField]
    GameObject imageInstantiatedObject;
    [SerializeField]
    TextMeshProUGUI objectNameField;
    [SerializeField]
    AudioSource detectedSound;


    private void OnEnable()
    {
        TrackedImageManager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        TrackedImageManager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var newImage in args.added)
        {
            GameObject spawnedObject = Instantiate(imageInstantiatedObject, newImage.transform);


            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.transform.localRotation = Quaternion.identity;

            objectNameField.text = "added image: " + newImage.referenceImage.name;

            if (!detectedSound.isPlaying)
            {
                detectedSound.Play();
            }
        }
        foreach (var updatedImage in args.updated)
        {
            objectNameField.text = "updated image: " + updatedImage.referenceImage.name;
        }

        foreach (var removedImage in args.removed)
        {
            objectNameField.text = "removed image: " + removedImage.referenceImage.name;
        }
    }
}


