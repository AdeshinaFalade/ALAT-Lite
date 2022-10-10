using ALAT_Lite.Classes;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

using RecyclerView = AndroidX.RecyclerView.Widget.RecyclerView;

namespace ALAT_Lite.Adapters
{
    internal class WardRecyclerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<WardrecyclerAdapterClickEventArgs> ItemClick;
        public event EventHandler<WardrecyclerAdapterClickEventArgs> ItemLongClick;

        List<ChildClass> listOfChild;
        Context context;

        public WardRecyclerAdapter(Context context, List<ChildClass> listOfChild)
        {
            this.listOfChild = listOfChild;
            this.context = context;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.ChildListLayout;
            itemView = LayoutInflater.From(parent.Context).
                  Inflate(id, parent, false);

            var vh = new WardrecyclerAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = listOfChild[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as WardrecyclerAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.txtWardListName.Text = item.account_Name;
            holder.txtWardListAcctNum.Text = item.account_Number;
        }

        public override int ItemCount => listOfChild.Count;

        void OnClick(WardrecyclerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(WardrecyclerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class WardrecyclerAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView txtWardListName, txtWardListAcctNum;


        public WardrecyclerAdapterViewHolder(View itemView, Action<WardrecyclerAdapterClickEventArgs> clickListener,
                            Action<WardrecyclerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            txtWardListName = itemView.FindViewById<TextView>(Resource.Id.txtWardListName);
            txtWardListAcctNum = itemView.FindViewById<TextView>(Resource.Id.txtWardListAcctNum);
            itemView.Click += (sender, e) => clickListener(new WardrecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new WardrecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class WardrecyclerAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}