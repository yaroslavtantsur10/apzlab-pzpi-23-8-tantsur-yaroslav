package com.comfortspace.data.model

data class LoginRequest(val email: String, val password: String)

data class LoginResponse(
    val token: String,
    val userId: Int,
    val roomId: Int,
    val role: String,
    val name: String,
    val email: String
)

data class RegisterRequest(
    val surname: String,
    val name: String,
    val patronymic: String,
    val email: String,
    val password: String
)

data class MessageResponse(val message: String)
data class ModeRequest(val modeName: String)

