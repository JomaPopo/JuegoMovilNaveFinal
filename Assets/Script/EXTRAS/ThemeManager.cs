using UnityEngine;
using UnityEngine.SceneManagement;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    public SimpleColorPalette currentPalette;
    private System.Collections.Generic.List<UIThemeApplier> appliers = new System.Collections.Generic.List<UIThemeApplier>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void RegisterApplier(UIThemeApplier applier) => appliers.Add(applier);
    public void UnregisterApplier(UIThemeApplier applier) => appliers.Remove(applier);
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aplicar el tema actual cuando se carga una nueva escena
        if (currentPalette != null)
        {
            ApplyCurrentTheme();
        }
    }

    public void SetTheme(SimpleColorPalette newPalette)
    {
        currentPalette = newPalette;
        ApplyCurrentTheme();
    }

    private void ApplyCurrentTheme()
    {
        for (int i = 0; i < appliers.Count; i++)
        {
            appliers[i].ApplyColors(currentPalette);
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse para evitar memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}