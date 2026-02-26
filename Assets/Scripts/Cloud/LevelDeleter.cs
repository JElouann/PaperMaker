using System;
using Unity.Services.CloudSave;
using UnityEngine;

public class LevelDeleter : MonoBehaviour
{
    private LevelCollectionHandler _levelCollectionHandler;

    private void Awake() => TryGetComponent(out _levelCollectionHandler);

    /// <summary>
    /// Method that erases current selected level from files and Cloud.
    /// </summary>
    public async void DeleteLevel()
    {
        string levelName = LevelHandler.Instance.SelectedLevelTitle;

        if (levelName == "")
        {
            LogManager.Instance.Loggers["Error"].Log($"Level name is not valid.");
            return;
        }

        try
        {
            await CloudSaveService.Instance.Files.Player.DeleteAsync($"{levelName}_levelData");
            await CloudSaveService.Instance.Files.Player.DeleteAsync($"{levelName}_previewImage");
        }
        catch (Exception e)
        {
            LogManager.Instance.Log(e.ToString(), "Error");
        }

        if (System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + levelName + "_levelData.xml"))
            System.IO.File.Delete(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + levelName + "_levelData.xml");

        if (System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelPreviews/" + levelName + "_previewImage.png"))
            System.IO.File.Delete(Application.dataPath + "/Resources/RenderOutput/LevelPreviews/" + levelName + "_previewImage.png");


        LogManager.Instance.Loggers["System"].Log($"Level {levelName} successfully deleted.");

        _levelCollectionHandler.UpdateLevelCollection();
    }
}
