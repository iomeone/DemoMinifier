using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoMinifier.Models
{
    public class Player
    {
        public string Name { get; set; }

        public long SteamID { get; set; }

        public Vector Position { get; set; }

        //Custom
        public Vector ViewOffset { get; set; }

        public Vector EyesPosition
        {
            get { return Position + ViewOffset; }
        }

        public bool IsScoped { get; set; }

        public int ShotsFired { get; set; }

        public Vector AimPunchAngle { get; set; }
        //End Custom

        public int EntityID { get; set; }

        public int HP { get; set; }

        public int Armor { get; set; }

        public Vector LastAlivePosition { get; set; }

        public Vector Velocity { get; set; }

        public float ViewDirectionX { get; set; }

        public float ViewDirectionY { get; set; }

        public float FlashDuration { get; set; }

        public int Money { get; set; }

        public int CurrentEquipmentValue { get; set; }

        public int FreezetimeEndEquipmentValue { get; set; }

        public int RoundStartEquipmentValue { get; set; }

        public bool IsDucking { get; set; }

        //internal Entity Entity;

        public bool Disconnected { get; set; }

        public int ActiveWeaponID { get; set; }

        //public Equipment ActiveWeapon
        //{
        //    get
        //    {
        //        if (ActiveWeaponID == DemoParser.INDEX_MASK) return null;
        //        return rawWeapons[ActiveWeaponID];
        //    }
        //}

        public Dictionary<short, Weapon> RawWeapons = new Dictionary<short, Weapon>();
        //public IEnumerable<Equipment> Weapons { get { return rawWeapons.Values; } }

        public bool HasSmokeGrenade { get { return RawWeapons.Values.Any(w => w.Equipment == EquipmentElement.Smoke); } }
        public bool HasMolotov { get { return RawWeapons.Values.Any(w => w.Equipment == EquipmentElement.Molotov); } }
        public bool HasIncendiary { get { return RawWeapons.Values.Any(w => w.Equipment == EquipmentElement.Incendiary); } }
        public bool HasHighExplosiveGrenade { get { return RawWeapons.Values.Any(w => w.Equipment == EquipmentElement.HE); } }
        public int FlasbangCount { get { return RawWeapons.Values.Count(w => w.Equipment == EquipmentElement.Flash); } }


        public bool IsAlive
        {
            get { return HP > 0; }
        }

        public Team Team { get; set; }

        public bool HasDefuseKit { get; set; }

        public bool HasHelmet { get; set; }

        public int TeamID { get; set; }

        public int[] AmmoLeft { get; set; } = new int[32];

        //public AdditionalPlayerInformation AdditionaInformations { get; internal set; }
    }
}
