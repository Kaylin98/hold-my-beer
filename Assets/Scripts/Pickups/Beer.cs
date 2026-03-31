using UnityEngine;

public class Beer : Pickup
{
    [SerializeField] float SpeedAmount = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(SpeedAmount);
    }
}
