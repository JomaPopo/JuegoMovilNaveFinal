using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tiempoText;
    [SerializeField] private TMP_Text puntosText;
    [SerializeField] private TMP_Text maxPuntosText;
    [SerializeField] private ScoreData scoreData; 

    void Start()
    {
        // Recuperar datos guardados
        tiempoText.text = $"TIEMPO: {System.TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Tiempo")).ToString("mm\\:ss")}";
        puntosText.text = $"PUNTOS: {PlayerPrefs.GetInt("Puntos")}";

        // Mostrar el máximo puntaje desde el ScriptableObject
        maxPuntosText.text = $"MAXIMO PUNTOS: {scoreData.MaxScore}";
    }

    //public void ReiniciarJuego()
    //{
      
    //    SceneManager.LoadScene("MainGame");
    //}
    public void Tryagain()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneGlobalManager.Instance.CargarSplashScreen();
    }
}