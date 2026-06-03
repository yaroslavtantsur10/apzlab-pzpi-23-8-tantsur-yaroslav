import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export default function RegisterPage() {
  const { register } = useAuth()
  const navigate = useNavigate()

  const [form, setForm] = useState({
    surname: '',
    name: '',
    patronymic: '',
    email: '',
    password: '',
  })
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      await register(form.surname, form.name, form.patronymic, form.email, form.password)
      navigate('/login')
    } catch (err) {
      setError('Помилка реєстрації. Спробуйте ще раз.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2 style={styles.title}>Реєстрація</h2>

        {error && <p style={styles.error}>{error}</p>}

        <form onSubmit={handleSubmit}>
          {[
            { name: 'surname', placeholder: 'Прізвище' },
            { name: 'name', placeholder: "Ім'я" },
            { name: 'patronymic', placeholder: 'По батькові' },
            { name: 'email', placeholder: 'Електронна пошта', type: 'email' },
            { name: 'password', placeholder: 'Пароль', type: 'password' },
          ].map((field) => (
            <input
              key={field.name}
              style={styles.input}
              type={field.type || 'text'}
              name={field.name}
              placeholder={field.placeholder}
              value={form[field.name]}
              onChange={handleChange}
              required
            />
          ))}

          <button style={styles.button} type="submit" disabled={loading}>
            {loading ? 'Завантаження...' : 'Зареєструватись'}
          </button>
        </form>

        <Link to="/login" style={styles.link}>
          Вже є акаунт? Увійти
        </Link>
      </div>
    </div>
  )
}

const styles = {
  container: {
    minHeight: '100vh',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#f0f0f0',
  },
  card: {
    backgroundColor: '#fff',
    padding: '40px',
    borderRadius: '12px',
    width: '360px',
    boxShadow: '0 4px 20px rgba(0,0,0,0.1)',
    display: 'flex',
    flexDirection: 'column',
    gap: '8px',
  },
  title: {
    textAlign: 'center',
    color: '#6200ea',
    marginBottom: '8px',
  },
  input: {
    width: '100%',
    padding: '12px',
    borderRadius: '8px',
    border: '1px solid #ccc',
    fontSize: '14px',
    marginBottom: '10px',
    boxSizing: 'border-box',
  },
  button: {
    width: '100%',
    padding: '12px',
    backgroundColor: '#6200ea',
    color: '#fff',
    border: 'none',
    borderRadius: '8px',
    fontSize: '16px',
    cursor: 'pointer',
  },
  error: {
    color: 'red',
    fontSize: '13px',
    textAlign: 'center',
  },
  link: {
    textAlign: 'center',
    color: '#6200ea',
    fontSize: '13px',
    textDecoration: 'none',
  },
}