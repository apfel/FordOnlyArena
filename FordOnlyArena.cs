// Copyright (c) 2021 apfel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute and/or sublicense copies of
// the Software.
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using HarmonyLib;
using MelonLoader;
using ModThatIsNotMod.BoneMenu;
using StressLevelZero.Arena;
using StressLevelZero.Data;
using UnityEngine;

[HarmonyPatch(typeof(Arena_SpawnEnemies), "SpawnEnemy")]
internal static class Arena_SpawnEnemiesFordPatch
{
    private static void Prefix(Arena_SpawnEnemies __instance, ref SpawnableObject spawnObject, ref Arena_EnemyReference.EnemyType enemyType, bool launch, Transform spawnTrans, EnemyProfile.EntranceType eType, ref bool isFriendly, bool isClone)
    {
        if (FordOnlyArena.DisableReplacement) return;

        spawnObject = __instance.shortHairedBrettSpawnable;
        enemyType   = Arena_EnemyReference.EnemyType.ENEMY_FORD;
        isFriendly  = false;
    }
}

public class FordOnlyArena : MelonMod
{
    public static bool DisableReplacement = false;

    private bool keepTurret = false;

    public override void OnApplicationStart()
    {
        MelonPreferences_Category prefCategory = MelonPreferences.CreateCategory("FordOnlyArena", "Ford-Only Arena");

        MelonPreferences_Entry<bool> disableEntry   = prefCategory.CreateEntry<bool>("Disabled", false, "Disable replacing enemies with the short-haired Ford", "Whether to replace all enemies with the short-haired Ford in the fantasy arena or not.");
        DisableReplacement                          = disableEntry.Value;
        disableEntry.OnValueChanged                 += (_, _new) => DisableReplacement = _new;

        MelonPreferences_Entry<bool> iconEntry  = prefCategory.CreateEntry<bool>("KeepArenaTurret", false, "Don't remove the arena turret", "Whether to keep the turret in the fantasy arena or not (scene reload required).");
        keepTurret                              = iconEntry.Value;
        iconEntry.OnValueChanged                += (_, _new) => keepTurret = _new;

        MenuCategory category = ModThatIsNotMod.BoneMenu.MenuManager.CreateCategory("Ford-Only Arena", Color.white);
        category.CreateBoolElement("Disable Replacement", Color.white, DisableReplacement, (_new) => DisableReplacement = _new);
        category.CreateBoolElement("Keep Arena Turret", Color.white, keepTurret, (_new) => keepTurret = _new);
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (keepTurret || sceneName != "arena_fantasy") return;

        GameObject.Destroy(GameObject.Find("TurretRail"));
    }
}
