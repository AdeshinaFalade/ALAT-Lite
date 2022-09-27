using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;

namespace ALAT_Lite.Adapters
{
    internal class RecyclerAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemClick;
        public event EventHandler<RecyclerAdapterClickEventArgs> ItemLongClick;
        List<TransactionModel> listOfTranx;

        public RecyclerAdapter(List<TransactionModel> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            var vh = new RecyclerAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as RecyclerAdapterViewHolder;
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => items.Length;

        void OnClick(RecyclerAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RecyclerAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RecyclerAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }


        public RecyclerAdapterViewHolder(View itemView, Action<RecyclerAdapterClickEventArgs> clickListener,
                            Action<RecyclerAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
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