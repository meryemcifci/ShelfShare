import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as any,
    token: localStorage.getItem('token') || '',
  }),
  actions: {
    async login(email: string, password: string) {
      const res = await axios.post('http://localhost:5000/api/auth/login', { email, password })
      this.token = res.data.token
      this.user = res.data.user
      if (this.token) localStorage.setItem('token', this.token)
    },
    async register(fullName: string, email: string, password: string) {
      await axios.post('http://localhost:5000/api/auth/register', { fullName, email, password })
    },
    logout() {
      this.user = null
      this.token = ''
      localStorage.removeItem('token')
    },
  },
})
