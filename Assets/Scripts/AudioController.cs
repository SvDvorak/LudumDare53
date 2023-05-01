using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private PlayerGroup playerGroup;
    [SerializeField] private LocationImages locationImages;

    private AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerGroup.EnteredLocation += OnPlayLocationMusic;
    }

    private void OnPlayLocationMusic(string characterID, Location currentLocation)
    {
        audioSource.PlayOneShot(locationImages.GetMusic(currentLocation.LocationID), locationImages.GetVolume(currentLocation.LocationID));
    }
}
