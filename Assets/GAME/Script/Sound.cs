using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    public AudioClip ClickSound;
    [Range(0f, 1f)]
    public float volume= 1f;

    AudioSource audioSource;

    // Update is called once per frame
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }
    public void PlayClickSound()
    {
        if (ClickSound == null) return;
        audioSource.PlayOneShot(ClickSound , volume);
    }

}
