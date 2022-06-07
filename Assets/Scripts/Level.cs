using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region // ==============================[Sub Class]============================== //

    #endregion

    #region // ==============================[Struct]============================== //

    #endregion

    #region // ==============================[Inspector Variables]============================== //

        [SerializeField] private Character  m_playerCharacter   = null;
        [SerializeField] private float      m_groundWidth       = 10f;

    #endregion

    #region // ==============================[Private Variables]============================== //

    #endregion

    #region // ==============================[Properties]============================== //

    #endregion

    #region // ==============================[Level Methods]============================== //

        public void Init()
        {
            m_playerCharacter.Init(m_groundWidth);
        }

    #endregion

    #region // ==============================[MonoBehaviour Methods]============================== //

        private void Awake()
    	{
            Init();
        }

        private void Start()
    	{
            
    	}

        private void Update()
    	{
            
    	}

    #endregion
}
