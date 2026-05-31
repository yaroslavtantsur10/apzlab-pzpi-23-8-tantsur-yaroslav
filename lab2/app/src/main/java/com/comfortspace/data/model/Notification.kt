package com.comfortspace.data.model

import com.google.gson.annotations.SerializedName

data class Notification(
    val notificationId: Int,
    val title: String,
    val message: String,
    val type: String,
    @SerializedName("created_at")
    val createdAt: String,
    val isRead: Boolean = false
)

