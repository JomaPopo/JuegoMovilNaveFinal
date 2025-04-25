using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlSelector : MonoBehaviour
{

    [SerializeField] Button accelerometerButton;
    [SerializeField] Button gyroButton;

    void Start()
    {
        // Verificar si el dispositivo tiene giroscopio
        gyroButton.interactable = SystemInfo.supportsGyroscope;

        accelerometerButton.onClick.AddListener(() => {
            GameManager.Instance.controlScheme = GameManager.ControlType.Accelerometer;
            LoadGameplay();
        });

        if (SystemInfo.supportsGyroscope)
        {
            gyroButton.onClick.AddListener(() => {
                GameManager.Instance.controlScheme = GameManager.ControlType.Gyroscope;
                LoadGameplay();
            });
        }
    }

    void LoadGameplay()
    {
        SceneGlobalManager.Instance.CargarMainGame();
    }
}