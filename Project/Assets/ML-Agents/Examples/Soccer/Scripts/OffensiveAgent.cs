using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class OffensiveAgent: AgentSoccer
{
    public Transform ball;
    public Rigidbody ballRb;
    public Transform teammate;
    public Transform opponentGoal; 
    public Transform opponentAgent;
    public float fieldLength = 40f;

    private int stepsSinceLastTouch = 0;
    private Rigidbody agentRb;
    private Rigidbody opponentRb;

    private const float MOVEMENT_MULTIPLIER = 10f; // scales the resultant force for singnificant output

    public override void Initialize()
    {
        base.Initialize();
        if (ball != null && ballRb) ballRb = ball.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        stepsSinceLastTouch = 0;
    }

    //
    public override void CollectObservations(VectorSensor sensor)
    {
        if (ball == null || teammate == null) return;

        sensor.AddObservation(Vector3.Distance(transform.position, ball.position));
        sensor.AddObservation(transform.InverseTransformPoint(teammate.position));

        float fieldHalf = fieldLength / 2f;

        //ball distance
        float distanceToBall = Vector3.Distance(transform.position, ball.position);
        sensor.AddObservation(distanceToBall); 

        // teammate relative position (calculated with vector difference) 
        sensor.AddObservation(transform.InverseTransformPoint(teammate.position)); 

        // goal angle (opponent)
        Vector3 oppGoalDirection = (opponentGoal.position - transform.position).normalized;
        sensor.AddObservation(transform.InverseTransformDirection(oppGoalDirection));

        // 4. Normalized X-Position
        // Normalized position from -1 (own side) to +1 (opponent side).
        float normalizedZ = transform.localPosition.z / fieldHalf;
        sensor.AddObservation(normalizedZ); 

        //opponent position 
        if (opponentAgent != null && opponentRb != null)
        {
            // nearest opponent distance (records only one so adequate for both games)
            sensor.AddObservation(Vector3.Distance(transform.position, opponentAgent.position));

            // opponent relative velocity
            sensor.AddObservation(transform.InverseTransformDirection(opponentRb.velocity));
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(Vector3.zero); 
        }

        //ball velocity 
        sensor.AddObservation(ballRb != null ? ballRb.velocity : Vector3.zero);

        //time since last touch 
        sensor.AddObservation(stepsSinceLastTouch);

    }

    public override void OnActionReceived(ActionBuffers actions){
        stepsSinceLastTouch++;

        if (position == Position.Goalie)
        {
            // Existential bonus for Goalies.
            AddReward(m_Existential);
        }
        else if (position == Position.Striker)
        {
            // Existential penalty for Strikers
            AddReward(-m_Existential);
        }

        MoveAgent(actionBuffers.DiscreteActions);

    }

        private void OnCollisionEnter(Collision collision){
        base.OnCollisionEnter(collision); // call parent collision for kick

        if(collision.gameObject.CompareTag("ball"))
        {
            stepsSinceLastTouch = 0;
        }
    }
        
    

}