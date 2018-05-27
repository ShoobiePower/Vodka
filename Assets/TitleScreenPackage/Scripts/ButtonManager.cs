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
    private GameObject loadingPanel, titlePanel, NamesInCredits;

    [SerializeField]
    private Image titleImage;

    [SerializeField]
    private float speedOfFade;

    [SerializeField]
    private Image creditPanel;

    private const float fadeOffeset = 10f;
    private const float fullAlphaValue = 255;

    private Vector4 fadeToBlackColor = new Vector4(0, 0, 0, fullAlphaValue);

    private enum AnimationStates { OPEN, FADING, CLOSED}
    private AnimationStates currentAnimationState;

    public void Start()
    {
        SetupButtonsList();
        setAnimationState(AnimationStates.OPEN);
    }

    private void setAnimationState(AnimationStates anaimationStateToSwapTo)
    {
        currentAnimationState = anaimationStateToSwapTo;
    }

    private void Update()
    {
        if (currentAnimationState == AnimationStates.FADING)
        {
            titlePanel.gameObject.GetComponent<Image>().color = Vector4.Lerp(titlePanel.gameObject.GetComponent<Image>().color, fadeToBlackColor, speedOfFade * Time.deltaTime);
            titleImage.color = Vector4.Lerp(titleImage.color, fadeToBlackColor, speedOfFade * Time.deltaTime);
            checkIfFadeHasEnded(titleImage.color.a);
        }
    }

    private void checkIfFadeHasEnded(float fadeColor)
    {
        if(fadeColor > (fullAlphaValue - fadeOffeset))
        {
            titlePanel.gameObject.GetComponent<Image>().color = fadeToBlackColor;
            titleImage.color = fadeToBlackColor;
           setAnimationState(AnimationStates.CLOSED);
            loadingPanel.SetActive(true);
            SceneManager.LoadScene("Scencely");

        }
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
        foreach (Button button in MainMenuButtons)
        {
            button.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        setAnimationState(AnimationStates.FADING);
    }
    
    public void FadeOutMainMenuButtonsAndOpenCredits()
    {
        foreach (Button button in MainMenuButtons)
        {
            button.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        creditPanel.gameObject.SetActive(true);

        BackButton.GetComponent<Animator>().SetTrigger("FadeIn");

        NamesInCredits.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void FadeInMainMenuButtonsAndCloseCredits()
    {

        creditPanel.gameObject.SetActive(false);

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

}