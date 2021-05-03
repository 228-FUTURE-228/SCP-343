using Exiled.API.Interfaces;
using System.ComponentModel;
namespace SCP_343
{
    public class Config : IConfig
    {
        [Description("Is the SCP-343 plugin enabled? (true/false)")]
        public bool IsEnabled { get; set; } = true;
        [Description("The chance of SCP-343 appearing at the beginning of the round. (int)")]
        public int SpawnChance { get; set; } = 15;
        [Description("The minimum amount of ClassD for SCP-343 to spawn. (int)")]
        public int MinimumClassD { get; set; } = 5;
        [Description("The name of the role that is displayed when you hover over the player. (string)")]
        public string RoleName { get; set; } = "<color=#FF0000>SCP-343</color>";
        [Description("Message to SCP-343. (string)")]
        public string SCP343SpawnBroadcast { get; set; } = "You are SCP-343. You have immortality. You will be able to open all doors after {time} seconds.";
        [Description("Message to all. (string)")]
        public string BroadcastForAll { get; set; } = "SCP-343 appeared in the complex.";
        [Description("The time after which SCP-343 will be able to open all doors. (int)")]
        public int TimeToOpenDoors { get; set; } = 120;
        [Description("Message when SCP-343 failed to open the door. (string)")]
        public string SCP343WaitBroadcast { get; set; } = "To open all the doors, you need to wait {time} seconds.";
        [Description("Does SCP-343 turn raised things into painkillers? (true/false)")]
        public bool ConvertToPainkillers { get; set; } = true;
        [Description("SCP-343 death alert. (string)")]
        public string DieCassie { get; set; } = "scp 3 4 3 was lost";
        [Description("Message when SCP-343 died. (string)")]
        public string DieBroadcast { get; set; } = "SCP-343 was lost!";
    }
}
