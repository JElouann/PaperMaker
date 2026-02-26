using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class LevelExportManager : MonoBehaviour
{
    [field: SerializeField] public CloudSaver CloudSaver;
    [field: SerializeField] public Save Save;

    // Singleton and Don't destroy on load
    #region Singleton
    private static LevelExportManager _instance;

    public static LevelExportManager Instance
    {
        get
        {
            if (_instance == null)
                LogManager.Instance.Loggers["Singleton"].Log("LevelExportManager instance <color=#eb624d>destroyed</color>");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            LogManager.Instance.Loggers["Singleton"].Log("LevelExportManager instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
            LogManager.Instance.Loggers["Singleton"].Log("LevelExportManager instance <color=#58ed7d>created</color>");
        }

        //DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public async void ExportSelectedLevel()
    {
        string levelName = LevelHandler.Instance.SelectedLevelTitle;
        Level selectedLevel = Save.GetLevel(levelName);

        await CloudSaver.SaveOnCloud(selectedLevel);
        LogManager.Instance.Loggers["System"].Log($"Level {selectedLevel.LevelName} sent to Cloud.");
    }
}
