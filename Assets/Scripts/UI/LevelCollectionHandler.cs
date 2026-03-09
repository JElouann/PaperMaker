using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelCollectionHandler : MonoBehaviour
{
    [SerializeField] private Transform _containerTransform;

    void Start() => UpdateLevelCollection();

    // Update each LevelBlocks from collection to have correct Image and Name
    public void UpdateLevelCollection()
    {
        StartCoroutine(UpdateLevelCollectionCoroutine());
    }

    private IEnumerator UpdateLevelCollectionCoroutine()
    {
        yield return null;
        //DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/RenderOutput/LevelPreviews");
        //FileInfo[] info = dir.GetFiles("*.png");

        string path = Path.Combine(Application.persistentDataPath, "RenderOutput/LevelPreviews");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string[] files = Directory.GetFiles(path, "*.png");

        int levelNb = 0;

        for (int i = 0; i < /*info*/files.Length; i++, levelNb++)
        {
            string fileName = Path.GetFileName(files[i]);

            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);

            //string url = Path.Combine(Application.streamingAssetsPath, "RenderOutput/LevelPreviews/" + info[i].Name.Replace(".png", ""));
            string url = Path.Combine(Application.persistentDataPath, "RenderOutput/LevelPreviews/" + fileName);

            Texture2D texture = null;

            if (File.Exists(url))
            {
                byte[] fileData = File.ReadAllBytes(url);

                texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
            }
            else
            {
                Debug.LogError("Texture non trouvée : " + url);
            }
            

            levelBlock.InitBlock(fileName.Replace("_previewImage.png", ""), texture as Texture);
        }

        for (int i = levelNb; i < _containerTransform.childCount; i++)
        {
            //_containerTransform.GetChild(i).gameObject.SetActive(false);
            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);
            levelBlock.InitEmptyBlock();
        }
    }
}
