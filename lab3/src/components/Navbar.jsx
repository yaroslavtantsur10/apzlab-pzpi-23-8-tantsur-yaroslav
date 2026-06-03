import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

export default function Navbar() {
  const { user, logout, isAdmin } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <nav style={styles.nav}>
      <span style={styles.logo}>ComfortSpace</span>
      <div style={styles.links}>
        <Link style={styles.link} to="/dashboard">Головна</Link>
        <Link style={styles.link} to="/mode">Режим</Link>
        <Link style={styles.link} to="/notifications">Сповіщення</Link>
        <Link style={styles.link} to="/profile">Профіль</Link>
        {isAdmin() && (
          <>
            <Link style={styles.link} to="/admin/users">Користувачі</Link>
            <Link style={styles.link} to="/admin/data">Дані</Link>
          </>
        )}
        <button style={styles.logout} onClick={handleLogout}>Вийти</button>
      </div>
    </nav>
  )
}

const styles = {
  nav: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#6200ea',
    padding: '12px 24px',
  },
  logo: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: '18px',
  },
  links: {
    display: 'flex',
    gap: '16px',
    alignItems: 'center',
  },
  link: {
    color: '#fff',
    textDecoration: 'none',
    fontSize: '14px',
  },
  logout: {
    backgroundColor: '#fff',
    color: '#6200ea',
    border: 'none',
    borderRadius: '6px',
    padding: '6px 14px',
    cursor: 'pointer',
    fontWeight: 'bold',
  },
}