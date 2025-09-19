using UnityEngine;

public class MenuGame : MonoBehaviour
{
    public GameObject pauseMenuUi;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;          
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUi.SetActive(true); 
        Time.timeScale = 0f;          
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Saindo...");
        Application.Quit(); 
    }
}
