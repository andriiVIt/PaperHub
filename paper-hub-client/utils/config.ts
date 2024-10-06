import axios from 'axios'

export const controller = new AbortController()



export const api = axios.create({
  validateStatus: function(status: number) {
    return Boolean(status)
  },
  //baseURL: process.env.REACT_APP_BASE_URL,
  baseURL: 'http://localhost:5183/api',
  headers: {
    'Access-Control-Allow-Origin': '*',
  },
  signal: controller.signal
})

