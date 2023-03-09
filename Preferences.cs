using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class Preferences
    {
        private const string PK_AUDIO_MUSIC = "Audio_MusicVolume";
        private const string PK_AUDIO_EFFECT = "Audio_EffectVolume";
        private const string PK_AUDIO_MASTER = "Audio_MasterVolume";
        private const string PK_VIBRATION = "System_Vibration";
        private const int TRUE = 0xFF, FALSE = 0x00, TRUE_FALSE_DIFF = 0x80;

        private static float Get(string key, float f) => PlayerPrefs.GetFloat(key, f);
        private static void Set(string key, float f) => PlayerPrefs.SetFloat(key, f);
        private static bool Get(string key, bool f) => PlayerPrefs.GetInt(key, f ? TRUE : FALSE) > TRUE_FALSE_DIFF;
        private static void Set(string key, bool f) => PlayerPrefs.GetInt(key, f ? TRUE : FALSE);

        public static float MasterVolume { get => Get(PK_AUDIO_MASTER, 1f); set => Set(PK_AUDIO_MASTER, value); }
        public static float MusicVolume { get => Get(PK_AUDIO_MUSIC, 1f); set => Set(PK_AUDIO_MUSIC, value); }
        public static float EffectVolume { get => Get(PK_AUDIO_EFFECT, 1f); set => Set(PK_AUDIO_EFFECT, value); }
        public static bool Vibration { get => Get(PK_VIBRATION, true); set => Set(PK_VIBRATION, value); }
    }

