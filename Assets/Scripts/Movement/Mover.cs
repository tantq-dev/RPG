
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private float speed = 6f;
        private NavMeshAgent navMeshAgent;


        private Health health;
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();

        }
        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void MoveTo(Vector3 destination, float moveFraction)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.speed = speed * moveFraction;
            navMeshAgent.isStopped = false;
        }
        public void StartMoveAction(Vector3 destination, float moveFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, moveFraction);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}


