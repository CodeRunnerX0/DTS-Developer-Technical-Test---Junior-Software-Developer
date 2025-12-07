import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

// Create the root element for rendering the React app
const root = ReactDOM.createRoot(document.getElementById('root'));

// Render the App component within React's StrictMode 
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
reportWebVitals();
