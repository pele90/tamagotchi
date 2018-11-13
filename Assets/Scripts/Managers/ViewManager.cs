using UnityEngine;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour
{
    #region FIELDS

    private static ViewManager _viewManager;

    public GameObject topActions;
    public GameObject bottomActions;
    public GameObject viewGameObject;
    public GameObject lights;
    public List<GameObject> availableViews;
    public static Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
    public static ViewManager Instance
    {
        get
        {
            if (!_viewManager)
            {
                _viewManager = FindObjectOfType(typeof(ViewManager)) as ViewManager;

                if (!_viewManager)
                {
                    Debug.LogError("There needs to be one active StateManager script on a GameObject in your scene.");
                }
                else
                {

                }
            }

            return _viewManager;
        }
    }

    #endregion

    #region PUBLIC_METHODS

    // Use this for initialization
    void Start()
    {
        SetupViews();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateView(string viewName)
    {
        // Check if view exists
        // Get view from dictionary views
        GameObject view;
        if (!views.TryGetValue(viewName, out view))
        {
            // the key isn't in the dictionary.
            return; // or whatever you want to do
        }

        // Hide action icons
        topActions.SetActive(false);
        bottomActions.SetActive(false);

        // SetActive(true) on the returned view
        view.SetActive(true);
    }

    public void DeactivateView(string viewName)
    {
        // Check if view exists
        // Get view from dictionary views
        GameObject view;
        if (!views.TryGetValue(viewName, out view))
        {
            // the key isn't in the dictionary.
            return; // or whatever you want to do
        }

        // SetActive(false) on the returned view
        view.SetActive(false);

        // Show action icons
        topActions.SetActive(true);
        bottomActions.SetActive(true);
        StateManager.Instance.SetDefaultActiveState();
    }

    public GameObject GetView(string viewName)
    {
        GameObject view;
        if (!views.TryGetValue(viewName, out view))
        {
            // the key isn't in the dictionary.
            return null; // or whatever you want to do
        }

        return view;
    }

    #endregion

    #region PRIVATE_METHODS

    private void SetupViews()
    {
        foreach (Transform child in viewGameObject.transform)
        {
            availableViews.Add(child.gameObject);
        }

        foreach (var item in availableViews)
        {
            string name = item.name;
            item.SetActive(false);
            views.Add(name, item);
        }
    }

    #endregion
}
