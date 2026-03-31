using UnityEngine;

public class Beer : Pickup
{
    protected override void OnPickUp()
    {
        Debug.Log("Player is now drunk! Movement speed reduced for 10 seconds.");
    }
}
