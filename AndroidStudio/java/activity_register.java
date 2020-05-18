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
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.SimpleTimeZone;

public class activity_register extends AppCompatActivity{

    private EditText txtTagRegister, txtIDRegister, txtNameRegister, txtProjectRegister;
    private Button btnRegisterReg;
    private Spinner spnBuildingRegister, spnFloorRegister;
    //private ProgressDialog progressDialog;

    Boolean buildingNotSelectedReg, floorNotSelectedReg;
    String selectedBuildingReg, selectedFloorReg;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        btnRegisterReg = findViewById(R.id.btnRegisterReg);

        txtTagRegister = findViewById(R.id.txtTagRegister);
        txtIDRegister = findViewById(R.id.txtIDRegister);
        txtNameRegister = findViewById(R.id.txtNameRegister);
        txtProjectRegister = findViewById(R.id.txtProjectRegister);

        spnBuildingRegister = findViewById(R.id.spnBuildingRegister);
        spnFloorRegister = findViewById(R.id.spnFloorRegister);

        spnBuildingRegister.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                buildingNotSelectedReg = false;
                selectedBuildingReg = spnBuildingRegister.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                buildingNotSelectedReg = true;
            }

        });

        spnFloorRegister.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                floorNotSelectedReg = false;
                selectedFloorReg = spnFloorRegister.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                floorNotSelectedReg = true;
            }

        });

        //progressDialog = new ProgressDialog(this);

        btnRegisterReg.setOnClickListener(v -> {

            if (txtTagRegister.getText().toString().matches("")){

                Toast.makeText(activity_register.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else if (txtIDRegister.getText().toString().matches("")){

                Toast.makeText(activity_register.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else  if (txtNameRegister.getText().toString().matches("")){

                Toast.makeText(activity_register.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else  if (buildingNotSelectedReg) {

                Toast.makeText(activity_register.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else  if (floorNotSelectedReg) {

                Toast.makeText(activity_register.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else {

                String emailFlag = SharedPrefManager.getInstance(this).getUserEmail();
                createHistory();
                registerPrototype(emailFlag);

            }

        });

    }

    private void createHistory(){

        final String tag_code = txtTagRegister.getText().toString().trim();
        final String location = selectedBuildingReg + ", " + selectedFloorReg;
        final String date_reg;
        //final String time_reg;

        Date date = new Date();
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        date_reg = df.format(date);

        /*Date time = new Date();
        DateFormat tf = new SimpleDateFormat("HH:mm:ss");
        time_reg = tf.format(time);*/

        //progressDialog.setMessage("Registering History Entry...");
        //progressDialog.show();

        StringRequest stringRequestHistory = new StringRequest(Request.Method.POST,
                Constants.URL_CREATE_HISTORY,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        //progressDialog.dismiss();

                        try {
                            JSONObject jsonObject = new JSONObject(response);

                            Toast.makeText(getApplicationContext(), jsonObject.getString("message"), Toast.LENGTH_LONG).show();

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
                params.put("tag_code", tag_code);
                params.put("location", location);
                params.put("date", date_reg);
                //params.put("time", time_reg);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequestHistory);

    }

    private void registerPrototype(String emailFlag){

        final String tag_code = txtTagRegister.getText().toString().trim();
        final String name = txtNameRegister.getText().toString().trim();
        final String prototype_id = txtIDRegister.getText().toString().trim();
        final String project = txtProjectRegister.getText().toString().trim();
        final String location = selectedBuildingReg + ", " + selectedFloorReg;
        final String name_reg = emailFlag;
        final String date_reg;
        final String device = "Android";

        Date date = new Date();
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        date_reg = df.format(date);


        //progressDialog.setMessage("Registering Prototype...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_REGISTER_PROTOTYPE,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        //progressDialog.dismiss();

                        try {
                            JSONObject jsonObject = new JSONObject(response);

                            Toast.makeText(getApplicationContext(), jsonObject.getString("message"), Toast.LENGTH_LONG).show();

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
                params.put("tag_code", tag_code);
                params.put("name", name);
                params.put("prototype_id", prototype_id);
                params.put("project", project);
                params.put("location", location);
                params.put("name_reg", name_reg);
                params.put("date_reg", date_reg);
                params.put("device", device);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequest);

        finish();
        Intent intent = new Intent(this, activity_admin_menu.class);
        startActivity(intent);

    }

    public void onBackPressed(){

        finish();
        Intent intent = new Intent(this, activity_admin_menu.class);
        startActivity(intent);

    }
}
