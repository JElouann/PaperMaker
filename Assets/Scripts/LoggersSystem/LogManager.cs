using AYellowpaper.SerializedCollections;
using UnityEngine;


public class LogManager : MonoBehaviour
{
    [Space(10)]
    [Tooltip("The logger used by default when using Log() without precising a logger to use.")][SerializeField] private Logger DefaultLogger;
    [Space(10)]
    [Tooltip("A dictionary containg the loggers and their respective key.")] [field: SerializeField] public SerializedDictionary<string, Logger> Loggers = new();

    //Singleton
    #region Singleton

    private static LogManager _instance;

    public static LogManager Instance
    {
        get
        {
            if (_instance == null) LogManager.Instance.Loggers["Singleton"].Log("LogManager is null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            LogManager.Instance.Loggers["Singleton"].Log("LogManager instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
            LogManager.Instance.Loggers["Singleton"].Log("LogManager instance <color=#58ed7d>created</color>");

        }
    }
    #endregion

    /// <summary>
    /// Prints a message in the console. Takes in input a message and the key to the logger it will use. If no logger key is specified, it will use the default logger.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="loggerKey"></param>
    public void Log(object message, string loggerKey = null)
    {
        if (loggerKey == null)
        {
            DefaultLogger.Log(message);
        }
        else if (!Loggers.ContainsKey(loggerKey))
        {
            Debug.LogError($"Specified logger ({loggerKey}) doesn't exist.");
            return;
        }
        else
        {
            Loggers[loggerKey].Log(message);
        }
    }
}
