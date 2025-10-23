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
        Time.timeScale = 1f;  // Asegurarse que el tiempo está normal antes de reiniciar
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(obj);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        Application.Quit();
    }
}
