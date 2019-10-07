using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Sydney_Naylor_19013037
{
    abstract class Unit  // abstract class
    {
        // protected declarations 

        protected int xPosition;
        protected int yPosition;
        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int attack;
        protected int attackRange;
        protected string team;
        protected char image;
        protected bool attacking = false;
        protected bool destroyed = false;
        protected string name;

        public static Random random = new Random();


        // constructor that recieves parameters for every class variable excluding maxHealth
        public Unit(int xPosition, int yPosition, int health, int speed, int attack, int attackRange, string team, char image)  
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.health = health;
            maxHealth = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.team = team;
            this.image = image;
        }


        // abstract methods within class
        public abstract void Move(Unit closestUnit);
        public abstract void Attack(Unit otherUnit);
        public abstract bool InAttackRange(Unit otherUnit);
        public abstract void Death();
        public abstract void RunAway();
        public abstract Unit GetClosestUnit(Unit[] units);


        public abstract string Save();


        public override string ToString()
        {
            return ("\nPosition: " + xPosition + ", " + yPosition +
                "\nHealth: " + health +
                /*"\nMax Health: " + maxHealth +*/
                "\nSpeed: " + speed +
                 /*"\nAttack: " + attack + 
                  * "\nAttack Range: " + attackRange +*/
                 "\nTeam: " + team +
                 "\nImage: " + image +"\n"
                 /*+ "\nAttacking: " + attack*/ +
                 "Name: " + name);
        }

        public double GetDistance(Unit otherUnit)
        {
            double xDistance = otherUnit.xPos - xPos;
            double yDistance = otherUnit.yPos - yPos;
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }


        public abstract int xPos 
        {
            get; set;
        }
        public abstract int yPos 
        {
            get; set;
        }
        public abstract int Health 
        {
            get; set;
        }
        public abstract string Team
        {
            get;
        }
        public abstract char Image
        {
            get;
        }
        public abstract bool Destroyed
        {
            get;
        }
        public abstract int MaxHealth
        {
            get;
        }


        //public abstract int Speed
        //{
        //    get; set;
        //}
        //public abstract int Attack
        //{
        //    get; set;
        //}
        //public abstract int AttackRange
        //{
        //    get; set;
        //}
        //public abstract bool Attacking
        //{
        //    get; set;
        //}
        //public abstract int Name
        //{
        //    get;
        //}
    }
}
