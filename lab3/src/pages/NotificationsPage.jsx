import { useEffect, useState } from 'react'
import axiosClient from '../api/axiosClient'

export default function NotificationsPage() {
  const [notifications, setNotifications] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  const loadNotifications = async () => {
    setLoading(true)
    try {
      const res = await axiosClient.get('/api/notifications')
      setNotifications(res.data)
    } catch {
      setError('Помилка завантаження сповіщень')
    } finally {
      setLoading(false)
    }
  }

  const markAsRead = async (id) => {
    try {
      await axiosClient.put(`/api/notifications/${id}/read`)
      setNotifications((prev) =>
        prev.map((n) => n.notificationId === id ? { ...n, isRead: true } : n)
      )
    } catch {
      alert('Помилка')
    }
  }

  useEffect(() => { loadNotifications() }, [])

  return (
    <div style={styles.container}>
      <h2 style={styles.heading}>Сповіщення</h2>

      {loading && <p>Завантаження...</p>}
      {error && <p style={styles.error}>{error}</p>}
      {!loading && notifications.length === 0 && (
        <p style={{ color: '#888' }}>Сповіщень немає</p>
      )}

      {notifications.map((n) => (
        <div
          key={n.notificationId}
          style={{
            ...styles.card,
            opacity: n.isRead ? 0.6 : 1,
            borderLeft: n.isRead ? '4px solid #ccc' : '4px solid #6200ea',
          }}
        >
          <div style={styles.cardHeader}>
            <strong>{n.title}</strong>
            <span style={styles.date}>
              {new Date(n.created_at || n.createdAt).toLocaleString('uk-UA')}
            </span>
          </div>
          <p style={styles.message}>{n.message}</p>
          {!n.isRead && (
            <button style={styles.button} onClick={() => markAsRead(n.notificationId)}>
              Позначити як прочитане
            </button>
          )}
        </div>
      ))}
    </div>
  )
}

const styles = {
  container: { padding: '24px', maxWidth: '700px', margin: '0 auto' },
  heading: { color: '#6200ea' },
  card: {
    backgroundColor: '#fff',
    borderRadius: '10px',
    padding: '16px 20px',
    marginBottom: '14px',
    boxShadow: '0 2px 8px rgba(0,0,0,0.07)',
  },
  cardHeader: {
    display: 'flex',
    justifyContent: 'space-between',
    marginBottom: '6px',
  },
  date: { fontSize: '12px', color: '#888' },
  message: { fontSize: '14px', margin: '0 0 10px' },
  button: {
    padding: '6px 14px',
    backgroundColor: '#6200ea',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    cursor: 'pointer',
    fontSize: '12px',
  },
  error: { color: 'red' },
}