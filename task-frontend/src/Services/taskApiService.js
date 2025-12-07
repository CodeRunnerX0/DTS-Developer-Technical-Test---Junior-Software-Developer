import api from "./api";

export async function createTask (taskData) {
try {

    // Send a POST request to create a new task using task data
    const response = await api.post('/TaskItem', taskData);

    return response.data;
    }
    catch (error) {
        console.error("Error creating task");

        // If the API responded with an error log the details
        if (error.response) {
            console.error("Response data:", error.response.data);
            console.error("Response status:", error.response.status);
            console.error("Response headers:", error.response.headers);

            // Throw a specific error including the status code
            throw new Error(`API Error ${error.response.status}`);
        } 
        // If no response was received it was likely a network issue
        throw new Error('Network error: Could not connect to the API server.');
    }
    
}