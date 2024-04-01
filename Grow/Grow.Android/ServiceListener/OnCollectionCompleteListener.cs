using System;
using System.Collections.Generic;
using Android.Gms.Tasks;
using Firebase.Firestore;
//using Grow.Droid.Extensions;
using Grow.Core.Base;

namespace Grow.Droid.ServiceListener
{
    public class OnCollectionCompleteListener<T> : Java.Lang.Object, IOnCompleteListener
    {

        public OnCollectionCompleteListener() {  }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                var docsObj = task.Result;
                if (docsObj is QuerySnapshot docs)
                {
                    //_tcs.TrySetResult(docs.Convert<T>());
                }
            }
        }
    }
}