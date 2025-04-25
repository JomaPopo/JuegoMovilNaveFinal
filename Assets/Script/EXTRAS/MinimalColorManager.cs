using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIThemeApplier : MonoBehaviour
{
   

   
    [Header("Referencias Manuales")]
    [SerializeField] private Image[] backgroundImages; // Arrastra TODOS los fondos desde el Inspector
    [SerializeField] private TMP_Text[] themeTexts;    // Arrastra SOLO los textos temáticos
    [SerializeField] private Button[] themeButtons;

    private void OnEnable()
    {
       /* if (ThemeManager.Instance != null)
            ThemeManager.Instance.RegisterApplier(this);
        else
            Debug.LogError("ThemeManager no encontrado. Asegúrate de tenerlo en la escena.");*/
    }
    private void OnDisable()
    {
        if (ThemeManager.Instance != null)
            ThemeManager.Instance.UnregisterApplier(this);
    }
    private void Awake()
    {
        if (ThemeManager.Instance != null)
            ThemeManager.Instance.RegisterApplier(this);
        else
            Debug.LogError("ThemeManager no encontrado. Asegúrate de tenerlo en la escena.");
    }

    private void Start()
    {
        // Aplicar el tema actual al iniciar
        if (ThemeManager.Instance != null && ThemeManager.Instance.currentPalette != null)
        {
            ApplyColors(ThemeManager.Instance.currentPalette);
        }
    }

    private void ApplyAndSetTheme(SimpleColorPalette palette)
    {
        ThemeManager.Instance.SetTheme(palette);
    }

    public void ApplyColors(SimpleColorPalette palette)
    {
        // Aplicar a fondos
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            backgroundImages[i].color = palette.backgroundColor;
        }

        // Aplicar a textos
        for (int i = 0; i < themeTexts.Length; i++)
        {
            themeTexts[i].color = palette.textColor;
        }

        // Aplicar a botones
        for (int i = 0; i < themeButtons.Length; i++)
        {
            ColorBlock colors = themeButtons[i].colors;
            colors.normalColor = palette.buttonNormalColor;
            themeButtons[i].colors = colors;
        }
    }
}