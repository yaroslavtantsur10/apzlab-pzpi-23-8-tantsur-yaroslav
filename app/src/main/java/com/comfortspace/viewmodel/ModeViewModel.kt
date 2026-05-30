package com.comfortspace.viewmodel

import androidx.lifecycle.*
import com.comfortspace.data.repository.SensorRepository
import kotlinx.coroutines.launch

class ModeViewModel(private val repo: SensorRepository) : ViewModel() {

    private val _modeResult = MutableLiveData<Result<String>>()
    val modeResult: LiveData<Result<String>> = _modeResult

    private val _isLoading = MutableLiveData<Boolean>()
    val isLoading: LiveData<Boolean> = _isLoading

    fun setMode(roomId: Int, modeName: String) {
        viewModelScope.launch {
            _isLoading.value = true
            _modeResult.value = repo.setMode(roomId, modeName)
            _isLoading.value = false
        }
    }
}

