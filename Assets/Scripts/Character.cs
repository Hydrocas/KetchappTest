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
        [SerializeField] private Transform  m_stack           = null;
        [Space]
        [SerializeField] private float      m_forwardSpeed   = 0f;
        [SerializeField] private float      m_horizontalSpeed     = 1f;
        [SerializeField] private float      m_horizontalSmooth    = 0.01f;
        [Space]
        [SerializeField] private float      m_collectWidth   = 1f;
        [SerializeField] private LayerMask  m_collectLayer   = default;
        [Space]
        [SerializeField] private float      m_stackSpace     = 1f;
        [SerializeField] private float      m_stackSmooth    = 1f;

    #endregion

    #region // ==============================[Private Variables]============================== //

        private float m_horizontalLimit;
        private float m_horizontalVelocity;
        private float m_horizontalPosition;
        private List<Geode> m_geodesList;

        private const float c_characterWidth = 1.5f;

    #endregion

    #region // ==============================[Properties]============================== //

        public Vector3 StackEndPosition
        {
            get
            {
                if (m_geodesList == null || m_geodesList.Count == 0)
                    return m_stack.position;

                 return m_geodesList[m_geodesList.Count - 1].transform.position + Vector3.forward * m_stackSpace;
            }
        }

    #endregion

    #region // ==============================[Character Methods]============================== //

        public void Init(float horizontalLimit)
        {
            m_horizontalLimit = horizontalLimit - c_characterWidth;
            m_geodesList = new List<Geode>();
        }

        private void Move()
	    {
            transform.position += Vector3.forward * m_forwardSpeed * Time.deltaTime;

            m_horizontalPosition += Controller.Instance.CurrentDrag.x * m_horizontalSpeed * Time.deltaTime;
            m_horizontalPosition = Mathf.Clamp(m_horizontalPosition, -m_horizontalLimit / 2, m_horizontalLimit / 2);

            float horizontalDamping = Mathf.SmoothDamp(m_visualCharacter.localPosition.x, m_horizontalPosition, ref m_horizontalVelocity, m_horizontalSmooth);

            m_visualCharacter.localPosition = new Vector3(horizontalDamping, 0, 0);
        }

        private void Collect()
	    {
            Vector3 boxHalfExtents = new Vector3(m_collectWidth, 1, 0.5f) / 2;
            Vector3 boxPosition = m_stack.position + Vector3.up * boxHalfExtents.y;
            boxPosition.x = m_visualCharacter.position.x;
            Collider[] results = Physics.OverlapBox(boxPosition, boxHalfExtents, transform.rotation, m_collectLayer);

            foreach (Collider result in results)
            {
                Geode geode = result.GetComponent<Geode>();

                if (geode == null)
                    continue;

                AddInStack(geode);
            }

            if (m_geodesList.Count > 0)
		    {
                List<Geode> collectGeodes;

			    for (int i = 0; i < m_geodesList.Count; i++)
			    {
                    collectGeodes = m_geodesList[i].CollectOther(m_collectWidth, m_collectLayer);

                    foreach (Geode collectGeode in collectGeodes)
                        AddInStack(collectGeode);
                }
            }
	    }

        public void AddInStack(Geode geode)
	    {
            if (m_geodesList.Contains(geode))
                return;

            geode.transform.position = StackEndPosition;
            geode.transform.SetParent(m_stack, true);
            m_geodesList.Add(geode);
            geode.SetCollected();
        }

        private void SnakeMouvement()
	    {
            if (m_geodesList.Count == 0)
                return;
            
            int length = m_geodesList.Count;
            Transform lastTransform = m_visualCharacter;
            Geode geode;

		    for (int i = 0; i < length; i++)
		    {
                geode = m_geodesList[i];
                geode.SnakeMouvement(lastTransform, i == 0? 0: m_stackSmooth);
                lastTransform = geode.transform;
            }
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
            Collect();
            SnakeMouvement();
        }

	    private void OnDrawGizmos()
	    {
            if (m_stack == null)
                return;

            Vector3 boxExtents = new Vector3(m_collectWidth, 1, 0.5f);
            Vector3 boxPosition = m_stack.position + Vector3.up * boxExtents.y / 2;
            boxPosition.x = m_visualCharacter.position.x;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxPosition, boxExtents);

            if (m_geodesList == null)
                return;

		    foreach (Geode geode in m_geodesList)
		    {
                Gizmos.DrawWireSphere(geode.transform.position + Vector3.up / 2, m_collectWidth / 2);
		    }
	    }

	#endregion
}
