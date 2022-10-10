using ALAT_Lite.Classes;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;

namespace ALAT_Lite.Adapters
{
    internal class ViewPagerAdapter : RecyclerView.Adapter
    {
        List<ChildClass> listOfChild;
        Context context;


        public ViewPagerAdapter(Context context, List<ChildClass> listOfChild)
        {

            this.listOfChild = listOfChild;
            this.context = context;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
          
            //View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            return new ViewPagerAdapterViewHolder(LayoutInflater.From(context).Inflate(Resource.Layout.ViewPagerLayout, parent, false));
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = listOfChild[position];
            NumberFormatInfo myNumberFormatInfo = new CultureInfo("yo-NG", false).NumberFormat;

            // Replace the contents of the view with that element
            var holder = viewHolder as ViewPagerAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.txtAcctNumber.Text = item.account_Number;
            holder.txtWardName.Text = item.account_Name;
            holder.txtWardBalance.Text = item.account_Balance .ToString("C", myNumberFormatInfo);
            if (item.activity.ToLower() == "active")
            {
                holder.txtStatus.Text = "Active";
            }
            else
            {
                holder.txtStatus.Text = "Restricted";
            }
        }

        public override int ItemCount => listOfChild.Count;

       // void OnClick(ViewPagerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
       // void OnLongClick(ViewPagerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        /**
        public static implicit operator AndroidX.RecyclerView.Widget.RecyclerView.Adapter(ViewPagerAdapter v)
        {
            return v;
          //  throw new NotImplementedException();
        }
        **/
        
    }

    public class ViewPagerAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView txtAcctNumber, txtWardBalance, txtStatus, txtWardName;


        public ViewPagerAdapterViewHolder(View itemView) : base(itemView)
        {
            //TextView = v;
            txtAcctNumber = itemView.FindViewById<TextView>(Resource.Id.txtWardAcctNumber);
            txtWardBalance = itemView.FindViewById<TextView>(Resource.Id.txtWardBalance);
            txtWardName = itemView.FindViewById<TextView>(Resource.Id.txtWardName);
            txtStatus = itemView.FindViewById<TextView>(Resource.Id.txtWardStatus);
         //   itemView.Click += (sender, e) => clickListener(new ViewPagerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
         //   itemView.LongClick += (sender, e) => longClickListener(new ViewPagerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

}