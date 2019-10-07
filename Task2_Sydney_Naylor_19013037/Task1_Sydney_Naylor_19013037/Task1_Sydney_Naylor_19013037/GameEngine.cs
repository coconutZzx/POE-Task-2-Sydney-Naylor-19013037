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
        Map map;
        bool gameOver = false;
        string winningTeam = "";
        int round = 0;

        public GameEngine()
        {
            map = new Map(10, 4);
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

    }
}
