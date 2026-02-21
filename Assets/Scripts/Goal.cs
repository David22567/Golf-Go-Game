using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    [SerializeField] GameObject dummy;
    [SerializeField] GameObject winPanel;
    [SerializeField] float panelDelay = 2f;

    bool triggered;

    void Start() { dummy.SetActive(false); winPanel.SetActive(false); }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;
        dummy.SetActive(true);
        other.gameObject.SetActive(false);
        StartCoroutine(ShowWinPanelAfterDelay());
    }

    IEnumerator ShowWinPanelAfterDelay()
    {
        yield return new WaitForSeconds(panelDelay);
        winPanel.SetActive(true);
    }
}
