using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Animator transitionAnim;
    public float transitionTime = 1;
    public bool transitioning = false;

    public void LoadNextSceneInQueue()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        // MAGIC NUMBER
        if (nextScene == 4)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        StartCoroutine(LoadScene(nextScene));
    }

    IEnumerator LoadScene(int nextScene)
    {
        transitioning = true;
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(transitionTime+0.5f);
        SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    public void ReloadCurrentLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
