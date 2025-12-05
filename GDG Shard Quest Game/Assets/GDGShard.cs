using UnityEngine;

public class GDGShard : MonoBehaviour
{
    [Header("Shard Settings")]
    public GDGShardType shardType;
    public GDGShardState shardState = GDGShardState.Inactive;

    [Header("Visual Settings")]
    public float rotationSpeed = 20f;


    public void ActivateShard()
    {
        if (shardState == GDGShardState.Inactive)
        {
            shardState = GDGShardState.Active;
            
        }
    }
    public void CollectShard()
    {
        if (shardState == GDGShardState.Active)
        {
            shardState = GDGShardState.Collected;

        }
    }

    private void Update()
    {
        if(shardState == GDGShardState.Inactive)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }
        else if(shardState == GDGShardState.Active)
        {
            Debug.Log("Shard is active and ready to be collected!");
        }
        else if(shardState == GDGShardState.Collected)
        {
            Debug.Log("Shard has been collected!");
            Destroy(gameObject);
        }
    }


}

public enum GDGShardType
{
    Red,
    Blue,
    Green,
    Yellow
}

public enum GDGShardState
{
    Inactive,
    Active,
    Collected
}

