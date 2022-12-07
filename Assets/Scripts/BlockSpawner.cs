using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    public MovingBlock blockPrefab;
    //public BaseImage baseImage;
    [SerializeField]
    private MoveDirection moveDirection;

    public void spawnBlock()
    {
        var block = Instantiate(blockPrefab);
        //block.transform.parent = GameObject.Find("ImageTarget").transform;
        //block.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (MovingBlock.LastBlock != null && MovingBlock.LastBlock.gameObject != GameObject.Find("Base"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingBlock.LastBlock.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingBlock.LastBlock.transform.position.z;

            block.transform.position = new Vector3(x, MovingBlock.LastBlock.transform.position.y + blockPrefab.transform.localScale.y, z);
        }
        else
        {
           block.transform.position = transform.position;
        }

        block.MoveDirection = moveDirection;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, blockPrefab.transform.localScale);
    }
}

public enum MoveDirection
{
    X,
    Z
}
