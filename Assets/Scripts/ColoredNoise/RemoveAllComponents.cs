using UnityEngine;

/// <summary>
/// This will prevent multiple instances being created.
/// </summary>
public class RemoveAllComponents : MonoBehaviour
{
    /// <summary>
    /// When you create a new instance of colored noise,
    /// this will remove previous instance.
    /// </summary>
    public void RemoveAllNoiseComponents()
    {
        foreach( var comp in this.gameObject.GetComponents<Component>() )
        {
            if( ( comp is Transform ) != true && ( comp is RemoveAllComponents ) != true )
            {
                Destroy( comp );
            }
        }
    }
}
