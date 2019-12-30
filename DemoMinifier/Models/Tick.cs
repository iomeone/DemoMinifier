using DemoMinifier.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProtoBuf;

namespace DemoMinifier.Models
{
    [ProtoContract]
    public class Tick
    {
        [ProtoMember(1)]
        public List<BasePlayerState> PlayerStates { get; set; }

        [ProtoMember(2)]
        public List<BaseEntityState> EntityStates { get; set; }

        [ProtoMember(3)]
        public List<BaseEvent> Events { get; set; }

        [ProtoMember(4)]
        public int IngameTick { get; set; }

        public Tick()
        {
            PlayerStates = new List<BasePlayerState>();
            EntityStates = new List<BaseEntityState>();
            Events = new List<BaseEvent>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null && this != null)
                return false;

            if (obj != null && this == null)
                return false;

            Tick other = (Tick)obj;

            if (Events.Count != other.Events.Count)
                return false;

            if (PlayerStates.Count != other.PlayerStates.Count)
                return false;

            for (int i = 0; i < PlayerStates.Count; i++)
            {
                if (!PlayerStates[i].Equals(other.PlayerStates.FirstOrDefault(a => a.SteamIDIndex == PlayerStates[i].SteamIDIndex)))
                    return false;
            }

            //for (int i = 0; i < Events.Count; i++)
            //{
            //    if (!Events[i].Equals(other.Events[i]))
            //        return false;
            //}

            return true;
        }
    }
}
