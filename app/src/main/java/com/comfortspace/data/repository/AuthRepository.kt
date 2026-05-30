package com.comfortspace.data.repository

import com.comfortspace.data.local.TokenStorage
import com.comfortspace.data.model.*
import com.comfortspace.data.remote.ApiService

class AuthRepository(private val api: ApiService, private val tokenStorage: TokenStorage) {

    suspend fun login(email: String, password: String): Result<LoginResponse> {
        return try {
            val response = api.login(LoginRequest(email, password))
            if (response.isSuccessful) {
                val body = response.body()!!
                tokenStorage.saveToken(body.token)
                tokenStorage.saveUserId(body.userId)
                tokenStorage.saveRoomId(body.roomId)
                Result.success(body)
            } else Result.failure(Exception("Невірний email або пароль"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    suspend fun register(surname: String, name: String, patronymic: String, email: String, password: String): Result<String> {
        return try {
            val response = api.register(RegisterRequest(surname, name, patronymic, email, password))
            if (response.isSuccessful) Result.success("Реєстрація успішна")
            else Result.failure(Exception("Помилка реєстрації"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    fun logout() = tokenStorage.logout()
    fun isLoggedIn() = tokenStorage.isLoggedIn()
}

