using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingBlock : MonoBehaviour
{
    public static MovingBlock CurrentBlock
    {
        get;
        private set;
    }

    public static MovingBlock LastBlock
    {
        get;
        private set;
    }

    public MoveDirection MoveDirection
    {
        get;
        set;
    }

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        if (LastBlock == null)
        {
            LastBlock = GameObject.Find("Base").GetComponent<MovingBlock>();
        }
        CurrentBlock = this;
        GetComponent<Renderer>().material.color = GetRandomColour();

        transform.localScale = new Vector3(LastBlock.transform.localScale.x, transform.localScale.y, LastBlock.transform.localScale.z);
    }

    // You can add a set of colours instead of random colours - 30:00
    private Color GetRandomColour()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.X ? LastBlock.transform.localScale.x : LastBlock.transform.localScale.z;
        if (Mathf.Abs(hangover) >= max)
        {
            LastBlock = null;
            CurrentBlock = null;
            SceneManager.LoadScene(0);
        }

        Debug.Log(LastBlock.transform.position.z);
        Debug.Log(hangover);

        float direction = hangover > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.X)
            SplitBlockOnX(hangover, direction);
        if (MoveDirection == MoveDirection.Z)
            SplitBlockOnZ(hangover, direction);

        LastBlock = this;
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.X)
            return transform.position.x - LastBlock.transform.position.x;
        else
            return transform.position.z - LastBlock.transform.position.z;
    }

    private void SplitBlockOnX(float hangover, float direction)
    {
        float newXSize = LastBlock.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize; // Doesn't work well in Vuforia

        float newXPosition = LastBlock.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float blockEdge = transform.position.x + (newXSize / 2f * direction); // Doesn't work well in Vuforia
        float fallingBlockXPosition = blockEdge + fallingBlockSize / 2f * direction; // Doesn't work well in Vuforia

        SpawnDropBlock(fallingBlockXPosition, fallingBlockSize); // Doesn't work well in Vuforia

    }

    private void SplitBlockOnZ(float hangover, float direction)
    {
        float newZSize = LastBlock.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize; // Doesn't work well in Vuforia

        float newZPosition = LastBlock.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float blockEdge = transform.position.z + (newZSize / 2f * direction); // Doesn't work well in Vuforia
        float fallingBlockZPosition = blockEdge + fallingBlockSize / 2f * direction; // Doesn't work well in Vuforia

        SpawnDropBlock(fallingBlockZPosition, fallingBlockSize); // Doesn't work well in Vuforia

    }

    private void SpawnDropBlock(float fallingBlockZPosition, float fallingBlockSize) // Doesn't work well in Vuforia
    {
        var block = GameObject.CreatePrimitive(PrimitiveType.Cube); // Doesn't work well in Vuforia

        if (MoveDirection == MoveDirection.X)
        {
            block.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z); // Doesn't work well in Vuforia
            block.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z); // Doesn't work well in Vuforia
        }

        if (MoveDirection == MoveDirection.Z)
        {
            block.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize); // Doesn't work well in Vuforia
            block.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition); // Doesn't work well in Vuforia
        }

        block.AddComponent<Rigidbody>(); // Doesn't work well in Vuforia
        block.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color; // I ended here!!! It's 31:05!!!
        Destroy(block.gameObject, 1f); // Doesn't work well in Vuforia
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
