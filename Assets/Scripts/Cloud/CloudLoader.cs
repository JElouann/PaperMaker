using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudLoader : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerIDInput;
    [SerializeField] private TMP_InputField _levelNameInput;
    [SerializeField] private LevelCollectionHandler _levelCollectionHandler;

    private XmlWriterSettings _xmlWriterSettings = new XmlWriterSettings
    {
        NewLineOnAttributes = true,
        Indent = true
    };

    public async void Load()
    {
        string playerID = _playerIDInput.text;
        string levelName = _levelNameInput.text;
        HashSet<string> keys = new() { $"{levelName}_levelData", $"{levelName}_previewImage" };

        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(keys, new LoadOptions(new PublicReadAccessClassOptions(_playerIDInput.text)));

        CreateXMLFile(playerData[$"{levelName}_levelData"].Value.GetAsString());
        CreateLevelPreview(playerData[$"{levelName}_previewImage"].Value.GetAsString());
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        await Task.Delay(1000);
        LogManager.Instance.Log("Level downloaded", "System");
        _levelCollectionHandler.UpdateLevelCollection();
        SceneManager.LoadScene("LevelCollectionScene");
    }

    private void CreateXMLFile(string data)
    {
        // DATA FILE
        XmlDocument dataFile = new XmlDocument();
        dataFile.LoadXml(data.Trim());
        string path = Path.Combine(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + _levelNameInput.text + "_levelData.xml");

        dataFile.Save(path);
    }

    private void CreateLevelPreview(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);

        Texture2D texture = new(2, 2);
        texture.LoadImage(bytes);

        File.WriteAllBytes(Application.dataPath + "/Resources/RenderOutput/LevelPreviews/" + _levelNameInput.text + "_previewImage.png", bytes);
    }
}
