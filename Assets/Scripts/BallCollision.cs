using UnityEngine;

public class BallCollision : MonoBehaviour
{
    [SerializeField] GameObject impactPrefab;

    BallShoot shooter;
    bool hasHit;

    void Awake()
    {
        shooter = GetComponent<BallShoot>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit || !other.CompareTag("Box")) return;

        hasHit = true;

        Vector3 direction = transform.forward;
        Vector3 hitPoint = other.ClosestPoint(transform.position);

        Instantiate(impactPrefab, hitPoint, Quaternion.LookRotation(direction));

        BoxDestructible box = other.GetComponent<BoxDestructible>();
        if (box != null)
            box.DestroyBox(hitPoint, direction);

        shooter.ResetShot();
        hasHit = false;
    }
}
