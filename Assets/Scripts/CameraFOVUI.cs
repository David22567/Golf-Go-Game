using UnityEngine;

public class CameraFOVUI : MonoBehaviour
{
    [SerializeField] Camera targetCamera;
    [SerializeField] float[] fovValues = { 70f, 100f, 150f };
    [SerializeField] float smoothSpeed = 10f;

    int currentIndex;
    float targetFOV;

    void Awake()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        currentIndex = 0;
        targetFOV = fovValues[currentIndex];
        targetCamera.fieldOfView = targetFOV;
    }

    void Update()
    {
        targetCamera.fieldOfView = Mathf.Lerp(targetCamera.fieldOfView, targetFOV, Time.deltaTime * smoothSpeed);
    }

    public void ToggleFOV()
    {
        currentIndex = (currentIndex + 1) % fovValues.Length;
        targetFOV = fovValues[currentIndex];
    }
}
