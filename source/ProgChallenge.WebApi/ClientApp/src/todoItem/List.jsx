import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { todoItemService,alertService } from '@/_services';

function List({ match }) {
    const { path } = match;
    const [todoItems, setTodoItems] = useState(null);

    useEffect(() => {
        todoItemService.getAll().then(x => setTodoItems(x));
    }, []);

    function deleteTodoItem(id) {
        setTodoItems(todoItems.map(x => {
                return x;
        }));
        todoItemService.delete(id).then(() => {
            setTodoItems(todoItems => todoItems.filter(x => x.id !== id));
            alertService.success('TodoItem delete', { keepAfterRouteChange: true });
        })
        .catch(alertService.error);
    }

    function doneListItem(id, done) {
        return todoItemService.update(id, {done: !done})
            .then(() => {
                setTodoItems(todoItems => todoItems.filter(x => true));
                alertService.success('TodoItem change done!', { keepAfterRouteChange: true });
            })
            .catch(alertService.error);
    }

    return (
        <div>
            <h1>TodoItem</h1>
            <Link to={`${path}/add`} className="btn btn-sm btn-success mb-2">Add TodoItem</Link>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th style={{ width: '30%' }}>Title</th>
                        <th style={{ width: '30%' }}>Note</th>
                        <th style={{ width: '30%' }}>Done</th>
                        <th style={{ width: '10%' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {todoItems && todoItems.map(todoItem =>
                        <tr key={todoItem.id}>
                            <td>{todoItem.title}</td>
                            <td>{todoItem.note}</td>
                            <td>
                                <button onClick={() => doneListItem(todoItem.id,todoItem.done)} className="btn btn-sm btn-secondary">
                                    {todoItem.done
                                        ? <span>Waiting</span>
                                        : <span>Done</span>
                                    }</button>
                                    <span>{todoItem.done ? ' is done' : 'not is done'}</span>
                            </td>
                            <td style={{ whiteSpace: 'nowrap' }}>
                                <Link to={`${path}/edit/${todoItem.id}`} className="btn btn-sm btn-primary mr-1">Edit</Link>
                                <button onClick={() => deleteTodoItem(todoItem.id)} className="btn btn-sm btn-danger btn-delete-TodoItem" disabled={todoItem.isDeleting}>
                                    {todoItem.isDeleting
                                        ? <span className="spinner-border spinner-border-sm"></span>
                                        : <span>Delete</span>
                                    }
                                </button>
                            </td>
                        </tr>
                    )}

                    {!todoItems &&
                        <tr>
                            <td colSpan="4" className="text-center">
                                <div className="spinner-border spinner-border-lg align-center"></div>
                            </td>
                        </tr>
                    }
                    {todoItems && !todoItems.length &&
                        <tr>
                            <td colSpan="4" className="text-center">
                                <div className="p-2">No TodoItems To Display</div>
                            </td>
                        </tr>
                    }

                </tbody>
            </table>
        </div>
    );
}

export { List };