using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
namespace SCP_343
{
    public class EventHandlers
    {
        public static System.Random random = new System.Random();
        public static List<Player> scp343 = new List<Player>();
        public void OnWaitingForPlayers() => scp343.Clear();
        public void OnRoundStarted()
        {
            List<Player> classd = Player.List.Where(x => x.Team == Team.CDP).ToList();
            if (random.Next(101) <= SCP_343.plugin.Config.SpawnChance && classd.Count() >= SCP_343.plugin.Config.MinimumClassD)
                SpawnScp343(classd.FirstOrDefault());
        }
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (scp343.Contains(ev.Player) && !ev.IsAllowed && ev.Door.ActiveLocks == 0)
            {
                if (Round.ElapsedTime.TotalSeconds >= SCP_343.plugin.Config.TimeToOpenDoors)
                    ev.IsAllowed = true;
                else if (SCP_343.plugin.Config.SCP343WaitBroadcast != "")
                {
                    ev.Player.ClearBroadcasts();
                    ev.Player.Broadcast(5, SCP_343.plugin.Config.SCP343WaitBroadcast.Replace("{time}", (SCP_343.plugin.Config.TimeToOpenDoors - (int)Round.ElapsedTime.TotalSeconds).ToString()));
                }
            }
        }
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
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
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (scp343.Contains(ev.Target))
                ev.IsAllowed = false;
        }
        public void OnEscaping(EscapingEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnDetonated()
        {
            foreach (Player scp343 in scp343.Where(x => x.CurrentRoom.Zone != ZoneType.Surface))
                scp343.Position = Exiled.API.Extensions.Role.GetRandomSpawnPoint(RoleType.ChaosInsurgency);
        }
        public void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            foreach (Player scp343 in scp343.Where(x => x.CurrentRoom.Zone == ZoneType.LightContainment))
                scp343.Position = Exiled.API.Extensions.Role.GetRandomSpawnPoint(RoleType.Scp096);
        }
        public void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsTriggerable = false;
        }
        public void OnContaining(ContainingEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            if (scp343.Count() > 0)
            {
                if (ev.ClassList.scps_except_zombies + ev.ClassList.zombies > 0 && ev.ClassList.mtf_and_guards == 0 && ev.ClassList.scientists == 0 && ev.ClassList.chaos_insurgents >= 0 && ev.ClassList.class_ds == scp343.Count())
                {
                    ev.IsAllowed = true;
                    ev.IsRoundEnded = true;
                }
                else if (ev.ClassList.scps_except_zombies + ev.ClassList.zombies == 0 && ev.ClassList.mtf_and_guards >= 0 && ev.ClassList.scientists >= 0 && ev.ClassList.chaos_insurgents == 0 && ev.ClassList.class_ds == scp343.Count())
                {
                    ev.IsAllowed = true;
                    ev.IsRoundEnded = true;
                }
            }
        }
        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (scp343.Contains(ev.Player) && !ev.IsEscaped)
                KillScp343(ev.Player);
        }
        public void OnDestroying(DestroyingEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                KillScp343(ev.Player);
        }
        public static void SpawnScp343(Player player)
        {
            player.SetRole(RoleType.ClassD);
            player.CustomInfo = SCP_343.plugin.Config.RoleName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            Scp173.TurnedPlayers.Add(player);
            Scp096.TurnedPlayers.Add(player);
            player.IsUsingStamina = false;
            scp343.Add(player);
            if (SCP_343.plugin.Config.SCP343SpawnBroadcast != "")
            {
                player.ClearBroadcasts();
                player.Broadcast(15, SCP_343.plugin.Config.SCP343SpawnBroadcast.Replace("{time}", SCP_343.plugin.Config.TimeToOpenDoors.ToString()));
            }
            if (SCP_343.plugin.Config.BroadcastForAll != "")
            {
                foreach (Player player_to_broadcast in Player.List.Where(x => !scp343.Contains(x)))
                {
                    player_to_broadcast.ClearBroadcasts();
                    player_to_broadcast.Broadcast(15, SCP_343.plugin.Config.BroadcastForAll);
                }
            }
        }
        public static void KillScp343(Player player)
        {
            player.CustomInfo = string.Empty;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
            Scp173.TurnedPlayers.Remove(player);
            Scp096.TurnedPlayers.Remove(player);
            player.IsUsingStamina = true;
            scp343.Remove(player);
            if (SCP_343.plugin.Config.DieCassie != "")
                Cassie.DelayedMessage(SCP_343.plugin.Config.DieCassie, 1);
            if (SCP_343.plugin.Config.DieBroadcast != "")
            {
                Map.ClearBroadcasts();
                Map.Broadcast(15, SCP_343.plugin.Config.DieBroadcast);
            }
        }
        public void OnHurting(HurtingEventArgs ev)
        {
            if (scp343.Contains(ev.Target))
                ev.IsAllowed = false;
        }
        public void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (scp343.Contains(ev.Player))
                ev.IsAllowed = false;
        }
    }
}
