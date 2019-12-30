using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMinifier.Models.Events
{
    public enum EventType
    {
        MatchStarted,
        RoundAnnounceMatchStarted,
        RoundStart,
        RoundEnd,
        WinPanelMatch,
        RoundFinal,
        LastRoundHalf,
        RoundOfficiallyEnd,
        RoundMVP,
        FreezetimeEnded,
        PlayerKilled,
        PlayerTeam,
        WeaponFired,
        SmokeNadeStarted,
        SmokeNadeEnded,
        DecoyNadeStarted,
        DecoyNadeEnded,
        FireNadeStarted,
        FireNadeWithOwnerStarted,
        FireNadeEnded,
        FlashNadeExploded,
        ExplosiveNadeExploded,
        NadeReachedTarget,
        BombBeginPlant,
        BombAbortPlant,
        BombPlanted,
        BombDefused,
        BombExploded,
        BombBeginDefuse,
        BombAbortDefuse,
        PlayerHurt,
        Blind,
        PlayerBind,
        PlayerDisconnect,
        SayText,
        SayText2,
        RankUpdate,
        PlayerJump,
        PlayerFootstep,
        OtherDeath,
        EntitySpawned,
        EntityRemoved
    }

    [ProtoContract]
    [ProtoInclude(10, typeof(BaseNadeEvent))]
    [ProtoInclude(11, typeof(BaseBombEvent))]
    [ProtoInclude(12, typeof(BlindEvent))]
    [ProtoInclude(13, typeof(BombAbortDefuseEvent))]
    [ProtoInclude(14, typeof(BombBeginDefuseEvent))]
    [ProtoInclude(15, typeof(PlayerBindEvent))]
    [ProtoInclude(16, typeof(PlayerDisconnectEvent))]
    [ProtoInclude(17, typeof(PlayerHurtEvent))]
    [ProtoInclude(18, typeof(PlayerKilledEvent))]
    [ProtoInclude(19, typeof(PlayerTeamEvent))]
    [ProtoInclude(20, typeof(RoundEndEvent))]
    [ProtoInclude(21, typeof(RoundMVPEvent))]
    [ProtoInclude(22, typeof(RoundStartEvent))]
    [ProtoInclude(23, typeof(SayTextEvent))]
    [ProtoInclude(24, typeof(SayText2Event))]
    [ProtoInclude(25, typeof(WeaponFiredEvent))]
    [ProtoInclude(26, typeof(RankUpdateEvent))]
    [ProtoInclude(27, typeof(PlayerJumpEvent))]
    [ProtoInclude(28, typeof(PlayerFootstepEvent))]
    [ProtoInclude(29, typeof(OtherDeathEvent))]
    [ProtoInclude(30, typeof(EntitySpawnedEvent))]
    [ProtoInclude(31, typeof(EntityRemovedEvent))]
    public class BaseEvent
    {
        [ProtoMember(1)]
        public EventType Type { get; set; }

        public BaseEvent()
        {

        }

        public BaseEvent(EventType type)
        {
            Type = type;
        }
    }

    [ProtoContract]
    [ProtoInclude(10, typeof(DecoyNadeEndedEvent))]
    [ProtoInclude(11, typeof(DecoyNadeStartedEvent))]
    [ProtoInclude(12, typeof(ExplosiveNadeExplodedEvent))]
    [ProtoInclude(13, typeof(FireNadeEndedEvent))]
    [ProtoInclude(14, typeof(FireNadeStartedEvent))]
    [ProtoInclude(15, typeof(FireNadeWithOwnerStartedEvent))]
    [ProtoInclude(16, typeof(FlashNadeExplodedEvent))]
    [ProtoInclude(17, typeof(NadeReachedTargetEvent))]
    [ProtoInclude(18, typeof(SmokeNadeEndedEvent))]
    [ProtoInclude(19, typeof(SmokeNadeStartedEvent))]
    public class BaseNadeEvent : BaseEvent
    {
        [ProtoMember(1)]
        public Vector Position { get; set; }
        [ProtoMember(2)]
        public EquipmentElement NadeType { get; set; }
        [ProtoMember(3)]
        public long? ThrownBySteamID { get; set; }
    }

    [ProtoContract]
    [ProtoInclude(10, typeof(BombAbortPlantEvent))]
    [ProtoInclude(11, typeof(BombBeginPlantEvent))]
    [ProtoInclude(12, typeof(BombDefusedEvent))]
    [ProtoInclude(13, typeof(BombExplodedEvent))]
    [ProtoInclude(14, typeof(BombPlantedEvent))]
    public class BaseBombEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long? SteamID { get; set; }
        [ProtoMember(2)]
        public char Site { get; set; }
    }

    [ProtoContract]
    public class BlindEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long PlayerSteamID { get; set; }
        [ProtoMember(2)]
        public long AttackerSteamID { get; set; }
        [ProtoMember(3)]
        public float? FlashDuration { get; set; }

        public BlindEvent()
        {
            Type = EventType.Blind;
        }
    }

    [ProtoContract]
    public class BombAbortDefuseEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SteamID { get; set; }
        [ProtoMember(2)]
        public bool HasKit { get; set; }

        public BombAbortDefuseEvent()
        {
            Type = EventType.BombAbortDefuse;
        }
    }

    [ProtoContract]
    public class BombAbortPlantEvent : BaseBombEvent
    {
        public BombAbortPlantEvent()
        {
            Type = EventType.BombAbortPlant;
        }
    }

    [ProtoContract]
    public class BombBeginDefuseEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SteamID { get; set; }
        [ProtoMember(2)]
        public bool HasKit { get; set; }

        public BombBeginDefuseEvent()
        {
            Type = EventType.BombBeginDefuse;
        }
    }

    [ProtoContract]
    public class BombBeginPlantEvent : BaseBombEvent
    {
        public BombBeginPlantEvent()
        {
            Type = EventType.BombBeginPlant;
        }
    }

    [ProtoContract]
    public class BombDefusedEvent : BaseBombEvent
    {
        public BombDefusedEvent()
        {
            Type = EventType.BombDefused;
        }
    }


    [ProtoContract]
    public class BombExplodedEvent : BaseBombEvent
    {
        public BombExplodedEvent()
        {
            Type = EventType.BombExploded;
        }
    }

    [ProtoContract]
    public class BombPlantedEvent : BaseBombEvent
    {
        public BombPlantedEvent()
        {
            Type = EventType.BombPlanted;
        }
    }

    [ProtoContract]
    public class DecoyNadeEndedEvent : BaseNadeEvent
    {
        public DecoyNadeEndedEvent()
        {
            Type = EventType.DecoyNadeEnded;
        }
    }

    [ProtoContract]
    public class DecoyNadeStartedEvent : BaseNadeEvent
    {
        public DecoyNadeStartedEvent()
        {
            Type = EventType.DecoyNadeStarted;
        }
    }

    [ProtoContract]
    public class ExplosiveNadeExplodedEvent : BaseNadeEvent
    {
        public ExplosiveNadeExplodedEvent()
        {
            Type = EventType.ExplosiveNadeExploded;
        }
    }

    [ProtoContract]
    public class FireNadeEndedEvent : BaseNadeEvent
    {
        public FireNadeEndedEvent()
        {
            Type = EventType.FireNadeEnded;
        }
    }

    [ProtoContract]
    public class FireNadeStartedEvent : BaseNadeEvent
    {
        public FireNadeStartedEvent()
        {
            Type = EventType.FireNadeStarted;
        }
    }

    [ProtoContract]
    public class FireNadeWithOwnerStartedEvent : BaseNadeEvent
    {
        public FireNadeWithOwnerStartedEvent()
        {
            Type = EventType.FireNadeWithOwnerStarted;
        }
    }

    [ProtoContract]
    public class FlashNadeExplodedEvent : BaseNadeEvent
    {
        public FlashNadeExplodedEvent()
        {
            Type = EventType.FlashNadeExploded;
        }
    }

    [ProtoContract]
    public class NadeReachedTargetEvent : BaseNadeEvent
    {
        public NadeReachedTargetEvent()
        {
            Type = EventType.NadeReachedTarget;
        }
    }

    [ProtoContract]
    public class PlayerBindEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SteamID { get; set; }

        public PlayerBindEvent()
        {
            Type = EventType.PlayerBind;
        }
    }

    [ProtoContract]
    public class PlayerDisconnectEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SteamID { get; set; }

        public PlayerDisconnectEvent()
        {
            Type = EventType.PlayerDisconnect;
        }
    }

    [ProtoContract]
    public class PlayerHurtEvent : BaseEvent
    {
        /// <summary>
		/// The hurt player
		/// </summary>
        [ProtoMember(1)]
        public long PlayerSteamID { get; set; }

        /// <summary>
        /// The attacking player
        /// </summary>
        [ProtoMember(2)]
        public long? AttackerSteamID { get; set; }

        /// <summary>
        /// Remaining health points of the player
        /// </summary>
        [ProtoMember(3)]
        public int Health { get; set; }

        /// <summary>
        /// Remaining armor points of the player
        /// </summary>
        [ProtoMember(4)]
        public int Armor { get; set; }

        /// <summary>
        /// The Weapon used to attack. 
        /// Note: This might be not the same as the raw event
        /// we replace "hpk2000" with "usp-s" if the attacker
        /// is currently holding it - this value is originally
        /// networked "wrong". By using this property you always
        /// get the "right" weapon
        /// </summary>
        /// <value>The weapon.</value>
        [ProtoMember(5)]
        public Weapon Weapon { get; set; }

        /// <summary>
        /// The original "weapon"-value from the event. 
        /// Might be wrong for USP, CZ and M4A1-S
        /// </summary>
        /// <value>The weapon string.</value>
        [ProtoMember(6)]
        public string WeaponString { get; set; }

        /// <summary>
        /// The damage done to the players health
        /// </summary>
        [ProtoMember(7)]
        public int HealthDamage { get; set; }

        /// <summary>
        /// The damage done to the players armor
        /// </summary>
        [ProtoMember(8)]
        public int ArmorDamage { get; set; }

        /// <summary>
        /// Where the Player was hit. 
        /// </summary>
        /// <value>The hitgroup.</value>
        [ProtoMember(9)]
        public Hitgroup Hitgroup { get; set; }

        public PlayerHurtEvent()
        {
            Type = EventType.PlayerHurt;
        }
    }

    [ProtoContract]
    public class PlayerKilledEvent : BaseEvent
    {
        [ProtoMember(1)]
        public Weapon Weapon { get; set; }
        [ProtoMember(2)]
        public long VictimSteamID { get; set; }
        [ProtoMember(3)]
        public long? KillerSteamID { get; set; }
        [ProtoMember(4)]
        public long? AssisterSteamID { get; set; }
        [ProtoMember(5)]
        public int PenetratedObjects { get; set; }
        [ProtoMember(6)]
        public bool Headshot { get; set; }
        [ProtoMember(7)]
        public bool AssistedFlash { get; set; }

        public PlayerKilledEvent()
        {
            Type = EventType.PlayerKilled;
        }
    }

    [ProtoContract]
    public class PlayerTeamEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SwappedSteamID { get; set; }
        [ProtoMember(2)]
        public Team NewTeam { get; set; }
        [ProtoMember(3)]
        public Team OldTeam { get; set; }
        [ProtoMember(4)]
        public bool Silent { get; set; }
        [ProtoMember(5)]
        public bool IsBot { get; set; }

        public PlayerTeamEvent()
        {
            Type = EventType.PlayerTeam;
        }
    }

    [ProtoContract]
    public class RankUpdateEvent : BaseEvent
    {
        /// <summary>
        /// Player's SteamID64
        /// </summary>
        [ProtoMember(1)]
        public long SteamId { get; set; }

        /// <summary>
        /// Player's rank at the beginning of the match
        /// </summary>
        [ProtoMember(2)]
        public int RankOld { get; set; }

        /// <summary>
        /// Player's rank the end of the match
        /// </summary>
        [ProtoMember(3)]
        public int RankNew { get; set; }

        /// <summary>
        /// Number of win that the player have
        /// </summary>
        [ProtoMember(4)]
        public int WinCount { get; set; }

        /// <summary>
        /// Number of rank the player win / lost between the beggining and the end of the match
        /// </summary>
        [ProtoMember(5)]
        public float RankChange { get; set; }

        public RankUpdateEvent()
        {
            Type = EventType.RankUpdate;
        }
    }

    [ProtoContract]
    public class RoundEndEvent : BaseEvent
    {
        [ProtoMember(1)]
        public RoundEndReason Reason { get; set; }
        [ProtoMember(2)]
        public string Message { get; set; }
        [ProtoMember(3)]
        public Team Winner { get; set; }

        public RoundEndEvent()
        {
            Type = EventType.RoundEnd;
        }
    }

    [ProtoContract]
    public class RoundMVPEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long SteamID { get; set; }
        [ProtoMember(2)]
        public RoundMVPReason Reason { get; set; }

        public RoundMVPEvent()
        {
            Type = EventType.RoundMVP;
        }
    }

    [ProtoContract]
    public class RoundStartEvent : BaseEvent
    {
        [ProtoMember(1)]
        public int TimeLimit { get; set; }
        [ProtoMember(2)]
        public int FragLimit { get; set; }
        [ProtoMember(3)]
        public string Objective { get; set; }

        public RoundStartEvent()
        {
            Type = EventType.RoundStart;
        }
    }

    [ProtoContract]
    public class SayTextEvent : BaseEvent
    {
        /// <summary>
        /// Should be everytime 0 as it's a message from the server
        /// </summary>
        [ProtoMember(1)]
        public int EntityIndex { get; set; }

        /// <summary>
        /// Message sent by the server
        /// </summary>
        [ProtoMember(2)]
        public string Text { get; set; }

        /// <summary>
        /// Everytime false as the message is public
        /// </summary>
        [ProtoMember(3)]
        public bool IsChat { get; set; }

        /// <summary>
        /// Everytime false as the message is public
        /// </summary>
        [ProtoMember(4)]
        public bool IsChatAll { get; set; }

        public SayTextEvent()
        {
            Type = EventType.SayText;
        }
    }

    [ProtoContract]
    public class SayText2Event : BaseEvent
    {
        /// <summary>
        /// The player who sent the message
        /// </summary>
        [ProtoMember(1)]
        public long SenderSteamID { get; set; }

        /// <summary>
        /// The message sent
        /// </summary>
        [ProtoMember(2)]
        public string Text { get; set; }

        /// <summary>
        /// Not sure about it, maybe it's to indicate say_team or say
        /// </summary>
        [ProtoMember(3)]
        public bool IsChat { get; set; }

        /// <summary>
        /// true if the message is for all players ?
        /// </summary>
        [ProtoMember(4)]
        public bool IsChatAll { get; set; }

        public SayText2Event()
        {
            Type = EventType.SayText2;
        }
    }

    [ProtoContract]
    public class SmokeNadeEndedEvent : BaseNadeEvent
    {
        public SmokeNadeEndedEvent()
        {
            Type = EventType.SmokeNadeEnded;
        }
    }

    [ProtoContract]
    public class SmokeNadeStartedEvent : BaseNadeEvent
    {
        public SmokeNadeStartedEvent()
        {
            Type = EventType.SmokeNadeStarted;
        }
    }

    [ProtoContract]
    public class WeaponFiredEvent : BaseEvent
    {
        [ProtoMember(1)]
        public Weapon Weapon { get; set; }
        [ProtoMember(2)]
        public long ShooterSteamID { get; set; }

        public WeaponFiredEvent()
        {
            Type = EventType.WeaponFired;
        }
    }

    [ProtoContract]
    public class PlayerJumpEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long JumperSteamID{ get; set; }

        public PlayerJumpEvent()
        {
            Type = EventType.PlayerJump;
        }
    }

    [ProtoContract]
    public class PlayerFootstepEvent : BaseEvent
    {
        [ProtoMember(1)]
        public long PlayerSteamID { get; set; }

        public PlayerFootstepEvent()
        {
            Type = EventType.PlayerJump;
        }
    }

    [ProtoContract]
    public class OtherDeathEvent : BaseEvent
    {
        [ProtoMember(1)]
        public Weapon Weapon { get; internal set; }

        [ProtoMember(2)]
        public int EntityID { get; internal set; }

        [ProtoMember(3)]
        public Entity Entity { get; internal set; }

        [ProtoMember(4)]
        public string EntityType { get; internal set; }

        [ProtoMember(5)]
        public long KillerSteamID { get; internal set; }

        [ProtoMember(6)]
        public int PenetratedObjects { get; internal set; }

        [ProtoMember(7)]
        public bool Headshot { get; internal set; }

        public OtherDeathEvent()
        {
            Type = EventType.OtherDeath;
        }
    }

    [ProtoContract]
    public class EntitySpawnedEvent : BaseEvent
    {
        [ProtoMember(1)]
        public int EntityID { get; internal set; }

        [ProtoMember(2)]
        public Entity Entity { get; internal set; }

        public EntitySpawnedEvent()
        {
            Type = EventType.EntitySpawned;
        }
    }

    [ProtoContract]
    public class EntityRemovedEvent : BaseEvent
    {
        [ProtoMember(1)]
        public int EntityID { get; internal set; }

        [ProtoMember(2)]
        public Entity Entity { get; internal set; }

        public EntityRemovedEvent()
        {
            Type = EventType.EntityRemoved;
        }
    }
}
