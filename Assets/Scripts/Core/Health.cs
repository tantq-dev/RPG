using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{

    public class Health : MonoBehaviour
    {
        bool isDead = false;
        [SerializeField] private float health = 10;

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(0, health - damage);

            if (health <= 0)
            {
                Die();

            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            this.GetComponent<Animator>().SetTrigger("die");
            this.GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
