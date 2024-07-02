using UnityEngine;
using UnityEngine.UI;
using VictorDev.UI;

/// <summary>
/// CCTV¦Cªí
/// </summary>
public class CCTVList : ScrollRectToggleList<CCTVListItem, SO_CCTV>
{
    [Header(">>> ·j´M¿é¤J®Ø")]
    [SerializeField] private SearchBar searchBar;
    [SerializeField] private ToggleGroup toggleGroup;

    private void Awake()
    {
        SetDataList(SoDataList);
        searchBar.onClickSearchButton.AddListener(OnSearchHandler);
    }

    protected override void OnCreateEachItem(CCTVListItem item, SO_CCTV soData)
    {
        item.toggleGroup = toggleGroup;
    }

    private void OnSearchHandler(string searchString)
    {
        Debug.Log($"OnSearchHandler: {searchString}");
    }

    private void OnValidate()
    {
        searchBar ??= transform.Find("SearchBar").GetComponent<SearchBar>();
        toggleGroup ??= GetComponent<ToggleGroup>();
    }
}
