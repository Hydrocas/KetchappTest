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

        [SerializeField] private float m_smoothDrag = 0.05f;

    #endregion

    #region // ==============================[Private Variables]============================== //

        private static Controller m_instance;

        private Vector2 m_touchDelta    = Vector2.zero;
        private Vector2 m_dragVelocity  = Vector2.zero;

    #endregion

    #region // ==============================[Properties]============================== //

        public static Controller Instance => m_instance;

        public Vector2 CurrentDrag { get; private set; }

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
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

			    switch (touch.phase)
			    {
				    case TouchPhase.Moved:
				    case TouchPhase.Stationary:
                        m_touchDelta = touch.deltaPosition;
                        break;
				    case TouchPhase.Ended:
				    case TouchPhase.Canceled:
					    m_touchDelta = Vector2.zero;
					    break;
			    }
		    }


            Vector2 currentDrag = CurrentDrag;

            currentDrag.x = Mathf.SmoothDamp(currentDrag.x, m_touchDelta.x, ref m_dragVelocity.x, m_smoothDrag);
            currentDrag.y = Mathf.SmoothDamp(currentDrag.y, m_touchDelta.y, ref m_dragVelocity.y, m_smoothDrag);

            CurrentDrag = currentDrag;
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

	    private void LateUpdate()
	    {
		
	    }

	#endregion
}
