using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Image _transitionMask;

    private async void OnEnable()
    {
        _transitionMask.material.SetFloat("_Transition", 1);
        await Task.Delay(1000);
        _transitionMask.material.DOFloat(0f, "_Transition", 0.35f).SetEase(Ease.InQuad);
    }

    public async void ChangeScene(string sceneName)
    {
        await _transitionMask.material.DOFloat(1f, "_Transition", 1.5f).SetEase(Ease.InSine).AsyncWaitForCompletion();
        SceneManager.LoadScene(sceneName);
    }
}
