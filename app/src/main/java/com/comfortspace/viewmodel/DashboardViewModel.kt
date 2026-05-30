package com.comfortspace.viewmodel

import androidx.lifecycle.*
import com.comfortspace.data.model.*
import com.comfortspace.data.repository.SensorRepository
import kotlinx.coroutines.launch

class DashboardViewModel(private val repo: SensorRepository) : ViewModel() {

    private val _readings = MutableLiveData<Result<List<SensorReading>>>()
    val readings: LiveData<Result<List<SensorReading>>> = _readings

    private val _comfort = MutableLiveData<Result<ComfortScore>>()
    val comfort: LiveData<Result<ComfortScore>> = _comfort

    private val _isLoading = MutableLiveData<Boolean>()
    val isLoading: LiveData<Boolean> = _isLoading

    fun loadData(roomId: Int) {
        viewModelScope.launch {
            _isLoading.value = true
            _readings.value = repo.getReadings(roomId)
            _comfort.value = repo.getComfort(roomId)
            _isLoading.value = false
        }
    }
}

