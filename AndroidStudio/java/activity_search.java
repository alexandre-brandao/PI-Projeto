package com.example.mainaltice;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class activity_search extends AppCompatActivity {

    private Button btnSearchReg, btnSearchLocation;
    private Spinner spnBuildingSearch, spnFloorSearch;
    private EditText txtTagSearch, txtIDSearch;
    //private ProgressDialog progressDialog;

    Boolean buildingNotSelectedSc, floorNotSelectedSc;
    String selectedBuildingSc, selectedFloorSc;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        btnSearchReg = findViewById(R.id.btnSearchReg);
        btnSearchLocation = findViewById(R.id.btnSearchLocation);

        txtTagSearch = findViewById(R.id.txtTagSearch);
        txtIDSearch = findViewById(R.id.txtIDSearch);

        spnBuildingSearch = findViewById(R.id.spnBuildingSearch);
        spnFloorSearch = findViewById(R.id.spnFloorSearch);

        spnBuildingSearch.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                buildingNotSelectedSc = false;
                selectedBuildingSc = spnBuildingSearch.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                buildingNotSelectedSc = true;
            }

        });

        spnFloorSearch.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                floorNotSelectedSc = false;
                selectedFloorSc = spnFloorSearch.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                floorNotSelectedSc = true;
            }

        });

        //progressDialog = new ProgressDialog(this);

        btnSearchReg.setOnClickListener(v -> {

            if (txtTagSearch.getText().toString().matches("")) {

                Toast.makeText(activity_search.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            }else if (txtIDSearch.getText().toString().matches("")){

                Toast.makeText(activity_search.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            }else{

                searchPrototype();

            }
        });

        btnSearchLocation.setOnClickListener(v -> {

            String locationSearch = selectedBuildingSc + ", " + selectedFloorSc;

            Bundle bundle = new Bundle();
            bundle.putString("location",locationSearch);
            Intent intent = new Intent(getApplicationContext(), activity_searched_location.class);
            intent.putExtras(bundle);
            startActivity(intent);
            finish();

        });
    }

    private void searchPrototype(){

        final String tagCode = txtTagSearch.getText().toString().trim();
        final String prototypeID = txtIDSearch.getText().toString().trim();

        //progressDialog.setMessage("Searching Prototype...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_SEARCH_PROTOTYPE,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        //progressDialog.dismiss();

                        try {
                            JSONObject obj = new JSONObject(response);
                            if (!obj.getBoolean("error")){

                                Bundle bundle = new Bundle();
                                bundle.putString("tag_code",obj.getString("tag_code"));
                                bundle.putString("name",obj.getString("name"));
                                bundle.putString("prototype_id",obj.getString("prototype_id"));
                                bundle.putString("project",obj.getString("project"));
                                bundle.putString("location",obj.getString("location"));
                                bundle.putString("name_reg",obj.getString("name_reg"));
                                bundle.putString("date_reg",obj.getString("date_reg"));
                                Intent intent = new Intent(getApplicationContext(), activity_searched.class);
                                intent.putExtras(bundle);
                                startActivity(intent);
                                finish();

                                Toast.makeText(
                                        getApplicationContext(),
                                        "Prototype search successful",
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
                params.put("prototype_id", prototypeID);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequest);

    }

    public void onBackPressed(){

        finish();
        if (SharedPrefManager.getInstance(this).getUserAccess().matches("1")){
            Intent intent = new Intent(this, activity_admin_menu.class);
            startActivity(intent);
        }else{
            Intent intent = new Intent(this, MenuActivity.class);
            startActivity(intent);
        }
    }
}
