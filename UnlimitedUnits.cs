
#define RCM_STANDALONE


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using RCM_GUI;
namespace RCM_UnlimitedUnits{
    #if RCM_STANDALONE
    [BepInDependency(RCMManager.IDENTIFIER, BepInDependency.DependencyFlags.HardDependency)]
    #endif
    [BepInPlugin(IDENTIFIER, "Unlimited Units", "1.0.0.0")]
    public class UnlimitedUnits : BaseUnityPlugin{
        const string IDENTIFIER = "RCM.plugins.unlimitedunits";

        #if RCM_STANDALONE
        static RCMModUI mod;
        #endif
        private void Awake(){
            new Harmony(IDENTIFIER).PatchAll();

            #if RCM_STANDALONE
            RCMManager.ConnectMod("Unlimited Units").ContinueWith(t => 
            {
                mod = t.Result;
                mod.CreateLabelField("patch applied");

            }, TaskScheduler.FromCurrentSynchronizationContext());
            #endif
        }

        [HarmonyPatch(typeof(GameBalancingStore), "get_UnitCap")] public static class Patch_UnitCap{
            public static void Postfix(ref int __result){
                __result = 1000;
            }
        }

    }
}
