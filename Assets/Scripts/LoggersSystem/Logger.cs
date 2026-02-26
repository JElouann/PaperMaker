using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Logger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool _showLogs;
    [Header("Announcer")]
    [SerializeField] private string _announcer;
    [SerializeField] private Color _announcerColor;
    [SerializeField] private bool _showAnnouncer;
    [Header("Tracked Logs")]
    [SerializeField] private bool _showTracker;
    [SerializeField] private bool _useAnnouncerColorForTracker;

    /// <summary>
    /// Prints a message in the console.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    public void Log(object message, Object sender = default)
    {
        if (!_showLogs) return;
        if (_showAnnouncer) Debug.Log($"<color=#{_announcerColor.ToHexString()}>[{_announcer}]</color> {message}", sender);
        else Debug.Log($"{message}", sender);
    }

    /// <summary>
    /// Prints a message in the console that contains the precise file, method and line that called this method.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="member"></param>
    /// <param name="file"></param>
    /// <param name="line"></param>
    /// <param name="sender"></param>
    public void TrackedLog(object message, [CallerMemberName] string member = "", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0, Object sender = default)
    {
        if (!_showLogs) return;
        string final = "";
        if (_showAnnouncer) final += $"<color=#{_announcerColor.ToHexString()}>[{_announcer}]</color> ";
        if (_showTracker)
        {
            if (_useAnnouncerColorForTracker) final += $"<color=#{_announcerColor.ToHexString()}>[{System.IO.Path.GetFileName(file)} : L{line} | {member}()]</color> {message}";
            else final += $"[{System.IO.Path.GetFileName(file)} : L{line} | {member}()] {message}";
        }

        Debug.Log($"{final}", sender);
    }
}
