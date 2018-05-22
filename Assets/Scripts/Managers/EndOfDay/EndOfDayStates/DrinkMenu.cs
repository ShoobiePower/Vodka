using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class DrinkMenu : AbstBookStates
{
    private List<Drink> allDrinksInGame = new List<Drink>();
    private DrinkLoader dLoader = new DrinkLoader(); // For now I am just going to list all of the possible drinks in the game.
                                                     // If an unlock system is required, I can do that too. 

    [SerializeField]
    GameObject propsForDrinkPage;

    [SerializeField]
    Text drinkName;

    [SerializeField]
    Text drinkDescriptionText;

    [SerializeField]
    Text ingredentsInDrink;

    [SerializeField]
    Button statButton;

    [SerializeField]
    Button bioButton;


    private void Start()
    {
        dLoader.init();
        allDrinksInGame = dLoader.populateDrinkCollection();
    }

    public override void ShowPresetAssets()
    {
        decideOnHowManyButtonsToShow(allDrinksInGame.Count);
        CurrentSelection = 0;
        CurrentTopOfPage = 0;
        base.ShowPresetAssets(propsForDrinkPage);
        statButton.enabled = false;
        bioButton.gameObject.SetActive(false);
    }

    public override void formatButtonsForThisPage()
    {
        //{ TODO: get the patron who is going on the quest to have their icon appear next to it.
        int i = CurrentTopOfPage;
        foreach (Button menuButton in menuButtons)
        {
            if (menuButton.isActiveAndEnabled)
            {
                menuButtons[i - CurrentTopOfPage].GetComponentInChildren<Text>().text = allDrinksInGame[i].DrinkName;
                menuTokens[i - CurrentTopOfPage].GetComponentInChildren<Image>().sprite = ApperanceManager.instance.getCoinImage(); // For now displays coin image 
                i++;
            }
        }
    }

    public override void HidePresetAssets()
    {
        base.HidePresetAssets(propsForDrinkPage);
        statButton.enabled = true;
        bioButton.gameObject.SetActive(true);
        CurrentSelection = 0;
        CurrentTopOfPage = 0;
    }

    public override void ScrollDown()
    {
     
        if (CurrentBottomOfPage < allDrinksInGame.Count)
        {
            CurrentTopOfPage++;
            CurrentBottomOfPage++;
            formatButtonsForThisPage();
        }

    }

    public override void ScrollUp()
    {
        if (CurrentTopOfPage > 0)
        {
            CurrentTopOfPage--;
            CurrentBottomOfPage--;
            formatButtonsForThisPage();
        }
    }

    public override void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager)
    {
        base.passRefrenceToEndOfDayManager(endOfDayManager);
    }

    public override void ShowStatsOnPage(byte index)
    {
        if (NumberOfActiveButtons > 0)
        {
            Drink drinkToInquireAbout = allDrinksInGame[index + CurrentTopOfPage];
            drinkName.text = drinkToInquireAbout.DrinkName;
            ingredentsInDrink.text = drinkToInquireAbout.RecipeForDrink;
            drinkDescriptionText.text = drinkToInquireAbout.DrinkDescription + addBuffToDescription(drinkToInquireAbout);
            CurrentSelection = index;
        }

    }

    private string addBuffToDescription(Drink drinkToInquireAbout)
    {
        return " \n  \n This drink grants the " + drinkToInquireAbout.Buff.ToString().ToLower() + " buff";
    }

}
