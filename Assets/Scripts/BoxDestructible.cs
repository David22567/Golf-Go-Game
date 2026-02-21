using UnityEngine;

public class BoxDestructible : MonoBehaviour
{
    [SerializeField] GameObject destroyedPrefab;
    [SerializeField] ParticleSystem destroyVFX;
    [SerializeField] float destroyDelay = 0.05f;

    [SerializeField] float explosionForce = 6f;
    [SerializeField] float explosionRadius = 2f;
    [SerializeField] float upwardModifier = 0.3f;

    bool isDestroyed;

    public void DestroyBox(Vector3 hitPoint, Vector3 forceDirection)
    {
        if (isDestroyed) return;
        isDestroyed = true;

        if (destroyedPrefab != null)
        {
            GameObject pieces = Instantiate(destroyedPrefab, transform.position, transform.rotation);
            ApplyForceToPieces(pieces, hitPoint, forceDirection);
        }

        if (destroyVFX != null)
        {
            ParticleSystem vfx = Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(vfx.gameObject, vfx.main.duration);
        }

        DisableBox();
        Destroy(gameObject, destroyDelay);
    }

    void ApplyForceToPieces(GameObject pieces, Vector3 hitPoint, Vector3 direction)
    {
        Rigidbody[] rbs = pieces.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(explosionForce, hitPoint, explosionRadius, upwardModifier, ForceMode.Impulse);
            rb.AddForce(direction * explosionForce, ForceMode.Impulse);
        }
    }

    void DisableBox()
    {
        Collider col = GetComponent<Collider>(); if (col != null) col.enabled = false;
        Renderer rend = GetComponent<Renderer>(); if (rend != null) rend.enabled = false;
    }
}
