using ALAT_Lite.Classes;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace ALAT_Lite.Adapters
{
    internal class RecyclerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemClick;
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemLongClick;
        List<TransactionModel> listOfTranx;

        public RecyclerAdapter(List<TransactionModel> data)
        {
            listOfTranx = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.TransactionRecyclerLayout;
            itemView = LayoutInflater.From(parent.Context).
                  Inflate(id, parent, false);

            var vh = new RecyclerAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = listOfTranx[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as RecyclerAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.txtTransAmount.Text = item.Amount.ToString();
            holder.txtTransDate.Text = item.Date;
            holder.txtTransPhone.Text = item.Phone;

        }

        public override int ItemCount => listOfTranx.Count;

        void OnClick(RecyclerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RecyclerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RecyclerAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView txtTransPhone, txtTransAmount, txtTransDate;


        public RecyclerAdapterViewHolder(View itemView, Action<RecyclerAdapterClickEventArgs> clickListener,
                            Action<RecyclerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            txtTransAmount = itemView.FindViewById<TextView>(Resource.Id.txtTransAmount);
            txtTransPhone = itemView.FindViewById<TextView>(Resource.Id.txtTransPhone);
            txtTransDate = itemView.FindViewById<TextView>(Resource.Id.txtTransDate);

            itemView.Click += (sender, e) => clickListener(new RecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class RecyclerAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}