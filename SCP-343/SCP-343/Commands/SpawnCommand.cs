using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;
namespace SCP_343.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnCommand : ICommand
    {
        public string Command { get; } = "spawn343";
        public string[] Aliases { get; } = new string[] { "spawn343" };
        public string Description { get; } = "Spawn SCP-343.";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("scp343.spawn"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            if (arguments.Count != 1)
            {
                response = "Usage: spawn343 (player id / name)!";
                return false;
            }
            if (arguments.Count == 1)
            {
                Player scp343 = Player.Get(arguments.At(0));
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
                response = "Done!";
            }
            else
            {
                response = "Error!";
                return false;
            }
            return true;
        }
    }
}