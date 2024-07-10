using UnityEngine;

/// <summary>
/// 登入者資訊
/// <para> 登入後取得Token，才能使用其它WebAPI</para>
/// <para> Bearer Token型別</para>
/// </summary>
[CreateAssetMenu(fileName = "SO_SignInUser", menuName = "ScriptableObject/WebAPI/登入者資訊")]
public class SO_SignInUser : ScriptableObject
{
    [Header(">>> 帳號")]
    [SerializeField] private string account;

    [Header(">>> 密碼")]
    [SerializeField] private string pw;

    [Header(">>> 登入後取得Token")]
    [SerializeField] private string token;

    /// <summary>
    /// 帳號
    /// </summary>
    public string Account => account;
    /// <summary>
    /// 密碼
    /// </summary>
    public string Password => pw;
    /// <summary>
    /// 登入後取得Token
    /// </summary>
    public string Token
    {
        get => token;
        set => token = value;
    }
}
