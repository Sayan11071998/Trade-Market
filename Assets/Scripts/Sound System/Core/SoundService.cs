using System;
using UnityEngine;

namespace TradeMarket.SoundSystem
{
    public class SoundService
    {
        private SoundScriptableObject soundScriptableObject;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public SoundService(SoundScriptableObject soundData, AudioSource audioEffectSource, AudioSource bgMusicSource)
        {
            soundScriptableObject = soundData;
            audioEffects = audioEffectSource;
            backgroundMusic = bgMusicSource;

            PlaybackgroundMusic(SoundType.BackgroundMusic, true);
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);

            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;

                if (loopSound)
                    audioEffects.Play();
                else
                    audioEffects.PlayOneShot(clip);
            }
        }

        public void StopSoundEffects()
        {
            if (audioEffects.isPlaying)
                audioEffects.Stop();
        }

        private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(soundScriptableObject.audioList, item => item.soundType == soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}