using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppLauncher : MonoBehaviour
{
    public Canvas LoadingScreenCanvas;
    public Text LoadingText;
    public Text LoadingTimeText;
    public int minimumLoadingTimeSec = 3;

    private AsyncOperation _loadingOperation;
    private float _elapsedLoadingTime;

    void Start()
    {
        LoadScene("Test");
    }

    private void LoadScene(string sceneName)
    {
        ShowLoadingScreen();
        StartLoadingAsyncOperation(sceneName);
    }

    private void StartLoadingAsyncOperation(string sceneName)
    {
        _elapsedLoadingTime = 0;
        _loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadingOperation.allowSceneActivation = false;
    }

    private void ShowLoadingScreen()
    {
        LoadingScreenCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (IsLoading())
        {
            LoadingTextFlashingEffect();
            HandleLoadingOperation();
        }
    }

    private bool IsLoading()
    {
        return _loadingOperation != null;
    }

    private void HandleLoadingOperation()
    {
        if (_loadingOperation.isDone)
        {
            FinishLoadingProcess();    
        }
        else
        {
            UpdateElapsedLoadingTime();
        }
    }

    private void UpdateElapsedLoadingTime()
    {
        _elapsedLoadingTime += Time.deltaTime;
        LoadingTimeText.text = "Elapsed loading time : " + Mathf.Floor(_elapsedLoadingTime);

        if (_elapsedLoadingTime > minimumLoadingTimeSec)
        {
            _loadingOperation.allowSceneActivation = true;
            // The loading operation will be done on next update.
        }
    }

    private void LoadingTextFlashingEffect()
    {
        LoadingText.color = new Color(
                LoadingText.color.r,
                LoadingText.color.g,
                LoadingText.color.b,
                Mathf.PingPong(Time.time, 1)
            );
    }

    private void FinishLoadingProcess()
    {
        _loadingOperation = null;
        HideLoadingScreen();
    }
    
    private void HideLoadingScreen()
    {
        LoadingScreenCanvas.gameObject.SetActive(false);
    }
}
