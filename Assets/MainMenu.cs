using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource quitSound;

    public void PlayGame() {
        startSound.Play();
        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        quitSound.Play();
        StartCoroutine(WaitQuit());
    }

    IEnumerator WaitQuit() {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

}
