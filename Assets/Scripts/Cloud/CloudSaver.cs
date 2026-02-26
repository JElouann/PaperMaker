using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models.Data.Player;
using UnityEngine;
using SaveOptions = Unity.Services.CloudSave.Models.Data.Player.SaveOptions;

public class CloudSaver : MonoBehaviour, IDisposable
{
    public async Task SaveOnCloud(Level level)
    {
        Texture2D tex = level.LevelPreview as Texture2D;

        byte[] bytes = tex.EncodeToPNG();
        string base64 = Convert.ToBase64String(bytes);

        var data = new Dictionary<string, object> {
            { $"{level.LevelName}_levelData", level.LevelData },
            { $"{level.LevelName}_previewImage", base64}
        };

        SaveOptions saveOptions = new SaveOptions(new PublicWriteAccessClassOptions());

        await CloudSaveService.Instance.Data.Player.SaveAsync(data, saveOptions);
    }

    public void Dispose()
    {

    }
}
