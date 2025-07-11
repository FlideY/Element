using UnityEngine;

public class AudioManager : MonoBehaviour, IPlayClips
{
    public AudioClip _bossCollision;
    public AudioClip[] _eraserThrows;
    public AudioClip[] _onSwitchRooms;

    public AudioClip OnEnterBossRoom;

    public AudioClip[] GetHits;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip clip, float volume = 1)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayRandomClip(AudioClip[] clips, float volume = 1)
    {
        int index = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[index], volume);
    }
}

internal interface IPlayClips
{
    void PlayClip(AudioClip clip, float volume = 1);

    void PlayRandomClip(AudioClip[] clips, float volume = 1);
}