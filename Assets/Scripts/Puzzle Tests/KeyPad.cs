using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private int maxDigits = 4;
    [SerializeField] private DoorHinge door;

    private string currentInput = "";

    public void PressNumber(string number)
    {
        if (currentInput.Length >= maxDigits) return;
        currentInput += number;
        UpdateDisplay();
    }

    public void PressCancel()
    {
        currentInput = "";
        UpdateDisplay();
    }

    public void PressEnter()
    {
        if (currentInput == correctCode && door != null && door.isLocked)
        {
            displayText.text = "OK";
            door.Unlock();
        }

        else
        {
            displayText.text = "ERR";
            Invoke(nameof(ResetInput), 1.5f);
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
