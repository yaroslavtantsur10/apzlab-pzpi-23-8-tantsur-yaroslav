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
import com.comfortspace.viewmodel.DashboardViewModel

class DashboardFragment : Fragment(R.layout.fragment_dashboard) {

    private val viewModel: DashboardViewModel by viewModels {
        object : ViewModelProvider.Factory {
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                val api = RetrofitClient.create(requireContext())
                @Suppress("UNCHECKED_CAST")
                return DashboardViewModel(SensorRepository(api)) as T
            }
        }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val tvTemperature = view.findViewById<TextView>(R.id.tvTemperature)
        val tvHumidity = view.findViewById<TextView>(R.id.tvHumidity)
        val tvNoise = view.findViewById<TextView>(R.id.tvNoise)
        val tvLight = view.findViewById<TextView>(R.id.tvLight)
        val tvComfortScore = view.findViewById<TextView>(R.id.tvComfortScore)
        val tvComfortLevel = view.findViewById<TextView>(R.id.tvComfortLevel)
        val progressBar = view.findViewById<ProgressBar>(R.id.progressBar)
        val btnRefresh = view.findViewById<Button>(R.id.btnRefresh)

        val roomId = TokenStorage(requireContext()).getRoomId()
        viewModel.loadData(roomId)

        viewModel.isLoading.observe(viewLifecycleOwner) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        viewModel.readings.observe(viewLifecycleOwner) { result ->
            result.onSuccess { readings ->
                readings.forEach { reading ->
                    when (reading.sensorType) {
                        "temperature" -> tvTemperature.text = "Температура: ${reading.value}°C"
                        "humidity"    -> tvHumidity.text    = "Вологість: ${reading.value}%"
                        "noise"       -> tvNoise.text       = "Шум: ${reading.value}%"
                        "light"       -> tvLight.text       = "Освітлення: ${reading.value}%"
                    }
                }
            }
            result.onFailure {
                Toast.makeText(requireContext(), it.message, Toast.LENGTH_SHORT).show()
            }
        }

        viewModel.comfort.observe(viewLifecycleOwner) { result ->
            result.onSuccess { comfort ->
                tvComfortScore.text = "Рівень комфорту: ${comfort.score.toInt()}%"
                tvComfortLevel.text = comfort.level
                tvComfortLevel.setTextColor(
                    when (comfort.level) {
                        "Comfortable" -> android.graphics.Color.parseColor("#2e7d32")
                        "Moderate"    -> android.graphics.Color.parseColor("#f57c00")
                        else          -> android.graphics.Color.parseColor("#c62828")
                    }
                )
            }
        }

        btnRefresh.setOnClickListener { viewModel.loadData(roomId) }
    }
}

