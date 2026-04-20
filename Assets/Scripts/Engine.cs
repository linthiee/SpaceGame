using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] private ParticleSystem engineParticles;
    [SerializeField] private AudioSource engineAudio;

    [SerializeField] private float fadeSpeed = 5.0f; 
    [SerializeField] private float maxVolume = 0.5f; 

    private bool isRunning = false;

    private void Start()
    {
        if (engineAudio != null)
        {
            engineAudio.volume = 0f;
            engineAudio.loop = true; 
            engineAudio.Play();

            engineAudio.pitch = Random.Range(0.9f, 1.1f);
        }
    }

    public void Set(bool active)
    {
        if (active && !isRunning)
        {
            engineParticles.Play();
            isRunning = true;
        }
        else if (!active && isRunning)
        {
            engineParticles.Stop();
            isRunning = false;
        }
    }

    private void Update()
    {
        if (engineAudio != null)
        {
            float targetVolume = isRunning ? maxVolume : 0f;

            engineAudio.volume = Mathf.Lerp(engineAudio.volume, targetVolume, fadeSpeed * Time.deltaTime);
        }
    }
}