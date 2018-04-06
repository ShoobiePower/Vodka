using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class OrderManager : MonoBehaviour {

    private List<Drink> allLockedDrinksInGame = new List<Drink>();
    private DrinkLoader dLoader = new DrinkLoader();


    private List<Drink> allDrinksTheBartenderKnows = new List<Drink>();
    private List<Ingredient.ingredientColor> allUnlockedIngredentColors = new List<Ingredient.ingredientColor>();
    public enum OrderOptions {  BYCOLOR, BYWITHOUTCOLOR, LENGTH }

    private string[] drinkNames = new string[16];

    public enum orderAccuracy {NONE,MIXUP, CORRECT};

    [SerializeField]
    Image[] allQuestionMarkBlocks = new Image[16]; // only problem with this is that I need to know order of drinks and load them in in proper order... I need to find a more dynamic way of doing this thing. 

    public void init()
    {
        dLoader.init();
        allLockedDrinksInGame = dLoader.populateDrinkCollection();
        loadDrinkNames();
    }


    private Drink chooseRandomDrink()
    {
       int orderNumber = Random.Range(0, allDrinksTheBartenderKnows.Count);  //drinkCollection

        return allDrinksTheBartenderKnows[orderNumber];   //drinkCollection
    }

    public void unlockNewDrinksBasedOnIngredients(Ingredient.ingredientColor newIngredentColor)
    {
        Debug.Log("Unlocking color" + newIngredentColor.ToString());
        allUnlockedIngredentColors.Add(newIngredentColor);
        checkForNewDrinks();
    }

    private void checkForNewDrinks()
    {
        bool isDrinkAddable;
        bool isIngredentFound;
        for (int j = 0; j < allLockedDrinksInGame.Count; j++)
        {
            isDrinkAddable = true;
   
            for (Ingredient.ingredientColor i = 0; i < Ingredient.ingredientColor.LENGTH; i ++)
            {
                isIngredentFound = false;
                if (allLockedDrinksInGame[j].DrinkIngredents[(byte)i] == 0)
                {
                    continue;
                }
                
                for (int k = 0; k < allUnlockedIngredentColors.Count; k++)
                {
                    if (i == allUnlockedIngredentColors[k])
                    {
                        isIngredentFound = true;
                        break;
                    }

                }

                if (!isIngredentFound)
                {
                    isDrinkAddable = false;
                    break;
                }
               

            }
            if (isDrinkAddable)
            {
                //Debug.Log("FoundDrinkOfName" + allLockedDrinksInGame[j].DrinkName);
                allDrinksTheBartenderKnows.Add(allLockedDrinksInGame[j]);
                hideQuestionMarkOnDrinkChart(allLockedDrinksInGame[j].DrinkName);
                allLockedDrinksInGame.RemoveAt(j);
                j--;
            }
        }
    }

    private void loadDrinkNames() 
    {
        for( int i = 0; i < allLockedDrinksInGame.Count; i++)
        {
            drinkNames[i] = allLockedDrinksInGame[i].DrinkName;
            allQuestionMarkBlocks[i].gameObject.SetActive(true);
        }
    }

    private void hideQuestionMarkOnDrinkChart(string drinkUnlocked)
    {
      for (int i = 0; i < drinkNames.Length; i++)
        {
            if (drinkUnlocked == drinkNames[i])
            {
                allQuestionMarkBlocks[i].gameObject.SetActive(false);
            }
        }
    }

    public orderAccuracy determineDrinkPrice(IOrder PatronOrder , Drink drinkMade)
    {
        if (PatronOrder.checkAccuracy(drinkMade))
        { 
          
         SoundManager.Instance.AddCommand("Pay");
            return orderAccuracy.CORRECT;
        }
        else
        {
            SoundManager.Instance.AddCommand("No");
            return orderAccuracy.MIXUP;
        }
    }

    //Originally called: DoesDrinkExistAndIsUnlocked(Drink drinkTocheck)
    public Patron.SkillTypes GetDrinkBuffIfDrinkExistsAndIsUnlocked(Drink drinkTocheck)
    {

        for (int i = 0; i < allDrinksTheBartenderKnows.Count; i++)
        {

            // https://stackoverflow.com/questions/50098/comparing-two-collections-for-equality-irrespective-of-the-order-of-items-in-the
            // Note: if we want the order of ingredients to matter, we can remove the "OrderBy" part of the condition
            // NC: The order by was throwing off which buff was transfered, each drink returned the dragonbyte buff (Strong)\
            // Order also dosen't matter this way.
            if (allDrinksTheBartenderKnows[i].getIngredentsInDrink().SequenceEqual(drinkTocheck.getIngredentsInDrink()))
            {
                return allDrinksTheBartenderKnows[i].Buff;
            }
        }

        return Patron.SkillTypes.NONE;
    }


    //public orderAccuracy determineDrinkPrice(IOrder PatronOrder, Drink drinkMade)
    //{
    //    Drink drinkFromDatabase = findDrinkInDataBase(drinkMade);

    //    if (drinkFromDatabase == null)
    //    {
    //        Debug.Log("This drink dosen't even exist");
    //        SoundManager.Instance.AddCommand("No");
    //        return orderAccuracy.NONE;
    //    }

    //    if (PatronOrder.checkAccuracy(drinkFromDatabase))
    //    {
    //        Debug.Log("Correct Drink");
    //        SoundManager.Instance.AddCommand("Pay");
    //        drinkMade.CorrectPrice = drinkFromDatabase.CorrectPrice;
    //        return orderAccuracy.CORRECT;
    //    }
    //    else
    //    {
    //        Debug.Log("Mix up");
    //        SoundManager.Instance.AddCommand("Mix Up");
    //        drinkMade.MixUpPrice = drinkFromDatabase.MixUpPrice;
    //        return orderAccuracy.MIXUP;
    //    }

    //}

    //private Drink findDrinkInDataBase (Drink drinkToFind)
    //{

    //    bool canDrinkBeReturned;
    //    foreach (Drink d in allDrinksTheBartenderKnows)
    //    {
    //       canDrinkBeReturned = true;
    //        if (d.NumberOfIngredentsInDrink != drinkToFind.NumberOfIngredentsInDrink)
    //        {
    //            continue;
    //        }

    //        else
    //        {
    //            for (int i = 0; i < d.DrinkIngredents.Length; i++)
    //            {
    //                if (d.DrinkIngredents[i] != drinkToFind.DrinkIngredents[i])
    //                {
    //                    canDrinkBeReturned = false;
    //                    break;
    //                }
    //            }
    //            if (canDrinkBeReturned)
    //            {
    //                 return d;
    //            }
    //        }
    //    }

    //    return null;

    //}

    public IOrder makeARandomOrder()
    {
        int randomNumber = Random.Range(0, (byte)OrderOptions.LENGTH);
        return (makeSpecificOrder((OrderOptions)randomNumber));
    }

    public IOrder makeSpecificOrder(OrderOptions requestedOrder)
    {
        
        switch (requestedOrder)
        {
            //case OrderOptions.BYNAME:
            //    {
            //        return new OrderByName(chooseRandomDrink());
                  
            //    }
            //case OrderOptions.BYFLAVOR:
            //    {
            //        return new OrderByFlavor(chooseRandomDrink().ThisDrinksFlavor);
                   
            //    }
            case OrderOptions.BYCOLOR:
                {
                    return new OrderByIngredent(chooseRandomIngredentFromKnownIngredents());
                 
                }

            case OrderOptions.BYWITHOUTCOLOR:
                {
                    return new OrderByLackOfIngredent();
                   
                }

            default:
                {
                    return new OrderByName(chooseRandomDrink());
                 
                }
        }
    }

    private Ingredient.ingredientColor chooseRandomIngredentFromKnownIngredents()
    {
        int randomNumber = Random.Range(0, allUnlockedIngredentColors.Count);
        return allUnlockedIngredentColors[randomNumber];
    }

    #region drinkAccessors
    
    public Drink getDrinkByName(string nameOfDrinkToFind)
    {
        foreach (Drink D in allDrinksTheBartenderKnows)
        {
            if (D.DrinkName == nameOfDrinkToFind)
            {
                return D;
            }
        }

        return null;
    }
    #endregion
}
