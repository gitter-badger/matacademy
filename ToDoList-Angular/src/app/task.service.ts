/**
 * Created by atrepyto on 9/2/17.
 */
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Task } from './task';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TaskService {
    private static taskId = 1;
    private taskUrl = 'api/tasks';
    private headers = new Headers({'Content-Type': 'application/json'});
    constructor(private http: Http) { };

    getTasks(): Promise<Task[]> {
         return this.http.get(this.taskUrl)
            .toPromise()
            .then(response => response.json().data as Task[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    createTask(text: string):  Promise<Task> {
        return this.http
            .post(this.taskUrl, JSON.stringify({text: text, done: false, id: TaskService.taskId++}), {headers: this.headers})
            .toPromise()
            .then(res => res.json().data as Task)
            .catch(this.handleError);
    }

    deleteTask(id: number): Promise<void> {
        const url = `${this.taskUrl}/${id}`;
        return this.http.delete(url, {headers: this.headers})
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    updateTask(task: Task): Promise<Task> {
        const url = `${this.taskUrl}/${task.id}`;
        return this.http
            .put(url, JSON.stringify(task), {headers: this.headers})
            .toPromise()
            .then(() => task)
            .catch(this.handleError);
    }
}
