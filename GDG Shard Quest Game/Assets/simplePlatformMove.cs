using UnityEngine;

public class simplePlatformMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPose;
    [SerializeField]
    private Vector3 endPose;
    public float speed = 1.0f;


    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        // Move platform between startPose and endPose in easy back and forth motion
        float time = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f;
        if (time < startPose.x) {
            time = startPose.x;
        }
        if (time > endPose.x) {
            time = endPose.x;
        }
        transform.position = Vector3.Lerp(startPose, endPose, time);

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject == player)
        {
            Debug.Log("Player on platform");
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited");
        if (other.gameObject == player)
        {
            Debug.Log("Player off platform");
            player.transform.parent = null;
        }
    }
}
