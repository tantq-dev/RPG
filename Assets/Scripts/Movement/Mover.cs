using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {

            UpdateAnimator();

        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }


        public void MoveTo(Vector3 point)
        {
            navMeshAgent.SetDestination(point);
            navMeshAgent.isStopped = false;
        }
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().CancelAttack();
            MoveTo(destination);
        }
    }
}


