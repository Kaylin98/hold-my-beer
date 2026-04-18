using UnityEngine;

public class Wine : Pickup
{
    MagnetController magnet;

    public void Init(MagnetController magnet)
    {
        this.magnet = magnet;
    }

    protected override void OnPickUp()
    {
        if (magnet != null)
        {
            magnet.ActivateMagnet();
        }
    }
}