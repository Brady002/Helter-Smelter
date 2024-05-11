using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [Header("Main")]
    public Button Play;
    public Button Quit;
    public Image Banner;
    public GameObject MainScreen;

    [Header("Level Select")]
    public Button Level1;
    public Button Level2;
    public Button Back;
    public GameObject LevelSelectScreen;

    public Animator Transition;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetMain(true, 0));
        StartCoroutine(SetLevelSelect(false, 0f));
    }

    private IEnumerator SetMain(bool show, float time)
    {
        yield return new WaitForSeconds(time);

        MainScreen.SetActive(show);
    }

    private IEnumerator SetLevelSelect(bool show, float time)
    {
        yield return new WaitForSeconds(time);
        LevelSelectScreen.SetActive(show);
    }

    public void StartButton()
    {
        Transition.SetTrigger("Fade");
        StartCoroutine(SetMain(false, .5f));
        StartCoroutine(SetLevelSelect(true, .5f));
    }

    public void BackButton()
    {
        Transition.SetTrigger("Fade");
        StartCoroutine(SetMain(true, .5f));
        StartCoroutine(SetLevelSelect(false, .5f));
    }

    public void OnLevelOneSelected()
    {
        StartCoroutine(StartLevelOne());
    }

    public void OnLevelTwoSelected()
    {
        StartCoroutine(StartLevelTwo());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartLevelOne()
    {
        Transition.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level One");

    }

    private IEnumerator StartLevelTwo()
    {
        Transition.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level Two");

    }
}
