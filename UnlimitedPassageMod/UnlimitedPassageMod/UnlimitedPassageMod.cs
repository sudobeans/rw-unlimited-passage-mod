// Unlimited Passage Mod for Rain World
// By AHDog

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using UnityEngine;

namespace UnlimitedPassages
{
    [BepInPlugin("ahdog.unlimited_passages", "UnlimitedPassageMod", "0.1.0")]	// (GUID, mod name, mod version)
    public class UnlimitedPassageMod : BaseUnityPlugin
    {
        // This is called when the mod is loaded.
        public void OnEnable()
        {
            // subscribe InitSleepHudHook so it runs whenever the sleep and death screen comes up
            On.HUD.HUD.InitSleepHud += InitSleepHudHook;
        }

        // This method will be subscribed to HUD.InitSleepHud().
        // It gives the player all of their unlocked passages back.
        public void InitSleepHudHook(
            On.HUD.HUD.orig_InitSleepHud orig, 
            HUD.HUD self, 
            Menu.SleepAndDeathScreen sleepAndDeathScreen, 
            HUD.Map.MapData mapData, 
            SlugcatStats charStats)
        {
            // Give the user all their passages back.
            // (This code is based off the WinState.ConsumeEndGame() method.)
            WinState winState = sleepAndDeathScreen.winState;
            for (int i = 0; i < winState.endgameTrackers.Count; i++) // For every endgameTracker (passage):
            {   
                if (winState.endgameTrackers[i].GoalFullfilled)      // If the user has got the right achievement:
                {
                   winState.endgameTrackers[i].consumed = false;    // Give their passage to them
                }
            }
            Debug.Log("Gave user all their passages back");

            // Run SleepAndDeathScreen.GetDataFromGame()
            orig(self, sleepAndDeathScreen, mapData, charStats);
        }
    }
}