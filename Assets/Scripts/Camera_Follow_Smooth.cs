using UnityEngine;

public class Camera_Follow_Smooth : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSpeed = 5f;

    Vector3 offset;

    void OnEnable() { offset = transform.position - player.position; }

    void Update()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Slerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
