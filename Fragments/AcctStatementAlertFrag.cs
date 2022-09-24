using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALAT_Lite.Fragments
{
    [Obsolete]
    public class AcctStatementAlertFrag : DialogFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.AcctStatementAlertLayout, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        [Obsolete]
        public override void OnCancel(IDialogInterface dialog)
        {
            Activity.Finish();
            base.OnCancel(dialog);
        }
    }
}