using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;

    private int totalPlanets = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void RegisterPlanet()
    {
        totalPlanets++;
    }

    public void PlanetDestroyed()
    {
        totalPlanets--;
        if (totalPlanets <= 0)
        {
            Cursor.lockState = CursorLockMode.None;

            Victory();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    private void Victory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("exiting");
        Application.Quit(); 
    }
}