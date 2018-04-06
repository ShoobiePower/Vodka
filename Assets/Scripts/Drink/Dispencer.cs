using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Dispencer : MonoBehaviour, IPurchaseable
{
    private bool isDispencerSold = false;
    public int priceForNewDispencer;


    [SerializeField]
    private Ingredient.ingredientColor ingredientToDispence;

    //[SerializeField] DispencerSlot slot;

    //[SerializeField] InputManager inputManager; //HAAAACCCKKKKK

    [SerializeField]
    Sprite activeTapArt;

    [SerializeField]
    Sprite deactivatedTapArt;

    public Ingredient.ingredientColor dispenceIngredent()
    {
        return ingredientToDispence;
    }

    
    //public void ClickDispenserButton()
    //{
    //    if(slot.drinkInSlot != null)
    //    {
    //        inputManager.DispencerButtonPressed((byte)ingredientToDispence, slot.drinkInSlot.DrinkInMug);
    //    }
    //}

    //public void purchaseItem()
    //{
    //    this.gameObject.SetActive(true);
    //}


    //public void setPrice(int newPrice)
    //{
    //    throw new NotImplementedException();
    //}

    //public int getPrice()
    //{
    //   return priceForNewDispencer;
    //}

    public Ingredient.ingredientColor getThisDispencersColor()
    {
        return ingredientToDispence;
    }

    public string getName()
    {
        return ingredientToDispence.ToString().ToLower() + " Dispencer";
    }

    public string getDescription()
    {
        return "A new tap that will unlock " + ingredientToDispence.ToString().ToLower() + " ingredients. You will be able to make drinks that feature the color" + ingredientToDispence.ToString().ToLower() + " in them.";
    }

    public byte getImageIdentifier()
    {
        return (byte)ingredientToDispence;
    }

    public bool isSoldOut()
    {
        return isDispencerSold;
    }

    public void setSoldOut()
    {
        isDispencerSold = true;
    }

    public void swapToDisabled()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<Image>().sprite = deactivatedTapArt;
    }

    public void swapToEnabled()
    {
        GetComponent<Button>().enabled = true;
        GetComponent<Image>().sprite = activeTapArt;
    }

    #region Unneeded Code?
    //[SerializeField] float dispenseSpeed = 1;
    //public enum DispenserState { START_DISPENSING, DISPENSING, STOP_DISPENSING} //I (Nathan B) may not need "Dispencing".  It's only for potential animations
    //public DispenserState currentDispenserState { get; private set; }

    //void Start()
    //{
    //    currentDispenserState = DispenserState.STOP_DISPENSING;
    //}

    //public void StartDispensing()   //Called when the player is holding the button
    //{
    //    if (slot.mugInSlot != null)
    //    {
    //        currentDispenserState = DispenserState.START_DISPENSING;
    //    }
    //}

    //public void StopDispensing() //Called when the player releases the button
    //{
    //    currentDispenserState = DispenserState.STOP_DISPENSING;
    //}

    //Possibly for animations, but no functionality is needed in update
    //void Update()
    //{
    //    switch(currentDispenserState)
    //    {
    //        case (DispenserState.START_DISPENSING):
    //            //start pouring animation
    //            //initial burst animation
    //            currentDispenserState = DispenserState.DISPENSING;
    //            break;
    //        case (DispenserState.DISPENSING):
    //            inputManager.DispencerButtonHold((byte)ingredientToDispence, dispenseSpeed * Time.deltaTime, slot.mugInSlot.DrinkInMug);
    //            break;
    //        case (DispenserState.STOP_DISPENSING):
    //            //end animations
    //            //stop dispensing
    //            break;
    //    }
    //}
    #endregion
}
