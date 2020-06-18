package com.example.mainaltice;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

public class MyAdapter extends RecyclerView.Adapter<MyAdapter.MyViewHolder>{

    String data1[], data2[], data3[];
    String tagcodeLabel = "Tag Code: ";
    String prototypeIDLabel = "Prototype ID: ";
    Context context;

    public MyAdapter(Context ct, String name[], String tag_code[], String prototype_id[]){

        context = ct;
        data1 = name;
        data2 = tag_code;
        data3 = prototype_id;

    }

    @NonNull
    @Override
    public MyViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.my_row, parent, false);
        return new MyViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull MyViewHolder holder, int position) {

        holder.nameHolder.setText(data1[position]);
        holder.tagcodeHolder.setText(tagcodeLabel+data2[position]);
        holder.prototypeidHolder.setText(prototypeIDLabel+data3[position]);

    }

    @Override
    public int getItemCount() {
        return data1.length;
    }

    public class MyViewHolder extends RecyclerView.ViewHolder {

        TextView nameHolder, tagcodeHolder, prototypeidHolder;

        public MyViewHolder(@NonNull View itemView) {
            super(itemView);
            nameHolder = itemView.findViewById(R.id.txtRowName);
            tagcodeHolder = itemView.findViewById(R.id.txtRowTagCode);
            prototypeidHolder = itemView.findViewById(R.id.txtRowPrototypeID);
        }
    }
}
