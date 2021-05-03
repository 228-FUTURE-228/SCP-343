using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
namespace SCP_343.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnCommand : ICommand
    {
        public string Command { get; } = "spawn343";
        public string[] Aliases { get; } = new string[] { "343" };
        public string Description { get; } = "Spawn SCP-343.";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scp343.spawn"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            if (arguments.Count == 1)
            {
                Player scp343 = Player.Get(arguments.At(0));
                if (scp343 != null)
                {
                    EventHandlers.SpawnScp343(scp343);
                    response = $"Player {scp343.Nickname} is now SCP-343.";
                }
                else
                {
                    response = "Player not found!";
                    return false;
                }
            }
            else
            {
                response = "Usage: spawn343 (player id / name)!";
                return false;
            }
            return true;
        }
    }
}