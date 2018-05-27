using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApperanceManager : MonoBehaviour {

    public static ApperanceManager instance { get; private set; }

    public Sprite questAlert;

    Sprite[] ingredentApperance = new Sprite[4];
    public Sprite RED;
    public Sprite YELLOW;
    public Sprite GREEN;
    public Sprite BLUE;

    public Sprite[] allCurrenciesApperances = new Sprite[4];
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

    Dictionary<string, Sprite> patronArt = new Dictionary<string, Sprite>();


    
    
    [System.Serializable]
    public struct NamedImages
    {
        public string name;
        public Sprite sprite;
    }
    public NamedImages[] patronArtToLoad;


    public Sprite[] allFactionArt = new Sprite[4];


   // public Sprite[] PatronArtInGame = new Sprite[12];

    public Sprite[] PatronTokens = new Sprite[7];

    public Sprite[] PatronBarToken = new Sprite[7];

    [SerializeField]
    Sprite emptySeatToken; 


    private void Awake()
    {
        instance = this;
        ingredentApperance[0] = RED;
        ingredentApperance[1] = YELLOW;
        ingredentApperance[2] = GREEN;
        ingredentApperance[3] = BLUE;


        allCurrenciesApperances[0] = AApts;
        allCurrenciesApperances[1] = EvilOrderPts;
        allCurrenciesApperances[2] = CollegePts;
        allCurrenciesApperances[3] = CorporealPts;

       
        allFactionArt[0] = CollegePts;
        allFactionArt[1] = CorporealPts;
        allFactionArt[2] = AApts;
        allFactionArt[3] = EvilOrderPts;


        allIngredientApperances[0] = RedIngBox;
        allIngredientApperances[1] = YellowIngBox;
        allIngredientApperances[2] = GreenIngBox;
        allIngredientApperances[3] = BlueIngBox;
        allIngredientApperances[4] = BlankIngBox;


        initCharacterArtDictionary();
    }

    public Sprite whatDoesTheIngredentLookLike(Ingredient.ingredientColor theColorOfTheIngredent)
    {
        return ingredentApperance[(int)theColorOfTheIngredent];
    }

    public Sprite HowThisPatronLooks(string spriteToLookFor)
    {
        Sprite test;
        if (patronArt.TryGetValue(spriteToLookFor, out test)) 
        {
            return test;
        }
        string[] parts = spriteToLookFor.Split('_');

        return patronArt[parts[0]];
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

    // pack ratting this for now, design may want to keep the harry potter house idea rather than the choose your own adventure route, we will see. 
    public Sprite whatCurrencyLooksLike(Currency.whosCurrencyIsThis currencyIn)
    {
        return allCurrenciesApperances[(int)currencyIn];
    }

    public Sprite GetQuestAlertToken() 
    {
        return questAlert;
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

    private void initCharacterArtDictionary()
    {
        for (int i  = 0; i < patronArtToLoad.Length; i++)
        {
            patronArt.Add(patronArtToLoad[i].name, patronArtToLoad[i].sprite);
        }
    }
}
