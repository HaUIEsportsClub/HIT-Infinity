using System;

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundId
    {
        InGame,
        GameSelection,
        LoadingScene,
        Heal,
        InCorrect,
        Jump,
        Win,
        Wrong,
        CorrectAnswer,
        GameOver
    }
    [Serializable]
    public class SoundSource
    {
        [HideInInspector] public string Name;
        [SerializeField] private AudioClip m_Sound;

        public AudioClip Sound
        {
            get => m_Sound;
            set => m_Sound = value;
        }
    }

    public static AudioManager Instance;
    [Header("__________Sound Source__________")]
    [SerializeField] private AudioSource m_SoundSource;
    [SerializeField] private AudioSource m_SFXSource;
    [Header("______________Sound_____________")]
    [SerializeField] private List<SoundSource> m_SoundSources;
    [Header("__________Data__________")]
    [SerializeField] private float m_SoundVolumeRate = 1;
    [SerializeField] private float m_SFXVolumeRate = 1;
    private void LoadComponent()
    {
        if (m_SoundSource == null)
        {
            Transform soundSourceTrf = transform.Find("Music");
            if (soundSourceTrf == null)
            {
                soundSourceTrf = new GameObject("Music").transform;
                soundSourceTrf.parent = transform;
            }

            m_SoundSource = soundSourceTrf.GetComponent<AudioSource>();
            if (m_SoundSource == null)
            {
                m_SoundSource = soundSourceTrf.AddComponent<AudioSource>();
            }
        }
        if (m_SFXSource == null)
        {
            Transform sfxSourceTrf = transform.Find("SFX");
            if (sfxSourceTrf == null)
            {
                sfxSourceTrf = new GameObject("SFX").transform;
                sfxSourceTrf.parent = transform;
            }

            m_SFXSource = sfxSourceTrf.GetComponent<AudioSource>();
            if (m_SFXSource == null)
            {
                m_SFXSource = sfxSourceTrf.AddComponent<AudioSource>();
            }
        }
        SetAudioClipsName();
    }

    private void SetAudioClipsName()
    {
        string[] nameList = Enum.GetNames(typeof(SoundId));
        if (m_SoundSources == null) m_SoundSources = new List<SoundSource>();
        
        while(m_SoundSources.Count > nameList.Length) m_SoundSources.RemoveAt(m_SoundSources.Count - 1);

        for (int i = 0; i < nameList.Length; ++i)
        {
            if (i < m_SoundSources.Count) m_SoundSources[i].Name = nameList[i];
            else
            {
                m_SoundSources.Add(new SoundSource()
                {
                    Name = nameList[i],
                });
            }
        }
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        LoadComponent();
    }

    private void OnValidate()
    {
        SetAudioClipsName();
    }

    public static void PlayBackgroundSound(SoundId soundId)
    {
        Instance.m_SoundSource.Stop();

        Instance.m_SoundSource.clip = Instance.m_SoundSources[(int)soundId].Sound;
        Instance.m_SoundSource.volume = Instance.m_SoundVolumeRate;
        Instance.m_SoundSource.Play();
    }

    public static void PlaySound(SoundId soundId)
    {
        Instance.m_SoundSource.PlayOneShot(Instance.m_SoundSources[(int)(soundId)].Sound, Instance.m_SFXVolumeRate);
    }
}
