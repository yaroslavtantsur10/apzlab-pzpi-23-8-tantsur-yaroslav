package com.comfortspace.data.local

import android.content.Context

class TokenStorage(context: Context) {
    private val prefs = context.getSharedPreferences("comfortspace_prefs", Context.MODE_PRIVATE)

    fun saveToken(token: String) = prefs.edit().putString(KEY_TOKEN, token).apply()
    fun getToken(): String? = prefs.getString(KEY_TOKEN, null)
    fun clearToken() = prefs.edit().remove(KEY_TOKEN).apply()
    fun saveUserId(id: Int) = prefs.edit().putInt(KEY_USER_ID, id).apply()
    fun getUserId(): Int = prefs.getInt(KEY_USER_ID, -1)
    fun saveRoomId(id: Int) = prefs.edit().putInt(KEY_ROOM_ID, id).apply()
    fun getRoomId(): Int = prefs.getInt(KEY_ROOM_ID, -1)
    fun isLoggedIn(): Boolean = getToken() != null
    fun logout() = prefs.edit().clear().apply()

    companion object {
        private const val KEY_TOKEN = "jwt_token"
        private const val KEY_USER_ID = "user_id"
        private const val KEY_ROOM_ID = "room_id"
    }
}

