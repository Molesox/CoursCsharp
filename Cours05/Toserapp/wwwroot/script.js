document.addEventListener('DOMContentLoaded', function ()
{

    const loadTodosButton = document.getElementById('loadTodos');
    const userIdInput = document.getElementById('userIdInput');
    const todoList = document.getElementById('todoList');

    loadTodosButton.addEventListener('click', function() {
        const userId = userIdInput.value;
        if (userId) {
            fetchTodos(userId);
        } else {
            alert('Please enter a User ID.');
        }
    });

    function fetchTodos(userId)
    {
        showLoadingIndicator();
        const apiUrl = `/api/todoitems/user/${userId}`;
        fetch(apiUrl)
            .then(handleResponse)
            .then(updateTodoList)
            .catch(handleFetchError)
            .finally(hideLoadingIndicator);
    }

    function createTodoItem(item)
    {
        const div = document.createElement('div');
        div.id = `todo-item-${item.todoItemId}`;
        div.className = 'todo-item';
        div.innerHTML = `${item.todoItemId}) <strong>${item.description}</strong>`;

        // Create checkbox for isCompleted status
        const completedCheckbox = document.createElement('input');
        completedCheckbox.type = 'checkbox';
        completedCheckbox.checked = item.isCompleted;
        completedCheckbox.className = 'completed-checkbox';
        completedCheckbox.onchange = function () { toggleCompletion(item.todoItemId, completedCheckbox.checked); };
        div.appendChild(completedCheckbox);

        // Add delete button
        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'Delete';
        deleteButton.className = 'delete-button';
        deleteButton.onclick = function () { deleteTodoItem(item.todoItemId); };
        div.appendChild(deleteButton);

        if (item.subTasks && item.subTasks.length > 0)
        {
            const subTaskList = document.createElement('div');
            subTaskList.className = 'sub-task';
            item.subTasks.forEach(subTask =>
            {
                subTaskList.appendChild(createTodoItem(subTask));
            });
            div.appendChild(subTaskList);
        }

        return div;
    }
    function toggleCompletion(todoItemId, isCompleted)
    {
        console.log(`Item ID: ${todoItemId}, Completed: ${isCompleted}`);
    }

    function deleteTodoItem(todoItemId)
    {
        // Send a DELETE request to the server
        fetch(`/api/todoitems/${todoItemId}`, {
            method: 'DELETE'
        })
            .then(response =>
            {
                if (!response.ok)
                {
                    throw new Error('Deletion failed');
                }
                //return response.json();
            })
            .then(() =>
            {
                // Remove the item from the UI
                document.querySelector(`#todo-item-${todoItemId}`).remove();
            })
            .catch(error =>
            {
                console.error('Error:', error);
                alert('Failed to delete the item. Please try again later.');
            });
    }
    function showLoadingIndicator()
    {
        todoList.innerHTML = '<div>Loading...</div>'; // Show loading message
    }

    function handleResponse(response)
    {
        if (!response.ok) throw new Error('Network response was not ok.');
        return response.json();
    }

    function updateTodoList(data)
    {
        todoList.innerHTML = ''; // Clear existing todos
        data.forEach(item =>
        {
            todoList.appendChild(createTodoItem(item));
        });
    }

    function handleFetchError(error)
    {
        console.error('Error:', error);
        alert('Failed to fetch data. Please try again later.');
    }

    function hideLoadingIndicator()
    {
        // Optional: hide or remove loading indicator
    }
});
