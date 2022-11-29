using UnityEngine;

/// <summary>
/// Defines the functionality of the hand-bound UI.
/// </summary>
public class ToggleUIVisibility : SingletonMonobehaviour<ToggleUIVisibility>
{

    /// Reference to the parent object of Noise UI
    [SerializeField] private GameObject m_NoiseUI;

    /// Reference to the parent object of Noise Data Panel
    [SerializeField] private GameObject m_NoiseDataPanel;

    /// Reference to the parent object of Bar UI
    [SerializeField] private GameObject m_BarUI;

    /// Toggle the diagnostic system
    private bool m_ToggleDiagnosticFlag = false;

    /// Toggle Noise UI
    /// You can toggle this UI via voice command
    public void ToggleNoiseUI()
    {
        bool isObjectActive = m_NoiseUI.activeSelf;
        isObjectActive = !isObjectActive;
        m_NoiseUI.SetActive(isObjectActive);
    }

    /// Toggle Noise Data Panel
    /// You can toggle this UI via voice command
    public void ToggleNoiseDataPanel()
    {
        bool isObjectActive = m_NoiseDataPanel.activeSelf;
        isObjectActive = !isObjectActive;
        m_NoiseDataPanel.SetActive(isObjectActive);
    }

    /// <summary>
    /// Toggle moving bar UI.
    /// </summary>
    public void ToggleBarUI( bool isObjectActive )
    {
        m_BarUI.SetActive( isObjectActive );
    }

    /// <summary>
    /// Toggle Diagnostic profiler which shows memory and frame rates.
    /// Mapped to the 3rd button in hand bound UI.
    /// </summary>
    public void ToggleDiagnosticProfiler()
    {
        if( m_ToggleDiagnosticFlag )
        {
            Microsoft.MixedReality.Toolkit.CoreServices.DiagnosticsSystem.ShowDiagnostics = false;
            m_ToggleDiagnosticFlag = false;
        }
        else
        {
            Microsoft.MixedReality.Toolkit.CoreServices.DiagnosticsSystem.ShowDiagnostics = true;
            m_ToggleDiagnosticFlag = true;
        }       
    }
}
