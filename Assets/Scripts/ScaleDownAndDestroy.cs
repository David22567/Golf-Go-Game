using UnityEngine;

public class ScaleDownAndDestroy : MonoBehaviour
{
    [SerializeField] float scaleSpeed = 6f;
    [SerializeField] float destroyThreshold = 0.01f;

    Vector3 initialScale;

    void Awake() { initialScale = transform.localScale; }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, scaleSpeed * Time.deltaTime);
        if (transform.localScale.magnitude <= destroyThreshold) Destroy(gameObject);
    }
}
