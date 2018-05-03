using UnityEngine;
using System.Collections;

public abstract class StoreableItem  {

    private int worth;

    private int cost;

    private int numberLeft;
    public int NumberLeft { get { return numberLeft; } set { numberLeft = value; } }
     
	public StoreableItem()
    {

    } 

    public virtual void buy()
    {

    }

    public virtual void sell()
    {

    }

    public virtual string sayName()
    {
        return "ITEM HAS NULLNAME";
    }

    public virtual Sprite displayArt()
    {
        return null;
    }

        

}
