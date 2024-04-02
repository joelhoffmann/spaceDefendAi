using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    public float maxHealth = 100f;
    public float currentHealth;
    public float timeUnitlDeath = 5f;

    public int damage = 10;

    public Slider healthSlider;

     [SerializeField] private Transform baseTransform;

    void Start()
    {
        currentHealth = maxHealth;      

        // Initialisieren Sie den Slider, wenn er zugewiesen ist
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        // Starten Sie den Timer zum automatischen Zerst�ren des Feindes
        Invoke("Die", timeUnitlDeath);

        // Starten Sie den Timer zum Senken des Sliders
        InvokeRepeating("LowerHealth", 0f, timeUnitlDeath / maxHealth); // 3 Sekunden geteilt durch die maximale Gesundheit, um die Geschwindigkeit des Slider-R�ckgangs zu berechnen
    }

    void Update()
    {
        MoveTowardsCenter();
    }

    void MoveTowardsCenter()
    {
        // Bewegen Sie den Feind zur Mitte
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, moveSpeed * Time.deltaTime);

        // Drehen Sie den Feind zur Mitte
        Vector3 directionToCenter = Vector3.zero - transform.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        // �berpr�fen Sie, ob die Kollision mit einer Wand erfolgt
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Wenn der Feind eine Wand ber�hrt, soll keine Aktion ausgef�hrt werden
            // Sie k�nnen hier optional zus�tzliche Aktionen hinzuf�gen
            Debug.Log("Wall Hit");
        }    

        
     if (collision.gameObject.name == "Bomb")
    {  Debug.Log("Bomb Hit"); 
    // Partice effect
    //  GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //  Destroy(explosion, 1f);
    //  Destroy(gameObject);
    //  Destroy(collision.gameObject);   
                
       Destroy(gameObject);
    }        
    
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Trigger " + collision.gameObject.tag + " " + collision.gameObject.name);     
        
        if (collision.gameObject.tag == "Base")

    {   Debug.Log("Base Hit");   
        Player.instance.TakeDamage(damage);
            Die();
        }    

    if (collision.gameObject.tag == "Shield")
    {   Debug.Log("Shield Hit");   
        Player.instance.TakeShieldDamage(damage);
            Die();
    }    

  //  if (collision.gameObject.tag == "Wall")
  //  {  Debug.Log("Wall Hit");             
      // Destroy(gameObject); // Hier halt Ki zeug das der enemy woanders lang geht
   // }      
   
    }    

    void LowerHealth()
    {
        // Reduzieren Sie die aktuelle Gesundheit des Feindes
        currentHealth -= 1f;

        // Aktualisieren Sie den Slider, wenn er zugewiesen ist
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // �berpr�fen Sie, ob der Feind keine Gesundheit mehr hat
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Informieren Sie den RoundManager, dass dieser Feind gestorben ist
        RoundManager.Instance.DecreaseEnemyCount(gameObject);
        
        Player.instance.ReceiveCoins(100);

        // Fügen Sie hier die Logik hinzu, die ausgeführt wird, wenn der Feind stirbt
        Destroy(gameObject);
    }

}
