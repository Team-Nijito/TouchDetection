using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Touch Detection will handle particle spawning during mouse click or touch occurs
 **/
public class TouchDetection : MonoBehaviour
{
    //Prefab of particle system for the trailing effect when mouse click or touch is held
    [SerializeField]
    private GameObject Trailparticle;

    //Prefab of particle system for mouse click or touch occurs  
    [SerializeField]
    private GameObject Touchparticle;

    //Particle system for trailing effect to be instantiated
    private GameObject TrailEffect = null;
    //Particle system for touch effect to be instantiated
    private GameObject TouchEffect = null;

    void Update()
    {
        Vector3 touchPos;

        #region InstantiatingParticleSystems
        //Instantiate the particle system on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0.0f;

            TrailEffect = GameObject.Instantiate(Trailparticle, touchPos, Quaternion.identity);
            TouchEffect = GameObject.Instantiate(Touchparticle, touchPos, Quaternion.identity);
            return;
        }
        //Instantiate the particle system on touch
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            touchPos.z = 0.0f;

            TrailEffect = GameObject.Instantiate(Trailparticle, touchPos, Quaternion.identity);
            TouchEffect = GameObject.Instantiate(Touchparticle, touchPos, Quaternion.identity);
            return;
        }
        #endregion

        #region HandlingTrailingEffectWhenTouchHeld
        if (TrailEffect != null)
        {
            touchPos = new Vector3(0, 0, 0);
            if (Input.GetMouseButton(0))
            {
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPos.z = 0.0f;
            }else if(Input.touchCount > 0)
            {
                touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                touchPos.z = 0.0f;
            }
            TrailEffect.transform.position = touchPos;
        }
        #endregion

        #region StoppingParticleSystems
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended))
        {
            //CAUTION!!Particle systems should destroy self on stop
            //TrailEffect needs to be stopped manually, but TouchEffect stops by itself
            TrailEffect.GetComponent<ParticleSystem>().Stop();
            //Destroy(TrailEffect);
        }
        #endregion
    }
}
