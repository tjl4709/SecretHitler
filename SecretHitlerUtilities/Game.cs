﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretHitlerUtilities
{
    public class Game
    {
        private int m_nextPrezIdx;
        private string m_pres, m_chanc;
        public string President { get { return m_pres; } set { if (m_parties.ContainsKey(value)) m_pres = value; } }
        public string NextPrez {
            get { return m_parties.ElementAt(m_nextPrezIdx).Key; }
            set {
                if (m_parties.ContainsKey(value)) {
                    System.Collections.IEnumerator keys = m_parties.Keys.GetEnumerator();
                    for (m_nextPrezIdx = 0; m_nextPrezIdx < m_parties.Count; m_nextPrezIdx++, keys.MoveNext())
                        if ((string)keys.Current == value)
                            break;
                }
            }
        }
        public string Chancellor {
            get { return m_chanc; }
            set {
                if (!m_parties.ContainsKey(value)) return;
                m_chanc = value;
                if (m_parties[m_chanc] == Role.Hitler && NumFascistPolicies >= 3)
                    Winner = Role.Fascist;
            }
        }
        public string InvestigatedPlayer;

        public readonly int NumPlayers;
        public int NumAlive { get { return m_parties.Count; } }
        
        public int NumLiberalPolicies, NumFascistPolicies;
        public bool Veto { get { return NumFascistPolicies >= 5; } }
        public Role Winner;

        private Dictionary<string, Role> m_parties;
        private List<Role> draw, discard;
        private Random rand;


        public Game(List<string> players)
        {
            m_parties = new Dictionary<string, Role>(players.Count);
            draw = new List<Role>(17);
            discard = new List<Role>(10);
            m_pres = m_chanc = "";
            NumLiberalPolicies = NumFascistPolicies = 0;
            NumPlayers = players.Count;
            rand = new Random();
            m_nextPrezIdx = rand.Next(players.Count);

            int i, nf = (players.Count - 1) / 2, nl = players.Count - nf;
            for (i = 0; i < players.Count; i++) {
                if (rand.Next(nf+nl) < nl) {
                    nl--;
                    m_parties.Add(players[i], Role.Liberal);
                } else {
                    if (rand.Next(nf) == 0)
                        m_parties.Add(players[i], Role.Hitler);
                    else m_parties.Add(players[i], Role.Fascist);
                    nf--;
                }
            }
            for (i = 0; i < 6; i++) draw.Add(Role.Liberal);
            for (i = 0; i < 11; i++) draw.Add(Role.Fascist);
        }

        public List<Role> Draw(int num = 3)
        {
            if (draw.Count < num) {
                draw.AddRange(discard);
                discard.Clear();
            }
            List<Role> drew = new List<Role>(num);
            int j;
            for (int i = 0; i < num; i++) {
                j = rand.Next(draw.Count);
                drew[i] = draw[j];
                draw.RemoveAt(j);
            }
            return drew;
        }
        public void Discard(Role card)
        {
            if (card == Role.Liberal || card == Role.Fascist)
                discard.Add(card);
        }
        public FascistPowers Play(Role card)
        {
            FascistPowers pow = FascistPowers.None;
            if (card == Role.Liberal) {
                NumLiberalPolicies++;
            } else if (card == Role.Fascist) {
                NumFascistPolicies++;
                if (NumFascistPolicies >= 4)
                    pow = FascistPowers.Execution;
                else if (NumFascistPolicies == 3) {
                    if (m_parties.Count >= 7)
                        pow = FascistPowers.SpecialElection;
                    else pow = FascistPowers.PolicyPeek;
                }
                else if (NumFascistPolicies == 2 && m_parties.Count >= 7)
                    pow = FascistPowers.InvestigateLoyalty;
                else if (NumFascistPolicies == 1 && m_parties.Count >= 9)
                    pow = FascistPowers.InvestigateLoyalty;
            }
            Winner = NumFascistPolicies == 6 ? Role.Fascist : NumLiberalPolicies == 5 ? Role.Liberal : Role.None;
            return pow;
        }

        public bool Contains(string player) { return m_parties.ContainsKey(player); }
        public Role GetRole(string player) { if (m_parties.ContainsKey(player)) return m_parties[player]; else return Role.None; }
        public bool Kill(string player)
        {
            if (!m_parties.ContainsKey(player))
                return false;
            bool isHitler = m_parties[player] == Role.Hitler;
            m_parties.Remove(player);
            if (isHitler) Winner = Role.Liberal;
            return isHitler;
        }

        public void NextPrezResult(bool elected)
        {
            if (elected)
                President = NextPrez;
            m_nextPrezIdx = (m_nextPrezIdx + 1) % NumAlive;
        }
    }
}