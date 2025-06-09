// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : MonoBehaviour
    {
        private HealtSystem healtSystem;
        #region FIELDS SERIALIZED

        [Header("Settings")]

        [Tooltip("Canvas prefab spawned at start. Displays the player's user interface.")]
        [SerializeField]
        private GameObject canvasPrefab;
        public GameObject ss;

        #endregion

        #region UNITY FUNCTIONS

        /// <summary>
        /// Awake.
        /// </summary>
        private void Awake()
        {
            healtSystem = FindFirstObjectByType<HealtSystem>();
            //Spawn Interface.
            ss=Instantiate(canvasPrefab);
        }

        #endregion
        void Update()
        {
            if (healtSystem.lose==true)
            {
                ss.SetActive(false);
            }
        }
    }
}