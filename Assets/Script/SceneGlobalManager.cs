using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneGlobalManager : MonoBehaviour
{
    public static SceneGlobalManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   
    public void CargarMainGame()
    {
        SceneManager.LoadSceneAsync("MainGame");
    }
    public void CargarResult()
    {
        SceneManager.LoadSceneAsync("Resultados");
    }
    public void CargarSplashScreen()
    {
        SceneManager.LoadSceneAsync("SplashScreen");
    }
    public AsyncOperation CargarCharacters()
    {
        
        return SceneManager.LoadSceneAsync("Characters");
    }
}
