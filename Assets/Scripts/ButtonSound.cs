using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClick()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}