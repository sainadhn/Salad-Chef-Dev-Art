using UnityEngine;
using System.Collections;

public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool	_isApplicationQuitting = false;
    private static bool _doNotDestroyOnLoad = false;
    private static bool _checkForAppQuitting = true;

    public static bool CheckForAppQuitting
    {
        get { return SingleTon<T>._checkForAppQuitting; }
        set { SingleTon<T>._checkForAppQuitting = value; }
    }

    public static bool DoNotDestroyOnLoad
    {
        get
        {
            return _doNotDestroyOnLoad;
        }
        set
        {
            _doNotDestroyOnLoad = value;
            if(value)
                DontDestroyOnLoad(Instance);
        }
    }

    public static bool IsInstatiated()
    {
        return _instance != null;
    }

    public static T Instance
    {
        get
        {
            if (_isApplicationQuitting && CheckForAppQuitting == true)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed on application quit." +
                                 " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogWarning("[Singleton] Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it." + FindObjectOfType(typeof(T)).name);
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (DoNotDestroyOnLoad == true)
                            DontDestroyOnLoad(singleton);

                        //						Debug.Log("[Singleton] An instance of " + typeof(T) + 
                        //						          " is needed in the scene, so '" + singleton +
                        //						          "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        //Debug.Log("[Singleton] Using instance already created: " +
                        // _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    public void OnDestroy()
    {
        _isApplicationQuitting = true;	
    }
}
