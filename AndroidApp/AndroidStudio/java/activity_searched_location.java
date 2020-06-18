package com.example.mainaltice;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class activity_searched_location extends AppCompatActivity {

    //private ProgressDialog progressDialog;
    private RecyclerView recSearchedLocation;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_searched_location);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        recSearchedLocation = findViewById(R.id.recSearchedLocation);
        //progressDialog = new ProgressDialog(this);

        Bundle bundle = null;
        bundle = this.getIntent().getExtras();
        String location = bundle.getString("location");

        searchLocation(location);

    }

    private void searchLocation(String location){

        final String locationFinal = location;

        /*String tag_code[] = new String[100];
        String name[] = new String[100];;
        String prototype_id[] = new String[100];;*/

        //progressDialog.setMessage("Searching Prototype Entries...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_SEARCH_LOCATION,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        //progressDialog.dismiss();

                        try {
                            JSONArray array = new JSONArray(response);

                            String tag_code[] = new String[array.length()];
                            String name[] = new String[array.length()];
                            String prototype_id[] = new String[array.length()];

                            for (int i = 0; i < array.length(); i++){

                                JSONObject obj = array.getJSONObject(i);
                                if (!obj.getBoolean("error")){

                                    tag_code[i] = obj.getString("tag_code");
                                    name[i] = obj.getString("name");
                                    prototype_id[i] = obj.getString("prototype_id");

                                    Toast.makeText(
                                            getApplicationContext(),
                                            "Prototype search successful",
                                            Toast.LENGTH_SHORT
                                    ).show();

                                }else {
                                    Toast.makeText(
                                            getApplicationContext(),
                                            obj.getString("message"),
                                            Toast.LENGTH_LONG
                                    ).show();
                                }

                            }

                            MyAdapter myAdapter = new MyAdapter(getApplicationContext(), name, tag_code, prototype_id);
                            recSearchedLocation.setAdapter(myAdapter);
                            recSearchedLocation.setLayoutManager(new LinearLayoutManager(getApplicationContext()));

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        //progressDialog.hide();
                        Toast.makeText(getApplicationContext(), error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }){
            @Override
            protected Map<String, String> getParams() throws AuthFailureError {
                Map<String,String> params = new HashMap<>();
                params.put("location", locationFinal);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequest);

    }

    public void onBackPressed(){

        finish();
        Intent intent = new Intent(this, activity_search.class);
        startActivity(intent);

    }
}
