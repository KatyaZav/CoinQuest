using UnityEngine;

namespace Assets.System.Audio
{
    public class AudioPlayer
    {
        private AudioSource _audioSource;
        private AudioData _audioData;
        
        public AudioPlayer(AudioSource audioSource, AudioData audioData)
        {
            _audioSource = audioSource;
            _audioData = audioData;
        }

        public void PlaySound()
        {            
            _audioSource.Stop();
            _audioSource.clip = _audioData.GetRandomClip();
            _audioSource.pitch = _audioData.GetRandomPitch();
            _audioSource.Play();
        }
    }
}
