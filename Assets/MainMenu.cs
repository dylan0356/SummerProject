using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource quitSound;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        startSound.Play();
    }

    public void QuitGame() {
        Application.Quit();
        quitSound.Play();
    }

}
