using System;

namespace SecretHitlerUtilities
{
    public static class Parser
    {
        public static byte[] ErrMsg(string msg) { return ToBytes(Command.Error, msg); }

        public static byte[] ToBytes(Command cmd, string msg)
        { return Array.ConvertAll((""+ (char)(1+msg.Length) + (char)cmd + msg).ToCharArray(), Convert.ToByte); }

        public static byte[] FascPowToBytes(FascistPowers pow, string msg)
        { return ToBytes(Command.FascPow, (char)pow + msg); }

        public static byte[] UpdateToBytes(int numLibPol, int numFascPol, string pres, string chanc)
        { return ToBytes(Command.Update, $"{(byte)numLibPol}{(byte)numFascPol}{pres},{chanc}"); }

        public static string ToString(byte[] bytes, int startIdx = 1)
        { return new string(Array.ConvertAll(bytes, Convert.ToChar)).Substring(startIdx); }
    }
}
