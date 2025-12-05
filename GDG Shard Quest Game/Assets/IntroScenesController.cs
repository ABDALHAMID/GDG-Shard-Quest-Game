using UnityEngine;

public class IntroScenesController : MonoBehaviour
{


    [Header("player Settings")]
    public GameObject player;
    public Vector3 playerStartPosition;
    public Vector3 playerEndPositionOnTheHallway;

    [Header("hallway door Settings")]
    public GameObject hallwayDoor;


    public bool playerInTheHallway = false;


    private void Start()
    {
        player.SetActive(false);
        StartIntroScene("HallwayIntroScene");
    }

    public void StartIntroScene(string sceneName)
    {
        Debug.Log($"Starting intro scene: {sceneName}");
        player.SetActive(true);
        player.transform.position = playerStartPosition;
        playerInTheHallway = true;
    }

    private void Update()
    {
        if (playerInTheHallway)
            hallwaySction();
    }

    private void hallwaySction()
    {
        player.transform.Translate(playerEndPositionOnTheHallway * Time.deltaTime );
        if (player.transform.position.z <= playerEndPositionOnTheHallway.z)
        {
            playerInTheHallway = false;
            Debug.Log("Player has reached the end of the hallway.");
        }
    }
}
