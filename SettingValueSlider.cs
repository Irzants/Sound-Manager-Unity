using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

    public class SettingValueSlider : Monobehaviour
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;

        public void SetValueSlider()
        {
            musicSlider.value = Preferences.MusicVolume;
            sfxSlider.value = Preferences.EffectVolume;
            AttachValueListener();
        }

        public void AttachValueListener()
        {
            musicSlider.onValueChanged.AddListener(musicvol =>
            {
                Preferences.MusicVolume = musicvol;
                SoundManager.Instance.UpdateSetting();
            });
            sfxSlider.onValueChanged.AddListener(sfxvol =>
            {
                Preferences.EffectVolume = sfxvol;
                SoundManager.Instance.UpdateSetting();
            });
        }
        public override void OnOpen()
        {
            base.OnOpen();
        }

        private void Start()
        {
            SetValueSlider();
        }
        public void OnCloseButton()
        {
            SoundManager.Instance.PlayOneShot("click_button");
            base.Close();
        }

    }

    
}
