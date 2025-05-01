using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("Textos UI")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    [Header("Configuración Puntaje")]
    [SerializeField] int pointsPerInterval = 10;
    [SerializeField] float scoreInterval = 2f;
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private int multi;
    [SerializeField] private int currentScore;
    [SerializeField] private float elapsedTime;
    [SerializeField] private Coroutine scoreCoroutine;
    [SerializeField] private float vida = 2;
    [SerializeField] private NotificationSimple notificationSystem;

    void OnEnable()
    {
        Bullet.adpoints += AddPoints;
    }

    void OnDisable()
    {
        Bullet.adpoints -= AddPoints;
    }

    void Awake()
    {
        GameManager.Instance.sceneUI = this;
        UpdateHealth(GameManager.Instance.currentHealth, GameManager.Instance.selectedShip.maxHealth);
        InitializeUI();
        StartScoring();
    }

    void InitializeUI()
    {
        UpdateHealth(GameManager.Instance.currentHealth, GameManager.Instance.selectedShip.maxHealth);
        currentScore = 0;
        elapsedTime = 0f;
        UpdateScore();
        UpdateTimer();
        InitializeSystems();
    }

    void AddPoints()
    {
        currentScore += 50;
        UpdateScore();
    }

    void StartScoring()
    {
        if (scoreCoroutine != null) StopCoroutine(scoreCoroutine);
        scoreCoroutine = StartCoroutine(AutoAddPoints());
    }

    IEnumerator AutoAddPoints()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreInterval);
            multi = GameManager.Instance.selectedShip.speed;
            currentScore += pointsPerInterval * multi;
            UpdateScore();
        }
    }

    void InitializeSystems()
    {
        currentScore = 0;
        elapsedTime = 0f;
        scoreCoroutine = StartCoroutine(AutoAddPoints());
        UpdateHealth(GameManager.Instance.currentHealth, GameManager.Instance.selectedShip.maxHealth);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimer();

        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            UpdateTimer();
        }
    }

    public void UpdateHealth(int current, int max)
    {
        healthText.text = $"SALUD: {current}/{max}";
        vida = current;

        if (current <= 0)
        {
            bool isNewRecord = currentScore > scoreData.MaxScore;
            if (notificationSystem != null)
            {
                notificationSystem.SendScoreNotification(currentScore);
            }

            if (isNewRecord)
            {
                scoreData.MaxScore = currentScore;
            }


            GuardarDatosYCambiarEscena();
        }
    }

    void GuardarDatosYCambiarEscena()
    {
        PlayerPrefs.SetFloat("Tiempo", elapsedTime);
        PlayerPrefs.SetInt("Puntos", currentScore);
        PlayerPrefs.Save();
        SceneGlobalManager.Instance.CargarResult();
    }

    void UpdateScore()
    {
        scoreText.text = $"PUNTOS: {currentScore}";
    }

    void UpdateTimer()
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(elapsedTime);
        timerText.text = $"TIEMPO: {time:mm\\:ss}";
    }

    void OnDestroy()
    {
        if (scoreCoroutine != null) StopCoroutine(scoreCoroutine);
    }
}