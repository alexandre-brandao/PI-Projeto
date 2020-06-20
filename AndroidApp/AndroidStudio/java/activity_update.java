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
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class activity_update extends AppCompatActivity {

    private EditText txtTagUpdate, txtIDUpdate;
    private Button btnUpdateReg;
    private Spinner spnBuildingUpdate, spnFloorUpdate;
    //private ProgressDialog progressDialog;

    Boolean buildingNotSelectedUp, floorNotSelectedUp;
    String selectedBuildingUp, selectedFloorUp;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        btnUpdateReg = findViewById(R.id.btnUpdateReg);
        txtTagUpdate = findViewById(R.id.txtTagUpdate);
        txtIDUpdate = findViewById(R.id.txtIDUpdate);

        spnBuildingUpdate = findViewById(R.id.spnBuildingUpdate);
        spnFloorUpdate = findViewById(R.id.spnFloorUpdate);

        spnBuildingUpdate.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                buildingNotSelectedUp = false;
                selectedBuildingUp = spnBuildingUpdate.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                buildingNotSelectedUp = true;
            }

        });

        spnFloorUpdate.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {

            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                floorNotSelectedUp = false;
                selectedFloorUp = spnFloorUpdate.getItemAtPosition(position).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                floorNotSelectedUp = true;
            }

        });

        //progressDialog = new ProgressDialog(this);

        btnUpdateReg.setOnClickListener(v -> {

            if (txtTagUpdate.getText().toString().matches("") && txtIDUpdate.getText().toString().matches("")){

                Toast.makeText(activity_update.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else  if (buildingNotSelectedUp) {

                Toast.makeText(activity_update.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else  if (floorNotSelectedUp) {

                Toast.makeText(activity_update.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else {

                createHistory();
                updatePrototype();

                finish();
                if (SharedPrefManager.getInstance(this).getUserAccess().matches("1")){
                    Intent intent = new Intent(this, activity_admin_menu.class);
                    startActivity(intent);
                }else{
                    Intent intent = new Intent(this, MenuActivity.class);
                    startActivity(intent);
                }

            }
        });
    }

    private void createHistory(){

        //String tag_code_temp;
        final String tag_code = txtTagUpdate.getText().toString().trim();
        final String location = selectedBuildingUp + ", " + selectedFloorUp;
        final String prototype_id = txtIDUpdate.getText().toString().trim();
        final String date_reg;
        //final String time_reg;

        /*if (txtTagUpdate.getText().toString().matches("")){

            tag_code_temp = "0";

        } else {

            tag_code_temp = txtTagUpdate.getText().toString().trim();

        }

        final String tag_code = tag_code_temp;*/

        Date date = new Date();
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        date_reg = df.format(date);

        /*Date time = new Date();
        DateFormat tf = new SimpleDateFormat("HH:mm:ss");
        time_reg = tf.format(time);*/

        /*progressDialog.setMessage("Registering History Entry...");
        progressDialog.show();*/

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
                params.put("prototype_id", prototype_id);
                params.put("location", location);
                params.put("date", date_reg);
                //params.put("time", time_reg);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequestHistory);

    }

    private void updatePrototype(){

        final String tagCode = txtTagUpdate.getText().toString().trim();
        final String prototypeID = txtIDUpdate.getText().toString().trim();
        final String location = selectedBuildingUp + ", " + selectedFloorUp;
        final String device = "Android";

        //progressDialog.setMessage("Updating Prototype...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_UPDATE_PROTOTYPE,
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
                params.put("tag_code", tagCode);
                params.put("prototype_id", prototypeID);
                params.put("location", location);
                params.put("device", device);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequest);

        /*finish();
        if (SharedPrefManager.getInstance(this).getUserAccess().matches("1")){
            Intent intent = new Intent(this, activity_admin_menu.class);
            startActivity(intent);
        }else{
            Intent intent = new Intent(this, MenuActivity.class);
            startActivity(intent);
        }*/

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
