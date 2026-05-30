package com.comfortspace.data.repository

import com.comfortspace.data.model.Notification
import com.comfortspace.data.remote.ApiService

class NotificationRepository(private val api: ApiService) {

    suspend fun getNotifications(): Result<List<Notification>> {
        return try {
            val response = api.getNotifications()
            if (response.isSuccessful) Result.success(response.body() ?: emptyList())
            else Result.failure(Exception("Помилка отримання сповіщень"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    suspend fun markAsRead(id: Int): Result<String> {
        return try {
            val response = api.markAsRead(id)
            if (response.isSuccessful) Result.success("Позначено як прочитане")
            else Result.failure(Exception("Помилка"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }
}

