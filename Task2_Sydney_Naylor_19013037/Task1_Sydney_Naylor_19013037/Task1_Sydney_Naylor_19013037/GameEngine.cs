using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1_Sydney_Naylor_19013037
{
    class GameEngine
    {
        public static Random random = new Random();

        const string UNITS_FILENAME = "units.txt";
        const string BUILDINGS_FILENAME = "buildings.txt";
        const string ROUND_FILENAME = "rounds.txt";

        Map map;
        bool gameOver = false;
        string winningTeam = "";
        int round = 0;

        public GameEngine()
        {
            map = new Map(10, 5);
        }
        public bool GameOver
        {
            get { return gameOver; }
        }
        public string WinningTeam
        {
            get { return winningTeam;  }
        }
        public int Round
        {
            get { return round; }
        }
        public string GetMapDisplay()
        {
            return map.GetMapDisplay();
        }
        public string GetUnitInfo()
        {
            string unitInfo = "";
            foreach (Unit unit in map.Units)
            {
                unitInfo += unit + "\n";
            }
            return unitInfo;
        }
        public string GetBuildingInfo()
        {
            string buildingInfo = "";
            foreach (Building building in map.Buildings)
            {
                buildingInfo += building + "\n";
            }
            return buildingInfo;
        }
        public void Reset()
        {
            map.Reset();
            gameOver = false;
            round = 0;
        }
        public void GameLoop()
        {
            foreach (Unit unit in map.Units)
            {
                if (unit.Destroyed)
                {
                    continue;
                }

                Unit closestUnit = unit.GetClosestUnit(map.Units);

                if (closestUnit == null)
                {
                    gameOver = true;
                    winningTeam = unit.Team;
                    map.UpdateMap();
                    return;
                }

                double healthPercent = unit.Health / unit.MaxHealth;

                if(healthPercent <= 0.25)
                {
                    unit.RunAway();
                }
                else if (unit.InAttackRange(closestUnit))
                {
                    unit.Attack(closestUnit);
                }
                else
                {
                    unit.Move(closestUnit);
                }
                StayInBounds(unit, map.Size);
            }
            map.UpdateMap();
            round++;
        }

        private void StayInBounds(Unit unit, int size)
        {
            if(unit.xPos < 0)
            {
                unit.xPos = 0;
            }
            else if(unit.xPos >= size)
            {
                unit.xPos = size - 1;
            }

            if(unit.yPos < 0)
            {
                unit.yPos = 0;
            }
            else if(unit.yPos >= size)
            {
                unit.yPos = size - 1;
            }
        }

        public void SaveUnitsToFile()
        {
            FileStream uFile = new FileStream("units.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sr = new StreamWriter(uFile);

            foreach (Unit unit in map.Units)
            {
                sr.WriteLine(unit.Save());
            }
            sr.Close();
            uFile.Close();
        }
        public void SaveBuidlingsToFile()
        {
            FileStream bFile = new FileStream("buidlings.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter srw = new StreamWriter(bFile);

            foreach (Building building in map.Buildings)
            {
                srw.WriteLine(building.Save());
            }
            srw.Close();
            bFile.Close();
        }
        public void SaveGame()
        {
            Save(UNITS_FILENAME, map.Units);
            Save(BUILDINGS_FILENAME, map.Buildings);
            SaveRound();
        }
        public void LoadGame()
        {
            map.Clear();
            Load(UNITS_FILENAME);
            Load(BUILDINGS_FILENAME);
            LoadRound();
            map.UpdateMap();
        }
        private void Load(string filename)
        {
            FileStream inFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);

            string recordIn;
            recordIn = reader.ReadLine();
            while (recordIn != null)
            {
                int length = recordIn.IndexOf(",");
                string firstField = recordIn.Substring(0, length);
                switch (firstField)
                {
                    case "Melee": map.AddUnit(new MeleeUnit(recordIn)); break;
                    case "Ranged": map.AddUnit(new RangedUnit(recordIn)); break;
                    case "Factory": map.AddBuilding(new FactoryBuilding(recordIn)); break;
                    case "Resource": map.AddBuilding(new ResourceBuilding(recordIn)); break;
                }
                recordIn = reader.ReadLine();
            }
            reader.Close();
            inFile.Close();
        }
        private void Save(string filename, object[] objects)
        {
            FileStream outFile = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            foreach (object obj in objects)
            {
                if (obj is Unit)
                {
                    Unit unit = (Unit)obj;
                    writer.WriteLine(unit.Save());
                }
                else if (obj is Building)
                {
                    Building unit = (Building)obj;
                    writer.WriteLine(unit.Save());
                }
            }
            writer.Close();
            outFile.Close();
        }
        private void SaveRound()
        {
            FileStream outFile = new FileStream(ROUND_FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);
            writer.WriteLine(round);
            writer.Close();
            outFile.Close();
        }
        private void LoadRound()
        {
            FileStream inFile = new FileStream(ROUND_FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            round = int.Parse(reader.ReadLine());
            reader.Close();
            inFile.Close();
        }

    }
}
