using UnityEngine;

public class Coin : Pickup
{
    protected override void OnPickUp()
    {
        Debug.Log("Add 100 points to the player's score.");
    }
}
