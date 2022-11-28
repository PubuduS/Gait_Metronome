using TMPro;
using UnityEngine;

/// <summary>
/// This class is used to set noise pattern levels.
/// When user press a button, it will change the pattern.
/// </summary>
public class SetNoise : MonoBehaviour
{

    /// This will hold the user's selected pattern.
    private ColoredNoise m_PatternType = ColoredNoise.Pink;

    /// This will show the user selection of the pattern
    /// under the NoiseDataPanel.
    [SerializeField] private TextMeshPro m_NoiseLableInNoiseDataPanel;

    private GameObject m_NoiseObject = null;
    private RemoveAllComponents m_Remover = null;
    
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        m_NoiseObject = GameObject.FindGameObjectWithTag("NoisePattern");
        m_Remover = m_NoiseObject.GetComponent<RemoveAllComponents>();
        NoiseController.Instance.BaseNoise = m_NoiseObject.AddComponent<PinkNoise>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
    }

    /// <summary>
    /// Gets called from the button's OnClick event.
    /// 0 is for deafult pattern
    /// 1 is for Pink noise pattern.
    /// 2 is for White noise(Random) pattern.
    /// 3 is for ISO (Constant) pattern.
    /// </summary>
    public void SetNoisePattern( int pattern )
    {
        m_PatternType = (ColoredNoise)pattern;        
        SetPatternLable();
    }

    /// <summary>
    /// Sets the label according to the user selection.
    /// User can see what they selected in Avatar panel
    /// under the avatar.
    /// </summary>
    private void SetPatternLable()
    {
        switch( m_PatternType )
        {
            case ColoredNoise.Pink:
                SetColoredObject("Noise: Pink");
                NoiseController.Instance.BaseNoise = m_NoiseObject.AddComponent<PinkNoise>();                
                break;

            case ColoredNoise.ISO:
                SetColoredObject("Noise: ISO");
                NoiseController.Instance.BaseNoise = m_NoiseObject.AddComponent<ISONoise>();                
                break;

            case ColoredNoise.Random:               
                SetColoredObject("Noise: Random");
                NoiseController.Instance.BaseNoise = m_NoiseObject.AddComponent<WhiteNoise>();                
                break;

            default:
                Debug.Log("Incorect Input. Can't Change Labele");                
                m_NoiseLableInNoiseDataPanel.text = "!!! Noise: Error !!!";
                break;
        }
    }

    /// <summary>
    /// Set the lable and removed previously attached noise game objects.
    /// For example, when we transit from pink to white, we don't need pink
    /// gameobject. We can destroy that object.
    /// </summary>
    /// <param name="noise"></param>
    private void SetColoredObject( string noise )
    {        
        m_Remover.RemoveAllNoiseComponents();
        m_NoiseLableInNoiseDataPanel.text = noise;
    }

    /// <summary>
    /// Gettr to get the user's selection.
    /// </summary>
    public ColoredNoise GetNoisePattern()
    {
        return m_PatternType;
    }
}
