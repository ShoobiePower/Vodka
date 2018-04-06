using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TapSystem : MonoBehaviour {

    [SerializeField]
    Dispencer[] dispencersWeHaveEquipped = new Dispencer[4];

    [SerializeField]
    Button ServeDrinkButton;

    [SerializeField]
    Button RecycleDrinkButton;

    public void Start()
    {
        lockTapSystem();
    }
        

    public void lockTapSystem()
    {
        lockTapPulls();
        ServeDrinkButton.interactable = false;
        RecycleDrinkButton.interactable = false;
    }

    public void lockTapPulls()
    {
        for (int i = 0; i < dispencersWeHaveEquipped.Length; i++)
        {
            dispencersWeHaveEquipped[i].swapToDisabled();
        }
    }

    public void unlockTapSystem()
    {
        for (int i = 0; i < dispencersWeHaveEquipped.Length; i++)
        {
            dispencersWeHaveEquipped[i].swapToEnabled();
        }
       
    }

    public void unlockServeAndRecycleButtons()
    {
        ServeDrinkButton.interactable = true;
        RecycleDrinkButton.interactable = true;
    }

    public Ingredient useIngredent(byte slotToUse)
    {
        return new Ingredient(dispencersWeHaveEquipped[slotToUse].dispenceIngredent());
    }
}
