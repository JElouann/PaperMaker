using UnityEngine;
using UnityEngine.UI;

public class EditLevel : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        TryGetComponent(out _button);    
    }

    void Start()
    {
        _button.onClick.AddListener(LevelHandler.Instance.EditSelectedLevel);
    }
}

