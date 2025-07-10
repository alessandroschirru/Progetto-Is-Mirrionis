using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WalkingSoundTest : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float minVelocity = 0.1f;
    private AudioSource footstepAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footstepAudio = GetComponent<AudioSource>();

        if(controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float speed = horizontalVelocity.magnitude;
        bool isGrounded = controller.isGrounded;
        bool isMoving = speed > minVelocity && isGrounded;

        Debug.Log($"Speed: {speed:F2}, Grounded: {isGrounded}, IsMoving: {isMoving}");

        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
            {
                Debug.Log("START audio");
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                Debug.Log("STOP audio");
                footstepAudio.Stop();
            }
        }
    }
}
