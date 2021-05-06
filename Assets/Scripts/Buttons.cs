using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer am;
    [SerializeField] private Slider slide;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameState.endGame && GameState.titleScreenComplete)
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            if(pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 0;
                
            }else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void setAudio()
    {
        am.SetFloat("Dialog", slide.value);
    }

}
