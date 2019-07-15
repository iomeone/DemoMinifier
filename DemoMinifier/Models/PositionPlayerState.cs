using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models
{
    [ProtoContract]
    [ProtoInclude(31, typeof(FullPlayerState))]
    public class PositionPlayerState : BasePlayerState
    {
        [ProtoMember(4)]
        public Vector Position { get; set; }
        [ProtoMember(5)]
        public float ViewOffsetZ { get; set; }
        [ProtoMember(6)]
        public Vector Velocity { get; set; }
        [ProtoMember(7)]
        public float ViewDirectionX { get; set; }
        [ProtoMember(8)]
        public float ViewDirectionY { get; set; }

        public PositionPlayerState()
        {
            Type = StateType.Position;
        }

        public PositionPlayerState(FullPlayerState fullState)
        {
            Type = StateType.Position;
            NameIndex = fullState.NameIndex;
            SteamIDIndex = fullState.SteamIDIndex;
            Position = fullState.Position;
            ViewOffsetZ = fullState.ViewOffsetZ;
            Velocity = fullState.Velocity;
            ViewDirectionX = fullState.ViewDirectionX;
            ViewDirectionY = fullState.ViewDirectionY;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            PositionPlayerState other = (PositionPlayerState)obj;

            if (Position != other.Position)
                return false;

            if (ViewOffsetZ != other.ViewOffsetZ)
                return false;

            if (Velocity != other.Velocity)
                return false;

            if (ViewDirectionX != other.ViewDirectionX)
                return false;

            if (ViewDirectionY != other.ViewDirectionY)
                return false;

            return true;
        }
    }
}
