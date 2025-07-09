using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseSensitivity = 100f;
    public Transform playerBody; // L'oggetto che rappresenta il corpo del giocatore (es. il GameObject principale)
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Blocca il cursore al centro dello schermo
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Invertito per far salire quando si muove il mouse verso l'alto
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotazione verticale

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Ruota solo la camera sull'asse X
        playerBody.Rotate(Vector3.up * mouseX); // Ruota il corpo del giocatore orizzontalmente
    }
}