using System.IO;
using UnityEngine;

public class LevelCollectionHandler : MonoBehaviour
{
    [SerializeField] private Transform _containerTransform;

    void Start() => UpdateLevelCollection();

    // Update each LevelBlocks from collection to have correct Image and Name
    public void UpdateLevelCollection()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/RenderOutput/LevelPreviews");
        FileInfo[] info = dir.GetFiles("*.png");
        int levelNb = 0;

        for (int i = 0; i < info.Length; i++, levelNb++)
        {
            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);
            levelBlock.InitBlock(info[i].Name.Replace("_previewImage.png", ""),
                                                     (Texture)Resources.Load("RenderOutput/LevelPreviews/" + info[i].Name.Replace(".png", "")));
        }

        for (int i = levelNb; i < _containerTransform.childCount; i++)
        {
            //_containerTransform.GetChild(i).gameObject.SetActive(false);
            _containerTransform.GetChild(i).TryGetComponent(out LevelBlock levelBlock);
            levelBlock.InitEmptyBlock(); 
        }
    }
}
