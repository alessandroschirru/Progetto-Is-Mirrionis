using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private string correctCode = "1240";
    [SerializeField] private int maxDigits = 4;
    [SerializeField] private DoorHinge door;
    [SerializeField] private TrapdoorHinge trapdoor;

    [SerializeField] private AudioSource keyBuzzer;

    private string currentInput = "";

    public void PressNumber(string number)
    {
        if (currentInput.Length >= maxDigits) return;
        currentInput += number;
        UpdateDisplay();

        if (keyBuzzer != null)
        {
            keyBuzzer.PlayOneShot(keyBuzzer.clip);
        }
    }

    public void PressCancel()
    {
        currentInput = "";
        UpdateDisplay();

        if (keyBuzzer != null)
        {
            keyBuzzer.PlayOneShot(keyBuzzer.clip);
        }
    }

    public void PressEnter()
    {
        bool success = false;

        if (currentInput == correctCode)
        {
            if (door != null && door.isLocked)
            {
                door.Unlock();
                success = true;
            }

            if (trapdoor != null && trapdoor.isLocked)
            {
                trapdoor.Unlock();
                success = true;
                trapdoor.isOpen = true;
            }
        }

        if (success)
        {
            displayText.text = "OK";
        }
        else
        {
            displayText.text = "ERR";
            Invoke(nameof(ResetInput), 1.5f);
        }

        if (keyBuzzer != null)
        {
            keyBuzzer.PlayOneShot(keyBuzzer.clip);
        }
    }

    private void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    private void ResetInput()
    {
        currentInput = "";
        UpdateDisplay() ;
    }
}
