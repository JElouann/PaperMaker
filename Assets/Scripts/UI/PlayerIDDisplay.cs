using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerIDDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerIDText;

    private void Start()
    {
        if (PlayerIDHandler.Instance.PlayerID == "")
        {
            PlayerIDHandler.Instance.OnGetPlayerID += UpdateDisplay;
        }
        else
        {
            _playerIDText.text = PlayerIDHandler.Instance.PlayerID;
        }
    }

    private void UpdateDisplay() => _playerIDText.text = PlayerIDHandler.Instance.PlayerID;

}
