import { useEffect, useState } from 'react'
import axiosClient from '../api/axiosClient'

export default function DashboardPage() {
  const roomId = localStorage.getItem('room_id')
  const [comfort, setComfort] = useState(null)
  const [readings, setReadings] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  const loadData = async () => {
    setLoading(true)
    setError('')
    try {
      const [comfortRes, readingsRes] = await Promise.all([
        axiosClient.get(`/api/comfort/${roomId}`),
        axiosClient.get(`/api/readings?roomId=${roomId}`),
      ])
      setComfort(comfortRes.data)
      setReadings(readingsRes.data)
    } catch {
      setError('Помилка завантаження даних')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => { loadData() }, [])

  return (
    <div style={styles.container}>
      <h2 style={styles.heading}>Показники номера</h2>

      {loading && <p>Завантаження...</p>}
      {error && <p style={styles.error}>{error}</p>}

      {comfort && (
        <div style={styles.card}>
          <p style={{ color: '#6200ea', fontWeight: 'bold', fontSize: '18px' }}>
            {comfort.level}
          </p>
          <p>Рівень комфорту: {comfort.score}%</p>
          <hr />
          <p>🌡 Температура: {comfort.temperature}°C</p>
          <p>💧 Вологість: {comfort.humidity}%</p>
          <p>🔊 Шум: {comfort.noise}%</p>
          <p>💡 Освітлення: {comfort.light}%</p>
          <button style={styles.button} onClick={loadData}>Оновити</button>
        </div>
      )}

      {readings.length > 0 && (
        <div style={styles.card}>
          <h3>Історія показників</h3>
          <table style={styles.table}>
            <thead>
              <tr>
                <th>Тип</th>
                <th>Значення</th>
                <th>Час</th>
              </tr>
            </thead>
            <tbody>
              {readings.slice(0, 10).map((r) => (
                <tr key={r.readingId}>
                  <td>{r.sensorType}</td>
                  <td>{r.value}</td>
                  <td>{new Date(r.capturedAt).toLocaleString('uk-UA')}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}

const styles = {
  container: { padding: '24px', maxWidth: '800px', margin: '0 auto' },
  heading: { color: '#6200ea' },
  card: {
    backgroundColor: '#fff',
    borderRadius: '12px',
    padding: '20px',
    marginBottom: '20px',
    boxShadow: '0 2px 10px rgba(0,0,0,0.08)',
  },
  button: {
    marginTop: '12px',
    padding: '10px 20px',
    backgroundColor: '#6200ea',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    cursor: 'pointer',
  },
  error: { color: 'red' },
  table: { width: '100%', borderCollapse: 'collapse', fontSize: '13px' },
}