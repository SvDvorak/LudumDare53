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
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = locationImages.GetMusic(currentLocation.LocationID);
        audioSource.volume = locationImages.GetVolume(currentLocation.LocationID);
        audioSource.Play();
    }
}
