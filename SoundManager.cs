using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

    public class SoundManager : Singleton<SoundManager>
    {
        public enum SoundType
        {
            Music,
            SoundEffect
        }

        [Serializable]
        public class SoundInfo
        {
            public string id;
            public AudioClip clip;
            public SoundType type;
        }

        [NonSerialized] public SoundManagerConfig cfg;
        //Exposed Parameters in audio mixer
        private const string MKV_MASTER = "master_volume";
        private const string MKV_SNDFX = "sfx_volume";
        private const string MKV_MUSIC = "music_volume";

        private static float GetMixerFloat(AudioMixer mixer, string key, float defval)
        {
            bool success = mixer.GetFloat(key, out float retval);
            return Mathf.InverseLerp(-80.0f, 0.0f, success ? retval : defval);
        }

        public float MasterVolume
        {
            get => GetMixerFloat(mixer, MKV_MASTER, -1.0f);
            set { mixer.SetFloat(MKV_MASTER, Mathf.Lerp(-80.0f, 0.0f, value)); _masterVolumeTmp = value; }
        }
        public float SfxVolume
        {
            get => GetMixerFloat(mixer, MKV_SNDFX, -1.0f);
            set => mixer.SetFloat(MKV_SNDFX, Mathf.Lerp(-80.0f, 0.0f, value));
        }
        public float MusicVolume
        {
            get => GetMixerFloat(mixer, MKV_MUSIC, -1.0f);
            set => mixer.SetFloat(MKV_MUSIC, Mathf.Lerp(-80.0f, 0.0f, value));
        }
        private float _masterVolumeTmp = 1.0f;

        private AudioMixer mixer;
        private List<AudioSource> poolSource = new();
        private AudioMixerGroup masterMixerGroup;
        private AudioMixerGroup sfxMixerGroup;
        private AudioMixerGroup musicMixerGroup;

        private void Awake()
        {
            cfg = SoundManagerConfig.Instance;
            mixer = cfg.mixer;
            sfxMixerGroup = cfg.sfxGroup;
            musicMixerGroup = cfg.musicGroup;
            masterMixerGroup = cfg.masterGroup;

        }
        public void UpdateSetting()
        {
            MusicVolume = Preferences.MusicVolume;
            SfxVolume = Preferences.EffectVolume;
        }

        private void Start()
        {
            UpdateSetting();
        }
        private AudioSource Create(SoundType type)
        {
            GameObject gob = new(Guid.NewGuid().ToString());
            gob.transform.SetParent(transform);
            AudioSource source = gob.AddComponent<AudioSource>();
            poolSource.Add(source);
            source.outputAudioMixerGroup = type switch
            {
                SoundType.Music => musicMixerGroup,
                SoundType.SoundEffect => sfxMixerGroup,
                _ => masterMixerGroup
            };
            return source;
        }
        private AudioSource Request(SoundType type)
        {
            AudioMixerGroup targetGroup = type switch
            {
                SoundType.Music => musicMixerGroup,
                SoundType.SoundEffect => sfxMixerGroup,
                _ => masterMixerGroup
            };
            AudioSource src = poolSource.Find(it => !it.isPlaying && it.outputAudioMixerGroup == targetGroup);
            if (src == null) src = Create(type);
            return src;
        }
        private bool FindSoundData(string id, out SoundInfo data)
        {
            data = null;
            foreach (var pack in cfg.packs)
            {
                foreach (var snd in pack.soundList)
                {
                    if (snd.id == id)
                    {
                        data = snd;
                        break;
                    }
                }
            }
            return data != null;
        }

        public void Play(string id, bool loop)
        {
            if (FindSoundData(id, out SoundInfo snd))
            {
                AudioSource src = poolSource.Find(it => it.clip == snd.clip && !it.isPlaying);
                if (src == null)
                {
                    src = Request(snd.type);
                    src.clip = snd.clip;
                }
                src.loop = loop;
                src.Play();
            }
        }

        public void PlayOneShot(string id)
        {
            if (FindSoundData(id, out SoundInfo snd))
            {
                AudioSource src = Request(snd.type);
                src.PlayOneShot(snd.clip);
            }
        }

        public bool IsPlaying(string id)
        {
            if (FindSoundData(id, out SoundInfo snd))
            {
                return poolSource.FindIndex(it => it.clip == snd.clip && it.isPlaying) > -1;
            }
            return false;
        }

        /// <summary> Stop this Sound with this ID </summary>
        /// <param name="id">ID of the packs. `null` = Stop all </param>
        public void Stop(string id = null)
        {
            if (id == null)
            {
                foreach (var src in poolSource)
                {
                    src.Stop();
                }
            }
            else
            {
                if (FindSoundData(id, out SoundInfo snd))
                {
                    AudioSource src = poolSource.Find(it => it.clip == snd.clip && it.isPlaying);
                    if (src != null) src.Stop();
                }
            }
        }


        [RuntimeInitializeOnLoadMethod]
        private static void RTInit()
        {
            Instance.Dummy();
        }
    }

