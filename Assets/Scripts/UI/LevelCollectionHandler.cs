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
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/RenderOutput/LevelPreviews");
        FileInfo[] info = dir.GetFiles("*.png");
        int levelNb = 0;



        for (int i = 0; i < info.Length; i++, levelNb++)
        {
            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);

            //string url = Path.Combine(Application.streamingAssetsPath, "RenderOutput/LevelPreviews/" + info[i].Name.Replace(".png", ""));
            string url = Path.Combine(Application.streamingAssetsPath, "RenderOutput/LevelPreviews/" + info[i].Name);

            Texture2D texture = null;

            Debug.Log("PATH: " + url);
            Debug.Log("EXISTS: " + File.Exists(url));
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
            

            levelBlock.InitBlock(info[i].Name.Replace("_previewImage.png", ""), texture as Texture);
        }

        for (int i = levelNb; i < _containerTransform.childCount; i++)
        {
            //_containerTransform.GetChild(i).gameObject.SetActive(false);
            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);
            levelBlock.InitEmptyBlock();
        }
    }
}
