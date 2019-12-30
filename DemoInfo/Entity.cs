using System;
using System.Collections.Generic;
using System.Text;

namespace DemoInfo
{
    public class Entity
    {
        public int ID { get; set; }
        public EntityType Type { get; set; }
        public int CellX { get; set; }
        public int CellY { get; set; }
        public int CellZ { get; set; }
        public Vector LocalPosition { get; set; }
        public Vector Position
        {
            get
            {
                return new Vector(CellX * 32 - 16384 + LocalPosition.X, CellY * 32 - 16384 + LocalPosition.Y, CellZ * 32 - 16384 + LocalPosition.Z);
            }
        }
        public Vector Rotation { get; set; }
        public int ModelIndex { get; set; }
        public string ModelLocation { get; set; }
    }

    public enum EntityType
    {
        Base = 0,
        Dynamic = 1,
        PhysicsPropMultiplayer = 2,
        PropDoorRotating = 3,
        Chicken = 4,
    }
}
