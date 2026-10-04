using UnityEngine;

[CreateAssetMenu(fileName = "TextEffectData", menuName = "Scriptable Objects/TextEffectData")]
public class TextEffectData : ScriptableObject
{
    public Gradient Gradient;
    public float GradientSpeed;

    public AnimationCurve XCurve;
    public AnimationCurve YCurve;
    public Vector2 CurveSpeed;
    public Vector2 CurveFactors;
}
