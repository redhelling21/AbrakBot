using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Messages
{
    class PlayerStateChangedMessage
    {
        public bool isFighting { get; set; }
        public bool isMoving { get; set; }
        public bool isHarvesting { get; set; }
        public bool isInExchange { get; set; }
    }
}
