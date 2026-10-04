using TMPro;
using UnityEngine;

public class EnhancedText : TextMeshProUGUI
{
    private TMP_Text _textComponent;
    private Mesh _mesh;
    private Vector3[] _vertices;
    private Color[] _colors;

    protected override void OnEnable()
    {
        _textComponent = TryGetComponent(out TMP_Text textMesh) ? textMesh : null;
    }

    void Update()
    {
        if (!this.isActiveAndEnabled) return;
        _textComponent.ForceMeshUpdate();
        _mesh = _textComponent.mesh;
        _vertices = _mesh.vertices;
        _colors = _mesh.colors;

        foreach (TMP_LinkInfo link in _textComponent.textInfo.linkInfo)
        {
            ApplyEffectFromLink(link);
        }
    }

    public void ApplyEffectFromLink(TMP_LinkInfo link)
    {
        if (EnhancedTextTagHandler.Instance == null) return;

        // Loops through each character contained in tag
        for (int i = link.linkTextfirstCharacterIndex; i < link.linkTextfirstCharacterIndex + link.linkTextLength; i++)
        {
            if (!EnhancedTextTagHandler.Instance.TextEffects.ContainsKey(link.GetLinkID())) continue;

            TMP_CharacterInfo charInfo = _textComponent.textInfo.characterInfo[i];
            int materialIndex = charInfo.materialReferenceIndex; // Gets the index of the current character material
            Vector3[] newVertices = _textComponent.textInfo.meshInfo[materialIndex].vertices;

            // Loop through all vertices of the current characters
            for (int j = 0; j < 4; j++)
            {
                if (charInfo.character == ' ') continue;

                int vertexIndex = charInfo.vertexIndex + j;


                TextEffectData effectData = EnhancedTextTagHandler.Instance.TextEffects[link.GetLinkID()];

                Gradient gradient = effectData.Gradient;

                if (effectData.GradientSpeed > 0) _colors[vertexIndex] = gradient.Evaluate(Mathf.Repeat(Time.time + _vertices[vertexIndex].x * 0.001f * effectData.GradientSpeed, 1f));
                
                Vector3 offset2 = new Vector2(effectData.XCurve.Evaluate((Time.realtimeSinceStartup * effectData.CurveSpeed.x) + vertexIndex * 0.01f) * effectData.CurveFactors.x,
                                              effectData.YCurve.Evaluate((Time.realtimeSinceStartup * effectData.CurveSpeed.y) + vertexIndex * 0.01f) * effectData.CurveFactors.y);
                
                newVertices[vertexIndex] += offset2;
            }
            _vertices = newVertices;
        }

        _mesh.vertices = _vertices;
        _mesh.colors = _colors;
        _textComponent.canvasRenderer.SetMesh(_mesh);
    }
}
