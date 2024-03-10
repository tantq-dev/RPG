using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttack = 1f;

        [SerializeField] private float weaponDamge = 5f;

        private Transform target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.transform.GetComponent<Health>().IsDead()) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health targetHeath = target.GetComponent<Health>();
            return targetHeath != null && (!targetHeath.IsDead());
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target);
            if (timeSinceLastAttack >= timeBetweenAttack)
            {

                timeSinceLastAttack = 0;
                ResetAttack();
            }
        }

        private void ResetAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private bool IsInRange()
        {

            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(GameObject target)
        {

            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.transform;
        }

        public void Cancel()
        {
            StopAttack();
            this.target = null;

        }

        //Animation event
        public void Hit()
        {
            if (target == null) return;
            this.target.GetComponent<Health>().TakeDamage(weaponDamge);
        }
    }

}