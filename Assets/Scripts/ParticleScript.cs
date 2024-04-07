using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private AudioManager m_AudioManager;

    private void Awake()
    {
        m_AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<ParticleSystem>().Play();
            m_AudioManager.PlaySFX(m_AudioManager.baseHit);
            if (Player.instance.shieldHealth <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                m_AudioManager.PlaySFX(m_AudioManager.shieldDistruction);
            }
        }
    }

}