import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import "bootstrap/dist/css/bootstrap.min.css";
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,

  
  <div className="container mt-4">

    <h2 className="mb-4">Employee Management</h2>

    <button className="btn btn-primary">
        Add Employee
    </button>

    <button className="btn btn-warning ms-2">
        Edit
    </button>

    <button className="btn btn-danger ms-2">
        Delete
    </button>

</div>

)

