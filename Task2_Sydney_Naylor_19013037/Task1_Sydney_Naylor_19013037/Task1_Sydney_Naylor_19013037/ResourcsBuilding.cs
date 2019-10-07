using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Sydney_Naylor_19013037
{
    class ResourceBuilding : Building // inherits from building  // this building will create resources
    {
        private ResourceType rType;
        private int rGenerated;
        private int rPerRound;
        private int rPool;
        

        public ResourceBuilding(int xB, int yB, string teamB) : base(xB, yB, 500, teamB, 'R')
        {
            rPerRound = GameEngine.random.Next(0, 4);
            rGenerated = 0;
            rPool = GameEngine.random.Next(100, 200);
            rType = (ResourceType)GameEngine.random.Next(0, 4);
        }
        public ResourceBuilding(string values)
        {
            string[] parameters = values.Split(',');

            xB = int.Parse(parameters[1]);
            yB = int.Parse(parameters[2]);
            healthB = int.Parse(parameters[3]);
            maxHealthB = int.Parse(parameters[4]);
            rType = (ResourceType)int.Parse(parameters[5]);
            rPerRound = int.Parse(parameters[6]);
            rGenerated = int.Parse(parameters[7]);
            rPool = int.Parse(parameters[8]);
            teamB = parameters[9];
            imageB = parameters[10][10];
            destroyedB = parameters[11] == "True" ? true : false;
        }
        public override void Destruction()  // for when the building gets destroyed
        {
            destroyedB = true;
            imageB = '_';
        }

        public override string Save()
        {
            return string.Format(
             $"Resource, {xB}, {yB}, {healthB}, {maxHealthB}, {(int)rType}," +
             $"{rPerRound}, {rGenerated}, {rPool}," +
             $"{teamB}, {imageB}, {destroyedB}"
             );
        }
        public void GenerateResources()
        {
            if (destroyedB)
            {
                return;
            }
            if (rPool > 0)
            {
                int resourcesGenerated = Math.Min(rPool, rPerRound);
                rGenerated += resourcesGenerated;
                rPool -= resourcesGenerated;
            }
        }
        private string GetResourceName()
        {
            return new string[] { "Wood", "Food", "Rock", "Gold" }[(int)type];
        }
        public override string ToString()
        {
            return
            "----------------------------------------------" + Environment.NewLine +
            "Resource Building (" + imageB + "/" + teamB[0] + ")" + Environment.NewLine +
            "----------------------------------------------" + Environment.NewLine +
            GetResourceName() + ": " + rGenerated + Environment.NewLine +
            "Pool: " + rPool + Environment.NewLine +
            base.ToString() + Environment.NewLine;
        }

        //public override int XB
        //{
        //    get { return xB; }
        //    set { xB = value; }
        //}
        //public override int YB
        //{
        //    get { return yB; }
        //    set { yB = value; }
        //}
        //public override int HealthB
        //{
        //    get { return healthB; }
        //    set { healthB = value; }
        //}
        //public override int MaxHealthB
        //{
        //    get { return maxHealthB; }
        //}
        //public override string TeamB
        //{
        //    get { return teamB; }
        //}
        //public override char ImageB
        //{
        //    get { return imageB; }
        //}
        //public override bool Destroyed
        //{
        //    get { return destroyedB; }
        //}
    }
    public enum ResourceType
    {
        WOOD,
        FOOD,
        ROCK,
        GOLD
    }
}
