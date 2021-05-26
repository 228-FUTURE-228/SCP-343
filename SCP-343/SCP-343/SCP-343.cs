using Exiled.API.Features;
using System;
namespace SCP_343
{
    public class SCP_343 : Plugin<Config>
    {
        public EventHandlers EventHandlers;
        public static SCP_343 plugin;
        public override string Author { get; } = "ФУТУР";
        public override string Name { get; } = "SCP-343";
        public override Version Version { get; } = new Version(1, 5, 5);
        public override Version RequiredExiledVersion { get; } = new Version(2, 10, 0);
        public override void OnEnabled()
        {
            plugin = this;
            EventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingDoor += EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.PickingUpItem += EventHandlers.OnPickingUpItem;
            Exiled.Events.Handlers.Player.ReceivingEffect += EventHandlers.OnReceivingEffect;
            Exiled.Events.Handlers.Player.Handcuffing += EventHandlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.Escaping += EventHandlers.OnEscaping;
            Exiled.Events.Handlers.Warhead.Detonated += EventHandlers.OnDetonated;
            Exiled.Events.Handlers.Map.Decontaminating += EventHandlers.OnDecontaminating;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += EventHandlers.OnEnteringPocketDimension;
            Exiled.Events.Handlers.Player.EnteringFemurBreaker += EventHandlers.OnEnteringFemurBreaker;
            Exiled.Events.Handlers.Player.TriggeringTesla += EventHandlers.OnTriggeringTesla;
            Exiled.Events.Handlers.Scp106.Containing += EventHandlers.OnContaining;
            Exiled.Events.Handlers.Player.ActivatingWarheadPanel += EventHandlers.OnActivatingWarheadPanel;
            Exiled.Events.Handlers.Server.EndingRound += EventHandlers.OnEndingRound;
            Exiled.Events.Handlers.Player.ChangingRole += EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Destroying += EventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnHurting;
            Exiled.Events.Handlers.Map.PlacingBlood += EventHandlers.OnPlacingBlood;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.InteractingDoor -= EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.PickingUpItem -= EventHandlers.OnPickingUpItem;
            Exiled.Events.Handlers.Player.ReceivingEffect -= EventHandlers.OnReceivingEffect;
            Exiled.Events.Handlers.Player.Handcuffing -= EventHandlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.Escaping -= EventHandlers.OnEscaping;
            Exiled.Events.Handlers.Warhead.Detonated -= EventHandlers.OnDetonated;
            Exiled.Events.Handlers.Map.Decontaminating -= EventHandlers.OnDecontaminating;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= EventHandlers.OnEnteringPocketDimension;
            Exiled.Events.Handlers.Player.EnteringFemurBreaker -= EventHandlers.OnEnteringFemurBreaker;
            Exiled.Events.Handlers.Player.TriggeringTesla -= EventHandlers.OnTriggeringTesla;
            Exiled.Events.Handlers.Scp106.Containing -= EventHandlers.OnContaining;
            Exiled.Events.Handlers.Player.ActivatingWarheadPanel -= EventHandlers.OnActivatingWarheadPanel;
            Exiled.Events.Handlers.Server.EndingRound -= EventHandlers.OnEndingRound;
            Exiled.Events.Handlers.Player.ChangingRole -= EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.Destroying -= EventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnHurting;
            Exiled.Events.Handlers.Map.PlacingBlood -= EventHandlers.OnPlacingBlood;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}
