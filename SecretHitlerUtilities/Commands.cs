using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerUtilities
{
    public enum Commands : byte
    {
        General = 0x00, //Context specific
        Start   = 0x01, //to server means start the game, to client followed by role
        Error   = 0xFF  //followed by message
    }
}
