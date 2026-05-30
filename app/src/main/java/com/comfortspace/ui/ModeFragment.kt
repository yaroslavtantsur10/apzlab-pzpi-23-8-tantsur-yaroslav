package com.comfortspace.ui

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
import com.comfortspace.data.repository.SensorRepository
import com.comfortspace.viewmodel.ModeViewModel

class ModeFragment : Fragment(R.layout.fragment_mode) {

    private val viewModel: ModeViewModel by viewModels {
        object : ViewModelProvider.Factory {
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                val api = RetrofitClient.create(requireContext())
                @Suppress("UNCHECKED_CAST")
                return ModeViewModel(SensorRepository(api)) as T
            }
        }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val btnSleep = view.findViewById<Button>(R.id.btnSleep)
        val btnRelax = view.findViewById<Button>(R.id.btnRelax)
        val btnFocus = view.findViewById<Button>(R.id.btnFocus)
        val tvCurrentMode = view.findViewById<TextView>(R.id.tvCurrentMode)
        val progressBar = view.findViewById<ProgressBar>(R.id.progressBar)

        val roomId = TokenStorage(requireContext()).getRoomId()

        btnSleep.setOnClickListener { viewModel.setMode(roomId, "Сон") }
        btnRelax.setOnClickListener { viewModel.setMode(roomId, "Відпочинок") }
        btnFocus.setOnClickListener { viewModel.setMode(roomId, "Фокус") }

        viewModel.isLoading.observe(viewLifecycleOwner) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        viewModel.modeResult.observe(viewLifecycleOwner) { result ->
            result.onSuccess { msg ->
                Toast.makeText(requireContext(), "Режим встановлено!", Toast.LENGTH_SHORT).show()
                tvCurrentMode.text = msg
            }
            result.onFailure {
                Toast.makeText(requireContext(), it.message, Toast.LENGTH_SHORT).show()
            }
        }
    }
}

