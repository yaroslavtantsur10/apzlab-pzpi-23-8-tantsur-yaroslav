package com.comfortspace.ui

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.comfortspace.R
import com.comfortspace.data.local.TokenStorage
import com.comfortspace.data.remote.RetrofitClient
import com.comfortspace.data.repository.UserRepository
import com.comfortspace.viewmodel.ProfileViewModel

class ProfileFragment : Fragment(R.layout.fragment_profile) {

    private val viewModel: ProfileViewModel by viewModels {
        object : ViewModelProvider.Factory {
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                val api = RetrofitClient.create(requireContext())
                @Suppress("UNCHECKED_CAST")
                return ProfileViewModel(UserRepository(api)) as T
            }
        }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val etSurname = view.findViewById<EditText>(R.id.etSurname)
        val etName = view.findViewById<EditText>(R.id.etName)
        val etEmail = view.findViewById<EditText>(R.id.etEmail)
        val btnSave = view.findViewById<Button>(R.id.btnSave)
        val btnLogout = view.findViewById<Button>(R.id.btnLogout)
        val progressBar = view.findViewById<ProgressBar>(R.id.progressBar)

        val tokenStorage = TokenStorage(requireContext())
        val userId = tokenStorage.getUserId()

        viewModel.loadUser(userId)

        viewModel.isLoading.observe(viewLifecycleOwner) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        viewModel.user.observe(viewLifecycleOwner) { result ->
            result.onSuccess { user ->
                etSurname.setText(user.surname)
                etName.setText(user.name)
                etEmail.setText(user.email)
            }
        }

        btnSave.setOnClickListener {
            viewModel.updateUser(
                userId,
                etSurname.text.toString().trim(),
                etName.text.toString().trim(),
                "",
                etEmail.text.toString().trim(),
                null
            )
        }

        btnLogout.setOnClickListener {
            tokenStorage.logout()
            startActivity(Intent(requireContext(), LoginActivity::class.java))
            requireActivity().finish()
        }

        viewModel.updateResult.observe(viewLifecycleOwner) { result ->
            result.onSuccess {
                Toast.makeText(requireContext(), "Профіль оновлено!", Toast.LENGTH_SHORT).show()
            }
            result.onFailure {
                Toast.makeText(requireContext(), it.message, Toast.LENGTH_SHORT).show()
            }
        }
    }
}

