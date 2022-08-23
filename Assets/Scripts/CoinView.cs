using System;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    private const int PlayerCollisionLayer = 7;

    [SerializeField] private ParticleSystem _particleSystem;

    public event Action CoinAdded;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == PlayerCollisionLayer)
        {
            CoinAdded?.Invoke();
            var particle = Instantiate(_particleSystem, transform.position, _particleSystem.transform.rotation);
            Destroy(particle, 5f);
            Destroy(this.gameObject);
        }
    }
}
