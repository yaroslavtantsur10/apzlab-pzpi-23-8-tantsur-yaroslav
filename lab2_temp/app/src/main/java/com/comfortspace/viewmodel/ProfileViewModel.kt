package com.comfortspace.viewmodel

import androidx.lifecycle.*
import com.comfortspace.data.model.*
import com.comfortspace.data.repository.UserRepository
import kotlinx.coroutines.launch

class ProfileViewModel(private val repo: UserRepository) : ViewModel() {

    private val _user = MutableLiveData<Result<User>>()
    val user: LiveData<Result<User>> = _user

    private val _updateResult = MutableLiveData<Result<User>>()
    val updateResult: LiveData<Result<User>> = _updateResult

    private val _isLoading = MutableLiveData<Boolean>()
    val isLoading: LiveData<Boolean> = _isLoading

    fun loadUser(id: Int) {
        viewModelScope.launch {
            _isLoading.value = true
            _user.value = repo.getUser(id)
            _isLoading.value = false
        }
    }

    fun updateUser(id: Int, surname: String, name: String, patronymic: String, email: String, phone: String?) {
        viewModelScope.launch {
            _isLoading.value = true
            _updateResult.value = repo.updateUser(id, UpdateUserRequest(surname, name, patronymic, email, phone))
            _isLoading.value = false
        }
    }
}

