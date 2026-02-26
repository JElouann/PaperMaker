using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class PlayerIDHandler : MonoBehaviour
{
    [field: SerializeField]
    public string PlayerID { get; private set; }

    public event Action OnGetPlayerID;

    // Singleton and Don't destroy on load
    #region Singleton
    private static PlayerIDHandler _instance;

    public static PlayerIDHandler Instance
    {
        get
        {
            if (_instance == null)
                LogManager.Instance.Loggers["Singleton"].Log("PlayerIDHandler instance <color=#eb624d>destroyed</color>");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            LogManager.Instance.Loggers["Singleton"].Log("PlayerIDHandler instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
            LogManager.Instance.Loggers["Singleton"].Log("PlayerIDHandler instance <color=#58ed7d>created</color>");
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        PlayerID = AuthenticationService.Instance.PlayerId;
        OnGetPlayerID?.Invoke();
    }
}
