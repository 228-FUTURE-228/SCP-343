using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
namespace SCP_343
{
    public class EventHandlers
    {
        public static System.Random random = new System.Random();
        public void OnRoundStarted()
        {
            if (Chance(SCP_343.plugin.Config.SpawnChance) && Player.List.Where(x => x.Team == Team.CDP).Count() >= SCP_343.plugin.Config.MinimumClassD)
            {
                Player scp343 = Player.List.Where(x => x.Team == Team.CDP).ToList()[random.Next(Player.List.Where(x => x.Team == Team.CDP).Count())];
                scp343.SetRole(RoleType.ClassD);
                scp343.CustomInfo = SCP_343.plugin.Config.RoleName;
                scp343.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
                scp343.IsGodModeEnabled = true;
                scp343.Ammo[(int)AmmoType.Nato556] = 0;
                scp343.Ammo[(int)AmmoType.Nato762] = 0;
                scp343.Ammo[(int)AmmoType.Nato9] = 0;
                Scp173.TurnedPlayers.Add(scp343);
                Scp096.TurnedPlayers.Add(scp343);
                if (SCP_343.plugin.Config.SCP343SpawnBroadcast != "")
                {
                    scp343.ClearBroadcasts();
                    scp343.Broadcast(15, SCP_343.plugin.Config.SCP343SpawnBroadcast.Replace("{time}", SCP_343.plugin.Config.TimeToOpenDoors.ToString()));
                }
                if (SCP_343.plugin.Config.BroadcastForAll != "")
                {
                    foreach (Player player in Player.List.Where(x => x.CustomInfo != SCP_343.plugin.Config.RoleName))
                    {
                        player.ClearBroadcasts();
                        player.Broadcast(15, SCP_343.plugin.Config.BroadcastForAll);
                    }
                }
            }
        }
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName && !ev.IsAllowed && ev.Door.ActiveLocks == 0)
            {
                if (Round.ElapsedTime.TotalSeconds >= SCP_343.plugin.Config.TimeToOpenDoors)
                    ev.IsAllowed = true;
                else if (SCP_343.plugin.Config.SCP343WaitBroadcast != "")
                {
                    ev.Player.ClearBroadcasts();
                    ev.Player.Broadcast(15, SCP_343.plugin.Config.SCP343WaitBroadcast.Replace("{time}", (SCP_343.plugin.Config.TimeToOpenDoors - (int)Round.ElapsedTime.TotalSeconds).ToString()));
                }
            }
        }
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
            {
                ev.IsAllowed = false;
                if (SCP_343.plugin.Config.ConvertToPainkillers)
                {
                    ev.Pickup.Delete();
                    ev.Player.AddItem(ItemType.Painkillers);
                }
            }
        }
        public void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (ev.Target.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnDetonated()
        {
            foreach (Player scp343 in Player.List.Where(x => x.CustomInfo == SCP_343.plugin.Config.RoleName))
                scp343.Position = Exiled.API.Extensions.Role.GetRandomSpawnPoint(RoleType.ChaosInsurgency);
        }
        public void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            foreach (Player scp343 in Player.List.Where(x => x.CustomInfo == SCP_343.plugin.Config.RoleName))
                scp343.Position = Exiled.API.Extensions.Role.GetRandomSpawnPoint(RoleType.Scp096);
        }
        public void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsTriggerable = false;
        }
        public void OnContaining(ContainingEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
                ev.IsAllowed = false;
        }
        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (ev.ClassList.scps_except_zombies + ev.ClassList.zombies > 0 && ev.ClassList.mtf_and_guards == 0 && ev.ClassList.scientists == 0 && ev.ClassList.chaos_insurgents >= 0 && ev.ClassList.class_ds == Player.List.Where(x => x.CustomInfo == SCP_343.plugin.Config.RoleName).Count())
            {
                ev.IsAllowed = true;
                ev.IsRoundEnded = true;
            }
            else if (ev.ClassList.scps_except_zombies + ev.ClassList.zombies == 0 && ev.ClassList.mtf_and_guards >= 0 && ev.ClassList.scientists >= 0 && ev.ClassList.chaos_insurgents == 0 && ev.ClassList.class_ds == Player.List.Where(x => x.CustomInfo == SCP_343.plugin.Config.RoleName).Count())
            {
                ev.IsAllowed = true;
                ev.IsRoundEnded = true;
            }
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName && !ev.IsEscaped)
            {
                ev.Player.CustomInfo = string.Empty;
                ev.Player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
                ev.Player.IsGodModeEnabled = false;
                Scp173.TurnedPlayers.Remove(ev.Player);
                Scp096.TurnedPlayers.Remove(ev.Player);
                if (SCP_343.plugin.Config.DieCassie != "")
                    Cassie.DelayedMessage(SCP_343.plugin.Config.DieCassie, 1);
                if (SCP_343.plugin.Config.DieBroadcast != "")
                {
                    Map.ClearBroadcasts();
                    Map.Broadcast(15, SCP_343.plugin.Config.DieBroadcast);
                }
            }
        }
        public void OnDestroying(DestroyingEventArgs ev)
        {
            if (ev.Player.CustomInfo == SCP_343.plugin.Config.RoleName)
            {
                ev.Player.CustomInfo = string.Empty;
                ev.Player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
                ev.Player.IsGodModeEnabled = false;
                Scp173.TurnedPlayers.Remove(ev.Player);
                Scp096.TurnedPlayers.Remove(ev.Player);
                if (SCP_343.plugin.Config.DieCassie != "")
                    Cassie.DelayedMessage(SCP_343.plugin.Config.DieCassie, 1);
                if (SCP_343.plugin.Config.DieBroadcast != "")
                {
                    Map.ClearBroadcasts();
                    Map.Broadcast(15, SCP_343.plugin.Config.DieBroadcast);
                }
            }
        }
        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Target.CustomInfo == SCP_343.plugin.Config.RoleName && (ev.HitInformations.GetDamageType() == DamageTypes.Decont || (ev.DamageType == DamageTypes.Nuke && Warhead.IsDetonated)))
                ev.IsAllowed = false;
        }
        public bool Chance(int percent)
        {
            if (random.Next(101) <= percent)
                return true;
            else
                return false;
        }
    }
}