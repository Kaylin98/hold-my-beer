using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunks = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 5f;

    GameObject[] chunks = new GameObject[12];
    
    void Start()
    {
        SpawnChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    private void SpawnChunks()
    {
        for (int i = 0; i < startingChunks; i++)
        {
            Vector3 chunkPosition = new Vector3(transform.position.x, transform.position.y, i * chunkLength);
            GameObject newChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
            chunks[i] = newChunk;
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            if (chunks[i] != null)
            {
                chunks[i].transform.Translate(Vector3.back * (chunkMoveSpeed * Time.deltaTime));
            }
        }
    }
}
