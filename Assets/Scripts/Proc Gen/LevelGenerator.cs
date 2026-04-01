using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform chunkParent;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [Tooltip("The number of chunks to spawn at the start of the game.")]
    [SerializeField] int startingChunks = 12;
    [Tooltip("The length of each chunk. This should match the length of the chunk prefab.")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

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
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);
            
            cameraController.ChangeCameraFOV(speedAmount);
        }

    }   
    

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunks; i++)
        {
            Vector3 chunkPosition = new Vector3(transform.position.x, transform.position.y, i * chunkLength);
            GameObject newChunkGO = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, chunkParent);
            chunks.Add(newChunkGO);

            Chunk newChunk = newChunkGO.GetComponent<Chunk>();
            newChunk.Init(this, scoreManager);
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(Vector3.back * (moveSpeed * Time.deltaTime));

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

        GameObject newChunkGO = Instantiate(chunkPrefab, newChunkPosition, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);

        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
        
        return i;
    }
}
