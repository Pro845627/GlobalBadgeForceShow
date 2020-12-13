using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RemoteAdmin;
using CommandSystem;

namespace GlobalBadgeForceShow
{
    public class GlobalBadgeShowForce : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Highest;
        public override string Author { get; } = "Gsuto_Maple";
        public override string Name { get; } = "Global Badge Forcely Show";
        public Harmony Harmony { get; set; }
        public override void OnEnabled()
        {
            Harmony = new Harmony("com.ghost.scpsl");
            Harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(ServerRoles), "SetText")]
    public static class EventHandlers
    {
        public static bool Prefix(ref ServerRoles __instance, string i)
        {
            CharacterClassManager cmm = __instance.gameObject.GetComponent<CharacterClassManager>();
            if (cmm.NetworkIsVerified)
            {
                if (Player.Get(cmm.UserId).GlobalBadge.HasValue)
                {
                    Log.Warn("不可修改拥有全球称号玩家的称号！");
                    return false;
                }
            }
            return true;
        }
    }
}
