//FB stands for followed by

namespace SecretHitlerUtilities
{
    public enum Command : byte
    {
        Name     = 0x01, //to server FB new playername, to client FB T/F then connected playernames
        Start    = 0x02, //to server means start the game, to client FB role
        PosAssign= 0x03, //to client FB playername for P, to server FB playername for C
        Vote     = 0x04, //to client FB playername for C, to server FB bool
        VoteCnt  = 0x05, //FB number of 'yes' votes
        Policy   = 0x06, //FB policy choices List<L/F>
        FascPow  = 0x07, //FB FascistPower
        Winner   = 0x08, //FB L/F
        VIP      = 0x09, //to client means they are VIP FB anonVote(T/F)
        Settings = 0xFB, //FB Setting
        Update   = 0xFC, //to client when joining during game FB num lib pol, num fasc pol, curr pres, curr chanc (audience role implied)
        Disconnect=0xFD, //to client FB playername that disconnected
        General  = 0xFE, //FB message
        Error    = 0xFF  //FB message
    }
    public enum Role : byte
    {
        None     = 0,
        Liberal  = 1,
        Fascist  = 2,
        Hitler   = 3,
        Audience = None
    }
    public enum FascistPowers : byte
    {
        None             = 0,
        InvestigateLoyalty=1, //to server FB playername, response to client FB playerrole
        SpecialElection  = 2, //to server FB playername
        PolicyPeek       = 3, //to client FB next three policies
        Execution        = 4, //to server FB playername, braodcast to clients
        Veto             = 5  //to server from chanc, to pres, from pres FB T/F, to chanc FB T/F
    }
    public enum Setting : byte
    {
        Participation,   //FB T/F
        AnonymousVoting, //FB T/F
    }
}
