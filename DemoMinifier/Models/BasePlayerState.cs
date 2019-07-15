using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    [ProtoInclude(30, typeof(PositionPlayerState))]
    public class BasePlayerState
    {
        [ProtoMember(1)]
        public StateType Type { get; set; }
        [ProtoMember(2)]
        public byte NameIndex { get; set; }
        [ProtoMember(3)]
        public byte SteamIDIndex { get; set; }

        public BasePlayerState()
        {
            Type = StateType.Base;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            BasePlayerState other = (BasePlayerState)obj;

            if (Type != other.Type)
                return false;

            if (NameIndex != other.NameIndex)
                return false;

            if (SteamIDIndex != other.SteamIDIndex)
                return false;

            return true;
        }
    }

    public enum StateType : byte
    {
        Base,
        Position,
        Full
    }
}
