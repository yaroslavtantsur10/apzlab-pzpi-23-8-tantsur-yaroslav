package com.comfortspace.data.model

data class SensorReading(
    val readingId: Int,
    val sensorId: Int,
    val value: Double,
    val capturedAt: String,
    val sensorType: String
)

