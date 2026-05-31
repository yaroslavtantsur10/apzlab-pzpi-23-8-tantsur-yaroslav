package com.comfortspace.ui

import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.comfortspace.R
import com.comfortspace.data.local.TokenStorage
import com.comfortspace.data.remote.RetrofitClient
import com.comfortspace.data.repository.AuthRepository
import com.comfortspace.viewmodel.AuthViewModel

class RegisterActivity : AppCompatActivity() {

    private val viewModel: AuthViewModel by viewModels {
        object : ViewModelProvider.Factory {
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                val api = RetrofitClient.create(applicationContext)
                val tokenStorage = TokenStorage(applicationContext)
                @Suppress("UNCHECKED_CAST")
                return AuthViewModel(AuthRepository(api, tokenStorage)) as T
            }
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)

        val etSurname = findViewById<EditText>(R.id.etSurname)
        val etName = findViewById<EditText>(R.id.etName)
        val etPatronymic = findViewById<EditText>(R.id.etPatronymic)
        val etEmail = findViewById<EditText>(R.id.etEmail)
        val etPassword = findViewById<EditText>(R.id.etPassword)
        val btnRegister = findViewById<Button>(R.id.btnRegister)
        val progressBar = findViewById<ProgressBar>(R.id.progressBar)

        btnRegister.setOnClickListener {
            val surname = etSurname.text.toString().trim()
            val name = etName.text.toString().trim()
            val patronymic = etPatronymic.text.toString().trim()
            val email = etEmail.text.toString().trim()
            val password = etPassword.text.toString().trim()

            if (surname.isEmpty() || name.isEmpty() || email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Заповніть всі поля", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }
            viewModel.register(surname, name, patronymic, email, password)
        }

        viewModel.isLoading.observe(this) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        viewModel.registerResult.observe(this) { result ->
            result.onSuccess {
                Toast.makeText(this, "Реєстрація успішна! Увійдіть.", Toast.LENGTH_LONG).show()
                finish()
            }
            result.onFailure {
                Toast.makeText(this, it.message, Toast.LENGTH_SHORT).show()
            }
        }
    }
}

