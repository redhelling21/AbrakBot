using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBot
{
    /* principe de l'algo A*Star
     * 
     * Chaque case a trois poids : 
     *  - La distance avec la case de départ
     *  - La distance avec la case d'arrivée
     *  - le poids résultant de la somme des deux
     *  
     *  Chaque case a en plus un parent.
     *  
     *  On a deux listes : la openlist et la closelist
     *  Déroulement d'une boucle
     *  
     *  On récupère les 9 cases qui entourent la case sélectionnée, on enlève celle qui appartiennent à la closelist
     *  On calcule le poid de celles qui restent
     *  On choisit celle qui a le poids total le plus faible
     *  Elle aura pour parent la case au centre des 9
     *  On ajoute la case au centre à la closelist
     *  On recommence avec cette nouvelle case
     */
    class Pathfinding
    {

        public List<int> openlist = new List<int>();
        public List<int> closelist = new List<int>();
        private int[] parentlist = new int[1025];
        private int[] poidslist = new int[1025];
        private int[] distOriginlist = new int[1025];

        private int[] distDestlist = new int[1025];
        private bool fight;

        private int nombreDePM;

        //initialisation des listes
        private void loadCell()
        {
            for (int i = 0; i <= 1024; i++)
            {
                parentlist[i] = 0;
                poidslist[i] = 0;
                distOriginlist[i] = 0;
                distDestlist[i] = 0;
            }

        }

        //Ajout des obstacles de la map à la closelist
        private void loadSprites(Cell[] mapHandler, bool eviterChangeurs)
        {
            for (int i = 0; i < 1000; i++)
            {
                if(mapHandler[i] != null)
                {
                    if (mapHandler[i].movement == 0 || mapHandler[i].movement == 1)
                    {
                        closelist.Add(i);
                    }
                    else if (mapHandler[i].object2Movement == true)
                    {
                        closelist.Add(i);
                    }
                    else if ((eviterChangeurs))
                    {
                        if (mapHandler[i].movement == 2)
                            closelist.Add(i);
                    }
                }
                
            }

        }

        //Lancement de l'algo de pathfinding
        public string pathing(Cell[] mapHandler, int nCellBegin, int nCellEnd, bool eviterChangeurs = false, bool isfight = false, int numberPM = 9999)
        {
           
            loadCell();
            loadSprites(mapHandler, eviterChangeurs);
            closelist.Remove(nCellEnd);

            fight = isfight;
            nombreDePM = numberPM;
            string returnPath = findpath(nCellBegin, nCellEnd);

            return cleanPath(returnPath);
        }

        //calcul du chemin entre les deux cases
        private string findpath(int cell1, int cell2)
        {
            int current = 0;
            int i = 0;

            openlist.Add(cell1);

            //Tant qu'on est pas arrivé à la case de fin
            while (!(openlist.Contains(cell2)))
            {
                i += 1;
                if (i > 1000)
                    return "";

                
                current = getFpoint();
                if (current == cell2)
                    break; // TODO: might not be correct. Was : Exit Do
                closelist.Add(current);
                openlist.Remove(current);


                foreach (int cell in getChild(current))
                {

                    if (closelist.Contains(cell) == false)
                    {

                        if (openlist.Contains(cell))
                        {
                            if (distOriginlist[current] + 5 < distOriginlist[cell])
                            {
                                parentlist[cell] = current;
                                distOriginlist[cell] = distOriginlist[current] + 5;
                                distDestlist[cell] = goalDistance("X", cell, cell2);
                                poidslist[cell] = distOriginlist[cell] + distDestlist[cell];
                            }

                        }
                        else
                        {
                            openlist.Add(cell);
                            openlist[openlist.Count - 1] = cell;
                            distOriginlist[cell] = distOriginlist[current] + 5;
                            distDestlist[cell] = goalDistance("X", cell, cell2);
                            poidslist[cell] = distOriginlist[cell] + distDestlist[cell];
                            parentlist[cell] = current;
                        }

                    }

                }
            }

            return (getParent(cell1, cell2));

        }

        private string getParent(int cell1, int cell2)
        {
            int current = cell2;
            List<int> pathCell = new List<int>();
            pathCell.Add(current);

            while (!(current == cell1))
            {
                pathCell.Add(parentlist[current]);
                current = parentlist[current];
            }

            return getPath(pathCell);
        }


        private string getPath(List<int> pathCell)
        {
            pathCell.Reverse();
            string pathing = "";
            int current = 0;
            int child = 0;
            int PMUsed = 0;
            for (int i = 0; i <= pathCell.Count - 2; i++)
            {
                PMUsed += 1;
                if ((PMUsed > nombreDePM))
                    return pathing;
                current = pathCell[i];
                child = pathCell[i + 1];
                Globals.writeToDebugBox(child.ToString() + " -> ", System.Drawing.Color.Orange);
                pathing += getOrientation(current, child) + Globals.cases[child];
            }

            return pathing;
        }

        private List<int> getChild(int cell)
        {

            int x = getCaseCoordonneeX("X", cell);
            int y = getCaseCoordonneeY("X", cell);
            List<int> children = new List<int>();
            int temp = 0;
            int locx = 0;
            int locy = 0;


            if (fight == false)
            {
                temp = cell - 29;
                locx = getCaseCoordonneeX("X", temp);
                locy = getCaseCoordonneeY("X", temp);
                if (temp > 1 & temp < 1024 & locx == x - 1 & locy == y - 1 & closelist.Contains(temp) == false)
                {
                    children.Add(temp);
                }

                temp = cell + 29;
                locx = getCaseCoordonneeX("X", temp);
                locy = getCaseCoordonneeY("X", temp);
                if (temp > 1 & temp < 1024 & locx == x + 1 & locy == y + 1 & closelist.Contains(temp) == false)
                {
                    children.Add(temp);
                }

            }

            temp = cell - 15;
            locx = getCaseCoordonneeX("X", temp);
            locy = getCaseCoordonneeY("X", temp);
            if (temp > 1 & temp < 1024 & locx == x - 1 & locy == y & closelist.Contains(temp) == false)
            {
                children.Add(temp);
            }

            temp = cell + 15;
            locx = getCaseCoordonneeX("X", temp);
            locy = getCaseCoordonneeY("X", temp);
            if (temp > 1 & temp < 1024 & locx == x + 1 & locy == y & closelist.Contains(temp) == false)
            {
                children.Add(temp);
            }

            temp = cell - 14;
            locx = getCaseCoordonneeX("X", temp);
            locy = getCaseCoordonneeY("X", temp);
            if (temp > 1 & temp < 1024 & locx == x & locy == y - 1 & closelist.Contains(temp) == false)
            {
                children.Add(temp);
            }

            temp = cell + 14;
            locx = getCaseCoordonneeX("X", temp);
            locy = getCaseCoordonneeY("X", temp);
            if (temp > 1 & temp < 1024 & locx == x & locy == y + 1 & closelist.Contains(temp) == false)
            {
                children.Add(temp);
            }


            if (fight == false)
            {
                temp = cell - 1;
                locx = getCaseCoordonneeX("X", temp);
                locy = getCaseCoordonneeY("X", temp);
                if (temp > 1 & temp < 1024 & locx == x - 1 & locy == y + 1 & closelist.Contains(temp) == false)
                {
                    children.Add(temp);
                }

                temp = cell + 1;
                locx = getCaseCoordonneeX("X", temp);
                locy = getCaseCoordonneeY("X", temp);
                if (temp > 1 & temp < 1024 & locx == x + 1 & locy == y - 1 & closelist.Contains(temp) == false)
                {
                    children.Add(temp);
                }

            }

            return children;

        }

        private int getFpoint()
        {

            int x = 9999;
            int cell = 0;

            foreach (int item in openlist)
            {
                if (closelist.Contains(item) == false)
                {
                    if (poidslist[item] < x)
                    {
                        x = poidslist[item];
                        cell = item;
                    }
                }
            }

            return cell;
        }

        public class loc8
        {
            public int y = 0;
            public int x = 0;
        }

        public int getCaseCoordonneeX(string mapHandler, int nNum)
        {
            int _loc4 = 15;
            //mapHandler.Length()
            int _loc5 = (int)Math.Floor((float)nNum / (_loc4 * 2 - 1));
            int _loc6 = nNum - _loc5 * (_loc4 * 2 - 1);
            int _loc7 = _loc6 % _loc4;
            int y = _loc5 - _loc7;
            int x = (nNum - (_loc4 - 1) * y) / _loc4;
            return x;
        }

        public int getCaseCoordonneeY(string mapHandler, int nNum)
        {
            int _loc4 = 15;
            //mapHandler.Length()
            int _loc5 = (int)Math.Floor((float)nNum / (_loc4 * 2 - 1));
            int _loc6 = nNum - _loc5 * (_loc4 * 2 - 1);
            int _loc7 = _loc6 % _loc4;

            int y = _loc5 - _loc7;
            int x = (nNum - (_loc4 - 1) * y) / _loc4;
            return y;
        }

        public int goalDistance(string mapHandler, int nCell1, int nCell2)
        {

            int _loc5x = getCaseCoordonneeX(mapHandler, nCell1);
            int _loc5y = getCaseCoordonneeY(mapHandler, nCell1);
            int _loc6x = getCaseCoordonneeX(mapHandler, nCell2);
            int _loc6y = getCaseCoordonneeY(mapHandler, nCell2);
            int _loc7 = Math.Abs(_loc5x - _loc6x);
            int _loc8 = Math.Abs(_loc5y - _loc6y);

            return (_loc7 + _loc8);

        }

        public object getOrientation(int cell1, int cell2)
        {
            switch (cell2 - cell1)
            {
                case -29:
                    return "g";
                case 29:
                    return "c";
                case -1:
                    return "e";
                case 1:
                    return "a";
                case -15:
                    return "f";
                case 15:
                    return "b";
                case -14:
                    return "h";
                case 14:
                    return "d";
            }
            return "a";
        }


        public int getDirectionFromCoordinates(int x1, int y1, int x2, int y2, bool bAllDirections)
        {

            double _loc7 = Math.Atan2(y2 - y1, x2 - x1);

            if (bAllDirections)
            {

                if ((_loc7 >= -Math.PI / 8 && _loc7 < Math.PI / 8))
                {
                    return (0);
                }

                if ((_loc7 >= Math.PI / 8 && _loc7 < Math.PI / 3))
                {
                    return (1);
                }

                if ((_loc7 >= Math.PI / 3 && _loc7 < 2 * Math.PI / 3))
                {
                    return (2);
                }

                if ((_loc7 >= 2 * Math.PI / 3 && _loc7 < 7 * Math.PI / 8))
                {
                    return (3);
                }

                if ((_loc7 >= 7 * Math.PI / 8 || _loc7 < -7 * Math.PI / 8))
                {
                    return (4);
                }

                if ((_loc7 >= -7 * Math.PI / 8 && _loc7 < -2 * Math.PI / 3))
                {
                    return (5);
                }

                if ((_loc7 >= -2 * Math.PI / 3 && _loc7 < -Math.PI / 3))
                {
                    return (6);
                }

                if ((_loc7 >= -Math.PI / 3 && _loc7 < -Math.PI / 8))
                {
                    return (7);
                }


            }
            else
            {

                if ((_loc7 >= 0 && _loc7 < Math.PI / 2))
                {
                    return (1);
                }

                if ((_loc7 >= Math.PI / 2 && _loc7 <= Math.PI))
                {
                    return (3);
                }

                if ((_loc7 >= -Math.PI && _loc7 < -Math.PI / 2))
                {
                    return (5);
                }

                if ((_loc7 >= -Math.PI / 2 && _loc7 < 0))
                {
                    return (7);
                }
            }
            return (1);
        }

        private string cleanPath(string path)
        {

            string cleanedPath = "";

            if ((path.Length > 3))
            {
                for (int i = 0; i < path.Length; i += 3)
                {
                    if (i == path.Length - 3 || (path.Substring(i, 1) != path.Substring(i + 3, 1)))
                    {
                        cleanedPath += path.Substring(i, 3);
                    }
                }
                //cleanedPath += path.Substring(path.Length - 3);
            }
            else
            {
                cleanedPath = path;
            }

            return cleanedPath;

        }

    }
}
