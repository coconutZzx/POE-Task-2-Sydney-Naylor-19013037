using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Sydney_Naylor_19013037
{
    class FactoryBuilding : Building // this building will create units
    {
        private FactoryType type;
        private int productionSpeed;
        private int spawnY;

        enum FactoryType
        {
            MELEE,
            RANGED
        }
        public FactoryBuilding(int xB, int yB, string teamB) : base(xB, yB, 350, "Factory", 'F')
        {

        }

        public override void Destruction()  // for when the building gets destroyed
        {
            destroyedB = true;
            imageB = 'X';
        }

        public override string Save()
        {
            string info = xB + "," + yB + "," + healthB + "," + teamB;
            return info;
        }
        public void SpawnUnits()
        {

        }

        public int ProductionSpeed
        {
            get { return productionSpeed; }
        }

        public override int XB
        {
            get { return xB; }
            set { xB = value; }
        }
        public override int YB
        {
            get { return yB; }
            set { yB = value; }
        }
        public override int HealthB
        {
            get { return healthB; }
            set { healthB = value; }
        }
        public override int MaxHealthB
        {
            get { return maxHealthB; }
        }
        public override string TeamB
        {
            get { return teamB; }
        }
        public override char ImageB
        {
            get { return imageB; }
        }
        public override bool Destroyed
        {
            get { return destroyedB; }
        }

    }
}
