using DemoMinifier.Models;
using DemoMinifier.Models.Events;
using DemoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DemoMinifier
{
    public class Minifier
    {
        DemoParser Parser;
        MinifiedDemo Demo;
        Action<string, float> ProgressCallback;

        Tick CurrentTick;

        byte CurrentSteamIDIndex = 0;
        byte CurrentNameIndex = 0;

        Dictionary<byte, FullPlayerState> MostRecentPlayerStates;

        public async Task<MinifiedDemo> MinifyDemoAsync(string path, CancellationToken token, Action<string, float> progressCallback = null)
        {
            FileStream stream = File.OpenRead(path);
            Parser = new DemoParser(stream);
            Demo = new MinifiedDemo();
            CurrentTick = new Tick();

            MostRecentPlayerStates = new Dictionary<byte, FullPlayerState>();

            ProgressCallback = progressCallback;

            RegisterEvents();

            ParseHeader();

            await Task.Run(() => Parser.ParseToEnd(token), token);
            stream.Dispose();

            return Demo;
        }

        public void Dispose()
        {
            Parser.Dispose();
            Demo = null;
            CurrentTick = null;
            MostRecentPlayerStates = null;
            ProgressCallback = null;
        }

        private void ParseHeader()
        {
            Parser.ParseHeader();

            Demo.ServerName = Parser.Header.ServerName;
            Demo.ClientName = Parser.Header.ClientName;
            Demo.MapName = Parser.Header.MapName;
            Demo.PlaybackTime = Parser.Header.PlaybackTime;
            Demo.PlaybackTicks = Parser.Header.PlaybackTicks;
            Demo.PlaybackFrames = Parser.Header.PlaybackFrames;
        }

        private void RegisterEvents()
        {
            Parser.MatchStarted += HandleMatchStarted;
            Parser.RoundAnnounceMatchStarted += HandleRoundAnnounceMatchStarted;
            Parser.RoundStart += HandleRoundStart;
            Parser.RoundEnd += HandleRoundEnd;
            Parser.WinPanelMatch += HandleWinPanelMatch;
            Parser.RoundFinal += HandleRoundFinal;
            Parser.LastRoundHalf += HandleLastRoundHalf;
            Parser.RoundOfficiallyEnd += HandleRoundOfficiallyEnd;
            Parser.RoundMVP += HandleRoundMVP;
            Parser.FreezetimeEnded += HandleFreezetimeEnded;
            Parser.PlayerKilled += HandlePlayerKilled;
            Parser.PlayerTeam += HandlePlayerTeam;
            Parser.WeaponFired += HandleWeaponFired;
            Parser.SmokeNadeStarted += HandleSmokeNadeStarted;
            Parser.SmokeNadeEnded += HandleSmokeNadeEnded;
            Parser.DecoyNadeStarted += HandleDecoyNadeStarted;
            Parser.DecoyNadeEnded += HandleDecoyNadeEnded;
            Parser.FireNadeStarted += HandleFireNadeStarted;
            Parser.FireNadeWithOwnerStarted += HandleFireNadeWithOwnerStarted;
            Parser.FireNadeEnded += HandleFireNadeEnded;
            Parser.FlashNadeExploded += HandleFlashNadeExploded;
            Parser.ExplosiveNadeExploded += HandleExplosiveNadeExploded;
            Parser.NadeReachedTarget += HandleNadeReachedTarget;
            Parser.BombBeginPlant += HandleBombBeginPlant;
            Parser.BombAbortPlant += HandleBombAbortPlant;
            Parser.BombPlanted += HandleBombPlanted;
            Parser.BombDefused += HandleBombDefused;
            Parser.BombExploded += HandleBombExploded;
            Parser.BombBeginDefuse += HandleBombBeginDefuse;
            Parser.BombAbortDefuse += HandleBombAbortDefuse;
            Parser.PlayerHurt += HandlePlayerHurt;
            Parser.Blind += HandleBlind;
            Parser.PlayerBind += HandlePlayerBind;
            Parser.PlayerDisconnect += HandlePlayerDisconnect;
            Parser.SayText += HandleSayText;
            Parser.SayText2 += HandleSayText2;
            Parser.RankUpdate += HandleRankUpdate;

            Parser.TickDone += HandleTickDone;
        }

        private void HandleTickDone(object sender, TickDoneEventArgs e)
        {
            //Store the names and steam IDs for every steam account in the server, including casters, bots and referees
            foreach (var participant in Parser.Participants)
            {
                byte NameIndex = 0;
                if (Demo.NameMappings.ContainsValue(participant.Name))
                {
                    NameIndex = Demo.NameMappings.FirstOrDefault(x => x.Value == participant.Name).Key;
                }
                else
                {
                    NameIndex = CurrentNameIndex;
                    Demo.NameMappings.Add(NameIndex, participant.Name);

                    CurrentNameIndex++;
                }

                byte SteamIDIndex = 0;
                if (Demo.SteamIDMappings.ContainsValue(participant.SteamID))
                {
                    SteamIDIndex = Demo.SteamIDMappings.FirstOrDefault(x => x.Value == participant.SteamID).Key;
                }
                else
                {
                    SteamIDIndex = CurrentSteamIDIndex;
                    Demo.SteamIDMappings.Add(SteamIDIndex, participant.SteamID);

                    CurrentSteamIDIndex++;
                }
            }

            //Loop though players that are actually playing the match
            foreach (var player in Parser.PlayingParticipants)
            {
                byte NameIndex = 0;
                if (Demo.NameMappings.ContainsValue(player.Name))
                {
                    NameIndex = Demo.NameMappings.FirstOrDefault(x => x.Value == player.Name).Key;
                }
                else
                {
                    NameIndex = CurrentNameIndex;
                    Demo.NameMappings.Add(NameIndex, player.Name);

                    CurrentNameIndex++;
                }

                byte SteamIDIndex = 0;
                if (Demo.SteamIDMappings.ContainsValue(player.SteamID))
                {
                    SteamIDIndex = Demo.SteamIDMappings.FirstOrDefault(x => x.Value == player.SteamID).Key;
                }
                else
                {
                    SteamIDIndex = CurrentSteamIDIndex;
                    Demo.SteamIDMappings.Add(SteamIDIndex, player.SteamID);

                    CurrentSteamIDIndex++;
                }

                Dictionary<byte, byte> AmmoLeft = new Dictionary<byte, byte>();
                for (int i = 0; i < 32; i++)
                {
                    if (player.AmmoLeft[i] != 0)
                        AmmoLeft.Add((byte)i, (byte)player.AmmoLeft[i]);
                }

                Dictionary<short, Weapon> Weapons = new Dictionary<short, Weapon>();
                foreach (var pair in player.rawWeapons)
                {
                    Weapons.Add((short)pair.Key, new Weapon()
                    {
                        Equipment = (Models.EquipmentElement)((int)pair.Value.Weapon),
                        OriginalString = pair.Value.OriginalString,
                        AmmoInMagazine = (short)pair.Value.AmmoInMagazine,
                        AmmoType = (short)pair.Value.AmmoType,
                        ZoomLevel = (byte)pair.Value.ZoomLevel
                    });
                }

                FullPlayerState fullPlayerState = new FullPlayerState()
                {
                    Type = StateType.Full,
                    NameIndex = NameIndex,
                    SteamIDIndex = SteamIDIndex,
                    Position = new Models.Vector(player.Position.X, player.Position.Y, player.Position.Z),
                    ViewOffsetZ = player.ViewOffset.Z,
                    HP = (byte)player.HP,
                    Armor = (byte)player.Armor,
                    Velocity = new Models.Vector(player.Velocity.X, player.Velocity.Y, player.Velocity.Z),
                    ViewDirectionX = player.ViewDirectionX,
                    ViewDirectionY = player.ViewDirectionY,
                    FlashDuration = player.FlashDuration,
                    Money = (short)player.Money,
                    CurrentEquipmentValue = (short)player.CurrentEquipmentValue,
                    FreezetimeEndEquipmentValue = (short)player.FreezetimeEndEquipmentValue,
                    RoundStartEquipmentValue = (short)player.RoundStartEquipmentValue,
                    IsDucking = player.IsDucking,
                    ActiveWeaponID = (byte)player.ActiveWeaponID,
                    Team = (Models.Team)((int)player.Team),
                    HasDefuseKit = player.HasDefuseKit,
                    HasHelmet = player.HasHelmet,
                    AmmoLeft = AmmoLeft,
                    Weapons = Weapons,
                    IsScoped = player.IsScoped,
                    ShotsFired = player.ShotsFired,
                    AimPunchAngle = new Models.Vector(player.AimPunchAngle.X, player.AimPunchAngle.Y, player.AimPunchAngle.Z),
                };

                PositionPlayerState positionPlayerState = new PositionPlayerState(fullPlayerState);

                bool positionStateEqual = false;
                bool fullStateEqual = false;

                if (MostRecentPlayerStates.ContainsKey(SteamIDIndex))
                {
                    FullPlayerState mostRecentFullState = MostRecentPlayerStates[SteamIDIndex];

                    positionStateEqual = positionPlayerState.Equals(mostRecentFullState);
                    fullStateEqual = fullPlayerState.Equals(mostRecentFullState);
                }

                if (fullStateEqual && positionStateEqual)
                {
                    CurrentTick.PlayerStates.Add(new BasePlayerState() { Type = StateType.Base, SteamIDIndex = SteamIDIndex, NameIndex = NameIndex });
                }
                else
                {
                    MostRecentPlayerStates[SteamIDIndex] = fullPlayerState;

                    if (fullStateEqual && !positionStateEqual)
                        CurrentTick.PlayerStates.Add(positionPlayerState);
                    else
                        CurrentTick.PlayerStates.Add(fullPlayerState);
                }
            }
            
            Demo.Ticks.Add(CurrentTick);
            CurrentTick = new Tick();
        }

        private void HandleMatchStarted(object sender, MatchStartedEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.MatchStarted);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundAnnounceMatchStarted(object sender, RoundAnnounceMatchStartedEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.RoundAnnounceMatchStarted);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundStart(object sender, RoundStartedEventArgs e)
        {
            RoundStartEvent newEvent = new RoundStartEvent()
            {
                TimeLimit = e.TimeLimit,
                FragLimit = e.FragLimit,
                Objective = e.Objective,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundEnd(object sender, RoundEndedEventArgs e)
        {
            RoundEndEvent newEvent = new RoundEndEvent()
            {
                Reason = (Models.RoundEndReason)((int)e.Reason),
                Message = e.Message,
                Winner = (Models.Team)((int)e.Winner),
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleWinPanelMatch(object sender, WinPanelMatchEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.WinPanelMatch);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundFinal(object sender, RoundFinalEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.RoundFinal);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleLastRoundHalf(object sender, LastRoundHalfEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.LastRoundHalf);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundOfficiallyEnd(object sender, RoundOfficiallyEndedEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.RoundOfficiallyEnd);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRoundMVP(object sender, RoundMVPEventArgs e)
        {
            RoundMVPEvent newEvent = new RoundMVPEvent()
            {
                Reason = (Models.RoundMVPReason)((int)e.Reason),
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleFreezetimeEnded(object sender, FreezetimeEndedEventArgs e)
        {
            BaseEvent newEvent = new BaseEvent(EventType.FreezetimeEnded);
            CurrentTick.Events.Add(newEvent);
        }

        private void HandlePlayerKilled(object sender, PlayerKilledEventArgs e)
        {
            if (e.Victim == null)
                return;

            Weapon weapon = new Weapon()
            {
                Equipment = (Models.EquipmentElement)((int)e.Weapon.Weapon),
                OriginalString = e.Weapon.OriginalString,
                AmmoInMagazine = (short)e.Weapon.AmmoInMagazine,
                AmmoType = (short)e.Weapon.AmmoType,
            };

            PlayerKilledEvent newEvent = new PlayerKilledEvent()
            {
                Weapon = weapon,
                VictimSteamID = e.Victim.SteamID,
                KillerSteamID = e.Killer?.SteamID,
                AssisterSteamID = e.Assister?.SteamID,
                PenetratedObjects = e.PenetratedObjects,
                Headshot = e.Headshot,
                AssistedFlash = e.AssistedFlash,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandlePlayerTeam(object sender, PlayerTeamEventArgs e)
        {
            if (e.Swapped == null)
                return;

            PlayerTeamEvent newEvent = new PlayerTeamEvent()
            {
                SwappedSteamID = e.Swapped.SteamID,
                NewTeam = (Models.Team)((int)e.NewTeam),
                OldTeam = (Models.Team)((int)e.OldTeam),
                Silent = e.Silent,
                IsBot = e.IsBot,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleWeaponFired(object sender, WeaponFiredEventArgs e)
        {
            if (e.Shooter == null)
                return;

            Weapon weapon = new Weapon()
            {
                Equipment = (Models.EquipmentElement)((int)e.Weapon.Weapon),
                OriginalString = e.Weapon.OriginalString,
                AmmoInMagazine = (short)e.Weapon.AmmoInMagazine,
                AmmoType = (short)e.Weapon.AmmoType,
            };

            WeaponFiredEvent newEvent = new WeaponFiredEvent()
            {
                Weapon = weapon,
                ShooterSteamID = e.Shooter.SteamID,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleSmokeNadeStarted(object sender, SmokeEventArgs e)
        {
            SmokeNadeStartedEvent newEvent = new SmokeNadeStartedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleSmokeNadeEnded(object sender, SmokeEventArgs e)
        {
            SmokeNadeEndedEvent newEvent = new SmokeNadeEndedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleDecoyNadeStarted(object sender, DecoyEventArgs e)
        {
            DecoyNadeStartedEvent newEvent = new DecoyNadeStartedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleDecoyNadeEnded(object sender, DecoyEventArgs e)
        {
            DecoyNadeEndedEvent newEvent = new DecoyNadeEndedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleFireNadeStarted(object sender, FireEventArgs e)
        {
            FireNadeStartedEvent newEvent = new FireNadeStartedEvent()
            {
                ThrownBySteamID = e.ThrownBy?.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleFireNadeWithOwnerStarted(object sender, FireEventArgs e)
        {
            FireNadeWithOwnerStartedEvent newEvent = new FireNadeWithOwnerStartedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleFireNadeEnded(object sender, FireEventArgs e)
        {
            FireNadeEndedEvent newEvent = new FireNadeEndedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleFlashNadeExploded(object sender, FlashEventArgs e)
        {
            FlashNadeExplodedEvent newEvent = new FlashNadeExplodedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleExplosiveNadeExploded(object sender, GrenadeEventArgs e)
        {
            ExplosiveNadeExplodedEvent newEvent = new ExplosiveNadeExplodedEvent()
            {
                ThrownBySteamID = e.ThrownBy.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleNadeReachedTarget(object sender, NadeEventArgs e)
        {
            NadeReachedTargetEvent newEvent = new NadeReachedTargetEvent()
            {
                ThrownBySteamID = e.ThrownBy?.SteamID,
                NadeType = (Models.EquipmentElement)((int)e.NadeType),
                Position = new Models.Vector(e.Position.X, e.Position.Y, e.Position.Z)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombBeginPlant(object sender, BombEventArgs e)
        {
            BombBeginPlantEvent newEvent = new BombBeginPlantEvent()
            {
                SteamID = e.Player.SteamID,
                Site = e.Site
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombAbortPlant(object sender, BombEventArgs e)
        {
            BombAbortPlantEvent newEvent = new BombAbortPlantEvent()
            {
                SteamID = e.Player.SteamID,
                Site = e.Site
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombPlanted(object sender, BombEventArgs e)
        {
            BombPlantedEvent newEvent = new BombPlantedEvent()
            {
                SteamID = e.Player.SteamID,
                Site = e.Site
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombDefused(object sender, BombEventArgs e)
        {
            BombDefusedEvent newEvent = new BombDefusedEvent()
            {
                SteamID = e.Player.SteamID,
                Site = e.Site
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombExploded(object sender, BombEventArgs e)
        {
            BombExplodedEvent newEvent = new BombExplodedEvent()
            {
                SteamID = e.Player?.SteamID,
                Site = e.Site
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombBeginDefuse(object sender, BombDefuseEventArgs e)
        {
            BombBeginDefuseEvent newEvent = new BombBeginDefuseEvent()
            {
                SteamID = e.Player.SteamID,
                HasKit = e.HasKit
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBombAbortDefuse(object sender, BombDefuseEventArgs e)
        {
            BombAbortDefuseEvent newEvent = new BombAbortDefuseEvent()
            {
                SteamID = e.Player.SteamID,
                HasKit = e.HasKit
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandlePlayerHurt(object sender, PlayerHurtEventArgs e)
        {
            Weapon weapon = new Weapon()
            {
                Equipment = (Models.EquipmentElement)((int)e.Weapon.Weapon),
                OriginalString = e.Weapon.OriginalString,
                AmmoInMagazine = (short)e.Weapon.AmmoInMagazine,
                AmmoType = (short)e.Weapon.AmmoType,
            };

            PlayerHurtEvent newEvent = new PlayerHurtEvent()
            {
                PlayerSteamID = e.Player.SteamID,
                AttackerSteamID = e.Attacker?.SteamID,
                Health = e.Health,
                Armor = e.Armor,
                Weapon = weapon,
                WeaponString = e.WeaponString,
                HealthDamage = e.HealthDamage,
                ArmorDamage = e.ArmorDamage,
                Hitgroup = (Models.Hitgroup)((int)e.Hitgroup)
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleBlind(object sender, BlindEventArgs e)
        {
            BlindEvent newEvent = new BlindEvent()
            {
                PlayerSteamID = e.Player.SteamID,
                AttackerSteamID = e.Player.SteamID,
                FlashDuration = e.FlashDuration
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandlePlayerBind(object sender, PlayerBindEventArgs e)
        {
            PlayerBindEvent newEvent = new PlayerBindEvent()
            {
                SteamID = e.Player.SteamID
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandlePlayerDisconnect(object sender, PlayerDisconnectEventArgs e)
        {
            if (e.Player == null)
                return;

            PlayerDisconnectEvent newEvent = new PlayerDisconnectEvent()
            {
                SteamID = e.Player.SteamID
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleSayText(object sender, SayTextEventArgs e)
        {
            SayTextEvent newEvent = new SayTextEvent()
            {
                EntityIndex = e.EntityIndex,
                Text = e.Text,
                IsChat = e.IsChat,
                IsChatAll = e.IsChatAll,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleSayText2(object sender, SayText2EventArgs e)
        {
            if (e.Sender == null)
                return;

            SayText2Event newEvent = new SayText2Event()
            {
                SenderSteamID = e.Sender.SteamID,
                Text = e.Text,
                IsChat = e.IsChat,
                IsChatAll = e.IsChatAll,
            };

            CurrentTick.Events.Add(newEvent);
        }

        private void HandleRankUpdate(object sender, RankUpdateEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
