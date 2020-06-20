package com.example.mainaltice;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.TextView;
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

public class activity_searched extends AppCompatActivity {

    private TextView txtTagSearched, txtNameSearched, txtIDSearched, txtProjectSearched, txtLocationSearched, txtUserSearched, txtDateSearched;
    private Button btnSearchedPrev;
    //private ProgressDialog progressDialog;
    private Integer n = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_searched);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        txtTagSearched = findViewById(R.id.txtTagSearched);
        txtNameSearched = findViewById(R.id.txtNameSearched);
        txtIDSearched = findViewById(R.id.txtIDSearched);
        txtProjectSearched = findViewById(R.id.txtProjectSearched);
        txtLocationSearched = findViewById(R.id.txtLocationSearched);
        txtUserSearched = findViewById(R.id.txtUserSearched);
        txtDateSearched = findViewById(R.id.txtDateSearched);

        btnSearchedPrev = findViewById(R.id.btnSearchedPrev);

        //progressDialog = new ProgressDialog(this);

        Bundle bundle = null;
        bundle = this.getIntent().getExtras();
        String tag_code = bundle.getString("tag_code");
        String name = bundle.getString("name");
        String prototype_id = bundle.getString("prototype_id");
        String project = bundle.getString("project");
        String location = bundle.getString("location");
        String name_reg = bundle.getString("name_reg");
        String date_reg = bundle.getString("date_reg");

        txtTagSearched.setText(tag_code);
        txtNameSearched.setText(name);
        txtIDSearched.setText(prototype_id);
        txtProjectSearched.setText(project);
        txtLocationSearched.setText(location);
        txtUserSearched.setText(name_reg);
        txtDateSearched.setText(date_reg);

        btnSearchedPrev.setOnClickListener(v -> {

            searchHistory(tag_code, n);
            n = n + 1;

        });

    }

    private void searchHistory(String tag_code, Integer n){

        final String tagCode = tag_code;
        //final String prototypeid = prototype_id;

        //progressDialog.setMessage("Searching History...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_SEARCH_HISTORY,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        //progressDialog.dismiss();

                        try {
                            JSONArray array = new JSONArray(response);
                            JSONObject obj = array.getJSONObject(n);
                            if (!obj.getBoolean("error")){

                                txtLocationSearched.setText(obj.getString("location"));
                                txtDateSearched.setText(obj.getString("date"));

                                Toast.makeText(
                                        getApplicationContext(),
                                        "History search successful",
                                        Toast.LENGTH_LONG
                                ).show();

                            }else {
                                Toast.makeText(
                                        getApplicationContext(),
                                        obj.getString("message"),
                                        Toast.LENGTH_LONG
                                ).show();
                            }
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
                params.put("tag_code", tagCode);
                //params.put("prototype_id", prototypeid);
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
