import axios from 'axios';

// Base URL for the API, loaded from env file
const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;

// Create an Axios instance with a default configuration
const api = axios.create({
    baseURL: `${API_BASE_URL}/api`,
    headers: {
        'Content-Type': 'application/json',
    },
})

// Export the configured Axios instance so it can be used across the app
export default api;