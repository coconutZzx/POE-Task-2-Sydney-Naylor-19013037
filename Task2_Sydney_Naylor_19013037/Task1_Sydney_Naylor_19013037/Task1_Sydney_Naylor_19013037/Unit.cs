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
        public Unit(int xPosition, int yPosition, int health, int speed, int attack, int attackRange, string team, char image, string name)  
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
            this.name = name;
        }


        // abstract methods within class
        //public abstract void Move(Unit closestUnit);
        //public abstract void Attack(Unit otherUnit);
        //public abstract bool InAttackRange(Unit otherUnit);
        //public abstract void Death();
        //public abstract void RunAway();
        //public abstract Unit GetClosestUnit(Unit[] units);
        public virtual void Move(Unit closestUnit)
        {
            int xDirection = closestUnit.xPos - xPos;
            int yDirection = closestUnit.yPos - yPos;

            if (Math.Abs(xDirection) > Math.Abs(yDirection))
            {
                xPosition += Math.Sign(xDirection);
            }
            else
            {
                yPosition += Math.Sign(yDirection);
            }
        }
        public virtual void Attack(Unit otherUnit)
        {
            attacking = true;
            otherUnit.Health -= attack;

            if (otherUnit.Health <= 0)
            {
                otherUnit.Health = 0;
                otherUnit.Destroy();
            }
        }
        public virtual void RunAway()
        {
            int direction = random.Next(0, 4);
            if (direction == 0)
            {
                xPosition += 1;
            }
            else if (direction == 1)
            {
                xPosition -= 1;
            }
            else if (direction == 2)
            {
                yPosition += 1;
            }
            else
            {
                yPosition -= 1;
            }
        }
        public virtual bool InAttackRange(Unit otherUnit)
        {
            return GetDistance(otherUnit) <= attackRange;
        }
        public virtual Unit GetClosestUnit(Unit[] units)
        {
            double closestDistance = int.MaxValue;
            Unit closestUnit = null;

            foreach (Unit otherUnit in units)
            {
                if (otherUnit == this || otherUnit.Team == team || otherUnit.Destroyed)
                {
                    continue;
                }
                double distance = GetDistance(otherUnit);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestUnit = otherUnit;
                }
            }
            return closestUnit;
        }
        public virtual void Death()
        {
            destroyed = true;
            attacking = false;
            image = 'X';
        }

        public Unit(string values)
        {
            string[] parameters = values.Split(',');

            xPosition = int.Parse(parameters[1]);
            yPosition = int.Parse(parameters[2]);
            health = int.Parse(parameters[3]);
            maxHealth = int.Parse(parameters[4]);
            speed = int.Parse(parameters[5]);
            attack = int.Parse(parameters[6]);
            attackRange = int.Parse(parameters[7]);
            team = parameters[8];
            image = parameters[9][0];
            name = parameters[10];
            destroyed = parameters[11] == "True" ? true : false;
        }

        public abstract string Save();


        public override string ToString()
        {
            return
                "----------------------------------------------" + Environment.NewLine +
                 name + " (" + image + "/" + team[0] + ") " + Environment.NewLine +
                 "----------------------------------------------" + Environment.NewLine +
                 "Faction: " + team + Environment.NewLine +
                 "Position: " + xPosition + ", " + yPosition + Environment.NewLine +
                 "Health: " + health + " / " + maxHealth + Environment.NewLine;
            //return ("\nPosition: " + xPosition + ", " + yPosition +
            //    "\nHealth: " + health +
            //    /*"\nMax Health: " + maxHealth +*/
            //    "\nSpeed: " + speed +
            //     /*"\nAttack: " + attack + 
            //      * "\nAttack Range: " + attackRange +*/
            //     "\nTeam: " + team +
            //     "\nImage: " + image +"\n"
            //     /*+ "\nAttacking: " + attack*/ +
            //     "Name: " + name);
        }

        protected double GetDistance(Unit otherUnit)
        {
            double xDistance = otherUnit.xPos - xPos;
            double yDistance = otherUnit.yPos - yPos;
            return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }

        public virtual int xPos
        {
            get { return xPosition; }
            set { xPosition = value; }
        }
        public virtual int yPos
        {
            get { return yPosition; }
            set { yPosition = value; }
        }
        public virtual int Health
        {
            get { return health; }
            set { health = value; }
        }
        public virtual int MaxHealth
        {
            get { return maxHealth; }
        }
        public virtual string Team
        {
            get { return team; }
        }
        public virtual char Image
        {
            get { return image; }
        }
        public virtual bool Destroyed
        {
            get { return destroyed; }
        }
        public virtual string Name
        {
            get { return name; }
        }
        //public abstract int xPos 
        //{
        //    get; set;
        //}
        //public abstract int yPos 
        //{
        //    get; set;
        //}
        //public abstract int Health 
        //{
        //    get; set;
        //}
        //public abstract string Team
        //{
        //    get;
        //}
        //public abstract char Image
        //{
        //    get;
        //}
        //public abstract bool Destroyed
        //{
        //    get;
        //}
        //public abstract int MaxHealth
        //{
        //    get;
        //}


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
