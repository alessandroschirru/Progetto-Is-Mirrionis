using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WalkingSoundTest : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float minVelocity = 0.1f;

    private PlayerMovement playerMovement;
    private AudioSource footstepAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footstepAudio = GetComponent<AudioSource>();

        if(controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
        
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement == null) return;

        Vector2 moveInput = playerMovement.moveInput;
        bool isGrounded = controller.isGrounded;
        bool isMoving = moveInput.magnitude > minVelocity && isGrounded;

        Debug.Log($"Grounded: {isGrounded}, IsMoving: {isMoving}");

        if (isMoving)
        {
            if (!footstepAudio.isPlaying)
            {
                Debug.Log("START audio");
                footstepAudio.Play();
            }

            else
            {
                if (!footstepAudio.isPlaying)
                {
                    Debug.Log("STOP audio");
                    footstepAudio.Stop();
                }
            }
        }
    }
}
