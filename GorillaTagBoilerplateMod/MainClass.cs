// Here Are All The References We Need
using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace GorillaTagBoilerplateMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LoaderClass : BaseUnityPlugin
    {
        // Setting Up The Projects Name, Version, Identifier. It's Really Important
        // That The Identifier (ModGUID) Is Unique.
        private const string modGUID = "GT.Modding.Mod";
        private const string modName = "A BoilerPlate Gorilla Tag Mod Brought To You By Vidde";
        private const string modVersion = "0.0.1";

        // The Awake Function Runs At The Start Of The Game Only Once
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
        public void Awake()
        {
            // Initiating The Harmony Patcher
            var harmony = new Harmony(modGUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    // We Patch The FixedUpdate Mether Inside the Player Script In The Namespace "GorillaLocomotion"
    // FixedUpdate Isn't The Best To Patch But Who Cares :)
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", MethodType.Normal)]
    public class MainPatch
    {
        // We Reference The Player Object
        static void Prefix(GorillaLocomotion.Player __instance)
        {
            try
            {
                // Everything Here Will Get Called Every Frame
                // This Will Print "Test" In The Console Every Frame
                ExampleFunction();

                // We Can Also Use The "__instance" Variable Too 
                // We Can For Example Get The Players Head Position And Store It In A Vector3 Variable:
                Vector3 heasPos = __instance.headCollider.transform.position;

                // We Can Also For Example Turn Off Player Movement Too Wth This:
                __instance.disableMovement = true;

                // The Player Also has Some Juicy Variables Like:
                //
                //  __instance.jumpMultiplier
                //  __instance.maxJumpSpeed
                //  __instance.maxArmLength
                //  __instance.slideVelocityLimit
                //  __instance.slidingMinimum
                //  __instance.slideControl
                //  __instance.rightHandTransform
                //  __instance.leftHandTransform
                //
            }
            catch (InvalidCastException e) when (e.Data != null)
            {
                // This Prints Every Error That We Get
                Debug.Log(e.Message);
            }
        }

        // We Can Also Declaer Function Like This (They Need To Be Static, The Same With Variables)
        public static void ExampleFunction()
        {
            // This Function Prints The Word "Test" To The Console When It's Ran
            Debug.Log("Test");
        }
    }
}
