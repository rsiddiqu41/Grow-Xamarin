using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Firestore;
using Task = Android.Gms.Tasks.Task;

namespace Grow.Droid.ServiceListener
{
    public class OnDeleteCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        private TaskCompletionSource<bool> _tcs;

        public OnDeleteCompleteListener(TaskCompletionSource<bool> tcs)
        {
            _tcs = tcs;
        }

        public void OnComplete(Task task)
        {
            _tcs.TrySetResult(task.IsSuccessful);
        }
    }
}