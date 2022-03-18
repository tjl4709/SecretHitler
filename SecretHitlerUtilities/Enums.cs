using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerUtilities
{
    public enum Command : byte
    {
        General = 0x00, //Context specific (usually info)
        Start   = 0x01, //to server means start the game, to client followed by role
        PosAssign=0x02, //to client followed by playername for P, to server followed by playername for C
        Vote    = 0x03, //to client followed by playername for C, to server followed by bool
        VoteCnt = 0x04, //followed by number of 'yes' votes
        Policy  = 0x04, //followed by policy choices List<L/F>
        FascPow = 0x05, //followed by FascistPower
        Winner  = 0x0A, //followed by L/F
        VIP     = 0x0B, //to client means they are VIP
        Error   = 0xFF  //followed by message
    }
    public enum Role : byte
    {
        None,
        Liberal,
        Fascist,
        Hitler,
        Audience = None
    }
    public enum FascistPowers : byte
    {
        None,
        InvestigateLoyalty,
        SpecialElection,
        PolicyPeek,
        Execution
    }
}
