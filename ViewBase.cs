using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RichGagak.Meowtyp
{
    public enum UILayer
    {
        Common = 0,
        Top = 1,
        Bottom = 2,
    }

    public abstract class ViewBase : MonoBehaviour
    {
        protected RectTransform mRectTransform;
        public RectTransform rectTransform
        {
            get
            {
                if (mRectTransform == null)
                    mRectTransform = GetComponent<RectTransform>();
                return mRectTransform;
            }
        }
        public virtual void OnOpen() { }
        public virtual void OnClose() { }
        protected void Close()
        {
            UIManager.Close(this);
        }
    }
}
