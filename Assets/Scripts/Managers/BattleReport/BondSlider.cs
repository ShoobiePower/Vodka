using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BondSlider : MonoBehaviour
{


    private Slider BondAmountSlider;

    private List<Check> checksToRedeemForBond = new List<Check>();

    private bool shouldSliderMove;

    private bool isSliderPaused;

    private float initialValue;

    private float ammountToMoveSliderBy;

    private const byte valueForMatchedBond = 5;

    private bool isPatronAtMaxLevel;
    public bool IsPatronAtMaxLevel { set { isPatronAtMaxLevel = value; } }

    [SerializeField]
    float HowLongIsPauseOnLevelUp;
    private float pauseCountDown;

    [SerializeField]
    float howLongIsBondGainLerp;

    [SerializeField]
    Text bondGainText;


    // load our list of checks to cash
    public void LoadListOfChecks(List<Check> checksToRedeemForBond)
    {
        //this.checksToRedeemForBond = checksToRedeemForBond;
        this.checksToRedeemForBond.Clear();
        foreach(Check c in checksToRedeemForBond)
        {
            this.checksToRedeemForBond.Add(c);
        }
    }

    // Set our default value IE how much bound did our patron start with
    // And how much bond is needed to level up
    public void SetSliderDefautValue(float currentBond, float bondNeededToNextLevel)
    {
        if (BondAmountSlider == null) { initBondAmountSlider(); }

        shouldSliderMove = false;
        BondAmountSlider.value = currentBond;
        initialValue = currentBond;
        BondAmountSlider.maxValue = bondNeededToNextLevel;
    }

    public void cashNextCheck()
    {
        if (checksToRedeemForBond.Count != 0)
        {
            determineIfPassOrFail(checksToRedeemForBond[0]);
            checksToRedeemForBond.RemoveAt(0);
        }

    }

    private void determineIfPassOrFail(Check checkToCash)
    {

        if (checkToCash.IsTrialPassed == true)
        {
            setAmmountToMoveSliderBy(valueForMatchedBond);
            bondGainText.text = valueForMatchedBond + " bond gained from " + checkToCash.skillToCheckFor.ToString().ToLower() + " skill match";
        }
        else
        {
            bondGainText.text = "Adventurer did not have the skill " + checkToCash.skillToCheckFor.ToString().ToLower() + " they did not gain any bond...";
        }
    }

    // Move the bond slider up this much;
    private void setAmmountToMoveSliderBy(float ammountToMoveSliderBy)
    {
        this.ammountToMoveSliderBy = ammountToMoveSliderBy;
        startAnimation();
    }

    private void startAnimation()
    {
        shouldSliderMove = true;
    }

    private void stopAnimation()
    {
        shouldSliderMove = false;
    }

    // The action that actually moves our guadge;
    private void Update()
    {

        if (isSliderPaused)
        {
            pauseBondGain();
        }

        else if (shouldSliderMove)
        {
            animateBondSlider();
        }
    }

    private void animateBondSlider()
    {
        BondAmountSlider.value += (ammountToMoveSliderBy / howLongIsBondGainLerp) * Time.deltaTime;

        if (BondAmountSlider.value >= BondAmountSlider.maxValue)
        {
            if (isPatronAtMaxLevel)  // Here
            {
                bondGainText.text = "Bond Has Reached Max Level!";
                checksToRedeemForBond.Clear();
            }
            else
            {
                pauseAnimation();
                ammountToMoveSliderBy -= (BondAmountSlider.maxValue - initialValue);
                initialValue = 0;
                BondAmountSlider.value = 0;
                bondGainText.text = "Bond Level Up!";
            }
        }

        if (BondAmountSlider.value >= initialValue + ammountToMoveSliderBy)
        {
            ammountToMoveSliderBy = 0;
            pauseAnimation();
        }
    }

    private void pauseAnimation()
    {
        shouldSliderMove = false;
        setPauseTimer();
    }

    private void setPauseTimer()
    {
        pauseCountDown = HowLongIsPauseOnLevelUp;
        isSliderPaused = true;
    }

    private void pauseBondGain()
    {
        pauseCountDown -= Time.deltaTime;
        if (pauseCountDown <= 0)
        {
            isSliderPaused = false;
            setUpNextGain();
        }
    }

    private void setUpNextGain()
    {
        if (ammountToMoveSliderBy > 0)
        {
            setAmmountToMoveSliderBy(ammountToMoveSliderBy);
        }
        else
        {
            cashNextCheck();
        }
    }

    private void initBondAmountSlider()
    {
        BondAmountSlider = this.GetComponent<Slider>();
    }

}
