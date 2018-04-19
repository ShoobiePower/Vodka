using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button QuitButton, StartButton, CreditsButton, BackButton;

    private List<Button> MainMenuButtons;

    [SerializeField]
    private GameObject loadingPanel, NamesInCredits;

    public void Start()
    {
        SetupButtonsList();
        
    }

    private void SetupButtonsList()
    {
        MainMenuButtons = new List<Button>();

        MainMenuButtons.Add(QuitButton);
        MainMenuButtons.Add(StartButton);
        MainMenuButtons.Add(CreditsButton);

        foreach (Button button in MainMenuButtons)
        {
            button.GetComponent<Animator>().SetTrigger("FadeIn");
        }
    }

    public void startGameButtonPressed()
    {
        StartCoroutine(PlayAndWaitForAnimation(StartButton.gameObject, "FadeOut"));

        SceneManager.LoadScene("Scencely");
    }
    
    public void FadeOutMainMenuButtonsAndOpenCredits()
    {
        foreach (Button button in MainMenuButtons)
        {
            button.GetComponent<Animator>().SetTrigger("FadeOut");
        }

        BackButton.GetComponent<Animator>().SetTrigger("FadeIn");

        NamesInCredits.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeInMainMenuButtonsAndCloseCredits()
    {
        StartCoroutine(PlayAndWaitForAnimation(NamesInCredits, "FadeOut"));
        StartCoroutine(PlayAndWaitForAnimation(BackButton.gameObject, "FadeOut"));

        foreach (Button button in MainMenuButtons)
        {
            button.GetComponent<Animator>().SetTrigger("FadeIn");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator PlayAndWaitForAnimation(GameObject target, string stateName)
    {
        int animLayer = 0;

        Animator anim = target.GetComponent<Animator>();
        anim.Play(stateName);

        while (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }

    //public void ResetCreditAnimation(string animationName)
    //{
    //    //Animation.Play lets me reset the animation to the first frame ("State", layer, normalizedTime)
    //    CreditsButton.GetComponent<Animator>().Play(animationName, -1, 0f);
    //}

}