using System;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Common;

namespace VictorDev.Net.WebAPI
{
    /// <summary>
    /// 呼叫WebAPI的封包格式(ScriptableObject)
    /// </summary>
    [CreateAssetMenu(fileName = "WebAPI_Request", menuName = ">>VictorDev<</Net/WebAPI/WebAPI_Request - 網路請求設定")]
    public class WebAPI_Request : ScriptableObject
    {
        public RequestMethod method;
        public Authorization authorization;

        [Header(">>> 設定WebAPI的IP與PORT (選填)")]
        [SerializeField] private WebAPI_IPConfig ipConfig;

        /// <summary>
        /// 設定WebAPI的IP與PORT (可選)
        /// </summary>
        [Header(">>> WebAP完整路徑 / WebAPI IP之後的路徑")]
        [TextArea(1, 3)][SerializeField] private string apiURL;

        [Header(">>> Params設定")]
        [SerializeField] private QueryParams queryParams;
        [Header(">>> Body設定")]
        [SerializeField] private Body body;

        [ContextMenu("- Console Log: Request設定資料")]
        private void LogRequestInfo()
        {
            Debug.Log($">>> [{method}] URL: {url}");

            if (queryParams.IsActivated)
            {
                Debug.Log($"\t\tqueryString: {queryParams.queryString}");
            }

            if (body.IsActivated)
            {
                switch (body.bodyType)
                {
                    case enumBody.formData:
                        Debug.Log($"\t\tWWWForm binary長度: {body.formData.data.Length}");
                        break;
                }
            }
        }



        /* [Space(100)]
         [Header(">>> GET方法之後的變數 (選填)")]
         public string urlGetVariables;

         [Header(">>> formData (選填，POST使用)")]
         public KeyValueItem[] formDataRow;

         private WWWForm _formData;
         public WWWForm formData
         {
             get
             {
                 if (_formData == null)
                 {
                     _formData = new WWWForm();
                     formDataRow.ForEach((dataRow) => _formData.AddField(dataRow.key.Trim(), dataRow.value.Trim()));
                 }
                 return _formData;
             }
         }

         [Space(20)]



         [SerializeField] private AccessToken accessToken;
         [SerializeField] private JsonString jsonString;*/
        public WebAPI_Request(string url) => this.apiURL = url;

        private UriBuilder uriBuilder;

        /// <summary>
        /// 完整WebAPI網址
        /// </summary>
        public string url
        {
            get
            {
                if (ipConfig != null)
                {
                    uriBuilder = new UriBuilder(ipConfig.WebIP_Port);
                    uriBuilder.Path += apiURL.Trim();
                }
                else uriBuilder = new UriBuilder(apiURL.Trim());

                string resultString = uriBuilder.Uri.ToString();
                //檢查Params設定
                if (queryParams.IsActivated)
                {
                    resultString = StringHandler.StringBuilderAppend(resultString, queryParams.queryString);
                }
                return resultString;
            }
        }

        /// <summary>
        /// 外部需檢查Body類型，才能判定Request需要傳遞什麼資料
        /// </summary>
        public enumBody bodyType => body.bodyType;
        public WWWForm formData => body.formData;

        /// <summary>
        /// Authorization的Token值
        /// </summary>
        public string token
        {
            get => authorization.token.Trim();
            set => authorization.token = value;
        }
        public string BodyJSON => (body.bodyType == enumBody.rawJSON) ? body.rawString : "empty";

        [Serializable]
        public class QueryParams
        {
            [SerializeField] private bool isActivated = false;
            [SerializeField] private List<KeyValueItem> fieldList;

            public bool IsActivated => isActivated;

            public string queryString
            {
                get
                {
                    string resultString = "";
                    if (fieldList.Count > 0)
                    {
                        resultString = "?";
                        fieldList.ForEach(item =>
                        {
                            resultString = StringHandler.StringBuilderAppend(resultString, item.key.Trim(), "=", item.value.Trim());
                            if (fieldList.IndexOf(item) != fieldList.Count - 1) resultString += "&";
                        });
                    }
                    return resultString;
                }
            }

            /// <summary>
            /// 設定Filed資料集
            /// </summary>
            public void SetFiledValue(Dictionary<string, string> dataSet)
            {
                fieldList.Clear();
                foreach (string keyName in dataSet.Keys)
                {
                    fieldList.Add(new KeyValueItem(keyName, dataSet[keyName]));
                }
            }
        }

        [Serializable]
        private class Body
        {
            [SerializeField] private bool isActivated = false;
            public enumBody bodyType;
            [Header(">>>供form-data型態使用")]
            [SerializeField] private List<KeyValueItem> fieldList;
            [Header(">>>供raw型態: JSON、TEXT使用")]
            [SerializeField] private string _rawString;

            public bool IsActivated => isActivated;

            /// <summary>
            /// form-data型態的參數值
            /// </summary>
            public WWWForm formData
            {
                get
                {
                    if (IsActivated == false || bodyType != enumBody.formData) return null;

                    WWWForm result = new WWWForm();
                    fieldList.ForEach(item =>
                    {
                        Debug.Log($"\t\tform-data Field: {item.key.Trim()} / {item.value.Trim()}");
                        result.AddField(item.key.Trim(), item.value.Trim());
                    });
                    return result;
                }
            }

            public string rawString
            {
                set => _rawString = value;
                get
                {
                    //待優化處理
                    return rawString;
                }
            }

            /// <summary>
            /// 設定Filed資料集
            /// </summary>
            public void SetFiledValue(Dictionary<string, string> dataSet)
            {
                fieldList.Clear();
                foreach (string keyName in dataSet.Keys)
                {
                    fieldList.Add(new KeyValueItem(keyName, dataSet[keyName]));
                }
            }
        }


        [Serializable]
        public class KeyValueItem
        {
            public string key, value;

            public KeyValueItem(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }

    public enum RequestMethod { GET, HEAD, POST, PUT, CREATE, DELETE }
    public enum enumBody { none, formData, rawJSON, rawText, binary }

    [Serializable]
    public class Authorization
    {
        public AuthorizationType authorizationType = AuthorizationType.Bearer;
        [TextArea(1, 5)]
        public string token;
    }





    [Serializable]
    public struct AccessToken
    {
        [TextArea(1, 5)] public string token;
    }

    [Serializable]
    public struct JsonString
    {
        [TextArea(1, 100)] public string json;
    }

    public enum AuthorizationType
    {
        NoAuth, Bearer
    }

}