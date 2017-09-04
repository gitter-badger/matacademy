/**
 * Created by atrepyto on 9/2/17.
 */
import 'rxjs/add/operator/switchMap';

import { Component, OnInit } from '@angular/core';
import { Task } from './task';
import {TaskService} from './task.service';

import { Location } from '@angular/common';

@Component({
  selector: 'tasks',
  templateUrl: './tasks.component.html',
  // styleUrls: ['./task.component.css']
})
export class TasksComponent implements OnInit  {
  tasks: Task[];

  selectedTask: Task;



  constructor(private taskService: TaskService,
              private location: Location) { }

  ngOnInit() {
    this.taskService.getTasks().then(tasks => this.tasks = tasks);
  }

  addTask(text: string): void {
    text = text.trim();
    if (!text) { return; }
    this.taskService.createTask(text)
    .then(task => {
        this.tasks.push(task);
    });
  }

  deleteTask(task: Task): void {
        this.taskService
            .deleteTask(task.id)
            .then(() => {
                this.tasks = this.tasks.filter(h => h !== task);
            });
  }

  markDone(task: Task): void {
    task.done = true;
    this.taskService.updateTask(task);
  }

}

