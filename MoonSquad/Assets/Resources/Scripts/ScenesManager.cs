using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnMeuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("SquadSelect");
    }

    public void OnStartGameButton()
    {
        SceneManager.LoadScene("Game");

        StartCoroutine(DisableLoadingScreen());
    }

    public IEnumerator DisableLoadingScreen()
    {
        yield return new WaitForSeconds(1);

        GameManager.instance.InitializeGame();

        yield return new WaitForSeconds(2.5f);

        GameManager.instance.loadingScreen.enabled = false;
    }
}
