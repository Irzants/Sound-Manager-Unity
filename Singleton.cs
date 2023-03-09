using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
        public static T Instance => _instance == null ? (_instance = CreateNewInstance()) : _instance;

        protected void Dummy() { }

        private static GameObject GetParent()
        {
            const string pTitle = "Singletons";
            GameObject parent= GameObject.Find(pTitle);
            if (parent == null)
            {
                parent = new(pTitle);
                DontDestroyOnLoad(parent);
            }
            return parent;
        }

        protected static T CreateNewInstance()
        {
            GameObject parent = GetParent();

            string objName = typeof(T).Name;
            StringBuilder sb = new();

            for(int i =0; i < objName.Length; i++)
            {
                char c = objName[i];
                if (c >= 'A' && c <= 'Z' && i > 0)
                    sb.Append(' ');
                sb.Append(c);
            }

            GameObject obj = new(sb.ToString());
            obj.transform.SetParent(parent.transform.transform, false);
            return obj.AddComponent<T>();
        }

    }

