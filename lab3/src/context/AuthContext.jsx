import { createContext, useContext, useState } from 'react'
import axiosClient from '../api/axiosClient'

const AuthContext = createContext(null)

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    // Відновлюємо стан з localStorage при перезавантаженні
    const token = localStorage.getItem('jwt_token')
    const userId = localStorage.getItem('user_id')
    const roomId = localStorage.getItem('room_id')
    const role = localStorage.getItem('role')
    const name = localStorage.getItem('name')
    return token ? { token, userId, roomId, role, name } : null
  })

  const login = async (email, password) => {
    const res = await axiosClient.post('/api/auth/login', { email, password })
    const data = res.data
    // Зберігаємо як у TokenStorage.kt
    localStorage.setItem('jwt_token', data.token)
    localStorage.setItem('user_id', data.userId)
    localStorage.setItem('room_id', data.roomId)
    localStorage.setItem('role', data.role)
    localStorage.setItem('name', data.name)
    setUser(data)
    return data
  }

  const register = async (surname, name, patronymic, email, password) => {
    const res = await axiosClient.post('/api/auth/register', {
      surname, name, patronymic, email, password,
    })
    return res.data
  }

  const logout = () => {
    localStorage.clear()
    setUser(null)
  }

  const isAdmin = () => user?.role === 'Admin'

  return (
    <AuthContext.Provider value={{ user, login, register, logout, isAdmin }}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = () => useContext(AuthContext)