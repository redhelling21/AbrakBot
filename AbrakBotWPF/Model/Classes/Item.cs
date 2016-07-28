using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class Item
    {
        string id;
        string libelle;
        int quantite;

        public Item(string id, string libelle, int quantite)
        {
            this.id = id;
            this.libelle = libelle;
            this.quantite = quantite;
        }
    }
}
