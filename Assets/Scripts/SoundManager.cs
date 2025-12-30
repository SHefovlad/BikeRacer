using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Список звуков")]
    public List<AudioClip> sounds = new List<AudioClip>();

    private List<AudioSource> activeSources = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Воспроизвести звук по индексу
    /// </summary>
    public void PlaySound(int index, float volume = 1f, bool loop = false)
    {
        if (index < 0 || index >= sounds.Count) return;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = sounds[index];
        source.volume = volume;
        source.loop = loop;
        source.Play();

        activeSources.Add(source);

        if (!loop)
            StartCoroutine(RemoveWhenFinished(source));
    }

    /// <summary>
    /// Остановить все экземпляры звука по индексу
    /// </summary>
    public void StopSound(int index)
    {
        for (int i = activeSources.Count - 1; i >= 0; i--)
        {
            if (activeSources[i] != null && activeSources[i].clip == sounds[index])
            {
                Destroy(activeSources[i]);
                activeSources.RemoveAt(i);
            }
        }
    }

    private System.Collections.IEnumerator RemoveWhenFinished(AudioSource source)
    {
        yield return new WaitWhile(() => source != null && source.isPlaying);

        if (source != null)
        {
            activeSources.Remove(source);
            Destroy(source);
        }
    }
}
