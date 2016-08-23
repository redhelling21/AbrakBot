using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleCombat(string packet)
        {
            globals.fightHandler.PacketCombat(packet);
        }
    }
}
