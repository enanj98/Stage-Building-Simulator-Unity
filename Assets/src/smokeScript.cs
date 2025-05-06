using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public ParticleSystem smokeParticleSystem;

    private bool isSmokePlaying = false;
    private float elapsedTime = 0f;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S) && !this.isSmokePlaying)
        {
            // Activate the smoke particle system
            if (smokeParticleSystem != null)
            {
                smokeParticleSystem.Play();
                this.isSmokePlaying = true;
            }
            else
            {
                smokeParticleSystem.Pause();
                Debug.LogWarning("Smoke Particle System is not assigned!");
            }
        }
        if (isSmokePlaying)
        {
            this.elapsedTime += Time.deltaTime;
        }
        float totalDuration = smokeParticleSystem.duration + smokeParticleSystem.startLifetime;
        Debug.Log(this.elapsedTime);

        if(this.elapsedTime-1 >= totalDuration-1.5)
        {
            this.isSmokePlaying = false;
            this.elapsedTime = 0f;
            this.smokeParticleSystem.Stop();
        }
    }
    private void OnParticleSystemStopped()
    {
        // Call the method to notify the SmokeController that the smoke effect has finished playing
        this.SmokeEffectFinished();
    }

    public void SmokeEffectFinished()
    {
        this.isSmokePlaying = false;
    }
    public void Start()
    {
        this.isSmokePlaying = true;
        this.smokeParticleSystem = GetComponent<ParticleSystem>();
    }
}