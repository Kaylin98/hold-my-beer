using UnityEngine;

public class Beer : Pickup
{
    [SerializeField] float SpeedAmount = 3f;
    LevelGenerator levelGenerator;

    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void OnPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(SpeedAmount);
    }
}
