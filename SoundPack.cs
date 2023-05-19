using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "Data_Pack_Sound", menuName = "Rich Gagak/Audio/Sound Pack")]
    public class SoundPack : ScriptableObject
    {
        public List<SoundManager.SoundInfo> soundList;
    }
