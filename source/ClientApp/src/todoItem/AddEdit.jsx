import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

import { todoItemService, alertService } from '@/_services';

function AddEdit({ history, match }) {
    const { id,todoListId } = match.params;
    const isAddMode = !id;

    // form validation rules 
    const validationSchema = Yup.object().shape({
        title: Yup.string()
            .required('Title is required'),
        note: Yup.string()
            .required('Note is required')
    });

    // functions to build form returned by useForm() hook
    const { register, handleSubmit, reset, setValue, errors, formState } = useForm({
        resolver: yupResolver(validationSchema)
    });

    function onSubmit(data) {
        return isAddMode
            ? createTodoItem(data)
            : updateTodoItem(id, data);
    }

    function createTodoItem(data) {
        data.todoListId = todoListId
        return todoItemService.create(data)
            .then(() => {
                alertService.success('TodoItem added', { keepAfterRouteChange: true });
                history.go(-1)
            })
            .catch(alertService.error);
    }

    function updateTodoItem(id, data) {        
        return todoItemService.update(id, data)
            .then(() => {
                alertService.success('TodoItem updated', { keepAfterRouteChange: true });
                history.go(-1);
            })
            .catch(alertService.error);
    }

    const [todoItem, setTodoItem] = useState({});

    useEffect(() => {
            // get todoItem and set form fields
            if(id == undefined) return
            todoItemService.getById(id).then(todoItem => {
                const fields = ['title', 'note', 'done'];
                fields.forEach(field => setValue(field, todoItem[field]));
                setTodoItem(todoItem);
            });
    }, []);

    return (
        <form onSubmit={handleSubmit(onSubmit)} onReset={reset}>
            <h1>{isAddMode ? 'Add TodoItem' : 'Edit TodoItem'}</h1>
            <div className="form-row">
                <div className="form-group col-6">
                    <label>Title</label>
                    <input name="title" type="text" ref={register} className={`form-control ${errors.title ? 'is-invalid' : ''}`} />
                    <div className="invalid-feedback">{errors.title?.message}</div>
                </div>
                <div className="form-group col-6">
                    <label>Note</label>
                    <input name="note" type="text" ref={register} className={`form-control ${errors.note ? 'is-invalid' : ''}`} />
                    <div className="invalid-feedback">{errors.note?.message}</div>
                </div>
            </div>

            <div className="form-group">
                <button type="submit" disabled={formState.isSubmitting} className="btn btn-primary">
                    {formState.isSubmitting && <span className="spinner-border spinner-border-sm mr-1"></span>}
                    Save
                </button>
                <Link to={`/`} className="btn btn-link">Cancel</Link>
            </div>
        </form>
    );
}

export { AddEdit };