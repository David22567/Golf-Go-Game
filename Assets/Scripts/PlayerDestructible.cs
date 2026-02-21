using UnityEngine;

public class PlayerDestructible : MonoBehaviour
{
    [SerializeField] GameObject destroyedPrefab;
    [SerializeField] ParticleSystem destroyVFX;

    [SerializeField] float explosionForce = 8f;
    [SerializeField] float explosionRadius = 3f;
    [SerializeField] float upwardModifier = 0.5f;

    bool destroyed;

    public void DestroyPlayer(Vector3 hitPoint)
    {
        if (destroyed) return;
        destroyed = true;

        if (destroyedPrefab != null)
        {
            GameObject pieces = Instantiate(destroyedPrefab, transform.position, transform.rotation);
            ApplyForce(pieces, hitPoint);
        }

        if (destroyVFX != null)
        {
            ParticleSystem vfx = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(vfx.gameObject, vfx.main.duration);
        }

        Destroy(gameObject);
    }

    void ApplyForce(GameObject pieces, Vector3 hitPoint)
    {
        Rigidbody[] rbs = pieces.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs) rb.AddExplosionForce(explosionForce, hitPoint, explosionRadius, upwardModifier, ForceMode.Impulse);
    }
}
