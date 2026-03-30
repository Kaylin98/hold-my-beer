using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunks = 12;
    [SerializeField] Transform chunkParent;

    [SerializeField] float chunkLength = 10f;
    
    void Start()
    {
        for (int i = 0; i < startingChunks; i++)
        {
            Vector3 chunkPosition = new Vector3(transform.position.x, transform.position.y, i * chunkLength);
            Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
        }
    }
}
