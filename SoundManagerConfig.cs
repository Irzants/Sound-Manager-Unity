using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

    public class SoundManagerConfig : SingletonAsset<SoundManagerConfig>
    {
        [Header("Sound Pack")]
        //public int initialPoolCount;
        public List<SoundPack> packs = new();
        [Header("Sound Mixer")]
        public AudioMixer mixer;
        public AudioMixerGroup masterGroup, sfxGroup, musicGroup;
    }

