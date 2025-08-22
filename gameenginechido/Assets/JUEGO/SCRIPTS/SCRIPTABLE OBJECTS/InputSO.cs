using UnityEngine;

[CreateAssetMenu(menuName = "My Scriptable Objects/Input Config", fileName = "New Input Config")]
public class InputSO : ScriptableObject
{
    public KeyCode jump;

    public KeyCode interact;

    public KeyCode pause;
}
