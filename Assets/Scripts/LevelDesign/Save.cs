using System.Threading.Tasks;
using System.Xml;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Save : MonoBehaviour
{
    [SerializeField] private string _playerName;
    [SerializeField] private Transform _lineParent;
    public TMP_InputField LevelNameInputField;

    private XmlWriter _writer;
    private XmlWriterSettings _xmlWriterSettings = new XmlWriterSettings
    {
        NewLineOnAttributes = true,
        Indent = true
    };

    public void SaveFile()
    {
        _writer = XmlWriter.Create(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelNameInputField.text + "_levelData.xml", _xmlWriterSettings);
        _writer.WriteStartDocument();

        _writer.WriteStartElement("PlayerData");

        WriteXML(_writer, "PlayerName", _playerName);

        // pour chaque ligne
        for (int i = 0, count1 = _lineParent.childCount; i < count1; i++)
        {
            _writer.WriteStartElement("Line_" + (i + 1).ToString());
            _lineParent.GetChild(i).TryGetComponent(out LineRenderer lineRenderer);

            WriteXML(_writer, "Color", lineRenderer.startColor.ToString());

            // pour chaque point
            for (int j = 0, count2 = lineRenderer.positionCount; j < count2; j++)
            {
                WriteXML(_writer, "point_" + j, lineRenderer.GetPosition(j).ToString());
            }
            _writer.WriteEndElement();
        }

        for (int i = 0; i < StickerHandler.PlacedStickers.Count; i++)
        {
            Sticker sticker = StickerHandler.PlacedStickers[i];
            switch (sticker.StickerType)
            {
                case StickerType.Player:
                    _writer.WriteStartElement("Player");
                    WriteXML(_writer, "playerUIPos", sticker.transform.position.ToString());
                    WriteXML(_writer, "playerWorldPos", Camera.main.ScreenToWorldPoint(sticker.transform.position).ToString());
                    _writer.WriteEndElement();
                    break;

                case StickerType.LevelEnd:
                    _writer.WriteStartElement("LevelEnd");
                    WriteXML(_writer, "levelEndUIPos", sticker.transform.position.ToString());
                    WriteXML(_writer, "levelEndWorldPos", Camera.main.ScreenToWorldPoint(sticker.transform.position).ToString());
                    _writer.WriteEndElement();
                    break;
            }
        }
        _writer.Close();
    }

    public Level GetLevel(string levelName)
    {
        // checks if data exists and gets it
        XmlDocument levelDatas = new XmlDocument();
        if (!System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + levelName + "_levelData.xml")) return new();
        levelDatas.LoadXml(System.IO.File.ReadAllText(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + levelName + "_levelData.xml"));

        #region preview
        // gets preview
        Texture levelPreview = Resources.Load<Texture>("RenderOutput/LevelPreviews/" + levelName + "_previewImage".Replace(".png", ""));
        RenderTexture rt = RenderTexture.GetTemporary(
            levelPreview.width,
            levelPreview.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
        );

        Graphics.Blit(levelPreview, rt);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D readableTex = new Texture2D(levelPreview.width, levelPreview.height);
        readableTex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTex.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        #endregion
        return new Level(
            levelName,
            levelDatas.OuterXml,
            readableTex
        );
    }

    private void Start()
    {
        //return;
        //XmlDocument levelDatas = new XmlDocument();

        //if (!System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelNameInputField.text + "_levelData.xml"))
        //{
        //    _writer = XmlWriter.Create(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelNameInputField.text + "_levelData.xml", _xmlWriterSettings);

        //    // begin edit
        //    _writer.WriteStartDocument();

        //    _writer.WriteStartElement("PlayerData");

        //    _writer.WriteStartElement("PlayerName");
        //    _writer.WriteEndElement();

        //    _writer.WriteEndElement();

        //    _writer.WriteStartElement("LevelData"); 

        //    for (int lines = 0; lines < 20; lines++)
        //    {
        //        _writer.WriteStartElement("Line_" + lines + 1);

        //        _writer.WriteStartElement("Color");
        //        _writer.WriteEndElement();

        //        for (int points = 0; points < 300; points++)
        //        {
        //            _writer.WriteStartElement("point_" +  points);
        //            _writer.WriteEndElement();
        //        }
        //        _writer.WriteEndElement();
        //    }

        //    _writer.WriteEndDocument();
        //    _writer.Close();
        //}


        //LoadSave();
    }

    public void LoadSave()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelNameInputField.text + "_levelData.xml")) return;
        saveFile.LoadXml(System.IO.File.ReadAllText(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelNameInputField.text + "_levelData.xml"));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key)
            {
                case "PlayerName":
                    //_playerNameDisplay.text = value;
                    break;

                case "GameTime":
                    //_chrono.PreviousTime = float.Parse(value);
                    break;

                case "SaveNumber":
                    //_numOfSaveDisplay.text = value;
                    break;
            }
        }
    }

    static void WriteXML(XmlWriter writer, string key, string value)
    {
        writer.WriteStartElement(key);
        writer.WriteString(value);
        writer.WriteEndElement();
    }
}
