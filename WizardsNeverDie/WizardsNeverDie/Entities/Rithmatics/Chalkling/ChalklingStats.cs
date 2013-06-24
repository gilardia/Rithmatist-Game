using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rithmatist.Entities.Rithmatics.Chalkling
{
    class ChalklingStats
    {
        //Poor stats are ~7, Good stats are ~ 10

        float defense;          //Effective against other chalklings, ignored by lines of vigor
        float currentHealth;    //Effective against lines of Vigor
        float damage;           //Effective against other chalklings
        float erase;            //Effective against Rithmatics
        float speed;

        public float Defense
        {
            get { return defense; }
        }
        public float Health
        {
            get { return currentHealth; }
        }
        public float Damage
        {
            get { return damage; }
        }
        public float Erase
        {
            get { return erase; }
        }
        public float Speed
        {
            get { return speed; }
        }
        public void onLineOfVigorHit(float damage)
        {
            currentHealth = Math.Max(0, currentHealth - damage);
        }
        public void onLineOfMakingHit(float damage)
        {
            damage = Math.Max(0, damage - defense / 2);
            currentHealth = Math.Max(0, currentHealth - damage);
        }
        public bool isDead()
        {
            return currentHealth != 0;
        }
    }
}
