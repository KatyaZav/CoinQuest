using UnityEngine;

namespace Assets.System.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Configs/System/AudioData", order = 1)]
    public class AudioData : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; } = "Name noy choosed";
        [field: SerializeField] public AudioClip[] Clips { get; private set; }
        [field: SerializeField] public float MinPitch { get; private set; } = 1;
        [field: SerializeField] public float MaxPitch { get; private set; } = 1;

        public float GetRandomPitch()
        {
            return Random.Range(MinPitch, MaxPitch);
        }

        public AudioClip GetRandomClip()
        {
            int index = Random.Range(0, Clips.Length - 1);
            return Clips[index];
        }
    }
}

