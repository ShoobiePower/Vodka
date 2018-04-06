using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IconPack : ABSTFadableObject { 

    public Sprite patronWantsAdventureSprite;
    public Sprite patronReturningFromQuestSprite;
    public Sprite patronHasRumorSprite;

    SpriteRenderer spriteToDisplay;

    public void initIconsToDisplay()
    {
        spriteToDisplay = this.GetComponent<SpriteRenderer>();
        assignObjectToFade(spriteToDisplay);
    }

    public void initFadeTime(float fadeTime)
    {
        setFadeTime(fadeTime);
    }

    public void PatronWantsToGoOnAnAdventure()
    {
        this.gameObject.SetActive(true);
        spriteToDisplay.sprite = patronWantsAdventureSprite;
    }


    public void patronWantsToTellYouSomething()
    {
        this.gameObject.SetActive(true);
        spriteToDisplay.sprite = patronHasRumorSprite;
    }

    public void patronIsReturningFromQuest()
    {
        this.gameObject.SetActive(true);
        spriteToDisplay.sprite = patronReturningFromQuestSprite;
    }

    public void clearNeedsIcons()
    {
        this.gameObject.SetActive(false);
    }


}
