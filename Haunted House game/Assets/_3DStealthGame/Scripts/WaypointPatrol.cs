using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 5.0f;
    public Transform[] waypoints;

    private Rigidbody m_RigidBody;
    int m_CurrentWaypointIndex;

    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Transform currentWaypoint = waypoints[m_CurrentWaypointIndex];
        Vector3 currentToTarget = currentWaypoint.position - m_RigidBody.position;

        if (currentToTarget.magnitude < 0.1f)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            currentWaypoint = waypoints[m_CurrentWaypointIndex];
            currentToTarget = currentWaypoint.position - m_RigidBody.position;
        }
        Quaternion targetRotation = Quaternion.LookRotation(currentToTarget);
        Quaternion newRotation = Quaternion.Slerp(m_RigidBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        m_RigidBody.MoveRotation(newRotation);
        m_RigidBody.MovePosition(m_RigidBody.position + m_RigidBody.transform.forward * moveSpeed * Time.deltaTime);
    }
}