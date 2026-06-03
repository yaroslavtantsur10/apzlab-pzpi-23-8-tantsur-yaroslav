import { useState } from 'react'
import axiosClient from '../api/axiosClient'

const MODES = [
  { name: 'Sleep', label: '🌙 Сон' },
  { name: 'Rest', label: '🛋 Відпочинок' },
  { name: 'Focus', label: '🎯 Фокус' },
]

export default function ModePage() {
  const roomId = localStorage.getItem('room_id')
  const [selected, setSelected] = useState('')
  const [message, setMessage] = useState('')
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const handleSetMode = async (modeName) => {
    setSelected(modeName)
    setMessage('')
    setError('')
    setLoading(true)
    try {
      await axiosClient.post(`/api/rooms/${roomId}/mode`, { modeName })
      setMessage(`Режим «${modeName}» встановлено`)
    } catch {
      setError('Помилка встановлення режиму')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2 style={styles.heading}>Оберіть режим</h2>

        {message && <p style={styles.success}>{message}</p>}
        {error && <p style={styles.error}>{error}</p>}

        {MODES.map((mode) => (
          <button
            key={mode.name}
            style={{
              ...styles.button,
              backgroundColor: selected === mode.name ? '#4a00b0' : '#6200ea',
            }}
            onClick={() => handleSetMode(mode.name)}
            disabled={loading}
          >
            {mode.label}
          </button>
        ))}
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
    width: '320px',
    boxShadow: '0 2px 10px rgba(0,0,0,0.1)',
    display: 'flex',
    flexDirection: 'column',
    gap: '12px',
  },
  heading: { textAlign: 'center', color: '#6200ea' },
  button: {
    padding: '14px',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    fontSize: '15px',
    cursor: 'pointer',
  },
  success: { color: 'green', textAlign: 'center', fontSize: '13px' },
  error: { color: 'red', textAlign: 'center', fontSize: '13px' },
}