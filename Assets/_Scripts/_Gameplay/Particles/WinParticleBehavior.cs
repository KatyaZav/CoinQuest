using UnityEngine;

public class WinParticleBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void Init(Sprite sprite)
    {
        _particle.textureSheetAnimation.SetSprite(0, sprite);
        _particle.Play();
    }
}
