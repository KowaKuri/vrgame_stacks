using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BlockSpawner[] spawners;
    private int spawnerIndex;
    private BlockSpawner currentSpawner;

    private void Awake()
    {
        spawners = FindObjectsOfType<BlockSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MovingBlock.CurrentBlock != null)
            {
                MovingBlock.CurrentBlock.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.spawnBlock();
        }
    }
}
