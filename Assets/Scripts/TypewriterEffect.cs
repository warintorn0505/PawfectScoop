using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.03f;

    private Coroutine typingCoroutine;
    private string currentText;

    public void StartTyping(string newText)
    {
        currentText = newText;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        if (textUI == null)
        {
            Debug.LogWarning("Text UI is not assigned!");
            yield break;
        }

        textUI.text = "";

        foreach (char letter in currentText)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}