using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunks = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float chunkMoveSpeed = 5f;
    [SerializeField] float minMoveSpeed = 3f;

    List<GameObject> chunks = new List<GameObject>();
    
    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        chunkMoveSpeed += speedAmount;

        if (chunkMoveSpeed < minMoveSpeed)
        {
            chunkMoveSpeed = minMoveSpeed;
        }

        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);
        cameraController.ChangeCameraFOV(speedAmount);
    }   
    

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunks; i++)
        {
            Vector3 chunkPosition = new Vector3(transform.position.x, transform.position.y, i * chunkLength);
            GameObject newChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
            chunks.Add(newChunk);
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(Vector3.back * (chunkMoveSpeed * Time.deltaTime));

            if (chunk.transform.position.z < -chunkLength)
            {
                i = ReplaceChunk(i, chunk);
            }
        }
    }

    int ReplaceChunk(int i, GameObject chunk)
    {
        Destroy(chunk);
        chunks.RemoveAt(i);
        i--; // Adjust the loop index since we removed an item

        // 1. Grab the chunk that is currently at the very end of the line
        GameObject lastChunk = chunks[chunks.Count - 1];

        // 2. Spawn the new chunk exactly 'chunkLength' behind that last chunk
        Vector3 newChunkPosition = new Vector3(
            transform.position.x, 
            transform.position.y, 
            lastChunk.transform.position.z + chunkLength
        );

        GameObject newChunk = Instantiate(chunkPrefab, newChunkPosition, Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
        
        return i;
    }
}
