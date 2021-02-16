import React from 'react';
import { Link } from 'react-router-dom';

function Home() {
    return (
        <div>
            <h1>React Hook - Programming challenge</h1>
            <p>To do List</p>
            <div>
                <p><Link to="todoList">&gt;&gt; Manage TodoList</Link></p>
            </div>
        </div>
    );
}

export { Home };