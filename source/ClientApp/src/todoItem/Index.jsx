import React from 'react';
import { Route, Switch } from 'react-router-dom';

import { List } from './List';
import { AddEdit } from './AddEdit';

function TodoItem({ match }) {
    const { path } = match;
    
    return (
        <Switch>
            <Route exact path={`${path}`} component={List} />
            <Route path={`${path}/add/:todoListId`} component={AddEdit} />
            <Route path={`${path}/edit/:todoListId/:id`} component={AddEdit} />
        </Switch>
    );
}

export { TodoItem };