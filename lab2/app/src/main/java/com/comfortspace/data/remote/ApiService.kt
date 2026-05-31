package com.comfortspace.data.remote

import com.comfortspace.data.model.*
import retrofit2.Response
import retrofit2.http.*

interface ApiService {
    @POST("api/auth/login")
    suspend fun login(@Body request: LoginRequest): Response<LoginResponse>

    @POST("api/auth/register")
    suspend fun register(@Body request: RegisterRequest): Response<MessageResponse>

    @GET("api/users/{id}")
    suspend fun getUser(@Path("id") id: Int): Response<User>

    @PUT("api/users/{id}")
    suspend fun updateUser(@Path("id") id: Int, @Body user: UpdateUserRequest): Response<User>

    @GET("api/readings")
    suspend fun getReadings(@Query("roomId") roomId: Int): Response<List<SensorReading>>

    @GET("api/comfort/{roomId}")
    suspend fun getComfort(@Path("roomId") roomId: Int): Response<ComfortScore>

    @GET("api/comfort/{roomId}/history")
    suspend fun getComfortHistory(@Path("roomId") roomId: Int): Response<List<ComfortScore>>

    @POST("api/rooms/{id}/mode")
    suspend fun setMode(@Path("id") roomId: Int, @Body request: ModeRequest): Response<MessageResponse>

    @GET("api/notifications")
    suspend fun getNotifications(): Response<List<Notification>>

    @PUT("api/notifications/{id}/read")
    suspend fun markAsRead(@Path("id") id: Int): Response<MessageResponse>

    @DELETE("api/notifications/{id}")
    suspend fun deleteNotification(@Path("id") id: Int): Response<MessageResponse>
}

