using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geode : MonoBehaviour
{
    #region // ==============================[Sub Class]============================== //

    #endregion

    #region // ==============================[Struct]============================== //

    #endregion

    #region // ==============================[Inspector Variables]============================== //

        //[SerializeField] private float m_inspectorVariables = 0f;

    #endregion

    #region // ==============================[Private Variables]============================== //

    private float m_snakeMouvementVelocity;

    #endregion

    #region // ==============================[Properties]============================== //

    #endregion

    #region // ==============================[Geode Methods]============================== //

        public void Init()
        {
	    
        }

        public void SnakeMouvement(Transform toFollow, float smooth)
	    {
            Vector3 newPosition = transform.localPosition;
            newPosition.x = Mathf.SmoothDamp(newPosition.x, toFollow.localPosition.x, ref m_snakeMouvementVelocity, smooth);

            transform.localPosition = newPosition;
        }

        public void SetCollected()
	    {
            gameObject.layer = 0;
	    }

        public List<Geode> CollectOther(float collectSize, int layerMask)
	    {
            List<Geode> geodes = new List<Geode>();
            Collider[] results = Physics.OverlapSphere(transform.position + Vector3.up / 2, collectSize / 2, layerMask);

            foreach (Collider result in results)
            {
                Geode geode = result.GetComponent<Geode>();

                if (geode == null || geode == this)
                    continue;

                geodes.Add(geode);
            }

            return geodes;
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
            
    	}

    #endregion
}
