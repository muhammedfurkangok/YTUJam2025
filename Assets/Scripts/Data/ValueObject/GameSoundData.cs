using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct GameSoundData
    {
        public GameSoundType gameSoundType;
        public AudioClip audioClip;
        public bool hasRandomPitch;
        public bool hasGlissando;

        [Range(0f, 1f)] public float volume;
        // [Range(.1f, 2f)] 
        // public float pitch;
    }
}