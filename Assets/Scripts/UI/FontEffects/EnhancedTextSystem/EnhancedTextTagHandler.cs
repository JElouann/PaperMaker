using AYellowpaper.SerializedCollections;
using UnityEngine;

public class EnhancedTextTagHandler : MonoBehaviour
{
    public SerializedDictionary<string, TextEffectData> TextEffects = new();
    public bool OnOff;
    public static EnhancedTextTagHandler Instance;

    private void OnEnable()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
}
