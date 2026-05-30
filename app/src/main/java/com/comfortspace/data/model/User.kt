package com.comfortspace.data.model

data class User(
    val userId: Int,
    val surname: String,
    val name: String,
    val patronymic: String,
    val email: String,
    val phoneNumber: String?,
    val status: String,
    val role: String
)

data class UpdateUserRequest(
    val surname: String,
    val name: String,
    val patronymic: String,
    val email: String,
    val phoneNumber: String?
)

