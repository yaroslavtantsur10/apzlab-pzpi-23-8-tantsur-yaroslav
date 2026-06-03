import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axiosClient from '../api/axiosClient'
import { useAuth } from '../context/AuthContext'

export default function ProfilePage() {
  const { logout } = useAuth()
  const navigate = useNavigate()
  const userId = localStorage.getItem('user_id')

  const [form, setForm] = useState({
    surname: '', name: '', patronymic: '', email: '', phoneNumber: '',
  })
  const [loading, setLoading] = useState(true)
  const [message, setMessage] = useState('')
  const [error, setError] = useState('')

  useEffect(() => {
    axiosClient.get(`/api/users/${userId}`)
      .then((res) => {
        const u = res.data
        setForm({
          surname: u.surname || '',
          name: u.name || '',
          patronymic: u.patronymic || '',
          email: u.email || '',
          phoneNumber: u.phoneNumber || '',
        })
      })
      .catch(() => setError('Помилка завантаження профілю'))
      .finally(() => setLoading(false))
  }, [])

  const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  const handleSave = async () => {
    setMessage('')
    setError('')
    try {
      await axiosClient.put(`/api/users/${userId}`, form)
      setMessage('Профіль збережено')
    } catch {
      setError('Помилка збереження')
    }
  }

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  if (loading) return <p style={{ padding: 24 }}>Завантаження...</p>

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2 style={styles.heading}>Профіль</h2>

        {message && <p style={styles.success}>{message}</p>}
        {error && <p style={styles.error}>{error}</p>}

        {[
          { name: 'surname', placeholder: 'Прізвище' },
          { name: 'name', placeholder: "Ім'я" },
          { name: 'patronymic', placeholder: 'По батькові' },
          { name: 'email', placeholder: 'Email', type: 'email' },
          { name: 'phoneNumber', placeholder: 'Телефон' },
        ].map((field) => (
          <input
            key={field.name}
            style={styles.input}
            type={field.type || 'text'}
            name={field.name}
            placeholder={field.placeholder}
            value={form[field.name]}
            onChange={handleChange}
          />
        ))}

        <button style={styles.button} onClick={handleSave}>Зберегти</button>
        <button style={styles.logoutBtn} onClick={handleLogout}>Вийти</button>
      </div>
    </div>
  )
}

const styles = {
  container: {
    minHeight: '80vh',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  card: {
    backgroundColor: '#fff',
    padding: '40px',
    borderRadius: '12px',
    width: '360px',
    boxShadow: '0 2px 10px rgba(0,0,0,0.1)',
    display: 'flex',
    flexDirection: 'column',
    gap: '10px',
  },
  heading: { textAlign: 'center', color: '#6200ea' },
  input: {
    width: '100%',
    padding: '11px',
    borderRadius: '8px',
    border: '1px solid #ccc',
    fontSize: '14px',
    boxSizing: 'border-box',
  },
  button: {
    padding: '12px',
    backgroundColor: '#6200ea',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    cursor: 'pointer',
    fontSize: '15px',
  },
  logoutBtn: {
    padding: '12px',
    backgroundColor: '#d32f2f',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    cursor: 'pointer',
    fontSize: '15px',
  },
  success: { color: 'green', fontSize: '13px', textAlign: 'center' },
  error: { color: 'red', fontSize: '13px', textAlign: 'center' },
}