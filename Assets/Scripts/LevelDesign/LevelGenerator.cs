using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _lineParent;
    [SerializeField] private GameObject _brushPrefab;

    private void Start()
    {
        if(LevelHandler.Instance.SelectedLevelTitle != "") CreateBrushesFromXML();
    }

    public void CreateBrushesFromXML()
    {
        XmlDocument saveFile = new XmlDocument();

        if(LevelHandler.Instance.SelectedLevelTitle == "")
        {
            Debug.LogError("Selected level title is incorrect.");
            return;
        }

        if (!System.IO.File.Exists(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelHandler.Instance.SelectedLevelTitle + "_levelData.xml")) return;
        saveFile.LoadXml(System.IO.File.ReadAllText(Application.dataPath + "/Resources/RenderOutput/LevelDatas/" + LevelHandler.Instance.SelectedLevelTitle + "_levelData.xml"));

        string key;
        string value;

        foreach (XmlNode node in saveFile.ChildNodes[1])
        {
            key = node.Name;
            value = node.InnerText;
            switch (key)
            {
                case string s when s.Contains("Line"):
                    LogManager.Instance.Loggers["System"].Log(s); 

                    GameObject brushInstance = Instantiate(_brushPrefab, _lineParent);
                    brushInstance.TryGetComponent(out LineRenderer currentLineRenderer);

                    EdgeCollider2D collider = brushInstance.AddComponent<EdgeCollider2D>();
                    collider.edgeRadius = 0.1f;
                    List<Vector2> colliderPoints = new();

                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        //print($"Sub | Key : {node2.Name}, value : {node2.InnerText}");

                        switch (node2.Name)
                        {
                            case "Color":
                                currentLineRenderer.startColor = ParsingUtility.ColorFromString(node2.InnerText);
                                currentLineRenderer.endColor = ParsingUtility.ColorFromString(node2.InnerText);
                                break;
                            
                            case "point_0":
                                // split, parse and assemble point pos
                                currentLineRenderer.SetPosition(0, ParsingUtility.Vector3FromString(node2.InnerText));
                                currentLineRenderer.SetPosition(1, ParsingUtility.Vector3FromString(node2.InnerText));

                                colliderPoints.Add(ParsingUtility.Vector3FromString(node2.InnerText) + Vector3.right * 108.543f);
                                break;

                            default:
                                currentLineRenderer.positionCount++;
                                int positionIndex = currentLineRenderer.positionCount - 1;

                                // split, parse and assemble point pos
                                currentLineRenderer.SetPosition(positionIndex, ParsingUtility.Vector3FromString(node2.InnerText));

                                colliderPoints.Add(ParsingUtility.Vector3FromString(node2.InnerText) + Vector3.right * 108.543f);
                                break;
                        }

                    }
                    print(collider.SetPoints(colliderPoints));
                    break;

                default:
                    LogManager.Instance.Loggers["System"].Log($"Key : {key}, value : {value}");
                    break;
            }
        }

        //AddColliders(); 
    }

    private void AddColliders()
    {
        foreach (Transform lineTransform in _lineParent)
        {
            lineTransform.gameObject.TryGetComponent(out LineRenderer lineRenderer);

            Rigidbody rb = lineTransform.gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            MeshCollider meshCollider = lineTransform.gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;

            Mesh mesh = new Mesh();
            lineRenderer.BakeMesh(mesh, true);
            meshCollider.sharedMesh = mesh;

        }
    }
}
