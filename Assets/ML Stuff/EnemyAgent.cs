using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.Sentis.Layers;

public class EnemyAgent : Agent
{
    Rigidbody2D rBody;

    public EnemyVision vision;
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    public Transform target;
    public float spawnRadius = 5.0f;
    public GameObject wall;
    public int wallCount = 3;
    List<GameObject> walls = new List<GameObject>();
    
    public float wallRadius = 4.0f;

    public override void OnEpisodeBegin()
    {
        //spawn walls randomly in a circle around the target
        float wallAngle;
        Vector3 wallNewPos;
        for(int i = 0; i < wallCount; i++)
        {
            wallAngle = Random.Range(0.0f, 2 * Mathf.PI);
            wallNewPos = new Vector3(Mathf.Cos(wallAngle) * wallRadius, Mathf.Sin(wallAngle) * wallRadius, 0.0f);
            walls.Add(Instantiate(wall, target.position + wallNewPos, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))));
        }
        

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

    public float speed = 10;
    public float rotateSpeed = 5;
    public float targetRadius = 0.5f;
    public override void OnActionReceived(ActionBuffers actions)
    {
        //add force forward
        rBody.AddForce(transform.up * actions.ContinuousActions[1] * speed * Time.deltaTime);
        //rotate
        rBody.AddTorque(-actions.ContinuousActions[0] * rotateSpeed * Time.deltaTime);

        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < targetRadius) // radius of target plus radius of agent
        {
            SetReward(1.0f - StepCount / 1000.0f);
            EndEpisode();
        }

        else if(distanceToTarget > 2.5f)
        {
            SetReward(-1.0f);
            EndEpisode();
        }

        // took too long
        if (StepCount > 1000)
        {
            SetReward(-1.0f * distanceToTarget/spawnRadius);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}
