using UnityEngine;

namespace TradeMarket.SoundSystem
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance { get { return instance; } }

        [Header("Sound")]
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;

        public SoundService soundService { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            soundService = new SoundService(soundScriptableObject, sfxSource, bgMusicSource);
        }
    }
}