package com.comfortspace.data.model

data class ComfortScore(
    val roomId: Int,
    val score: Double,
    val level: String,
    val temperature: Double,
    val humidity: Double,
    val noise: Double,
    val light: Double,
    val timestamp: String
)

