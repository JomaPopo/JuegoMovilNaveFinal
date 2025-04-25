using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/Score Data")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int maxScore;

    public int MaxScore
    {
        get => maxScore;
        set
        {
            if (value > maxScore)
            {
                maxScore = value;
                SaveMaxScore();
            }
        }
    }

    private const string PlayerPrefsKey = "MaxScore";

    private void OnEnable()
    {
        LoadMaxScore();
    }

    private void SaveMaxScore()
    {
        PlayerPrefs.SetInt(PlayerPrefsKey, maxScore);
        PlayerPrefs.Save();
    }

    private void LoadMaxScore()
    {
        maxScore = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
    }
}