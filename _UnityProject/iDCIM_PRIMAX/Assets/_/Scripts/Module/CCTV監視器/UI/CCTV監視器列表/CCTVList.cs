using UnityEngine;
using VictorDev.UI;

/// <summary>
/// CCTV�C��
/// </summary>
public class CCTVList : ScrollRectToggleList<CCTVListItem, SO_CCTV>
{
    [Header(">>> �j�M��J��")]
    [SerializeField] private SearchBar searchBar;

    private void Awake()
    {
        SetDataList(SoDataList);
        searchBar.onClickSearchButton.AddListener(OnSearchHandler);

        onToggleChanged.AddListener(OnToggleChangedHandler);
    }

    private void OnSearchHandler(string searchString)
    {
        Debug.Log($"OnSearchHandler: {searchString}");
    }

    private void OnToggleChangedHandler(SO_CCTV soData, bool isOn)
    {
        Debug.Log($"Called: {soData.DeviceName} / {isOn}");
    }

    private void OnValidate()
    {
        searchBar ??= transform.Find("SearchBar").GetComponent<SearchBar>();
    }
}
