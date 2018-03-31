using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace PrisonLabor.Harmony
{
    internal class Patch_Command_SetPlantToGrow
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.operand == AccessTools.Property(typeof(MapPawns), nameof(MapPawns.FreeColonistsSpawned)).GetGetMethod())
                {
                    Log.Warning(instruction.opcode + " " + instruction.operand);
                    instruction.operand = AccessTools.Method(typeof(Patch_Command_SetPlantToGrow), nameof(Patch_FreeColonistsSpawned));
                    Log.Warning(instruction.opcode + " " + instruction.operand);
                }


                yield return instruction;
            }
        }


        private static IEnumerable<Pawn> Patch_FreeColonistsSpawned(MapPawns mapPawns)
        {
            foreach (Pawn p in mapPawns.FreeColonistsSpawned)
                yield return p;
            foreach (Pawn p in mapPawns.PrisonersOfColonySpawned)
                yield return p;
        }

    }
}