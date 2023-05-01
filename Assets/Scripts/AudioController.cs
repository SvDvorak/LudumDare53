using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private PlayerGroup playerGroup;
    [SerializeField] private GameObject characterWalkingSounds;
    [SerializeField] private LocationImages locationImages;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource mainWalkingSound;

    public float fadeInDuration = 5f;
    public float fadeOutDuration = 1f;
    [Range(0f, 1f)]
    public float backgroundMusicVolume = 0.3f;

    private AudioSource[] footstepSounds;
    private AudioSource audioSource;
    private Location destination;
    private float maxVolume = 0;
    private bool fadingIn = false;

    void Start()
    {
        footstepSounds = characterWalkingSounds.GetComponentsInChildren<AudioSource>();

        audioSource = GetComponent<AudioSource>();
        playerGroup.HalfWayToLocation += OnPlayLocationMusic;
        playerGroup.ExitLocation += OnChangeMusic;
        playerGroup.ExitLocation += OnPlayWalkingSound;
        playerGroup.EnteredLocation += OnStopWalkingSound;

        backgroundMusic.volume = backgroundMusicVolume;
        backgroundMusic.Play();
        OnPlayLocationMusic("", playerGroup.currentLocation);
    }

    private void OnStopWalkingSound(string characterID, Location currentLocation)
    {
        foreach (var footstepSound in footstepSounds)
        {
            footstepSound.Stop();
        }

        mainWalkingSound.Stop();
    }

    private void OnPlayWalkingSound(string characterID, Location currentLocation)
    {
        foreach (var aliveCharacter in GameState.Instance.Characters.Where(x => x.IsAlive))
        {
            foreach (var footstepSound in footstepSounds)
            {
                if (footstepSound.name == aliveCharacter.ID)
                    footstepSound.Play();
            }
        }

        mainWalkingSound.time = Random.Range(0, mainWalkingSound.clip.length - 0.1f);
        mainWalkingSound.Play();
    }

    private void OnChangeMusic(string characterID, Location destination)
    {
        this.destination = destination;
        fadingIn = false;
        StopCoroutine(FadeInMusic());
        StartCoroutine(FadeOutMusic());
    }

    private void OnPlayLocationMusic(string characterID, Location currentLocation)
    {
        audioSource.clip = locationImages.GetMusic(currentLocation.LocationID);

        audioSource.loop = true;
        audioSource.volume = 0;
        maxVolume = locationImages.GetVolume(currentLocation.LocationID);
        if (audioSource.clip != null)
            audioSource.Play();
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        fadingIn = true;
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (fadingIn && elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, maxVolume, elapsedTime / fadeInDuration);
            yield return null;
        }

        audioSource.volume = maxVolume;
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }
    }
}
