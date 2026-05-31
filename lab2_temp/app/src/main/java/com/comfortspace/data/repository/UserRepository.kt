package com.comfortspace.data.repository

import com.comfortspace.data.model.*
import com.comfortspace.data.remote.ApiService

class UserRepository(private val api: ApiService) {

    suspend fun getUser(id: Int): Result<User> {
        return try {
            val response = api.getUser(id)
            if (response.isSuccessful) Result.success(response.body()!!)
            else Result.failure(Exception("Помилка отримання профілю"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    suspend fun updateUser(id: Int, request: UpdateUserRequest): Result<User> {
        return try {
            val response = api.updateUser(id, request)
            if (response.isSuccessful) Result.success(response.body()!!)
            else Result.failure(Exception("Помилка оновлення профілю"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }
}

