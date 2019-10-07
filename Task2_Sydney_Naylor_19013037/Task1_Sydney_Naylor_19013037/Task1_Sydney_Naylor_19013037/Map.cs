using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace Task1_Sydney_Naylor_19013037
{
    class Map
    {
        //public int x = 20;
        //public int y = 20;

        //const int X = 20;
        //const int Y = 20;

        const int MAPSIZE = 20;
        Random random = new Random();
        int numberUnits;
        int numberBuildings;
        Building[] buildings;
        Unit[] units;
        string[,] map;
        string[] teams = { "Zombies", "Survivors" };

        //char[,] populate = new char[X, Y];  // string[,] populate = new string[20, 20];

        public Map(int numberUnits, int numberBuildings)
        {
            //this.units = new Unit[numberUnits];
            this.numberUnits = numberUnits;
            this.numberBuildings = numberBuildings;
            Reset();
        }
        public Unit[] Units
        {
            get
            {
                return units;
            }
        }
        public Building[] Buildings
        {
            get
            {
                return buildings;
            }
        }
        public int Size
        {
            get
            {
                return MAPSIZE;
            }
        }
        public string GetMapDisplay()
        {
            string mapString = "";
            for (int y = 0; y < MAPSIZE; y++)
            {
                for (int x = 0; x < MAPSIZE; x++)
                {
                    mapString += map[x, y];
                }
                mapString += "\n";
            }
            return mapString;
        }

        public void Reset()
        {
            map = new string[MAPSIZE, MAPSIZE];
            units = new Unit[numberUnits];
            buildings = new Building[numberBuildings];
            InitializeUnits();
            UpdateMap();
        }
        public void UpdateMap()
        {
            for (int y = 0; y < MAPSIZE; y++)
            {
                for (int x = 0; x < MAPSIZE; x++)
                {
                    map[x, y] = " . ";
                }
            }
            foreach (Unit unit in units)
            {
                map[unit.xPos, unit.yPos] = unit.Team[0] + "/" + unit.Image;
            }
            foreach (Building building in buildings)
            {
                map[building.XB, building.YB] = building.TeamB[0] + "/" + building.ImageB;
            }
        }

        private void InitializeUnits()
        {
            for (int i = 0; i < units.Length; i++)
            {
                int xPosition = random.Next(0, MAPSIZE);
                int yPosition = random.Next(0, MAPSIZE);
                int teamIndex = random.Next(0, 2);
                int unitType = random.Next(0, 2);

                while(map[xPosition,yPosition] != null)
                {
                    xPosition = random.Next(0, MAPSIZE);
                    yPosition = random.Next(0, MAPSIZE);
                }
                if (unitType == 0)
                {
                    units[i] = new MeleeUnit(xPosition, yPosition, teams[teamIndex]);
                }
                else
                {
                    units[i] = new RangedUnit(xPosition, yPosition, teams[teamIndex]);
                }
                map[xPosition, yPosition] = units[i].Team[0] + "/" + units[i].Image;
            }

            for (int i = 0; i < buildings.Length; i++)
            {
                int xB = random.Next(0, MAPSIZE);
                int yB = random.Next(0, MAPSIZE);
                int teamIndex = random.Next(0, 2);
                int buildingType = random.Next(0, 2);

                while (map[xB, yB] != null)
                {
                    xB = random.Next(0, MAPSIZE);
                    yB = random.Next(0, MAPSIZE);
                }
                if (buildingType == 0)
                {
                    buildings[i] = new ResourceBuilding(xB, yB, teams[teamIndex]);
                }
                else
                {
                    buildings[i] = new FactoryBuilding(xB, yB, teams[teamIndex]);
                }
                map[xB, yB] = buildings[i].TeamB[0] + "/" + units[i].Image;
            }
        }


        public void LoadUnits()
        {
            FileStream uFile = new FileStream("unitsInfo.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(uFile);

            string uLine = sr.ReadLine();
            string[] infoU = uLine.Split(',');

            //while (line != null)
            //{
            //    Array.Resize(ref units, units.Length - 1);
            //    if (info[3] == "M")
            //    {
            //        units[units.Length - 1] = new MeleeUnit();
            //    }
            //    else
            //    {
            //        units[units.Length - 1] = new RangedUnit();
            //    }
            //    units[units.Length - 1].xPos = Convert.ToInt32(info[0]);
            //    units[units.Length - 1].yPos = Convert.ToInt32(info[1]);
            //    units[units.Length - 1].Health = Convert.ToInt32(info[2]);
            //    units[units.Length - 1].Team = info[3]; 
            //}
        }
        public void LoadBuildings()
        {
            FileStream bFile = new FileStream("buidlingsInfo.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader srw = new StreamReader(bFile);

            string bLine = srw.ReadLine();
            string[] infoB = bLine.Split(',');


        }



        //public void GenerateBattlefield()
        //{
        //    // array to store things
        //    // for loop to generate units randomly 

        //    for(int i = 0; i < units.Length; i++)  // controls number of times the loop is executed, for loop will be executed as long as this condition is valid, increment 
        //    {
        //        int variable = random.Next(0, 2);

        //        // sequence of statements that will execute repeatedly 

        //        if (variable == 0)  // random.Next(0, 2) == 0
        //        {
        //            units[i] = new MeleeUnit();
        //        }
        //        else
        //        {
        //            units[i] = new RangedUnit();
        //        }
        //        units[i].xPos = random.Next(0, X);
        //        units[i].yPos = random.Next(0, Y);
        //    }
        //}

        //public void Populate()
        //{
        //    // 2d array of string and fill it with either nothing(blank spaces) or someother place holder
        //    // 2 for loops to manage x and y
        //    populate = new char[X, Y];

        //    for (int i = 0; i < units.Length; i++)
        //    {
        //        Unit unit = units[i];
        //        populate[unit.xPos, unit.yPos] = unit.Image;
        //    }

        //    for (int x = 0; x < 20; x++)
        //    {
        //        for(int y = 0; y < 20; y++)
        //        {
        //            if (populate[x, y] == '\0')
        //            {
        //                populate[x, y] = '_';
        //            }
        //        }
        //    }
        //}

        //public void Render(Label lbl)
        //{
        //    lbl.Text = "";
        //    for (int x = 0; x < 20; x++)
        //    {
        //        for (int y = 0; y < 20; y++)
        //        {
        //            lbl.Text = lbl.Text + populate[x, y];
        //        }
        //        lbl.Text += "\n";
        //    }
        //}
        ////public void Update(Label lbl)  // int x, int y, Unit u
        ////{
        ////    /*populate[u.xPos, u.yPos] = ',';
        ////    u.xPos = x;
        ////    u.yPos = y;
        ////    populate[u.xPos, u.yPos] = u.Image;*/
        ////    System.Diagnostics.Debug.WriteLine("Updating...");
        ////    lbl.Text = "";

        ////    for (int x = 0; x < X; x++)
        ////    {
        ////        for (int y = 0; y < Y; y++)
        ////        {
        ////            lbl.Text += populate[x, y];
        ////        }
        ////        lbl.Text += "\n";
        ////    }
        ////    System.Diagnostics.Debug.WriteLine("Units placed...");

        ////}
        //public string GetInfo()
        //{
        //    string info = "";
        //    for (int i = 0; i < units.Length; i++)
        //    {
        //        info += units[i].ToString() + "\n\n";
        //    }
        //    return info;
        //}
        ////public void Information (TextBox txtBox)
        ////{
        ////    for (int i = 0; i < numberUnits ; i++)
        ////    {
        ////        txtBox.Text += units[i].ToString();
        ////    }
        ////}
    }
}
