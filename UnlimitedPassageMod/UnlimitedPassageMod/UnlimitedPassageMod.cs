using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;

namespace UnlimitedPassages
{
    [BepInPlugin("ahdog.unlimited_passages", "UnlimitedPassageMod", "0.1.0")]	// (GUID, mod name, mod version)
    public class UnlimitedPassageMod : BaseUnityPlugin
    {
        public void OnEnable()
        {
            /* This is called when the mod is loaded. */

            // subscribe ConsumeEndgameHook to the WinState.ConsumeEndGame method from the game
            On.WinState.ConsumeEndGame += ConsumeEndgameHook;
        }

        // This method will be subscribed to WinState.ConsumeEndgame. 
        // It just gives the user all of their passages back after consuming one.
        void ConsumeEndgameHook(On.WinState.orig_ConsumeEndGame orig, WinState self)
        {
            // Run WinState.ConsumeEndGame()
            orig(self);

            // Give the user all their passages back

            // For every endgameTracker (passage):
            for (int i = 0; i < self.endgameTrackers.Count; i++)
            {   
                // If the user has fullfilled the goal needed to unlock the passage:
                if (self.endgameTrackers[i].GoalFullfilled)
                {
                    // Give their passage to them
                    self.endgameTrackers[i].consumed = false;
                }
            }
        }
    }
}