using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Sydney_Naylor_19013037
{
    class MeleeUnit : Unit   // inherits from unit
    {
        public MeleeUnit(int xPosition, int yPosition, string team) : base(xPosition, yPosition, 200, 1, 50, 1, team, 'Z', "Zombies")
        {

        }
        public MeleeUnit(string values) : base(values) { }
        public override string Save()
        {
            return string.Format(
            $"Melee, {xPosition}, {yPosition}, {health}, {maxHealth}, {speed}, {attack}, {attackRange}," +
            $"{team}, {image}, {name}, {destroyed}"
            );
        }

        public override void Move(Unit closestUnit)
        {
            attacking = false;
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

        public override void Attack(Unit otherUnit)
        {
            attacking = false;
            otherUnit.Health -= attack;

            if (otherUnit.Health <= 0)
            {
                otherUnit.Death();
            }
        }

        public override void Death()
        {
            destroyed = true;
            attacking = false;
            image = 'X';
        }

        public override bool InAttackRange(Unit otherUnit)
        {
            return GetDistance(otherUnit) <= attackRange;
        }

        public override Unit GetClosestUnit(Unit[] units)
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

        public override void RunAway()
        {
            attacking = false;
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

        public override int xPos
        {
            get { return xPosition; }
            set { xPosition = value; }
        }
        public override int yPos
        {
            get { return yPosition; }
            set { yPosition = value; }
        }
        public override int Health
        {
            get { return health; }
            set { health = value; }
        }
        public override int MaxHealth
        {
            get { return maxHealth; }
        }
        public override string Team
        {
            get { return team; }
        }
        public override char Image
        {
            get { return image; }
        }
        public override bool Destroyed
        {
            get { return destroyed; }
        }
    }
}
