import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { todoListService, alertService } from '@/_services';

function List({ match }) {
    const { path } = match;
    const [todoLists, setTodoLists] = useState(null);

    useEffect(() => {
        todoListService.getAll().then(x => setTodoLists(x));
    }, []);

    function deleteTodoList(id) {
        setTodoLists(todoLists.map(x => {
            return x;
        }));
        todoListService.delete(id).then(() => {
            setTodoLists(todoLists => todoLists.filter(x => x.id !== id));
            alertService.success('TodoList delete', { keepAfterRouteChange: true });
        })
            .catch(alertService.error);
    }

    return (
        <div>
            <h1>TodoList</h1>
            <Link to={`${path}/add`} className="btn btn-sm btn-success mb-2">Add TodoList</Link>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th style={{ width: '40%' }}>Title</th>
                        <th style={{ width: '40%' }}>Description</th>
                        <th style={{ width: '5%' }}></th>
                        <th style={{ width: '15%' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {todoLists && todoLists.map(todoList =>
                        <tr key={todoList.id}>
                            <td>{todoList.title}</td>
                            <td>{todoList.description}</td>
                            <td>
                            <Link to={`todoItem/${todoList.id}`} className="btn btn-sm btn-secondary mr-1">Details</Link>
                            </td>
                            <td style={{ whiteSpace: 'nowrap' }}>
                                <Link to={`${path}/edit/${todoList.id}`} className="btn btn-sm btn-primary mr-1">Edit</Link>
                                <button onClick={() => deleteTodoList(todoList.id)}  className="btn btn-sm btn-danger btn-delete-TodoList">
                                    <span>Delete</span>
                                </button>
                            </td>
                        </tr>
                    )}

                    {!todoLists &&
                        <tr>
                            <td colSpan="4" className="text-center">
                                <div className="spinner-border spinner-border-lg align-center"></div>
                            </td>
                        </tr>
                    }
                    {todoLists && !todoLists.length &&
                        <tr>
                            <td colSpan="4" className="text-center">
                                <div className="p-2">No TodoList To Display</div>
                            </td>
                        </tr>
                    }

                </tbody>
            </table>
        </div>
    );
}

export { List };