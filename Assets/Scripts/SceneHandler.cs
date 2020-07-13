using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    Manager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<Manager>();
    }

    //llamado desde Level Button y Back to Menu Button 
    public void ChangeScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
        //if (targetScene <= _manager.CurrentLevel)
        //{
            
        //}
        //else
        //{
        //    print("Nivel bloqueado");
        //}
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        if (!_manager.IsLastScene)
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Este es el <color = red>último</color> nivel");
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
