import { useEffect, useState } from 'react'
import axiosClient from '../../api/axiosClient'

export default function AdminUsersPage() {
  const [users, setUsers] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  const loadUsers = async () => {
    setLoading(true)
    try {
      const res = await axiosClient.get('/api/users')
      setUsers(res.data)
    } catch {
      setError('Помилка завантаження користувачів')
    } finally {
      setLoading(false)
    }
  }

  const deleteUser = async (id) => {
    if (!window.confirm('Видалити користувача?')) return
    try {
      await axiosClient.delete(`/api/users/${id}`)
      setUsers((prev) => prev.filter((u) => u.userId !== id))
    } catch {
      alert('Помилка видалення')
    }
  }

  useEffect(() => { loadUsers() }, [])

  return (
    <div style={styles.container}>
      <h2 style={styles.heading}>Управління користувачами</h2>

      {loading && <p>Завантаження...</p>}
      {error && <p style={styles.error}>{error}</p>}

      <table style={styles.table}>
        <thead style={styles.thead}>
          <tr>
            <th>ID</th>
            <th>Прізвище</th>
            <th>Ім'я</th>
            <th>Email</th>
            <th>Роль</th>
            <th>Статус</th>
            <th>Дії</th>
          </tr>
        </thead>
        <tbody>
          {users.map((u) => (
            <tr key={u.userId} style={styles.tr}>
              <td>{u.userId}</td>
              <td>{u.surname}</td>
              <td>{u.name}</td>
              <td>{u.email}</td>
              <td>{u.role}</td>
              <td>{u.status}</td>
              <td>
                <button
                  style={styles.deleteBtn}
                  onClick={() => deleteUser(u.userId)}
                >
                  Видалити
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

const styles = {
  container: { padding: '24px' },
  heading: { color: '#6200ea' },
  table: {
    width: '100%',
    borderCollapse: 'collapse',
    backgroundColor: '#fff',
    borderRadius: '10px',
    overflow: 'hidden',
    boxShadow: '0 2px 10px rgba(0,0,0,0.08)',
  },
  thead: { backgroundColor: '#6200ea', color: '#fff' },
  tr: { borderBottom: '1px solid #eee', textAlign: 'center' },
  deleteBtn: {
    padding: '5px 12px',
    backgroundColor: '#d32f2f',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    cursor: 'pointer',
    fontSize: '12px',
  },
  error: { color: 'red' },
}