import React, { useState } from "react"; 
import { createTask } from "../Services/taskApiService";
import '../Styles/TaskForm.css';

const TaskForm = () => {

    // Form state variables
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [status, setStatus] = useState(false);
    const [dueDate, setDueDate] = useState("");

    // UI and feedback state variables
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const[message, setMessage] = useState("");

    // Handle form submission and sends the data to the API
    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        // Basic validation for required fields

        if (!title || !dueDate) {

            setError("Title and Due Date are required fields.");
            setLoading(false);
            return;
    }

    try {

        // Build the task object from the form inputs
        const taskTemplate = {
        title: title,
        description: description,
        status: status,
        dueDate: new Date(dueDate).toISOString(),

    };

    // Send data to the backend API
     await createTask(taskTemplate);

        setSuccess(true);
        setMessage("Task created successfully!");

        setTitle("");
        setDescription("");
        setStatus(false);
        setDueDate("");
    }
    catch (error) {
        
        // Display any error returned by the API
        setError(error.message);
        setSuccess(false);
    }
    setLoading(false);
    
}

return (
    <div className="task-form-container">
        <h2>Create New Task</h2>
        <form onSubmit={handleSubmit} className="task-form">
            <div className="form-group">
                <label>Title:</label>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} required />
            </div>
            <div className="form-group">
                <label>Description:</label>
                <textarea value={description} onChange={(e) => setDescription(e.target.value)}></textarea>
            </div>
            <div className="form-group status-group"> 
                <label>Status (Completed):</label>
                <input type="checkbox" checked={status} onChange={(e) => setStatus(e.target.checked)} />
            </div>
            <div className="form-group">
                <label>Due Date:</label>
                <input type="datetime-local" value={dueDate} onChange={(e) => setDueDate(e.target.value)} required />
            </div>
            <button type="submit" disabled={loading}>{loading ? "Creating..." : "Create Task"}</button>
        </form>
        {error && <div className="error-message">{error}</div>}
        {success && <div className="success-message">{message}</div>}
    </div>
)
}

export default TaskForm;