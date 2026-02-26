using AYellowpaper.SerializedCollections;
using System;
using System.Xml;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelHandler : MonoBehaviour
{
    [SerializeField] private string _editorSceneName;
    [field: SerializeField] public string SelectedLevelTitle { get; private set; }

    // Singleton and Don't Destroy On Load
    #region Singleton
    private static LevelHandler _instance;

    public static LevelHandler Instance
    {
        get
        {
            if (_instance == null)
                LogManager.Instance.Loggers["Singleton"].Log("Levelhandler instance <color=#eb624d>destroyed</color>");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            LogManager.Instance.Loggers["Singleton"].Log("Levelhandler instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
            LogManager.Instance.Loggers["Singleton"].Log("Levelhandler instance <color=#58ed7d>created</color>");
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion 

    public void SelectLevelData(string levelTitle)
    {
        LogManager.Instance.Loggers["GameStep"].Log($"Selected level : {levelTitle}");
        SelectedLevelTitle = levelTitle;
    }

    public void EditSelectedLevel()
    {
        if (SelectedLevelTitle == "")
        {
            Debug.LogError("Selected level title is incorrect.");
        }
        LogManager.Instance.Loggers["GameStep"].Log("Edit " + SelectedLevelTitle);
        SceneManager.LoadScene(_editorSceneName);
    }
}
