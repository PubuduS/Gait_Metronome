using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This class controll the moving bar animation cycle.
/// </summary>
public class BarController : MonoBehaviour
{
    /// Reference to the main Title bar on the panel.
    [SerializeField] private TextMeshPro m_TitleLbl = null;

    /// Reference to the current noise Title under the panel.
    [SerializeField] private TextMeshPro m_NoiseLbl = null;

    /// Before we move to apply next noise value,
    /// we need to wait current value is being completed.
    /// This flag will lockdown till the current value is completed.
    private bool m_IsAnimationLocked = false;

    /// Reference to Animator.
    private Animator m_Animator;

    /// Length of the current animation cycle.
    private float m_AnimLength = 0.0f;

    /// Default Animation Speed.
    private const float m_DefaultAnimationSpeed = 1.0f;

    /// Noise index for traveling the noise list.
    /// At the end of the travel, it will be reset to 0.
    private int m_NoiseIndex = 0;

    /// Use to apply previous noise after changing idle to moving.
    private int m_Counter = 1;

    /// <summary>
    /// Initialized values.
    /// </summary>
    private void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        AnimationClip[] clips = m_Animator.runtimeAnimatorController.animationClips;

        foreach( AnimationClip clip in clips )
        {
            switch( clip.name )
            {
                case "movingAnimNew":
                    m_AnimLength = clip.length;
                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Reset the noise index to 0 if it go beyond size of the noise values.
    /// This means it will start with begining again.
    /// </summary>
    private void Update()
    {
        if( m_NoiseIndex >= NoiseController.Instance.BaseNoise.NoiseValueList.Count )
        {
            m_NoiseIndex = 0;
        }                
    }

    /// <summary>
    /// Expand and Shrink the time to complete one moving cycle according to the colored noise values.
    /// One cycle means time to return to the starting position. 
    /// </summary>
    private void MoveBar()
    {
        float noiseValue = m_DefaultAnimationSpeed;
        float desiredSpeed = m_DefaultAnimationSpeed;

        if( String.Equals( m_NoiseLbl.text, "Noise: Pink") || ( String.Equals( m_NoiseLbl.text, "Noise: Random") ) )
        {
            NoiseController.Instance.BaseNoise.NoiseValueList[10] = 10f;
            NoiseController.Instance.BaseNoise.NoiseValueList[20] = 20f;
            noiseValue = Mathf.Abs( NoiseController.Instance.BaseNoise.NoiseValueList[m_NoiseIndex] );
        }
        else if( String.Equals( m_NoiseLbl.text, "Noise: ISO") )
        {
            noiseValue = Mathf.Abs( NoiseController.Instance.BaseNoise.PreferredWalkingSpeed );
        }
        else
        {
            m_TitleLbl.text = "ERROR!!!";
            noiseValue = m_DefaultAnimationSpeed;
        }

        m_IsAnimationLocked = true;
        desiredSpeed = ( m_AnimLength / noiseValue );       
        m_Animator.speed = desiredSpeed;

        //float len = m_Animator.GetCurrentAnimatorStateInfo(0).length;
        //Debug.Log("Number: " + noiseValue + " Time: " + Time.realtimeSinceStartup + " Len: " + len);
    }

    /// <summary>
    /// Lockdown the animation until it played out.
    /// Animation will unlock after it complete the current loop.
    /// This event is mapped to animation event.
    /// </summary>
    private void LockdownAnimation()
    {        
        if( m_IsAnimationLocked == false && NoiseController.Instance.BaseNoise.NoiseAppliedFlag )
        {
            MoveBar();            
        }
    }

    /// <summary>
    /// Unlock the animation after it played out.
    /// Mapped to animation event.
    /// </summary>
    private void UnlockAnimation()
    {
        m_IsAnimationLocked = false;

        if( !String.Equals( m_NoiseLbl, "ISO" ) && ( m_IsAnimationLocked == false) && NoiseController.Instance.BaseNoise.NoiseAppliedFlag )
        {            
            m_NoiseIndex++;            
        }
        else
        {
            m_Counter++;

            // Prevent first element from being skipped.
            if( m_NoiseIndex == 0 && m_Counter == 2 )
            {
                m_Counter = 1;                
            }

            // Prevent element from being skipped after transtion from idle to moving.
            if( m_Counter == 3 )
            {
                m_Counter = 1;                
            }
        }
    }
}
