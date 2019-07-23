using DemoMinifier.Models;
using DemoMinifier.Models.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DemoMinifier
{
    public class MinifiedParser
    {
        public MinifiedDemo Demo;

        #region Events
        /// <summary>
        /// Occurs when the match started, so when the "begin_new_match"-GameEvent is dropped. 
        /// This usually right before the freezetime of the 1st round. Be careful, since the players
        /// usually still have warmup-money when this drops.
        /// </summary>
        public event EventHandler<BaseEvent> MatchStarted;

        /// <summary>
        /// Occurs when the first round of a new match start "round_announce_match_start"
        /// </summary>
        //public event EventHandler<RoundAnnounceMatchStartedEvent> RoundAnnounceMatchStarted;

        /// <summary>
        /// Occurs when round starts, on the round_start event of the demo. Usually the players haven't spawned yet, but have recieved the money for the next round. 
        /// </summary>
        public event EventHandler<RoundStartEvent> RoundStart;

        /// <summary>
        /// Occurs when round ends
        /// </summary>
        public event EventHandler<RoundEndEvent> RoundEnd;

        /// <summary>
        /// Occurs at the end of the match, when the scoreboard is shown
        /// </summary>
        public event EventHandler<BaseEvent> WinPanelMatch;

        /// <summary>
        /// Occurs when it's the last round of a match
        /// </summary>
        //public event EventHandler<RoundFinalEvent> RoundFinal;

        /// <summary>
        /// Occurs at the half of a side
        /// </summary>
        //public event EventHandler<LastRoundHalfEvent> LastRoundHalf;

        /// <summary>
        /// Occurs when round really ended
        /// </summary>
        public event EventHandler<BaseEvent> RoundOfficiallyEnd;

        /// <summary>
        /// Occurs on round end with the MVP
        /// </summary>
        public event EventHandler<RoundMVPEvent> RoundMVP;

        /// <summary>
        /// Occurs when freezetime ended. Raised on "round_freeze_end" 
        /// </summary>
        public event EventHandler<BaseEvent> FreezetimeEnded;

        /// <summary>
        /// Occurs on the end of every tick, after the gameevents were processed and the packet-entities updated
        /// </summary>
        public event EventHandler TickDone;

        /// <summary>
        /// This is raised when a player is killed. Not that the killer might be dead by the time is raised (e.g. nade-kills),
        /// also note that the killed player is still alive when this is killed
        /// </summary>
        public event EventHandler<PlayerKilledEvent> PlayerKilled;

		/// <summary>
		/// Occurs when a player select a team
		/// </summary>
		public event EventHandler<PlayerTeamEvent> PlayerTeam;

		/// <summary>
		/// Occurs when a weapon is fired.
		/// </summary>
		public event EventHandler<WeaponFiredEvent> WeaponFired;

		/// <summary>
		/// Occurs when smoke nade started.
		/// </summary>
		public event EventHandler<SmokeNadeStartedEvent> SmokeNadeStarted;

		/// <summary>
		/// Occurs when smoke nade ended. 
		/// Hint: When a round ends, this is *not* caĺled. 
		/// Make sure to clear nades yourself at the end of rounds
		/// </summary>
		public event EventHandler<SmokeNadeEndedEvent> SmokeNadeEnded;

		/// <summary>
		/// Occurs when decoy nade started.
		/// </summary>
		public event EventHandler<DecoyNadeStartedEvent> DecoyNadeStarted;

		/// <summary>
		/// Occurs when decoy nade ended. 
		/// Hint: When a round ends, this is *not* caĺled. 
		/// Make sure to clear nades yourself at the end of rounds
		/// </summary>
		public event EventHandler<DecoyNadeEndedEvent> DecoyNadeEnded;

		/// <summary>
		/// Occurs when a fire nade (incendiary / molotov) started. 
		/// This currently *doesn't* contain who it threw since this is for some weird reason not networked
		/// </summary>
		public event EventHandler<FireNadeStartedEvent> FireNadeStarted;

		/// <summary>
		/// FireNadeStarted, but with correct ThrownBy player.
		/// Hint: Raised at the end of inferno_startburn tick instead of exactly when the event is parsed
		/// </summary>
		public event EventHandler<FireNadeWithOwnerStartedEvent> FireNadeWithOwnerStarted;

		/// <summary>
		/// Occurs when fire nade ended.
		/// Hint: When a round ends, this is *not* caĺled. 
		/// Make sure to clear nades yourself at the end of rounds
		/// </summary>
		public event EventHandler<FireNadeEndedEvent> FireNadeEnded;

		/// <summary>
		/// Occurs when flash nade exploded.
		/// </summary>
		public event EventHandler<FlashNadeExplodedEvent> FlashNadeExploded;

		/// <summary>
		/// Occurs when explosive nade exploded.
		/// </summary>
		public event EventHandler<ExplosiveNadeExplodedEvent> ExplosiveNadeExploded;

		/// <summary>
		/// Occurs when any nade reached it's target.
		/// </summary>
		public event EventHandler<NadeReachedTargetEvent> NadeReachedTarget;

		/// <summary>
		/// Occurs when bomb is being planted.
		/// </summary>
		public event EventHandler<BombBeginPlantEvent> BombBeginPlant;

		/// <summary>
		/// Occurs when the plant is aborted
		/// </summary>
		public event EventHandler<BombAbortPlantEvent> BombAbortPlant;

		/// <summary>
		/// Occurs when the bomb has been planted.
		/// </summary>
		public event EventHandler<BombPlantedEvent> BombPlanted;

		/// <summary>
		/// Occurs when the bomb has been defused.
		/// </summary>
		public event EventHandler<BombDefusedEvent> BombDefused;

		/// <summary>
		/// Occurs when bomb has exploded.
		/// </summary>
		public event EventHandler<BombExplodedEvent> BombExploded;

		/// <summary>
		/// Occurs when someone begins to defuse the bomb.
		/// </summary>
		public event EventHandler<BombBeginDefuseEvent> BombBeginDefuse;

		/// <summary>
		/// Occurs when someone aborts to defuse the bomb.
		/// </summary>
		public event EventHandler<BombAbortDefuseEvent> BombAbortDefuse;

		/// <summary>
		/// Occurs when an player is attacked by another player.
		/// Hint: Only occurs in GOTV-demos. 
		/// </summary>
		public event EventHandler<PlayerHurtEvent> PlayerHurt;

		/// <summary>
		/// Occurs when player is blinded by flashbang
		/// Hint: The order of the blind event and FlashNadeExploded event is not always the same
		/// </summary>
		public event EventHandler<BlindEvent> Blind;

		/// <summary>
		/// Occurs when the player object is first updated to reference all the necessary information
		/// Hint: Event will be raised when any player with a SteamID connects, not just PlayingParticipants
		/// </summary>
		public event EventHandler<PlayerBindEvent> PlayerBind;

		/// <summary>
		/// Occurs when a player disconnects from the server. 
		/// </summary>
		public event EventHandler<PlayerDisconnectEvent> PlayerDisconnect;

		/// <summary>
		/// Occurs when the server uses the "say" command
		/// </summary>
		public event EventHandler<SayTextEvent> SayText;

		/// <summary>
		/// Occurs when a player uses the "say" command
		/// </summary>
		public event EventHandler<SayText2Event> SayText2;

		/// <summary>
		/// Occurs when the server display a player rank
		/// </summary>
		public event EventHandler<RankUpdateEvent> RankUpdate;
        #endregion

        public string Map
        {
            get { return Demo.MapName; }
        }

        public MinifiedParser(MinifiedDemo demo)
        {
            Demo = demo;
        }

        public void ParseToEnd()
        {
            for (int i = 0; i < Demo.Ticks.Count; i++)
                ParseNextTick();
        }

        int currentTickIndex = 0;

        Tick currentTick;
        Dictionary<long, Player> currentPlayers = new Dictionary<long, Player>();

        public IEnumerable<Player> PartipatingPlayers { get { return currentPlayers.Values.Where(p => p.Team != Team.Spectate); } }

        public void ParseNextTick()
        {
            if (currentTickIndex >= Demo.Ticks.Count)
                return;

            currentTick = Demo.Ticks[currentTickIndex];

            HandlePlayerStateUpdates();

            HandleCurrentEvents();

            TickDone?.Invoke(this, null);

            currentTickIndex++;
        }

        private void HandlePlayerStateUpdates()
        {
            foreach (BasePlayerState stateUpdate in currentTick.PlayerStates)
            {
                StateType updateType = stateUpdate.Type;

                long steamId = Demo.SteamIDMappings[stateUpdate.SteamIDIndex];
                string name = Demo.NameMappings[stateUpdate.NameIndex];

                Player p;

                //Get from or add current player to dict of known players
                if (currentPlayers.ContainsKey(steamId))
                    p = currentPlayers[steamId];
                else
                {
                    p = new Player()
                    {
                        SteamID = steamId,
                        Name = name
                    };
                    currentPlayers.Add(steamId, p);
                }

                //Update position
                if (updateType == StateType.Position || updateType == StateType.Full)
                {
                    PositionPlayerState positionUpdate = (PositionPlayerState)stateUpdate;

                    p.Position = positionUpdate.Position;
                    p.ViewOffset = new Vector(0, 0, positionUpdate.ViewOffsetZ);
                    p.Velocity = positionUpdate.Velocity;
                    p.ViewDirectionX = positionUpdate.ViewDirectionX;
                    p.ViewDirectionY = positionUpdate.ViewDirectionY;
                }

                //Update full
                if (updateType == StateType.Full)
                {
                    FullPlayerState fullUpdate = (FullPlayerState)stateUpdate;

                    p.HP = fullUpdate.HP;
                    p.Armor = fullUpdate.Armor;
                    p.FlashDuration = fullUpdate.FlashDuration;
                    p.Money = fullUpdate.Money;
                    p.CurrentEquipmentValue = fullUpdate.CurrentEquipmentValue;
                    p.FreezetimeEndEquipmentValue = fullUpdate.FreezetimeEndEquipmentValue;
                    p.RoundStartEquipmentValue = fullUpdate.RoundStartEquipmentValue;
                    p.IsDucking = fullUpdate.IsDucking;
                    p.ActiveWeaponID = fullUpdate.ActiveWeaponID;
                    p.Team = fullUpdate.Team;
                    p.HasDefuseKit = fullUpdate.HasDefuseKit;
                    p.HasHelmet = fullUpdate.HasHelmet;
                    p.RawWeapons = fullUpdate.Weapons;
                    p.IsScoped = fullUpdate.IsScoped;
                    p.ShotsFired = fullUpdate.ShotsFired;
                    p.AimPunchAngle = fullUpdate.AimPunchAngle;

                    p.AmmoLeft = new int[32];
                    for (int i = 0; i < 32; i++)
                    {
                        if (fullUpdate.AmmoLeft.ContainsKey((byte)i))
                            p.AmmoLeft[i] = fullUpdate.AmmoLeft[(byte)i];
                        else
                            p.AmmoLeft[i] = 0;
                    }
                }
            }
        }

        private void HandleCurrentEvents()
        {
            foreach (BaseEvent baseEvent in currentTick.Events)
            {
                switch (baseEvent.Type)
                {
                    case EventType.MatchStarted:
                        MatchStarted?.Invoke(this, baseEvent);
                        break;
                    case EventType.RoundAnnounceMatchStarted:

                        break;
                    case EventType.RoundStart:
                        RoundStart?.Invoke(this, (RoundStartEvent)baseEvent);
                        break;
                    case EventType.RoundEnd:
                        RoundEnd?.Invoke(this, (RoundEndEvent)baseEvent);
                        break;
                    case EventType.WinPanelMatch:
                        WinPanelMatch?.Invoke(this, baseEvent);
                        break;
                    case EventType.RoundFinal:
                        break;
                    case EventType.LastRoundHalf:
                        break;
                    case EventType.RoundOfficiallyEnd:
                        RoundOfficiallyEnd?.Invoke(this, baseEvent);
                        break;
                    case EventType.RoundMVP:
                        RoundMVP?.Invoke(this, (RoundMVPEvent)baseEvent);
                        break;
                    case EventType.FreezetimeEnded:
                        FreezetimeEnded?.Invoke(this, baseEvent);
                        break;
                    case EventType.PlayerKilled:
                        PlayerKilled?.Invoke(this, (PlayerKilledEvent)baseEvent);
                        break;
                    case EventType.PlayerTeam:
                        PlayerTeam?.Invoke(this, (PlayerTeamEvent)baseEvent);
                        break;
                    case EventType.WeaponFired:
                        WeaponFired?.Invoke(this, (WeaponFiredEvent)baseEvent);
                        break;
                    case EventType.SmokeNadeStarted:
                        SmokeNadeStarted?.Invoke(this, (SmokeNadeStartedEvent)baseEvent);
                        break;
                    case EventType.SmokeNadeEnded:
                        SmokeNadeEnded?.Invoke(this, (SmokeNadeEndedEvent)baseEvent);
                        break;
                    case EventType.DecoyNadeStarted:
                        DecoyNadeStarted?.Invoke(this, (DecoyNadeStartedEvent)baseEvent);
                        break;
                    case EventType.DecoyNadeEnded:
                        DecoyNadeEnded?.Invoke(this, (DecoyNadeEndedEvent)baseEvent);
                        break;
                    case EventType.FireNadeStarted:
                        FireNadeStarted?.Invoke(this, (FireNadeStartedEvent)baseEvent);
                        break;
                    case EventType.FireNadeWithOwnerStarted:
                        FireNadeWithOwnerStarted?.Invoke(this, (FireNadeWithOwnerStartedEvent)baseEvent);
                        break;
                    case EventType.FireNadeEnded:
                        FireNadeEnded?.Invoke(this, (FireNadeEndedEvent)baseEvent);
                        break;
                    case EventType.FlashNadeExploded:
                        FlashNadeExploded?.Invoke(this, (FlashNadeExplodedEvent)baseEvent);
                        break;
                    case EventType.ExplosiveNadeExploded:
                        ExplosiveNadeExploded?.Invoke(this, (ExplosiveNadeExplodedEvent)baseEvent);
                        break;
                    case EventType.NadeReachedTarget:
                        NadeReachedTarget?.Invoke(this, (NadeReachedTargetEvent)baseEvent);
                        break;
                    case EventType.BombBeginPlant:
                        BombBeginPlant?.Invoke(this, (BombBeginPlantEvent)baseEvent);
                        break;
                    case EventType.BombAbortPlant:
                        BombAbortPlant?.Invoke(this, (BombAbortPlantEvent)baseEvent);
                        break;
                    case EventType.BombPlanted:
                        BombPlanted?.Invoke(this, (BombPlantedEvent)baseEvent);
                        break;
                    case EventType.BombDefused:
                        BombDefused?.Invoke(this, (BombDefusedEvent)baseEvent);
                        break;
                    case EventType.BombExploded:
                        BombExploded?.Invoke(this, (BombExplodedEvent)baseEvent);
                        break;
                    case EventType.BombBeginDefuse:
                        BombBeginDefuse?.Invoke(this, (BombBeginDefuseEvent)baseEvent);
                        break;
                    case EventType.BombAbortDefuse:
                        BombAbortDefuse?.Invoke(this, (BombAbortDefuseEvent)baseEvent);
                        break;
                    case EventType.PlayerHurt:
                        PlayerHurt?.Invoke(this, (PlayerHurtEvent)baseEvent);
                        break;
                    case EventType.Blind:
                        Blind?.Invoke(this, (BlindEvent)baseEvent);
                        break;
                    case EventType.PlayerBind:
                        PlayerBind?.Invoke(this, (PlayerBindEvent)baseEvent);
                        break;
                    case EventType.PlayerDisconnect:
                        PlayerDisconnect?.Invoke(this, (PlayerDisconnectEvent)baseEvent);
                        break;
                    case EventType.SayText:
                        SayText?.Invoke(this, (SayTextEvent)baseEvent);
                        break;
                    case EventType.SayText2:
                        SayText2?.Invoke(this, (SayText2Event)baseEvent);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
