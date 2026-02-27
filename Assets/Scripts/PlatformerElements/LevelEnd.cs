using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameObject VictoryPanel;
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        VictoryPanel.SetActive(true);
        Player.SetActive(false);
    }
}
