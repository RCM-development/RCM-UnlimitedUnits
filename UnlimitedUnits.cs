using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using TestMod;
namespace RCM_UnlimitedUnits{

    [BepInDependency(RCMManager.IDENTIFIER, BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(IDENTIFIER, "Unlimited Units", "1.0.0.0")]
    public class UnlimitedUnits : BaseUnityPlugin{
        const string IDENTIFIER = "RCM.plugins.unlimitedunits";
        static RCMModUI mod;
        private void Awake(){
            new Harmony(IDENTIFIER).PatchAll();
            RCMManager.ConnectMod("Unlimited Units").ContinueWith(t => 
            {
                mod = t.Result;

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        [HarmonyPatch(typeof(GameBalancingStore), "get_UnitCap")] public static class Patch_UnitCap{
            public static void Postfix(ref int __result){
                __result = 1000;
            }
        }

    }
}
