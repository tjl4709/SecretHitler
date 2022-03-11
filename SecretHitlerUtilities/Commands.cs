using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerUtilities
{
    public enum Commands : byte
    {
        General = 0x00, //Context specific (usually info)
        Start   = 0x01, //to server means start the game, to client followed by role
        PosAssign=0x02, //to client followed by P/C, to server followed by playername for C
        Vote    = 0x03, //to client followed by playername for C, to server followed by bool
        Policy  = 0x04, //followed by policy choices List<L/F>
        FascPow = 0x05, //followed by 
        Error   = 0xFF  //followed by message
    }
    public enum Roles : byte
    {
        Liberal = 0,
        Fascist = 1,
        Hitler  = 2,
        Audience= 3
    }
    public enum FascistPowers : byte
    {
        InvestigateLoyalty = 0,
        SpecialElection = 1,
        PolicyPeek = 2,
        Execution = 3
    }
}
