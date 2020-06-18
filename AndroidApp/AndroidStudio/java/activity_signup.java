package com.example.mainaltice;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
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

import java.util.HashMap;
import java.util.Map;

public class activity_signup extends AppCompatActivity {

    private Button btnCreateSignUp;
    private EditText txtNameSignUp, txtPasswordSignUp, txtRepeatPasswordSignUp, txtEmailSignUp, txtPhoneSignUp;
    private CheckBox chbAccessSignUp;
    //private ProgressDialog progressDialog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signup);

        if(SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            if (SharedPrefManager.getInstance(this).getUserAccess().matches("1")){
                Intent intent = new Intent(this, activity_admin_menu.class);
                startActivity(intent);
            }else{
                Intent intent = new Intent(this, MenuActivity.class);
                startActivity(intent);
            }
            return;
        }

        btnCreateSignUp = findViewById(R.id.btnCreateSignUp);

        txtNameSignUp = findViewById(R.id.txtNameSignUp);
        txtPasswordSignUp = findViewById(R.id.txtPasswordSignUp);
        txtRepeatPasswordSignUp = findViewById(R.id.txtRepeatPasswordSignUp);
        txtEmailSignUp = findViewById(R.id.txtEmailSignUp);
        txtPhoneSignUp = findViewById(R.id.txtPhoneSignUp);

        chbAccessSignUp = findViewById(R.id.chbAccessSignUp);

        //progressDialog = new ProgressDialog(this);

        btnCreateSignUp.setOnClickListener(v -> {

            if (txtNameSignUp.getText().toString().matches("")){

                Toast.makeText(activity_signup.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else if (txtPasswordSignUp.getText().toString().matches("")){

                Toast.makeText(activity_signup.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else if (txtRepeatPasswordSignUp.getText().toString().matches("")){

                Toast.makeText(activity_signup.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else if (txtEmailSignUp.getText().toString().matches("")){

                Toast.makeText(activity_signup.this, "Please fill all of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else if (txtPhoneSignUp.getText().toString().length() < 9 || txtPhoneSignUp.getText().toString().length() > 9){

                Toast.makeText(activity_signup.this, "Phone number is not 9 digits", Toast.LENGTH_LONG).show();
                return;

            } else if (txtPasswordSignUp.getText().toString().matches(txtRepeatPasswordSignUp.getText().toString())){

                registerUser();

            } else {

                Toast.makeText(activity_signup.this, "Please make sure both passwords match", Toast.LENGTH_LONG).show();
                return;

            }

        });

    }

    private void registerUser(){

        final String name = txtNameSignUp.getText().toString().trim();
        final String password = txtPasswordSignUp.getText().toString().trim();
        final String email = txtEmailSignUp.getText().toString().trim();
        final String phone = txtPhoneSignUp.getText().toString().trim();
        final String access;

        if (chbAccessSignUp.isChecked()){
            access = "1";
        } else {
            access = "0";
        }

        //progressDialog.setMessage("Registering User...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_REGISTER_USER,
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
                params.put("name", name);
                params.put("password", password);
                params.put("email", email);
                params.put("phone", phone);
                params.put("access", access);
                return params;
            }
        };

        RequestHandler.getInstance(this).addToRequestQueue(stringRequest);

        finish();
        Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);

    }

    public void onBackPressed(){

        finish();
        Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);

    }

}
