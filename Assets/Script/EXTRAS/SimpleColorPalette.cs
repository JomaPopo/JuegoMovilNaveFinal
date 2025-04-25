// SimpleColorPalette.cs
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleColorPalette", menuName = "UI/Simple Color Palette")]
public class SimpleColorPalette : ScriptableObject
{
    [Header("Background")]
    public Color backgroundColor;

    [Header("UI Text (TMP)")]
    public Color textColor;

    [Header("Buttons")]
    public Color buttonNormalColor;
}