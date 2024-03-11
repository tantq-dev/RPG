using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 5f;

        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellingTime = 1f;



        private GameObject player;
        private Fighter fighter;
        private Health health;
        private Mover mover;

        private Vector3 guardPosition;
        int currentWaypointIndex = 0;

        private float lastTimeSawPlayer = Mathf.Infinity;
        private float timeSinceStartDwelling = Mathf.Infinity;
        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;

        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InRange() && fighter.CanAttack(player))
            {
                Attack();
                lastTimeSawPlayer = 0;

            }
            else if (lastTimeSawPlayer < suspicionTime)
            {
                SuspicionsBehaviour();
            }
            else
            {
                GaurdBehaviour();

            }

            UpdateTimers();

        }

        private void UpdateTimers()
        {
            lastTimeSawPlayer += Time.deltaTime;
            timeSinceStartDwelling += Time.deltaTime;
        }

        private void GaurdBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceStartDwelling = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceStartDwelling > dwellingTime)
            {
                mover.StartMoveAction(nextPosition);
            }


        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());

            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolPath.transform.childCount;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaitpoint(currentWaypointIndex);
        }

        private void SuspicionsBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private bool InRange()
        {
            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }

        private void Attack()
        {
            fighter.Attack(player);
        }




        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
        }

    }

}
