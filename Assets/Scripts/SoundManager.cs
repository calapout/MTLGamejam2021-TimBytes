using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bytes;

[System.Serializable]
[SerializeField]
public class AudioClipWithVolume
{
    public AudioClipWithVolume(string name, float vol = 1f) { Name = name; Volume = vol; }
    public string Name;
    public float Volume = 1f;
}

public class PlaySoundData : Bytes.Data
{
    static int NB_VARIATIONS = 1;
    static float VOLUME = -1f;
    static float PITCH_RANGE = 0f;

    public PlaySoundData(string name, float vol, int nbVariations) { Name = name; Volume = vol; NbVariations = nbVariations; PitchRange = PITCH_RANGE; }
    public PlaySoundData(string name, float vol = -1f) { Name = name; Volume = vol; NbVariations = 1; PitchRange = PITCH_RANGE; }
    public PlaySoundData(string name, float vol, float randomPitchRange) { Name = name; Volume = vol; NbVariations = NB_VARIATIONS; PitchRange = randomPitchRange; }
    public string Name { get; private set; }
    public float Volume { get; private set; }
    public int NbVariations { get; private set; }
    public float PitchRange { get; private set; }
}

public class SoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioSource musicSource;

    private void Start()
    {
        EventManager.AddEventListener("playSound", PlaySound);
        EventManager.AddEventListener("playEndMusic", (Bytes.Data d)=> { musicSource.Play(); });
    }

    private void PlaySound(Bytes.Data data)
    {
        PlaySoundData soundData = (PlaySoundData)data;
        float vol = soundData.Volume * source.volume;
        if (soundData.Volume == -1) { vol = source.volume; }

        string variation = "";
        if (soundData.NbVariations > 1) { variation = "_" + Random.Range(1, soundData.NbVariations).ToString(); }
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + soundData.Name + variation);

        if (clip != null)
        {
            source.PlayOneShot(clip, vol);
        }
    }

}
