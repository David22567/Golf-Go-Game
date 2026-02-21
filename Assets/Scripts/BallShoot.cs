using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BallShoot : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float maxDistance = 1000f;
    [SerializeField] LayerMask hitMask;

    Vector3 travelDirection;
    Vector3 targetPosition;
    bool isTravelling;

    InputSystem_Actions input;

    void Awake() { input = new InputSystem_Actions(); }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Attack.performed += OnAttack;
    }

    void OnDisable()
    {
        input.Player.Attack.performed -= OnAttack;
        input.Player.Disable();
    }

    void Update() { if (isTravelling) MoveBall(); }

    void OnAttack(InputAction.CallbackContext context)
    {
        if ((EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) || isTravelling) return;
        Shoot();
    }

    void Shoot()
    {
        isTravelling = true;
        input.Player.Attack.Disable();

        travelDirection = transform.forward.normalized;
        Ray ray = new Ray(transform.position, travelDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitMask))
            targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        else
        {
            Vector3 end = transform.position + travelDirection * maxDistance;
            targetPosition = new Vector3(end.x, transform.position.y, end.z);
        }
    }

    void MoveBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            ResetShot();
        }
    }

    public void ResetShot() { isTravelling = false; input.Player.Attack.Enable(); }

    public bool IsTravelling() { return isTravelling; }
}
