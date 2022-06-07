using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region // ==============================[Sub Class]============================== //

    #endregion

    #region // ==============================[Struct]============================== //

    #endregion

    #region // ==============================[Inspector Variables]============================== //

    #endregion

    #region // ==============================[Private Variables]============================== //

        private static Controller m_instance;
        private Vector2 m_startTouch            = Vector2.zero;
        private Vector2 m_startfromCurrentTouch = Vector2.zero;

    #endregion

    #region // ==============================[Properties]============================== //

        public static Controller Instance => m_instance;

        public Vector2 CurrentDrag
	    {
		    get
		    {
                return new Vector2(m_startfromCurrentTouch.x / Screen.width, m_startfromCurrentTouch.y / Screen.height);
		    }
	    }

    #endregion

    #region // ==============================[Controller Methods]============================== //

        private void Init()
        {
	        if(m_instance != null)
		    {
                Destroy(gameObject);

                return;
		    }

            m_instance = this;
        }

        private void ReadInput()
	    {
            if (Input.touchCount == 0)
                return;

            Touch touch = Input.GetTouch(0);

		    switch (touch.phase)
		    {
                case TouchPhase.Began:
                    m_startTouch = touch.position;
                    break;
                case TouchPhase.Moved:
                    m_startfromCurrentTouch = touch.position - m_startTouch;
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    m_startTouch = Vector2.zero;
                    m_startfromCurrentTouch = Vector2.zero;
                    break;
		    }
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
            ReadInput();
        }

    #endregion
}
