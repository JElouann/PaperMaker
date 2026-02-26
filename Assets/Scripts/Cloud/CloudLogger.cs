using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using Unity.Services.Core;
using UnityEngine;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

public class CloudLogger : MonoBehaviour
{
    [SerializeField] private TextAsset _toSend;
    [SerializeField] private Texture _toSend2;
    [SerializeField] private string _playerID;

    private async void Start()
    {
        // Init
        await UnityServices.InitializeAsync();
        
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        //await SaveOnCloud();
        //print("SaveOnCloud completed.");

        //await Load(_playerID);
        //print("Load completed.");
    }

    public async Task Save()
    {
        var data = new Dictionary<string, object> { { "KeyTest_levelData", _toSend }, { "KeyTest_previewImage", _toSend2 } };
        SaveOptions saveOptions = new SaveOptions(new PublicWriteAccessClassOptions());
        await CloudSaveService.Instance.Data.Player.SaveAsync(data, saveOptions);
    }

    public async Task Load(string playerID)
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "KeyTest_previewImage" }, new LoadOptions(new PublicReadAccessClassOptions(playerID)));
        //playerData.TryGetValue("KeyTest", out var KeyName);
        //print(KeyName.Value);
        
        if (playerData.TryGetValue("KeyTest", out var keyName))
        {
            Debug.Log($"KeyTest: {keyName.Value.GetAs<string>()}");
        }
    }
}
