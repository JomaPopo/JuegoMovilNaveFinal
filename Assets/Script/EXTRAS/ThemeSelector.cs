// ThemeSelector.cs
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelector : MonoBehaviour
{
    [SerializeField] SimpleColorPalette[] availablePalettes;
    [SerializeField] Button[] themeButtons;

    void Start()
    {
        for (int i = 0; i < themeButtons.Length; i++)
        {
            int index = i; // Capturar índice para el listener
            themeButtons[index].onClick.AddListener(() => ThemeManager.Instance.SetTheme(availablePalettes[index]));
        }
    }
}