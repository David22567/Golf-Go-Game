using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] float slowSpeed = 60f;
    [SerializeField] float normalSpeed = 150f;
    [SerializeField] float fastSpeed = 300f;

    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float maxLineLength = 1000f;

    [SerializeField] Transform shootPoint;
    [SerializeField] LineRenderer aimLine;
    [SerializeField] LayerMask hitMask;

    [SerializeField] Image speedIcon;
    [SerializeField] Sprite slowIcon;
    [SerializeField] Sprite normalIcon;
    [SerializeField] Sprite fastIcon;

    float rotationSpeed;
    Vector3 aimDirection;
    Vector3 destination;

    bool moving;
    int speedIndex = 1;

    void Start()
    {
        aimDirection = shootPoint.forward;
        aimLine.positionCount = 2;
        aimLine.useWorldSpace = true;
        SetRotationSpeed();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (moving) Move();
        else UpdateAimLine();
    }

    public void Shoot()
    {
        if (moving) return;

        moving = true;

        Ray ray = new Ray(shootPoint.position, aimDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, maxLineLength, hitMask))
            destination = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        else
            destination = new Vector3((shootPoint.position + aimDirection * maxLineLength).x, transform.position.y, (shootPoint.position + aimDirection * maxLineLength).z);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            transform.position = destination;
            moving = false;
        }
    }

    public void ToggleRotationSpeed()
    {
        if (moving) return;

        speedIndex = (speedIndex + 1) % 3;
        SetRotationSpeed();
    }

    void SetRotationSpeed()
    {
        if (speedIndex == 0) { rotationSpeed = slowSpeed; speedIcon.sprite = slowIcon; }
        else if (speedIndex == 1) { rotationSpeed = normalSpeed; speedIcon.sprite = normalIcon; }
        else { rotationSpeed = fastSpeed; speedIcon.sprite = fastIcon; }
    }

    void UpdateAimLine()
    {
        aimDirection = Vector3.Slerp(aimDirection, shootPoint.forward, Time.deltaTime * 20f);

        Ray ray = new Ray(shootPoint.position, aimDirection);

        Vector3 endPoint = Physics.Raycast(ray, out RaycastHit hit, maxLineLength, hitMask)
            ? hit.point
            : shootPoint.position + aimDirection * maxLineLength;

        aimLine.SetPosition(0, shootPoint.position);
        aimLine.SetPosition(1, endPoint);
    }
}
