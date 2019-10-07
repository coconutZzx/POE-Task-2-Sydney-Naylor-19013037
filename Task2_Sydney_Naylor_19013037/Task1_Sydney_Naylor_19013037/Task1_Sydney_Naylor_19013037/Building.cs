using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Sydney_Naylor_19013037
{
    abstract class Building
    {
        // protected fields

        protected int xB;
        protected int yB;
        protected int healthB;
        protected int maxHealthB;
        protected string teamB;
        protected char imageB;
        protected bool destroyedB = false;

        // abstract methods
        public abstract void Destruction();
        public abstract string Save();
        public override string ToString()
        {
            return ("\nPosition: " + xB + ", " + yB +
                "\nBuilding Health: " + healthB +
                "\nImage: " + imageB);
        }

        // constructor
        public Building(int xB, int yB, int healthB, string teamB, char imageB )
        {
            this.xB = xB;
            this.yB = yB;
            this.healthB = healthB;
            this.maxHealthB = healthB;
            this.teamB = teamB;
            this.imageB = imageB;
        }

        public Building() { }

        public int XB
        {
            get { return xB; }
        }
        public int YB
        {
            get { return yB; }
        }
        public string TeamB
        {
            get { return teamB; }
        }
        public char ImageB
        {
            get { return imageB; }
        }
        //public abstract int XB
        //{
        //    get; set;
        //}
        //public abstract int YB
        //{
        //    get; set;
        //}
        //public abstract int HealthB
        //{
        //    get; set;
        //}
        //public abstract int MaxHealthB
        //{
        //    get;
        //}
        //public abstract string TeamB
        //{
        //    get;
        //}
        //public abstract char ImageB
        //{
        //    get;
        //}
        //public abstract bool Destroyed
        //{
        //    get;
        //}
    }
}
