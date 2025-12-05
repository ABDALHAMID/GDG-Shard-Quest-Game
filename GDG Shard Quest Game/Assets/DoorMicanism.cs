using UnityEngine;

public class DoorMicanism : MonoBehaviour
{
    private Vector3 initialPosition;
    public Vector3 openPositionOffset;
    public float openSpeed = 1.0f;
    public AudioSource audioSource;

    private bool isOpen = false;

    private void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector3 targetPosition = isOpen ? initialPosition + openPositionOffset : initialPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the door trigger area.");
            isOpen = true;
            if (audioSource != null)
                audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the door trigger area.");
            isOpen = false;
            if (audioSource != null)
                audioSource.Play();
        }
    }


}
