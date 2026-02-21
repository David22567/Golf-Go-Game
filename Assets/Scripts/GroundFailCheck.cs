using System;
using UnityEngine;
using UnityEngine.UI;

public class GroundFailCheck : MonoBehaviour
{
    [SerializeField] float rayDistance = 1.5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] GameObject player;
    [SerializeField] GameObject retryPanel;
    [SerializeField] GameObject destroyedPlayer;

    [SerializeField] float failDelay = 0.5f;

    bool hasFailed;

    void Start() { player.SetActive(true); retryPanel.SetActive(false); }

    void Update() { CheckGround(); }

    void CheckGround()
    {
        if (hasFailed) return;

        Ray ray = new Ray(transform.position, Vector3.down);

        if (!Physics.Raycast(ray, rayDistance, groundMask))
        {
            hasFailed = true;
            Instantiate(destroyedPlayer, transform.position, transform.rotation);
            player.SetActive(false);
            Invoke(nameof(LevelFailed), failDelay);
        }
    }

    public void LevelFailed() { retryPanel.SetActive(true); }
}
