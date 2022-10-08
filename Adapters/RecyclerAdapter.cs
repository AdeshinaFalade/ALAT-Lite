using ALAT_Lite.Classes;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ALAT_Lite.Adapters
{
    internal class RecyclerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemClick;
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemLongClick;
        List<TransactionModel> listOfTranx;
        NumberFormatInfo myNumberFormatInfo = new CultureInfo("yo-NG", false).NumberFormat;

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
            var rawDate = item.trx_Date;
            var date = DateTime.Parse(rawDate).ToShortDateString();

            var bal = double.Parse(item.amount.ToString());
            var formattedBal = bal.ToString("C", myNumberFormatInfo);

            holder.txtTransAmount.Text = formattedBal;
            holder.txtTransDate.Text = date.ToString();
            holder.txtTransPhone.Text = item.phone_Number;
            holder.txtTransDesc.Text = item.trx_Description;

        }

        public override int ItemCount => listOfTranx.Count;

        void OnClick(RecyclerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RecyclerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RecyclerAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView txtTransPhone, txtTransAmount, txtTransDate, txtTransDesc;


        public RecyclerAdapterViewHolder(View itemView, Action<RecyclerAdapterClickEventArgs> clickListener,
                            Action<RecyclerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            txtTransAmount = itemView.FindViewById<TextView>(Resource.Id.txtTransAmount);
            txtTransPhone = itemView.FindViewById<TextView>(Resource.Id.txtTransPhone);
            txtTransDate = itemView.FindViewById<TextView>(Resource.Id.txtTransDate);
            txtTransDesc = itemView.FindViewById<TextView>(Resource.Id.txtTransDesc);

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