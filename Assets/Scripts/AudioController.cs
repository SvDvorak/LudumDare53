using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private PlayerGroup playerGroup;
    [SerializeField] private LocationImages locationImages;

    private AudioSource audioSource;

    private Location destination;
    private float maxVolume = 0;
    private bool fadingIn = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerGroup.HalfWayToLocation += OnPlayLocationMusic;
        playerGroup.ExitLocation += PlayerGroup_ExitLocation;

        OnPlayLocationMusic("", playerGroup.currentLocation);
    }

    private void PlayerGroup_ExitLocation(string characterID, Location destination)
    {
        this.destination = destination;
        fadingIn = false;
        StopCoroutine(FadeInMusic());
        StartCoroutine(FadeOutMusic());
    }

    private void OnPlayLocationMusic(string characterID, Location currentLocation)
    {
        audioSource.loop = true;
        audioSource.clip = locationImages.GetMusic(currentLocation.LocationID);
        audioSource.volume = 0;
        maxVolume = locationImages.GetVolume(currentLocation.LocationID);
        audioSource.Play();
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        fadingIn = true;
        float duration = 5f;
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (fadingIn && elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, maxVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = maxVolume;
    }

    private IEnumerator FadeOutMusic()
    {
        float duration = 1f;
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }
    }

    private void Update()
    {
        Debug.Log(audioSource.volume);
    }
}
