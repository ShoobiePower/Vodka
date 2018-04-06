using UnityEngine;
using System.Collections;

public class ApperanceManager : MonoBehaviour {

    public static ApperanceManager instance { get; private set; }

    public Sprite flagSprite;

    Sprite[] ingredentApperance = new Sprite[4];
    public Sprite RED;
    public Sprite YELLOW;
    public Sprite GREEN;
    public Sprite BLUE;

    public Sprite[] allCurrenciesApperances = new Sprite[5];
    public Sprite Gold;
    public Sprite AApts;
    public Sprite EvilOrderPts;
    public Sprite CollegePts;
    public Sprite CorporealPts;

    public Sprite[] allIngredientApperances = new Sprite[5];
    public Sprite RedIngBox;
    public Sprite YellowIngBox;
    public Sprite GreenIngBox;
    public Sprite BlueIngBox;
    public Sprite BlankIngBox;

    [SerializeField]
    Sprite coinImage;

    [SerializeField]
    Sprite HilightedBarSeatToken;

    [SerializeField]
    Sprite UnHilightedBarSeatToken;

    public Sprite[] allFactionArt = new Sprite[4];


    public Sprite[] PatronArtInGame = new Sprite[7];

    public Sprite[] PatronTokens = new Sprite[7];

    public Sprite[] PatronBarToken = new Sprite[7];

    [SerializeField]
    Sprite emptySeatToken; 

    public Sprite soldOut;


    private void Awake()
    {
        instance = this;
        ingredentApperance[0] = RED;
        ingredentApperance[1] = YELLOW;
        ingredentApperance[2] = GREEN;
        ingredentApperance[3] = BLUE;

        allCurrenciesApperances[0] = Gold;
        allCurrenciesApperances[1] = AApts;
        allCurrenciesApperances[2] = EvilOrderPts;
        allCurrenciesApperances[3] = CollegePts;
        allCurrenciesApperances[4] = CorporealPts;

       
        allFactionArt[0] = CollegePts;
        allFactionArt[1] = CorporealPts;
        allFactionArt[2] = AApts;
        allFactionArt[3] = EvilOrderPts;


        allIngredientApperances[0] = RedIngBox;
        allIngredientApperances[1] = YellowIngBox;
        allIngredientApperances[2] = GreenIngBox;
        allIngredientApperances[3] = BlueIngBox;
        allIngredientApperances[4] = BlankIngBox;

    }

    public Sprite whatDoesTheIngredentLookLike(Ingredient.ingredientColor theColorOfTheIngredent)
    {
        return ingredentApperance[(int)theColorOfTheIngredent];
    }

    public Sprite HowThisPatronLooks(byte patronsId)
    {
        return PatronArtInGame[patronsId];
    }

    public Sprite ThisPatronsToken(byte patronsId)
    {
        return PatronTokens[patronsId];
    }

    public Sprite ThisPatronsBarToken(byte patronsId)
    {
        return PatronBarToken[patronsId];
    }


    public Sprite GetEmptySeatToken()
    {
        return emptySeatToken;
    }


    public Sprite whatCurrencyLooksLike(Currency.whosCurrencyIsThis currencyIn)
    {
        return allCurrenciesApperances[(int)currencyIn];
    }

    public Sprite whatFactionIsThis(Patron.Aligence aligenceIn)
    {
        return allFactionArt[(int)aligenceIn];
    }

    public Sprite getFlag() 
    {
        return flagSprite;
    }

    public Sprite getSoldOut()
    {
        return soldOut;
    }


    public Sprite getHilightedBarSeatToken()
    {
        return HilightedBarSeatToken;
    }

    public Sprite getUnHilightedBarSeatToken()
    {
        return UnHilightedBarSeatToken;
    }

    public Sprite getCoinImage()
    {
        return coinImage;
    }

    public Sprite getIngredientColor(byte colorToGet)
    {
        return allIngredientApperances[colorToGet];
    }

    public Sprite getBlankIngredientColor()
    {
        return allIngredientApperances[allIngredientApperances.Length -1];
    }
}
