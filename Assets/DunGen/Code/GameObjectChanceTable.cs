﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Random = System.Random;

namespace DunGen
{
    /**
     * Lots of code rewriting since Unity doesn't support serializing generics
     */

    #region Helper Class

    [Serializable]
    public sealed class GameObjectChance
    {
        public GameObject Value;
        public float MainPathWeight;
        public float BranchPathWeight;
        public bool UseDepthScale;
        public AnimationCurve DepthWeightScale = AnimationCurve.Linear(0, 1, 1, 1);


        public GameObjectChance()
            : this(null, 1, 1)
        {
        }

        public GameObjectChance(GameObject value)
            : this(value, 1, 1)
        {
        }

        public GameObjectChance(GameObject value, float mainPathWeight, float branchPathWeight)
        {
            Value = value;
            MainPathWeight = mainPathWeight;
            BranchPathWeight = branchPathWeight;
        }

        public float GetWeight(bool isOnMainPath, float normalizedDepth)
        {
            float weight = (isOnMainPath) ? MainPathWeight : BranchPathWeight;

            if (UseDepthScale)
                weight *= DepthWeightScale.Evaluate(normalizedDepth);

            return weight;
        }
    }

    #endregion

    /// <summary>
    /// A table containing weighted values to be picked at random
    /// </summary>
    [Serializable]
    public class GameObjectChanceTable
    {
        public List<GameObjectChance> Weights = new List<GameObjectChance>();


        public GameObjectChanceTable Clone()
        {
            GameObjectChanceTable newTable = new GameObjectChanceTable();

            foreach (var w in Weights)
                newTable.Weights.Add(new GameObjectChance(w.Value, w.MainPathWeight, w.BranchPathWeight) { UseDepthScale = w.UseDepthScale, DepthWeightScale = w.DepthWeightScale });

            return newTable;
        }

        /// <summary>
        /// Does this chance table contain the specified GameObject?
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>True if the GameObject is included in the chance table</returns>
        public bool ContainsGameObject(GameObject obj)
        {
            foreach (var weight in Weights)
                if (weight.Value == obj)
                    return true;

            return false;
        }

        /// <summary>
        /// Picks an object from the table at random, taking weights into account
        /// </summary>
        /// <param name="random">The random number generator to use</param>
        /// <param name="isOnMainPath">Is this object to be spawn on the main path</param>
        /// <param name="normalizedDepth">The normalized depth (0-1) that this object is to be spawned at in the dungeon</param>
        /// <returns>A random value</returns>
        public GameObject GetRandom(Random random, bool isOnMainPath, float normalizedDepth, GameObject previouslyChosen, bool allowImmediateRepeats, bool removeFromTable = false)
        {
            float totalWeight = Weights.Select(x => x.GetWeight(isOnMainPath, normalizedDepth)).Sum();
            float randomNumber = (float)(random.NextDouble() * totalWeight);

            foreach (var w in Weights)
            {
                float weight = w.GetWeight(isOnMainPath, normalizedDepth);

                if (randomNumber < weight)
                {
                    if(removeFromTable)
                        Weights.Remove(w);

                    if (w.Value == previouslyChosen && Weights.Count > 1 && !allowImmediateRepeats && previouslyChosen != null)
                        return GetRandom(random, isOnMainPath, normalizedDepth, previouslyChosen, removeFromTable);
                    else
                        return w.Value;
                }

                randomNumber -= weight;
            }

            return null;
        }

        /// <summary>
        /// Picks an object at random from a collection of tables, taking weights into account
        /// </summary>
        /// <param name="random">The random number generator to use</param>
        /// <param name="isOnMainPath">Is this object to be spawn on the main path</param>
        /// <param name="normalizedDepth">The normalized depth (0-1) that this object is to be spawned at in the dungeon</param>
        /// <param name="tables">A list of chance tables to pick from</param>
        /// <returns>A random value</returns>
        public static GameObject GetCombinedRandom(Random random, bool isOnMainPath, float normalizedDepth, params GameObjectChanceTable[] tables)
        {
            float totalWeight = tables.SelectMany(x => x.Weights.Select(y => y.GetWeight(isOnMainPath, normalizedDepth))).Sum();
            float randomNumber = (float)(random.NextDouble() * totalWeight);

            foreach(var w in tables.SelectMany(x => x.Weights))
            {
                float weight = w.GetWeight(isOnMainPath, normalizedDepth);

                if (randomNumber < weight)
                    return w.Value;

                randomNumber -= weight;
            }

            return null;
        }
    }
}