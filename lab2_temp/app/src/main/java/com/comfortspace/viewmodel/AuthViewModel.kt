package com.comfortspace.viewmodel

import androidx.lifecycle.*
import com.comfortspace.data.model.LoginResponse
import com.comfortspace.data.repository.AuthRepository
import kotlinx.coroutines.launch

class AuthViewModel(private val repo: AuthRepository) : ViewModel() {

    private val _loginResult = MutableLiveData<Result<LoginResponse>>()
    val loginResult: LiveData<Result<LoginResponse>> = _loginResult

    private val _registerResult = MutableLiveData<Result<String>>()
    val registerResult: LiveData<Result<String>> = _registerResult

    private val _isLoading = MutableLiveData<Boolean>()
    val isLoading: LiveData<Boolean> = _isLoading

    fun login(email: String, password: String) {
        viewModelScope.launch {
            _isLoading.value = true
            _loginResult.value = repo.login(email, password)
            _isLoading.value = false
        }
    }

    fun register(surname: String, name: String, patronymic: String, email: String, password: String) {
        viewModelScope.launch {
            _isLoading.value = true
            _registerResult.value = repo.register(surname, name, patronymic, email, password)
            _isLoading.value = false
        }
    }

    fun logout() = repo.logout()
    fun isLoggedIn() = repo.isLoggedIn()
}

