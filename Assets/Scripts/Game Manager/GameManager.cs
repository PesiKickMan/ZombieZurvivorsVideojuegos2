using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1f)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Quit(){
        Application.Quit();
    }
}
