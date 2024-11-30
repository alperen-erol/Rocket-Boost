using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStr = 10f;
    [SerializeField] float rotationStr = 10f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;


    AudioSource audioSource;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        HandleThrust();
        RotationHandle();
    }

    private void HandleThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStr * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
            }
            mainEngineParticles.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    private void RotationHandle()
    {
        rb.freezeRotation = true;
        float rotationInput = rotation.ReadValue<float>() * -1;
        if (rotationInput > 0)
        {
            transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationStr * rotationInput);
            if (!rightEngineParticles.isPlaying)
            {
                rightEngineParticles.Play();
            }
        }
        else if (rotationInput < 0)
        {
            transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationStr * rotationInput);
            if (!leftEngineParticles.isPlaying)
            {
                leftEngineParticles.Play();
            }
        }
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
        rb.freezeRotation = false;

    }
}
