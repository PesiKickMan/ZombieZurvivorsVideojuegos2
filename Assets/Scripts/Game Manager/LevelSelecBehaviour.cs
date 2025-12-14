using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelecBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SelectLevel1()
    {
        SceneManager.LoadScene("LabLevel");
    }

    public void SelectLevel2()
    {
        SceneManager.LoadScene("Game");
    }
}
