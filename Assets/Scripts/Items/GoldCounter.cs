using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour {

    public Sprite[] numberSprites;
    public Image[] numberPlaces;   

   public void separateDigits(int valueToSeparate)
    {
        int numbersPlaceToGet = 1;
        for (int i = 0; ; i++)
        {
            
            if (numbersPlaceToGet > valueToSeparate) { break; }

            numberPlaces[i].sprite = numberSprites[valueToSeparate / numbersPlaceToGet % 10];

            numbersPlaceToGet *= 10;
        }
    }
}
