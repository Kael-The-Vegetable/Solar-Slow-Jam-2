using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    public enum SelectFirstElementMode
    {
        WhenShown, WhenOpened, Never
    }

    private CanvasGroup m_CanvasGroup;
    private EventSystem _eventSystem;

    [SerializeField] private bool _isOpen = true;

    [Space]
    [Tooltip("If a parent menu is set, this menu will be closed when the parent is.")]
    [SerializeField] Menu _parent;

    [Tooltip("Dictates when, if ever, the first UI element will be selected automatically.")]
    [SerializeField] private SelectFirstElementMode _selectFirstElement;

    [Tooltip("If true, child UI elements will be disabled/enabled when hidden/shown.")]
    [SerializeField] private bool _autoToggleElements = true;

    [Header("Events")]

    [SerializeField, Tooltip("This event is called when the menu is shown or opened.")]
    private UnityEvent _onMenuShown;

    [SerializeField, Tooltip("This event is called when the menu is hidden or closed.")]
    private UnityEvent _onMenuHidden;

    [Space]
    [SerializeField, Tooltip("This event is called when the menu is opened.")]
    private UnityEvent _onMenuOpened;

    [SerializeField, Tooltip("This event is called when the menu is closed.")]
    private UnityEvent _onMenuClosed;

    public UnityEvent OnMenuOpened { get => _onMenuOpened; set => _onMenuOpened = value; }
    public UnityEvent OnMenuClosed { get => _onMenuClosed; set => _onMenuClosed = value; }
    public UnityEvent OnMenuShown { get => _onMenuShown; set => _onMenuShown = value; }
    public UnityEvent OnMenuHidden { get => _onMenuHidden; set => _onMenuHidden = value; }

    private void Start()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        _eventSystem = FindFirstObjectByType<EventSystem>();

        if (_isOpen)
        {
            ForceOpenMenu(force: true);
        }
        else
        {
            ForceCloseMenu(force: true);
        }
    }


    // ContextMenu means you can run this method in the inspector by right-clicking the component
    [ContextMenu("Open Menu")]
    public void OpenMenu() => ForceOpenMenu(force: false);

    private void ForceOpenMenu(bool force = false)
    {
        if (!_isOpen || force) // Only open the menu if we are closed or if we force it
        {
            _isOpen = true;
            OnMenuOpened.Invoke();
            ShowMenu();
             
            if (_selectFirstElement == SelectFirstElementMode.WhenOpened)
            {
                SelectMenuElement(0);
            }
        }
    }

    [ContextMenu("Close Menu")]
    public void CloseMenu() => ForceCloseMenu(force: false);

    public void ForceCloseMenu(bool force = false)
    {
        if (_isOpen || force) // Only close the menu if we are open or if we force it
        {
            _isOpen = false;
            HideMenu();
            OnMenuClosed.Invoke();

            // Close all menus with this as its _parent
            foreach (var child in FindObjectsByType<Menu>(FindObjectsSortMode.None))
            {
                if (child._parent == this)
                {
                    child.CloseMenu();
                }
            }
        }
    }

    [ContextMenu("Show Menu")]
    public void ShowMenu()
    {
        if (_autoToggleElements)
        {
            SetSelectables(true);
        }

        OnMenuShown.Invoke();

        if (_selectFirstElement == SelectFirstElementMode.WhenShown)
        {
            SelectMenuElement(0);
        }
    }

    [ContextMenu("Hide Menu")]
    public void HideMenu()
    {
        if (_autoToggleElements)
        {
            SetSelectables(false);
        }

        OnMenuHidden.Invoke();
    }

    public void ToggleMenu()
    {
        if (_isOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    // Set all ui elements in the menu be interactable or not
    protected virtual void SetSelectables(bool state)
    {
        m_CanvasGroup.interactable = state;
    }

    public void SelectMenuElement(int element)
    {
        var Selectables = m_CanvasGroup.GetComponentsInChildren<Selectable>();
        if (_eventSystem != null && Selectables.Length > 0)
        {
            _eventSystem.SetSelectedGameObject(Selectables[element].gameObject);
        }
    }

    public void OpenSubmenu(Menu submenu)
    {
        this.HideMenu();
        Debug.Assert(submenu._parent == null || submenu._parent == this);
        submenu._parent = this;
        submenu.OnMenuClosed.AddListener(this.ShowMenu);

    }
}
