package com.comfortspace.data.repository

import com.comfortspace.data.model.*
import com.comfortspace.data.remote.ApiService

class SensorRepository(private val api: ApiService) {

    suspend fun getReadings(roomId: Int): Result<List<SensorReading>> {
        return try {
            val response = api.getReadings(roomId)
            if (response.isSuccessful) Result.success(response.body() ?: emptyList())
            else Result.failure(Exception("Помилка отримання даних датчиків"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    suspend fun getComfort(roomId: Int): Result<ComfortScore> {
        return try {
            val response = api.getComfort(roomId)
            if (response.isSuccessful) Result.success(response.body()!!)
            else Result.failure(Exception("Помилка отримання рівня комфорту"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }

    suspend fun setMode(roomId: Int, modeName: String): Result<String> {
        return try {
            val response = api.setMode(roomId, ModeRequest(modeName))
            if (response.isSuccessful) Result.success("Режим встановлено")
            else Result.failure(Exception("Помилка встановлення режиму"))
        } catch (e: Exception) {
            Result.failure(Exception("Помилка з'єднання з сервером"))
        }
    }
}

