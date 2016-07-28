using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class Cell
    {
        //Definit les mouvements possibles sur la case : 4->normal, 2->MapChanger, ...
        public int movement; 
        //La case contient-elle un objet avec lequel on peut interagir ?
        public bool layerObject2Interactive;
        //Sprite de l'objet present sur la case
        public int layerObject2Num;
        //Je sais pas ce que c'est
        public bool object2Movement;
    }
}
