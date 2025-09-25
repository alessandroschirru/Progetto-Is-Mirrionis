using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LeverSwitch : MonoBehaviour
{
    [Header("Riferimenti")]
    [Tooltip("Il transform della MANIGLIA che ruota (il cubo inclinato). "
           + "Metti il suo pivot dove vuoi l'asse di rotazione.")]
    public Transform handle;

    [Header("Rotazione OFF  ON (local euler angles)")]
    public Vector3 offLocalEuler = new Vector3(0, 0, 0);
    public Vector3 onLocalEuler = new Vector3(-45, 0, 0);

    [Header("Velocità e blocchi")]
    [Tooltip("Gradi al secondo circa (movimento smooth).")]
    public float rotateSpeed = 180f;

    [Tooltip("Stato iniziale")]
    public bool isOn = false;

    [Header("Events (opzionali)")]
    public UnityEvent OnTurnOn;
    public UnityEvent OnTurnOff;

    bool isAnimating;

    void Reset()
    {
        // Prova ad auto-trovare una maniglia figlia
        if (handle == null && transform.childCount > 0)
            handle = transform.GetChild(0);
    }

    public void Toggle()
    {
        if (isAnimating || handle == null) return;
        StopAllCoroutines();
        StartCoroutine(RotateTo(!isOn));
    }

    IEnumerator RotateTo(bool targetOn)
    {
        isAnimating = true;

        Quaternion start = handle.localRotation;
        Quaternion end = Quaternion.Euler(targetOn ? onLocalEuler : offLocalEuler);

        // durata in funzione della distanza angolare e della rotateSpeed
        float angle = Quaternion.Angle(start, end);
        float duration = Mathf.Max(0.01f, angle / Mathf.Max(1f, rotateSpeed));

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            handle.localRotation = Quaternion.Slerp(start, end, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        handle.localRotation = end;
        isOn = targetOn;

        if (isOn) OnTurnOn?.Invoke();
        else OnTurnOff?.Invoke();

        isAnimating = false;
    }
}