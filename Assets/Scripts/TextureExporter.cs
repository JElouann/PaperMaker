using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextureExporter : MonoBehaviour
{
    [SerializeField] RenderTexture _targetTexture;
    public TMP_InputField LevelNameInputField;

    public void ExportCurrentTexture()
    {
        Texture2D tex = RenderTextTo2DText(_targetTexture);

        SaveTexture(tex);

        //SaveTexture(RenderTextTo2DText(_targetTexture));
    }

    private Texture2D RenderTextTo2DText(RenderTexture targetTexture)
    {
        Texture2D result = new(
            targetTexture.width, 
            targetTexture.height, 
            TextureFormat.RGBA32, 
            false,
            false
        );

        RenderTexture.active = targetTexture;
        result.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
        result.Apply();

        return result;
    }

    private void SaveTexture(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();

        string dirPath = Application.dataPath + "/Resources/RenderOutput/LevelPreviews";

        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        System.IO.File.WriteAllBytes(dirPath + "/" + LevelNameInputField.text + "_previewImage.png", bytes);
        Debug.Log(bytes.Length / 1024 + "KB was saved as: " + dirPath);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); 
#endif
    }
}
