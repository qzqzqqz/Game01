using UnityEngine;
using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace QFramework
{
    public class ResHelper
    {
        public static T SyncLoad<T>(string name) where T : UnityEngine.Object
        {
            var res = Resources.Load<T>(name);
            return res is GameObject ? Object.Instantiate(res) : res;
        }
        private static IEnumerator AsyncLoadRes<T>(string name, Action<T> callback) where T : UnityEngine.Object
        {
            var res = Resources.LoadAsync<T>(name);
            while (!(res.isDone)) yield return null;
            callback(res.asset is GameObject ? Object.Instantiate(res.asset) as T : res.asset as T);
        }
        public static void AsyncLoad<T>(string name, Action<T> callback) where T : UnityEngine.Object
        {
            PublicMono.Instance.StartCoroutine(AsyncLoadRes(name, callback));
        }
    }
}