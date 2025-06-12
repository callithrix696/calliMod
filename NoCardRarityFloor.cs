using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NoCardRarityFloor.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        public void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            // Uncomment if you need harmony patches, if you are writing your own custom effects.
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }


	
	[HarmonyPatch(typeof(SaveManager), "SetupRun")]
    public class NoCardRarityFloorMain
    {
        public static void Postfix(SaveManager __instance)
        {
            GrantableRewardData cardDraftRewardData = __instance.GetAllGameData().FindRewardDataByName("CardDraftMainClassReward");
            if (cardDraftRewardData != null)
            {
                bool kiri = false;
                Traverse.Create(cardDraftRewardData).Field("useRunRarityFloors").SetValue(kiri);
            }
        }
    }
	[HarmonyPatch(typeof(SaveManager), "SetupRun")]
    public class NoCardRarityFloorSub
    {
        public static void Postfix(SaveManager __instance)
        {
            GrantableRewardData cardDraftRewardData = __instance.GetAllGameData().FindRewardDataByName("CardDraftSubClassReward");
            if (cardDraftRewardData != null)
            {
                bool kiri = false;
                Traverse.Create(cardDraftRewardData).Field("useRunRarityFloors").SetValue(kiri);
            }
        }
    }

}
