﻿using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Locks
{
    [StaticConstructorOnStartup]
    public static class Initialization
    {
        static Initialization()
        {
            HarmonyPatches.HPatcher.Init();
            InjectLockTab();
            CompatibilityPatches.ClutterStructure.Init();
        }

        static void InjectLockTab()
        {
            var door_defs = new List<ThingDef>();
            door_defs.AddRange(DefDatabase<ThingDef>.AllDefs.Where(d => d.thingClass == typeof(Building_Door)));
            foreach (var door_def in door_defs)
            {
                if (door_def.inspectorTabs == null)
                    door_def.inspectorTabs = new List<System.Type>();
                door_def.inspectorTabs.Add(typeof(ITab_Lock));

                if (door_def.comps == null)
                    door_def.comps = new List<CompProperties>();
                door_def.comps.Add(new CompProperties_Lock());

                door_def.ResolveReferences();
            }
        }
    }
}
