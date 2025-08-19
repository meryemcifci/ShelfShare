import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:7043/api', // .NET API’nizin base URL’i
  withCredentials: false, // cookie auth yoksa false
})

export default api
