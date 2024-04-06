using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;
//using Unity.Sentis.Layers;

public class EnemyAgent : Agent
{
    Rigidbody2D rBody;

    public EnemyVision vision;
    public EnemyAntenna antenna;
    public float rotateSpeed = 5;
    public float targetRadius = 0.5f;
    public float moveSpeed = 50;
    public float maxHealth = 100f;
    public float currentHealth;
    public float timeUnitlDeath = 50f;
    public int damage = 10;
    public int collisionType = 0;
    public Slider healthSlider;
    private bool isBeingPulled = false; // Überprüft, ob der Feind gerade zum Magneten gezogen wird
    private Vector3 magnetPosition; // Die Position des Magneten, zu dem der Feind gezogen wird
    private float pullDuration = 5f; // Die Dauer, für die der Feind zum Magneten gezogen wird
    private float pullTimer = 0f; // Ein Timer, um die Dauer des Ziehens zu verfolgen
    public GameObject baseTarget;
    private Transform target;
    public float spawnRadius = 10f;
    private bool enemyDead = false;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
       

        currentHealth = maxHealth;

        // Initialisieren Sie den Slider, wenn er zugewiesen ist
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        // Starten Sie den Timer zum Senken des Sliders
        InvokeRepeating("LowerHealth", 0f, timeUnitlDeath / maxHealth); // 3 Sekunden geteilt durch die maximale Gesundheit, um die Geschwindigkeit des Slider-R�ckgangs zu berechnen

    }

    public void Awake()
    {
        target = baseTarget.transform;
    }

    public override void OnEpisodeBegin()
    {
        // reset the velocity
        rBody.velocity = Vector2.zero;
        rBody.angularVelocity = 0;

        // spawn the agent at a random position in a circle around the target
        float angle = Random.Range(0.0f, 2 * Mathf.PI);
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * spawnRadius, Mathf.Sin(angle) * spawnRadius, 0.0f);
        transform.position = target.position + newPos;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // position relative to target
        sensor.AddObservation((Vector2)(target.position - transform.position));

        // angle to target
        sensor.AddObservation(Vector2.SignedAngle(transform.up, target.position - transform.position));

        // velocity and rotation
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);
        sensor.AddObservation(rBody.angularVelocity);
        sensor.AddObservation(transform.rotation.z);

        // number of objects in view
        sensor.AddObservation(vision.GetAiInput());
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //add force forward
        rBody.AddForce(transform.up * actions.ContinuousActions[1] * moveSpeed * Time.deltaTime);
        //rotate
        rBody.AddTorque(-actions.ContinuousActions[0] * rotateSpeed * Time.deltaTime);

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < targetRadius) // radius of target plus radius of agent
        {
            SetReward(1.0f - StepCount / 1000.0f);
            End();
        }

        else if (distanceToTarget > 2.5f)
        {
            SetReward(-1.0f);
            End();
        }

        // took too long
        if (StepCount > 1000)
        {
            Debug.Log("Enemy Dead");
            SetReward(-1.0f * distanceToTarget / spawnRadius);
            End();
        }

        if (isBeingPulled)
        {
            Vector3 direction = magnetPosition - transform.position;
            rBody.AddForce(direction.normalized * moveSpeed * Time.deltaTime * 50);

            // Überprüfen, ob die Ziehzeit abgelaufen ist
            if (pullTimer >= pullDuration)
            {
                isBeingPulled = false;
                pullTimer = 0f;
            }
            else
            {
                pullTimer += Time.deltaTime;
            }
        }
    }

    public void End()
    {
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
//        collisionType = antenna.GetCollision();
        Debug.Log("Collision Type: " + collisionType);

        if (collisionType == 1)
        {
            Player.instance.TakeDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(gameObject);
            collisionType = 0;
            End();
        }
        else if (collisionType == 2)
        {
            Player.instance.TakeShieldDamage(damage);
            RoundManager.Instance.DecreaseEnemyCount(gameObject);
            collisionType = 0;
            End();
        }
        else if (collisionType == 3)
        {
            RoundManager.Instance.DecreaseEnemyCount(gameObject);
            collisionType = 0;
            End();
        }
        else if (collisionType == 4)
        {
            Debug.Log("EMP Hit in Enemy");
            moveSpeed = 0f;
            rotateSpeed = 0f;
            Invoke("ResetSpeed", 3f);
            collisionType = 0;
        }
        else if (collisionType == 5)
        {
            Debug.Log("Magnet Hit in Enemy");
            isBeingPulled = true;
            magnetPosition = collision.transform.position;
            collisionType = 0;
        }
        else
        {
            Debug.Log("No Collision Detected with Antenna");
            collisionType = 0;
        }
    }
    void ResetSpeed()
    {
        moveSpeed = 50;
        rotateSpeed = 5;
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
            Player.instance.ReceiveCoins(100);
            enemyDead = true;
            End();
        }
    }

}
