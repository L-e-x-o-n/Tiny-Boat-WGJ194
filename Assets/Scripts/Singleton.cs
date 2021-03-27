using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
                // fallback, might not be necessary.
                if (m_Instance == null)
                    m_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return m_Instance;
        }
    }
}