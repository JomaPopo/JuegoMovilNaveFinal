using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }
    public Ship selectedShip;          
    public ControlType controlScheme; 
    public int currentHealth;
    public GameUI sceneUI;

    public enum ControlType
    {
        Accelerometer,
        Gyroscope
    }

    void Awake()
    {
        if (Instance == null)
        {


            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            if (selectedShip != null)
            {
                currentHealth = selectedShip.maxHealth;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void TakeDamage()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);

        // Actualizar UI si existe
        if (sceneUI != null)
        {
            sceneUI.UpdateHealth(currentHealth, selectedShip.maxHealth);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            currentHealth = selectedShip.maxHealth;

            if (sceneUI != null) sceneUI.UpdateHealth(currentHealth, selectedShip.maxHealth);
        }
    }
}
