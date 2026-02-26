using UnityEngine;

public struct Level
{
    public string LevelName { get; private set; }
    public string LevelData { get; private set; }
    public Texture LevelPreview { get; private set; }

    public Level(string levelName, string levelData, Texture levelPreview) 
    {
        LevelName = levelName;
        LevelData = levelData;
        LevelPreview = levelPreview;
    }
}
