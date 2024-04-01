using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Grow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Droid.ServiceListener
{
    public class OnCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        /*private TaskCompletionSource<UserAccount> _TaskCompletionSource;

        public OnCompleteListener(TaskCompletionSource<UserAccount> InTaskCompletionSource)
        {
            _TaskCompletionSource = InTaskCompletionSource;
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                //Process document
                var result = task.Result;
                if(result is DocumentSnapshot doc)
                {
                    var user = new UserAccount();
                    user.Id = doc.Id;
                    user.FirstName = doc.GetString("FirstName");
                    user.LastName = doc.GetString("LastName");
                    user.Email = doc.GetString("Email");
                    user.Password = doc.GetString("Password");
                    user.PhoneNumber = doc.GetString("PhoneNumber");

                    _TaskCompletionSource.TrySetResult(user);
                    return;
                }
            }
            else
            {
                //Unsuccessful 
                _TaskCompletionSource.TrySetResult(default(UserAccount));
            }
        }*/
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            throw new NotImplementedException();
        }
    }
}