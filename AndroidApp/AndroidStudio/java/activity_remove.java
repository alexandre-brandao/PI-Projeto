package com.example.mainaltice;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
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

public class activity_remove extends AppCompatActivity {

    private EditText txtTagRemove, txtIDRemove;
    private Button btnRemoveReg;
    //private ProgressDialog progressDialog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_remove);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        btnRemoveReg = findViewById(R.id.btnRemoveReg);
        txtTagRemove = findViewById(R.id.txtTagRemove);
        txtIDRemove = findViewById(R.id.txtIDRemove);

        //progressDialog = new ProgressDialog(this);

        btnRemoveReg.setOnClickListener(v -> {

            if (txtTagRemove.getText().toString().matches("") && txtIDRemove.getText().toString().matches("")){

                Toast.makeText(activity_remove.this, "Please fill one of the required parameters", Toast.LENGTH_LONG).show();
                return;

            } else {

                removePrototype();

                /*
                Toast.makeText(activity_remove.this, "Removed successfully!", Toast.LENGTH_LONG).show();
                finish();
                Intent intent = new Intent(this, activity_admin_menu.class);
                startActivity(intent);*/

            }
        });
    }

    private void removePrototype(){

        final String tagCode = txtTagRemove.getText().toString().trim();
        final String prototypeID = txtIDRemove.getText().toString().trim();

        //progressDialog.setMessage("Removing Prototype...");
        //progressDialog.show();

        StringRequest stringRequest = new StringRequest(Request.Method.POST,
                Constants.URL_REMOVE_PROTOTYPE,
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
