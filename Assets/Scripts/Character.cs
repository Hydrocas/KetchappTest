using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region // ==============================[Sub Class]============================== //

    #endregion

    #region // ==============================[Struct]============================== //

    #endregion

    #region // ==============================[Inspector Variables]============================== //

        [SerializeField] private Transform  m_visualCharacter = null;
        [Space]
        [SerializeField] private float      m_forwardSpeed   = 0f;
        [SerializeField] private float      m_rightSpeed     = 0f;

    #endregion

    #region // ==============================[Private Variables]============================== //

        private float m_horizontalLimit;

        private float c_characterWidth = 1.5f;

    #endregion

    #region // ==============================[Properties]============================== //

    #endregion

    #region // ==============================[Character Methods]============================== //

        public void Init(float horizontalLimit)
        {
            m_horizontalLimit = horizontalLimit - c_characterWidth;
        }

        private void Move()
	    {
            transform.position += Vector3.forward * m_forwardSpeed * Time.deltaTime;

            float horizontalAxis     = Controller.Instance.CurrentDrag.x;
            float horizontalPosition = m_visualCharacter.localPosition.x + horizontalAxis * m_rightSpeed * Time.deltaTime;
            horizontalPosition       = Mathf.Clamp(horizontalPosition, -m_horizontalLimit / 2, m_horizontalLimit / 2);

            m_visualCharacter.localPosition = new Vector3(horizontalPosition, 0, 0);
        }

    #endregion

    #region // ==============================[MonoBehaviour Methods]============================== //

        private void Awake()
    	{
            
    	}

        private void Start()
    	{
            
    	}

        private void Update()
    	{
            Move();
    	}

    #endregion
}
