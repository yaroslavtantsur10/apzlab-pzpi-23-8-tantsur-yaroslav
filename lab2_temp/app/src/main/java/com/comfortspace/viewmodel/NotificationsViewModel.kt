package com.comfortspace.viewmodel

import androidx.lifecycle.*
import com.comfortspace.data.model.Notification
import com.comfortspace.data.repository.NotificationRepository
import kotlinx.coroutines.launch

class NotificationsViewModel(private val repo: NotificationRepository) : ViewModel() {

    private val _notifications = MutableLiveData<Result<List<Notification>>>()
    val notifications: LiveData<Result<List<Notification>>> = _notifications

    private val _isLoading = MutableLiveData<Boolean>()
    val isLoading: LiveData<Boolean> = _isLoading

    fun loadNotifications() {
        viewModelScope.launch {
            _isLoading.value = true
            _notifications.value = repo.getNotifications()
            _isLoading.value = false
        }
    }

    fun markAsRead(id: Int) {
        viewModelScope.launch { repo.markAsRead(id) }
    }
}

