using System;
using UnityEngine;

namespace TradeMarket.SoundSystem
{
    [CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "Sound/SoundScriptableObject")]
    public class SoundScriptableObject : ScriptableObject
    {
        public Sounds[] audioList;
    }

    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }
}