package com.example.mainaltice;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;

public class activity_admin_menu extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin_menu);

        if(!SharedPrefManager.getInstance(this).isLoggedIn()){
            finish();
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

        Button btnUpdateAdmin = findViewById(R.id.btnUpdateAdmin);
        Button btnSearchAdmin = findViewById(R.id.btnSearchAdmin);
        Button btnRegisterAdmin = findViewById(R.id.btnRegisterAdmin);
        Button btnRemoveAdmin = findViewById(R.id.btnRemoveAdmin);

        btnUpdateAdmin.setOnClickListener(v -> {

            finish();
            Intent intent = new Intent(this, activity_update.class);
            startActivity(intent);

        });

        btnSearchAdmin.setOnClickListener(v -> {

            finish();
            Intent intent = new Intent(this, activity_search.class);
            startActivity(intent);

        });

        btnRegisterAdmin.setOnClickListener(v -> {

            finish();
            Intent intent = new Intent(this, activity_register.class);
            startActivity(intent);

        });

        btnRemoveAdmin.setOnClickListener(v -> {

           finish();
           Intent intent = new Intent(this, activity_remove.class);
           startActivity(intent);

        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()){
            case R.id.menuLogout:
                SharedPrefManager.getInstance(this).logout();
                finish();
                Intent intent = new Intent(this, MainActivity.class);
                startActivity(intent);
                break;
        }
        return true;
    }

    public void onBackPressed(){

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setMessage("Are you sure you want to exit?")
                .setCancelable(false)
                .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        Intent intent = new Intent(Intent.ACTION_MAIN);
                        intent.addCategory(Intent.CATEGORY_HOME);
                        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                        startActivity(intent);
                    }
                })

                .setNegativeButton("No", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int which) {
                        dialogInterface.cancel();
                    }
                });

        AlertDialog alertDialog = builder.create();
        alertDialog.show();

    }

}
