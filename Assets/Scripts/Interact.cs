using UnityEngine;

public class Interact : MonoBehaviour
{
    private AudioSource _footstepAudioSource;

    [SerializeField] public AudioClip FlowerPickupClip;
    [SerializeField] public AudioClip EnemyDiedClip;
    


    public int PickupAmount { get; set; }


    void Awake()
    {
        _footstepAudioSource = GetComponent<AudioSource>();
    }

    protected void PlayFlowerPickupClip()
    {
        _footstepAudioSource.Stop();
        _footstepAudioSource.clip = FlowerPickupClip;
        _footstepAudioSource.Play();
    }

    protected void PlayEnemyDiedClip()
    {
        _footstepAudioSource.Stop();
        _footstepAudioSource.clip = EnemyDiedClip;
        _footstepAudioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Flower")
        {
            Debug.Log("Flower picked up!");
            PlayFlowerPickupClip();
            PickupAmount += 1;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy died!");
            PlayEnemyDiedClip();
        }

        if (PickupAmount == 1)
        {
            this.GetComponent<PlayerController>().speed = 2.5f;
            this.GetComponent<PlayerController>().JumpHeight = 10f;
            this.GetComponent<FOV>().viewRadius = 5f;
        }
        else if (PickupAmount == 2)
        {

            this.GetComponent<PlayerController>().speed = 3f;
            this.GetComponent<PlayerController>().JumpHeight = 12f;
            this.GetComponent<FOV>().viewRadius = 7.5f;
        }
        Destroy(other.gameObject);
    }
}
