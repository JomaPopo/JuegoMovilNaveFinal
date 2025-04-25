using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;



public class InicioScrip : MonoBehaviour
{
    [Header("Carga de Escena")]
    [SerializeField] private Image loadingBarFill;

    //private string initialScene = "Characters";
    private void Start()
    {
        // Solo desde la SplashScreen se carga el Menu
        if (SceneManager.GetActiveScene().name == "SplashScreen")
        {
            StartCoroutine(LoadInitialSceneAsync());
        }
    }

    private IEnumerator LoadInitialSceneAsync()
    {
        AsyncOperation operation = SceneGlobalManager.Instance.CargarCharacters();
        operation.allowSceneActivation = false;

        float targetProgress = 0f;

        while (!operation.isDone)
        {
            float actualProgress = Mathf.Clamp01(operation.progress / 0.9f);
            targetProgress = Mathf.MoveTowards(targetProgress, actualProgress, Time.deltaTime * 0.5f);

            if (loadingBarFill != null)
                loadingBarFill.fillAmount = targetProgress;

            if (targetProgress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);

                while (loadingBarFill.fillAmount < 1f)
                {
                    loadingBarFill.fillAmount += Time.deltaTime;
                    yield return null;
                }

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void AssignLoadingBar(Image bar)
    {
        loadingBarFill = bar;
    }
}
